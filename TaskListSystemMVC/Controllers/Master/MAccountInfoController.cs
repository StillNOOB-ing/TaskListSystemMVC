using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TaskListSystemMVC.Database.Helper;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;

namespace TaskListSystemMVC.Controllers.Master
{
    [Authorize]
    public class MAccountInfoController : Controller
    {
        private readonly IMasterHelper mHelper;

        public MAccountInfoController(IMasterHelper masterHelper)
        {
            mHelper = masterHelper;
        }

        public async Task<IActionResult> Index(int index)
        {
            if (index <= 0) index = 1;
            int pageSize = 10;

            var dataList = mHelper.GetAccountInfoDB();
            var result = await PaginationList<MAccountInfo>.CreateAsync(dataList, index, pageSize);

            ViewData["PageIndex"] = index;
            ViewData["PageCount"] = result.TotalPage;
            ViewData["TotalItem"] = dataList.Count();

            return View("~/Views/Master/AccountInfo/Index.cshtml", result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.LevelList = await mHelper.GetUserLevelRightSelectItemList();

            return View("~/Views/Master/AccountInfo/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.InsertAccountInfo(item);
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
            var item = await mHelper.GetAccountInfoByID(id);
            if (item == null) return NotFound();

            item.ConfirmPassword = item.Password;

            ViewBag.LevelList = await mHelper.GetUserLevelRightSelectItemList();

            return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdateAccountInfo(item);
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
            var item = await mHelper.GetAccountInfoByID(id);
            if (item == null) return NotFound();

            item.ConfirmPassword = item.Password;

            return View("~/Views/Master/AccountInfo/Delete.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.DeleteAccountInfo(item);
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

        public async Task<IActionResult> ChangePassword(int id)
        {
            var item = await mHelper.GetAccountInfoByID(id);
            if (item == null) return NotFound();
            return View("~/Views/Master/AccountInfo/ChangePassword.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var result = await mHelper.UpdateAccountInfo(item);
                if (result.success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest(new { result.message });
                }
            }
            return BadRequest( new { message = "Invalid Model!" });
        }
    }
}
