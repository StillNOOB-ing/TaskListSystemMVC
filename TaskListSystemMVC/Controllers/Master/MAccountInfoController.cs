using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TaskListSystemMVC.Database.Helper;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchString)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchString"] = searchString;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamName"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "desc") ? "asc" : "desc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetAccountInfoDB().OrderByDescending(x => x.Name).AsQueryable(),
                _ => mHelper.GetAccountInfoDB().OrderBy(x => x.Name).AsQueryable()
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                dataList = dataList.Where(x =>
                    x.UID.ToString().Contains(searchString) ||
                    x.Name.ToString().Contains(searchString) ||
                    x.Active.ToString().Contains(searchString) ||
                    x.LevelRightName.ToString().Contains(searchString) ||
                    x.LastLoginOn.ToString().Contains(searchString) ||
                    x.CreatedBy.ToString().Contains(searchString) ||
                    x.CreatedOn.ToString().Contains(searchString) ||
                    x.UpdatedBy.ToString().Contains(searchString) ||
                    x.UpdatedOn.ToString().Contains(searchString)
                );
            }

            var result = await PaginationList<MAccountInfo>.CreateAsync(dataList, index.Value, pageSize);

            return View("~/Views/Master/AccountInfo/Index.cshtml", result);
        }

        public async Task<IActionResult> Create()
        {
            return View("~/Views/Master/AccountInfo/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var accountList = mHelper.GetAccountInfoDB().Where(x => x.UID != item.UID).ToList();
                if (accountList.Exists(x => !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(item.Name) && x.Name.Trim().Equals(item.Name.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This name already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => !string.IsNullOrEmpty(x.Username) && !string.IsNullOrEmpty(item.Username) && x.Username.Trim().Equals(item.Username.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This username already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(item.Email) && x.Email.Trim().Equals(item.Email.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This email already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }

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

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/Master/AccountInfo/Create.cshtml", item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await mHelper.GetAccountInfoByID(id);
            if (item == null) return NotFound();

            item.ConfirmPassword = item.Password;

            return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MAccountInfo item)
        {
            if (ModelState.IsValid)
            {
                var accountList = mHelper.GetAccountInfoDB().Where(x => x.UID != item.UID).ToList();
                if (accountList.Exists(x => !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(item.Name) && x.Name.Trim().Equals(item.Name.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This name already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => !string.IsNullOrEmpty(x.Username) && !string.IsNullOrEmpty(item.Username) && x.Username.Trim().Equals(item.Username.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This username already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(item.Email) && x.Email.Trim().Equals(item.Email.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ViewData["AlertMessage"] = "This email already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }

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

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
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
            else if (item.Password != item.ConfirmPassword)
            {
                ViewData["AlertMessage"] = "The Password and Confirm Password is not same!";
                return View("~/Views/Master/AccountInfo/ChangePassword.cshtml", item);
            }

            ViewData["AlertMessage"] = "Invalid Model!";
            return View("~/Views/Master/AccountInfo/ChangePassword.cshtml", item);
        }
    }
}
