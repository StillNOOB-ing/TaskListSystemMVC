using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;

namespace TaskListSystemMVC.Controllers.Master
{
    [Authorize]
    public class MTypeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMasterHelper mHelper;

        public MTypeController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index()
        {
            var dataList = await mHelper.GetTypeAll();

            return View("~/Views/Master/Type/Index.cshtml", dataList);
        }

        public IActionResult Create()
        {
            return View("~/Views/Master/Type/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MType item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.InsertType(item);
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
            var item = await mHelper.GetTypeByID(id);
            return View("~/Views/Master/Type/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MType item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdateType(item);
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
            var item = await mHelper.GetTypeByID(id);
            return View("~/Views/Master/Type/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MType item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteType(item);
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
