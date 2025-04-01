using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Helper;

namespace TaskListSystemMVC.Controllers.Master
{
    [Authorize]
    public class MStatusController : Controller
    {
        private readonly IMasterHelper mHelper;

        public MStatusController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index()
        {
            var dataList = await mHelper.GetStatusAll();
            return View("~/Views/Master/Status/Index.cshtml", dataList);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/Status/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MStatus item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.InsertStatus(item);
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
            return View("~/Views/Master/Status/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetStatusByID(id);
            if (item == null) return NotFound();
            return View("~/Views/Master/Status/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MStatus item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdateStatus(item);
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
            return View("~/Views/Master/Status/Edit.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await mHelper.GetStatusByID(id);
            if (item == null) return NotFound();
            return View("~/Views/Master/Status/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(MStatus item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteStatus(item);
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
