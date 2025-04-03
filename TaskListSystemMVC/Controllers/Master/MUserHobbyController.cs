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
    public class MUserHobbyController : Controller
    {
        private readonly IMasterHelper mHelper;

        public MUserHobbyController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamName"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetUserHobbyDB().OrderByDescending(x => x.Name),
                _ => mHelper.GetUserHobbyDB().OrderBy(x => x.Name).AsQueryable(),
            };

            if (!string.IsNullOrEmpty(searchFilter))
            {
                searchFilter = searchFilter.ToLower();

                dataList = dataList.Where(x =>
                    x.UID.ToString().Contains(searchFilter) ||
                    x.Name.ToString().Contains(searchFilter) ||
                    x.CreatedBy.ToString().Contains(searchFilter) ||
                    x.CreatedOn.ToString().Contains(searchFilter) ||
                    x.UpdatedBy.ToString().Contains(searchFilter) ||
                    x.UpdatedOn.ToString().Contains(searchFilter)
                );
            }

            var result = await PaginationList<MUserHobby>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/Master/UserHobby/Index.cshtml", result);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/UserHobby/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MUserHobby item)
        {
            if (ModelState.IsValid)
            {
                var HobbyList = await mHelper.GetUserHobbyAll();
                if (HobbyList.Exists(x => x.Name == item.Name))
                {
                    ViewData["AlertMessage"] = "This Hobby has been registered.";
                    return View("~/Views/Master/UserHobby/Create.cshtml", item);
                }

                var result = await mHelper.InsertUserHobby(item);
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
            return View("~/Views/Master/UserHobby/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetUserHobbyByID(id);
            return View("~/Views/Master/UserHobby/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MUserHobby item)
        {
            if (ModelState.IsValid)
            {
                var HobbyList = await mHelper.GetUserHobbyAll();
                if (HobbyList.Exists(x => x.Name == item.Name && x.Description == item.Description))
                {
                    ViewData["AlertMessage"] = "This Hobby has been registered.";
                    return View("~/Views/Master/UserHobby/Create.cshtml", item);
                }

                var result = await mHelper.UpdateUserHobby(item);
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
            return View("~/Views/Master/UserHobby/Delete.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await mHelper.GetUserHobbyByID(id);
            return View("~/Views/Master/UserHobby/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MUserHobby item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteUserHobby(item);
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
            return View("~/Views/Master/UserHobby/Delete.cshtml", item);
        }
    }
}
