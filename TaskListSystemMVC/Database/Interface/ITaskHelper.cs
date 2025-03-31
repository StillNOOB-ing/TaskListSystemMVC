using Microsoft.Data.SqlClient;
using TaskListSystemMVC.Database.Model;

namespace TaskListSystemMVC.Database.Interface
{
    public interface ITaskHelper
    {
        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB();
        public Task<List<TDailyTask>> GetDailyTaskAll();
        public Task<TDailyTask> GetDailyTaskByID(int id);
        public Task<ResultInfo> InsertDailyTask(TDailyTask item);
        public Task<ResultInfo> UpdateDailyTask(TDailyTask item);
        public Task<ResultInfo> DeleteDailyTask(TDailyTask item);

        #endregion
    }
}
