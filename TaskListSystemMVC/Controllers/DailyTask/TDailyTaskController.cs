using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;

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

        public async Task<IActionResult> Index(int? index, string sortOrder)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => taskHelper.GetDailyTaskDB().OrderByDescending(x => x.ReportByID),
                _ => taskHelper.GetDailyTaskDB()
            };
            
            var result = await PaginationList<TDailyTask>.CreateAsync(dataList, index.Value, pageSize, sortOrder);

            ViewData["PageIndex"] = index;
            ViewData["PageCount"] = result.TotalPage;
            ViewData["TotalItem"] = dataList.Count();           

            return View("~/Views/DailyTask/DailyTask/Index.cshtml", result);
        }

        public async Task<IActionResult> Create()
        {
            var item = new TDailyTask();

            item.ReportByID = "AUTO";
            item.ReportedOn = DateTime.Now;

            ViewBag.PICList = await masHelper.GetAccountInfoSelectItemList();
            ViewBag.StatusList = await masHelper.GetStatusSelectItemList();
            ViewBag.TypeList = await masHelper.GetTypeSelectItemList();

            return View("~/Views/DailyTask/DailyTask/Create.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TDailyTask item)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.InsertDailyTask(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest(new { message = "Invalid Model!" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await taskHelper.GetDailyTaskByID(id);
            if (item == null) return NotFound();

            ViewBag.PICList = await masHelper.GetAccountInfoSelectItemList();
            ViewBag.StatusList = await masHelper.GetStatusSelectItemList();
            ViewBag.TypeList = await masHelper.GetTypeSelectItemList();

            return View("~/Views/DailyTask/DailyTask/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TDailyTask item)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.UpdateDailyTask(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest(new { message = "Invalid Model!" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await taskHelper.GetDailyTaskByID(id);
            if (item == null) return NotFound();

            ViewBag.PICList = await masHelper.GetAccountInfoSelectItemList();
            ViewBag.StatusList = await masHelper.GetStatusSelectItemList();
            ViewBag.TypeList = await masHelper.GetTypeSelectItemList();

            return View("~/Views/DailyTask/DailyTask/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TDailyTask item)
        {
            if (ModelState.IsValid)
            {
                var result = await taskHelper.DeleteDailyTask(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest(new { message = "Invalid Model!" });
        }

        //[HttpPost]
        //public IActionResult CmbStatusChanged([FromBody] MStatus model)
        //{
        //    return Json(new { success = true, message = item.Description });
        //}
    }
}
