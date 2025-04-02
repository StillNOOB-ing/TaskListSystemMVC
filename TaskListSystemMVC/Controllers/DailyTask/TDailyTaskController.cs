using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;
using OfficeOpenXml;
using System.Diagnostics;
using System.ComponentModel;

namespace TaskListSystemMVC.Controllers.DailyTask
{
    [Authorize]
    public class TDailyTaskController : Controller
    {
        private readonly ITaskHelper taskHelper;
        private readonly IMasterHelper masHelper;

        public TDailyTaskController(ITaskHelper tHelper, IMasterHelper mHelper)
        {
            taskHelper = tHelper;
            masHelper = mHelper;
        }

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => taskHelper.GetPendingDailyTaskDB().OrderByDescending(x => x.ReportByID),
                _ => taskHelper.GetPendingDailyTaskDB()
            };

            if (!string.IsNullOrEmpty(searchFilter))
            {
                searchFilter = searchFilter.ToLower();

                dataList = dataList.Where(x =>
                    x.ReportByID.Contains(searchFilter) ||
                    x.ReportedOn.ToString().Contains(searchFilter) ||
                    x.ScheduledOn.ToString().Contains(searchFilter) ||
                    x.Title.ToLower().Contains(searchFilter) ||
                    x.CompletedOn.ToString().Contains(searchFilter)
                );
            }
            
