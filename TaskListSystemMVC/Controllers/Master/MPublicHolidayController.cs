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

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetPublicHolidayDB().OrderByDescending(x => x.UID),
                _ => mHelper.GetPublicHolidayDB()
            };

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
            return NotFound();
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
            return NotFound();
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
