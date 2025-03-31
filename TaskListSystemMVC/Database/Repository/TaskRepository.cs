using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskListSystemMVC.Database.Repository
{
    public class TaskRepository: ITaskRepository
    {
        private readonly ApplicationDbContext context;
        
        public TaskRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB(Expression<Func<TDailyTask, bool>> predicate)
        {
            return context.DailyTasks.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<TDailyTask>> GetDailyTaskAll(Expression<Func<TDailyTask, bool>> predicate)
        {
            return await context.DailyTasks.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertDailyTask(TDailyTask item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new TDailyTask();
                findItem = item.Clone();

                await context.DailyTasks.AddAsync(findItem);
                await context.SaveChangesAsync();

                result.message = "Successful";
                result.success = true;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.success = false;
            }
            return result;
        }
        public async Task<ResultInfo> UpdateDailyTask(TDailyTask item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.DailyTasks.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.Entry(findItem).CurrentValues.SetValues(item);

                    await context.SaveChangesAsync();

                    result.message = "Successful";
                    result.success = true;
                }
                else
                {
                    result.message = "No Item Found";
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.success = false;
            }
            return result;
        }
        public async Task<ResultInfo> DeleteDailyTask(TDailyTask item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.DailyTasks.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.DailyTasks.Remove(findItem);

                    await context.SaveChangesAsync();

                    result.message = "Successful";
                    result.success = true;
                }
                else
                {
                    result.message = "No Item Found";
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.success = false;
            }
            return result;
        }

        #endregion
    }
}
