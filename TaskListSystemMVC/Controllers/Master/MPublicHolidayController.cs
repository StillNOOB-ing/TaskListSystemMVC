using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListSystemMVC.Database.Helper;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;
using Microsoft.Data.SqlClient;

namespace TaskListSystemMVC.Controllers.Master
{
    [Authorize]
    public class MPublicHolidayController : Controller
    {
        private readonly IMasterHelper mHelper;

        public MPublicHolidayController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchString)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchString"] = searchString;
            ViewData["SortOrder"] = sortOrder;
            //ViewData["SortParamStartDate"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "desc") ? "asc" : "desc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetPublicHolidayDB().OrderByDescending(x => x.StartDate),
                _ => mHelper.GetPublicHolidayDB().OrderBy(x => x.StartDate).AsQueryable(),
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                dataList = dataList.Where(x =>
                    x.UID.ToString().Contains(searchString) ||
                    x.Name.ToString().Contains(searchString) ||
                    x.StartDate.ToString().Contains(searchString) ||
                    x.EndDate.ToString().Contains(searchString) ||
                    x.Day.ToString().Contains(searchString) ||
                    x.CreatedBy.ToString().Contains(searchString) ||
                    x.CreatedOn.ToString().Contains(searchString) ||
                    x.UpdatedBy.ToString().Contains(searchString) ||
                    x.UpdatedOn.ToString().Contains(searchString)
                );
            }

            var result = await PaginationList<MPublicHoliday>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/Master/PublicHoliday/Index.cshtml", result);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/PublicHoliday/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MPublicHoliday item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.InsertPublicHoliday(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/Master/PublicHoliday/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetPublicHolidayByID(id);
            return View("~/Views/Master/PublicHoliday/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MPublicHoliday item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdatePublicHoliday(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/Master/PublicHoliday/Delete.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await mHelper.GetPublicHolidayByID(id);
            return View("~/Views/Master/PublicHoliday/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MPublicHoliday item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeletePublicHoliday(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return NotFound();
        }
    }
}
