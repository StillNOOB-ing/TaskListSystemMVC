using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;

namespace TaskListSystemMVC.Controllers.Master
{
    [Authorize]
    public class MUserLevelRightController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMasterHelper mHelper;

        public MUserLevelRightController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index()
        {
            var dataList = await mHelper.GetUserLevelRightAll();
            return View("~/Views/Master/UserLevelRight/Index.cshtml", dataList);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/UserLevelRight/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MUserLevelRight item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.InsertUserLevelRight(item);
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
            return View("~/Views/Master/UserLevelRight/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetUserLevelRightByID(id);
            if (item == null) return NotFound();
            return View("~/Views/Master/UserLevelRight/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MUserLevelRight item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdateUserLevelRight(item);
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
            return View("~/Views/Master/UserLevelRight/Edit.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await mHelper.GetUserLevelRightByID(id);
            if (item == null) return NotFound();
            return View("~/Views/Master/UserLevelRight/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(MUserLevelRight item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteUserLevelRight(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest(new { message = "Invalid Model" });
        }
    }
}
