using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using TaskListSystemMVC.Helper;


namespace TaskListSystemMVC.Database.Helper
{
    public class TaskHelper: ITaskHelper
    {
        private readonly ITaskRepository repository;
        private readonly IAccountHelper accHelper;

        public TaskHelper(ITaskRepository Repository, IAccountHelper accountHelper)
        {
            repository = Repository;
            accHelper = accountHelper;
        }

        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB()
        {
            return repository.GetDailyTaskDB(x => true);
        }
        public async Task<List<TDailyTask>> GetDailyTaskAll()
        {
            return await repository.GetDailyTaskAll(x => true);
        }
        public async Task<TDailyTask> GetDailyTaskByID(int id)
        {
            return (await repository.GetDailyTaskAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<ResultInfo> InsertDailyTask(TDailyTask item)
        {
            var today = DateTime.Today;
            var tasklist = GetDailyTaskDB().Where(x => x.ReportedOn.Value.Date == DateTime.Today);

            int taskCount = tasklist.Count() + 1;
            string formattedDate = today.ToString("yyyyMMdd");
            string formattedCount = taskCount.ToString("D4");

            item.ReportByID = $"{formattedDate}{formattedCount}"; ;
            item.ReportedOn = DateTime.Now;

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertDailyTask(item);
        }
        public async Task<ResultInfo> UpdateDailyTask(TDailyTask item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateDailyTask(item);
        }
        public async Task<ResultInfo> DeleteDailyTask(TDailyTask item)
        {
            return await repository.DeleteDailyTask(item);
        }

        #endregion
    }
}
