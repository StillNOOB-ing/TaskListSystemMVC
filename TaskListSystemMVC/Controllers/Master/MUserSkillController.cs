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
    public class MUserSkillController : Controller
    {
        private readonly IMasterHelper mHelper;

        public MUserSkillController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchString)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchString"] = searchString;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamName"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetUserSkillDB().OrderByDescending(x => x.Name),
                _ => mHelper.GetUserSkillDB().OrderBy(x => x.Name).AsQueryable(),
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                dataList = dataList.Where(x =>
                    x.UID.ToString().Contains(searchString) ||
                    x.Name.ToString().Contains(searchString) ||
                    x.CreatedBy.ToString().Contains(searchString) ||
                    x.CreatedOn.ToString().Contains(searchString) ||
                    x.UpdatedBy.ToString().Contains(searchString) ||
                    x.UpdatedOn.ToString().Contains(searchString)
                );
            }

            var result = await PaginationList<MUserSkill>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/Master/UserSkill/Index.cshtml", result);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/UserSkill/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MUserSkill item)
        {
            if (ModelState.IsValid)
            {
                var skillList = await mHelper.GetUserSkillAll();
                if (skillList.Exists(x => x.Name == item.Name))
                {
                    ViewData["AlertMessage"] = "This skill has been registered.";
                    return View("~/Views/Master/UserSkill/Create.cshtml", item);
                }

                var result = await mHelper.InsertUserSkill(item);
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
            return View("~/Views/Master/UserSkill/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetUserSkillByID(id);
            return View("~/Views/Master/UserSkill/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MUserSkill item)
        {
            if (ModelState.IsValid)
            {
                var skillList = await mHelper.GetUserSkillAll();
                if (skillList.Exists(x => x.Name == item.Name && x.Description == item.Description))
                {
                    ViewData["AlertMessage"] = "This skill has been registered.";
                    return View("~/Views/Master/UserSkill/Create.cshtml", item);
                }

                var result = await mHelper.UpdateUserSkill(item);
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
            return View("~/Views/Master/UserSkill/Delete.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await mHelper.GetUserSkillByID(id);
            return View("~/Views/Master/UserSkill/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MUserSkill item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteUserSkill(item);
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
            return View("~/Views/Master/UserSkill/Delete.cshtml", item);
        }
    }
}
