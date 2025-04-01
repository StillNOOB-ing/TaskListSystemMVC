using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using TaskListSystemMVC.Helper;
using System.Threading.Tasks;


namespace TaskListSystemMVC.Database.Helper
{
    public class TaskHelper: ITaskHelper
    {
        private readonly ITaskRepository repository;
        private readonly IAccountHelper accountHelper;
        private readonly IMasterHelper masterHelper;

        public TaskHelper(ITaskRepository Repository, IAccountHelper AccountHelper, IMasterHelper MasterHelper)
        {
            repository = Repository;
            accountHelper = AccountHelper;
            masterHelper = MasterHelper;
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
        public IQueryable<TDailyTask> GetPendingDailyTaskDB()
        {
            return repository.GetDailyTaskDB(x => x.StatusID != FixedStatus.COMPLETED_UID);
        }
        public async Task<List<TDailyTask>> GetPendingDailyTaskAll()
        {
            return await repository.GetDailyTaskAll(x => x.StatusID != FixedStatus.COMPLETED_UID);
        }
        public IQueryable<TDailyTask> GetCompletedDailyTaskDB()
        {
            return repository.GetDailyTaskDB(x => x.StatusID == FixedStatus.COMPLETED_UID);
        }
        public async Task<List<TDailyTask>> GetCompletedDailyTaskAll()
        {
            return await repository.GetDailyTaskAll(x => x.StatusID == FixedStatus.COMPLETED_UID);
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

            item.StatusName = (await masterHelper.GetStatusByID(item.StatusID.Value)).Name;
            item.PICName = (await masterHelper.GetAccountInfoByID(item.PICID.Value)).Name;
            item.TypeName = (await masterHelper.GetTypeByID(item.TypeID.Value)).Name;

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accountHelper.GetName();

            return await repository.InsertDailyTask(item);
        }
        public async Task<ResultInfo> UpdateDailyTask(TDailyTask item)
        {
            item.StatusName = (await masterHelper.GetStatusByID(item.StatusID.Value)).Name;
            item.PICName = (await masterHelper.GetAccountInfoByID(item.PICID.Value)).Name;
            item.TypeName = (await masterHelper.GetTypeByID(item.TypeID.Value)).Name;

            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accountHelper.GetName();

            return await repository.UpdateDailyTask(item);
        }
        public async Task<ResultInfo> DeleteDailyTask(TDailyTask item)
        {
            return await repository.DeleteDailyTask(item);
        }

        #endregion
    }
}