            var result = await PaginationList<TDailyTask>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/DailyTask/DailyTask/Index.cshtml", result);
        }

        public async Task<IActionResult> Index_Completed(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => taskHelper.GetCompletedDailyTaskDB().OrderByDescending(x => x.ReportByID),
                _ => taskHelper.GetCompletedDailyTaskDB()
            };

            if (!string.IsNullOrEmpty(searchFilter))
            {
                searchFilter = searchFilter.ToLower();

                dataList = dataList.Where(x =>
                    x.ReportByID.Contains(searchFilter) ||
                    x.ReportedOn.ToString().Contains(searchFilter) ||
                    x.ScheduledOn.ToString().Contains(searchFilter) ||
                    x.Title.ToLower().Contains(searchFilter) ||
                    x.CompletedOn.ToString().Contains(searchFilter)
                );
            }

            var result = await PaginationList<TDailyTask>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/DailyTask/DailyTask/Index_Completed.cshtml", result);
        }

        public async Task<IActionResult> Index_Summary(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => taskHelper.GetDailyTaskDB().OrderByDescending(x => x.ReportByID),
                _ => taskHelper.GetDailyTaskDB()
            };

            if (!string.IsNullOrEmpty(searchFilter))
            {
                searchFilter = searchFilter.ToLower();

                dataList = dataList.Where(x =>
                    x.ReportByID.Contains(searchFilter) ||
                    x.ReportedOn.ToString().Contains(searchFilter) ||
                    x.ScheduledOn.ToString().Contains(searchFilter) ||
                    x.Title.ToLower().Contains(searchFilter) ||
                    x.CompletedOn.ToString().Contains(searchFilter)
                );
            }

            var result = await PaginationList<TDailyTask>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/DailyTask/DailyTask/Index_Summary.cshtml", result);
        }

        public async Task<IActionResult> Create(string sourcePage)
        {
            var item = new TDailyTask();

            item.ReportByID = "AUTO";
            item.ReportedOn = DateTime.Now;

            ViewData["SourcePage"] = sourcePage;

            return View("~/Views/DailyTask/DailyTask/Create.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TDailyTask item, [FromForm]string sourcePage)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.InsertDailyTask(item);
                if (result.success)
                {
                    if (!string.IsNullOrEmpty(sourcePage))
                    {
                        switch (sourcePage)
                        {
                            case "CompletedTask":
                                return RedirectToAction("Index_Completed");
                            case "SummaryTask":
                                return RedirectToAction("Index_Summary");
                            default:
                                return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/DailyTask/DailyTask/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id, string sourcePage)
        {
            var item = await taskHelper.GetDailyTaskByID(id);
            if (item == null) return NotFound();

            ViewData["SourcePage"] = sourcePage;

            return View("~/Views/DailyTask/DailyTask/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TDailyTask item, [FromForm]string sourcePage)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.UpdateDailyTask(item);
                if (result.success)
                {
                    if (!string.IsNullOrEmpty(sourcePage))
                    {
                        switch (sourcePage)
                        {
                            case "CompletedTask":
                                return RedirectToAction("Index_Completed");
                            case "SummaryTask":
                                return RedirectToAction("Index_Summary");
                            default:
                                return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/DailyTask/DailyTask/Edit.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id, string sourcePage)
        {
            var item = await taskHelper.GetDailyTaskByID(id);
            if (item == null) return NotFound();

            ViewData["SourcePage"] = sourcePage;

            return View("~/Views/DailyTask/DailyTask/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TDailyTask item, [FromForm]string sourcePage)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.DeleteDailyTask(item);
                if (result.success)
                {
                    if (!string.IsNullOrEmpty(sourcePage))
                    {
                        switch (sourcePage)
                        {
                            case "CompletedTask":
                                return RedirectToAction("Index_Completed");
                            case "SummaryTask":
                                return RedirectToAction("Index_Summary");
                            default:
                                return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest(new { message = "Invalid Model!" });
        }

        public async Task<IActionResult> ExportToExcel(string sourcePage)
        {
            var dataList = new List<TDailyTask>();
            switch (sourcePage)
            {
                case "DailyTask":
                    dataList = await taskHelper.GetPendingDailyTaskAll();
                    break;
                case "CompletedTask":
                    dataList = await taskHelper.GetCompletedDailyTaskAll();
                    break;
                case "SummaryTask":
                    dataList = await taskHelper.GetDailyTaskAll();
                    break;
                default:
                    break;
            }            

            if (dataList.Count > 0)
            {
                ExcelPackage.License.SetNonCommercialPersonal("JunQuan");
                using var package = new ExcelPackage(new FileInfo(sourcePage + ".xlsx"));
                {
                    var worksheet = package.Workbook.Worksheets.Add("Daily Task");

                    worksheet.Cells[1, 1].Value = "UID";
                    worksheet.Cells[1, 2].Value = "Report ID";
                    worksheet.Cells[1, 3].Value = "Reported On";
                    worksheet.Cells[1, 4].Value = "Scheduled On";
                    worksheet.Cells[1, 5].Value = "Title";
                    worksheet.Cells[1, 6].Value = "Description";
                    worksheet.Cells[1, 7].Value = "Remark";
                    worksheet.Cells[1, 8].Value = "Status";
                    worksheet.Cells[1, 9].Value = "PIC";
                    worksheet.Cells[1, 10].Value = "Type";
                    worksheet.Cells[1, 11].Value = "Completed On";
                    worksheet.Cells[1, 12].Value = "Created On";
                    worksheet.Cells[1, 13].Value = "Created By";
                    worksheet.Cells[1, 14].Value = "Updated On";
                    worksheet.Cells[1, 15].Value = "Updated By";

                    int row = 2;
                    foreach (var item in dataList)
                    {
                        worksheet.Cells[row, 1].Value = item.UID;
                        worksheet.Cells[row, 2].Value = item.ReportByID ?? "";
                        worksheet.Cells[row, 3].Value = item.ReportedOn?.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 4].Value = item.ScheduledOn?.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 5].Value = item.Title ?? "";
                        worksheet.Cells[row, 6].Value = item.Description ?? "";
                        worksheet.Cells[row, 7].Value = item.Remark ?? "";
                        worksheet.Cells[row, 8].Value = item.StatusName ?? "";
                        worksheet.Cells[row, 9].Value = item.PICName ?? "";
                        worksheet.Cells[row, 10].Value = item.TypeName ?? "";
                        worksheet.Cells[row, 11].Value = item.CompletedOn?.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 12].Value = item.CreatedOn?.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 13].Value = item.CreatedBy ?? "";
                        worksheet.Cells[row, 14].Value = item.UpdatedOn?.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 15].Value = item.UpdatedBy ?? "";

                        row++;
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sourcePage + ".xlsx");
            }
            return BadRequest(new { message = "No Data To Export!" });
        }
    }
}
