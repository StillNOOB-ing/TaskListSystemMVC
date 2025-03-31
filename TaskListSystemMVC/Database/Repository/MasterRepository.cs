using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace TaskListSystemMVC.Database.Repository
{
    public class MasterRepository: IMasterRepository
    {
        private readonly ApplicationDbContext context;
        
        public MasterRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB(Expression<Func<MAccountInfo, bool>> predicate)
        {
            return context.AccountInfos.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<MAccountInfo>> GetAccountInfoAll(Expression<Func<MAccountInfo, bool>> predicate)
        {
            return await context.AccountInfos.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertAccountInfo(MAccountInfo item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new MAccountInfo();
                findItem = item.Clone();                

                await context.AccountInfos.AddAsync(findItem);
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
        public async Task<ResultInfo> UpdateAccountInfo(MAccountInfo item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.AccountInfos.FindAsync(item.UID);
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
        public async Task<ResultInfo> DeleteAccountInfo(MAccountInfo item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.AccountInfos.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.AccountInfos.Remove(findItem);

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

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB(Expression<Func<MUserLevelRight, bool>> predicate)
        {
            return context.UserLevelRights.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<MUserLevelRight>> GetUserLevelRightAll(Expression<Func<MUserLevelRight, bool>> predicate)
        {
            return await context.UserLevelRights.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new MUserLevelRight();
                findItem = item.Clone();

                await context.UserLevelRights.AddAsync(findItem);
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
        public async Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.UserLevelRights.FindAsync(item.UID);
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
        public async Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.UserLevelRights.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.UserLevelRights.Remove(findItem);

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

        #region Status

        public IQueryable<MStatus> GetStatusDB(Expression<Func<MStatus, bool>> predicate)
        {
            return context.Statuss.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<MStatus>> GetStatusAll(Expression<Func<MStatus, bool>> predicate)
        {
            return await context.Statuss.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertStatus(MStatus item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new MStatus();
                findItem = item.Clone();

                await context.Statuss.AddAsync(findItem);
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
        public async Task<ResultInfo> UpdateStatus(MStatus item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.Statuss.FindAsync(item.UID);
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
        public async Task<ResultInfo> DeleteStatus(MStatus item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.Statuss.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.Statuss.Remove(findItem);

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

        #region Type

        public IQueryable<MType> GetTypeDB(Expression<Func<MType, bool>> predicate)
        {
            return context.Types.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<MType>> GetTypeAll(Expression<Func<MType, bool>> predicate)
        {
            return await context.Types.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertType(MType item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new MType();
                findItem = item.Clone();

                await context.Types.AddAsync(findItem);
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
        public async Task<ResultInfo> UpdateType(MType item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.Types.FindAsync(item.UID);
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
        public async Task<ResultInfo> DeleteType(MType item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.Types.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.Types.Remove(findItem);

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

        #region PublicHoliday

        public IQueryable<MPublicHoliday> GetPublicHolidayDB(Expression<Func<MPublicHoliday, bool>> predicate)
        {
            return context.PublicHolidays.AsNoTracking().Where(predicate).AsQueryable();
        }
        public async Task<List<MPublicHoliday>> GetPublicHolidayAll(Expression<Func<MPublicHoliday, bool>> predicate)
        {
            return await context.PublicHolidays.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ResultInfo> InsertPublicHoliday(MPublicHoliday item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = new MPublicHoliday();
                findItem = item.Clone();

                await context.PublicHolidays.AddAsync(findItem);
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
        public async Task<ResultInfo> UpdatePublicHoliday(MPublicHoliday item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.PublicHolidays.FindAsync(item.UID);
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
        public async Task<ResultInfo> DeletePublicHoliday(MPublicHoliday item)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                var findItem = await context.PublicHolidays.FindAsync(item.UID);
                if (findItem != null)
                {
                    context.PublicHolidays.Remove(findItem);

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
