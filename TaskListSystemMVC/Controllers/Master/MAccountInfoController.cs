﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TaskListSystemMVC.Database.Helper;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database;
using Microsoft.Data.SqlClient;

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

        public async Task<IActionResult> Index(int? index, string sortOrder, string searchFilter)
        {
            if (index == null || index <= 0) index = 1;
            int pageSize = 10;

            ViewData["SearchFilter"] = searchFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SortParamReportID"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "asc") ? "desc" : "asc";

            var dataList = sortOrder switch
            {
                "desc" => mHelper.GetAccountInfoDB().OrderByDescending(x => x.UID),
                _ => mHelper.GetAccountInfoDB()
            };

            if (!string.IsNullOrEmpty(searchFilter))
            {
                searchFilter = searchFilter.ToLower();

                dataList = dataList.Where(x =>
                    x.UID.ToString().Contains(searchFilter) ||
                    x.Name.ToString().Contains(searchFilter) ||
                    x.Active.ToString().Contains(searchFilter) ||
                    x.LastLoginOn.ToString().Contains(searchFilter) ||
                    x.CreatedBy.ToString().Contains(searchFilter) ||
                    x.CreatedOn.ToString().Contains(searchFilter) ||
                    x.UpdatedBy.ToString().Contains(searchFilter) ||
                    x.UpdatedOn.ToString().Contains(searchFilter)
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
                if (accountList.Exists(x => x.Name.Trim().ToLower() == item.Name.Trim().ToLower()))
                {
                    ViewData["AlertMessage"] = "This name already existed!";
                    return View("~/Views/Master/AccountInfo/Create.cshtml", item);
                }
                else if (accountList.Exists(x => x.Username.Trim().ToLower() == item.Username.Trim().ToLower()))
                {
                    ViewData["AlertMessage"] = "This username already existed!";
                    return View("~/Views/Master/AccountInfo/Create.cshtml", item);
                }
                else if (accountList.Exists(x => x.Email.Trim().ToLower() == item.Email.Trim().ToLower()))
                {
                    ViewData["AlertMessage"] = "This email already existed!";
                    return View("~/Views/Master/AccountInfo/Create.cshtml", item);
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
                if (accountList.Exists(x => x.Name.Trim().ToLower() == item.Name.Trim().ToLower()))
                {
                    ViewData["AlertMessage"] = "This name already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => x.Username.Trim().ToLower() == item.Username.Trim().ToLower()))
                {
                    ViewData["AlertMessage"] = "This username already existed!";
                    return View("~/Views/Master/AccountInfo/Edit.cshtml", item);
                }
                else if (accountList.Exists(x => x.Email.Trim().ToLower() == item.Email.Trim().ToLower()))
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
