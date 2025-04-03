using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TaskListSystemMVC.Database.Model;

namespace TaskListSystemMVC.Database.Interface
{
    public interface IMasterHelper
    {
        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB();
        public Task<List<MAccountInfo>> GetAccountInfoAll();
        public Task<MAccountInfo> GetAccountInfoByID(int id);
        public Task<List<SelectListItem>> GetAccountInfoSelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertAccountInfo(MAccountInfo item);
        public Task<ResultInfo> UpdateAccountInfo(MAccountInfo item);
        public Task<ResultInfo> DeleteAccountInfo(MAccountInfo item);

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB();
        public Task<List<MUserLevelRight>> GetUserLevelRightAll();
        public Task<MUserLevelRight> GetUserLevelRightByID(int id);
        public Task<List<SelectListItem>> GetUserLevelRightSelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item);

        #endregion

        #region Status

        public IQueryable<MStatus> GetStatusDB();
        public Task<List<MStatus>> GetStatusAll();
        public Task<MStatus> GetStatusByID(int id);
        public Task<List<SelectListItem>> GetStatusSelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertStatus(MStatus item);
        public Task<ResultInfo> UpdateStatus(MStatus item);
        public Task<ResultInfo> DeleteStatus(MStatus item);

        #endregion

        #region Type

        public IQueryable<MType> GetTypeDB();
        public Task<List<MType>> GetTypeAll();
        public Task<MType> GetTypeByID(int id);
        public Task<List<SelectListItem>> GetTypeSelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertType(MType item);
        public Task<ResultInfo> UpdateType(MType item);
        public Task<ResultInfo> DeleteType(MType item);

        #endregion

        #region PublicHoliday

        public IQueryable<MPublicHoliday> GetPublicHolidayDB();
        public Task<List<MPublicHoliday>> GetPublicHolidayAll();
        public Task<MPublicHoliday> GetPublicHolidayByID(int id);
        public Task<ResultInfo> InsertPublicHoliday(MPublicHoliday item);
        public Task<ResultInfo> UpdatePublicHoliday(MPublicHoliday item);
        public Task<ResultInfo> DeletePublicHoliday(MPublicHoliday item);

        #endregion

        #region UserSkill

        public IQueryable<MUserSkill> GetUserSkillDB();
        public Task<List<MUserSkill>> GetUserSkillAll();
        public Task<MUserSkill> GetUserSkillByID(int id);
        public Task<List<SelectListItem>> GetUserSkillSelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertUserSkill(MUserSkill item);
        public Task<ResultInfo> UpdateUserSkill(MUserSkill item);
        public Task<ResultInfo> DeleteUserSkill(MUserSkill item);

        #endregion

        #region UserHobby

        public IQueryable<MUserHobby> GetUserHobbyDB();
        public Task<List<MUserHobby>> GetUserHobbyAll();
        public Task<MUserHobby> GetUserHobbyByID(int id);
        public Task<List<SelectListItem>> GetUserHobbySelectItemList(bool setDefault = false);
        public Task<ResultInfo> InsertUserHobby(MUserHobby item);
        public Task<ResultInfo> UpdateUserHobby(MUserHobby item);
        public Task<ResultInfo> DeleteUserHobby(MUserHobby item);

        #endregion
    }
}
