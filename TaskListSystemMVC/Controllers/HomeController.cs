using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database;
using TaskListSystemMVC.Models;
using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Helper;

namespace TaskListSystemMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITaskHelper taskHelper;
        private readonly IMasterHelper masterHelper;
        private readonly IAccountHelper accountHelper;

        public HomeController(ILogger<HomeController> Logger, ITaskHelper TaskHelper, IMasterHelper MasterHelper, IAccountHelper AccountHelper)
        {
            logger = Logger;
            taskHelper = TaskHelper;
            masterHelper = MasterHelper;
            accountHelper = AccountHelper;
        }     

        public async Task<IActionResult> Index()
        {
            var taskList = taskHelper.GetDailyTaskDB().Where(x => 
                x.PICName == accountHelper.GetName() && 
                ((x.ScheduledOn >= DateTime.Today && x.ScheduledOn <= DateTime.Today.AddDays(1)) || 
                (x.ScheduledOn == null))
            ).OrderBy(x => x.ScheduledOn).ToList();

            var holidayList = masterHelper.GetPublicHolidayDB().Where(x => x.StartDate >= DateTime.Today && x.StartDate <= DateTime.Today.AddMonths(1)).ToList();

            return View((taskList, holidayList));
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
