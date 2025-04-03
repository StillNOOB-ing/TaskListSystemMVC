using TaskListSystemMVC.Database.Model;
using TaskListSystemMVC.Database.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskListSystemMVC.Helper;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;


namespace TaskListSystemMVC.Database.Helper
{
    public class MasterHelper: IMasterHelper
    {      
        private readonly IMasterRepository repository;
        private readonly IAccountHelper accHelper;

        public MasterHelper(IMasterRepository masterRepository, IAccountHelper accountHelper)
        {
            repository = masterRepository;
            accHelper = accountHelper;
        }

        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB()
        {
            return repository.GetAccountInfoDB(x => true);
        }
        public async Task<List<MAccountInfo>> GetAccountInfoAll()
        {
            return await repository.GetAccountInfoAll(x => true);
        }
        public async Task<MAccountInfo> GetAccountInfoByID(int id)
        {
            return (await repository.GetAccountInfoAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetAccountInfoSelectItemList(bool setDefault = false)
        {
            var result = await GetAccountInfoAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem());
            }           
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertAccountInfo(MAccountInfo item)
        {
            item.Password = accHelper.HashPassword(item.Password);

            item.LevelRightName = (await GetUserLevelRightByID(item.LevelRightID.Value)).Name;

            if (!string.IsNullOrEmpty(item.Skill))
            {
                string[] skillItems = item.Skill.Split(',');
                var skillList = await GetUserSkillAll();
                foreach (string skill in skillItems)
                {
                    if (!string.IsNullOrEmpty(skill) && !skillList.Exists(x => x.Name == skill.Trim()))
                    {
                        await InsertUserSkill(new MUserSkill { Name = skill.Trim() });
                    }
                }
            }
            if (!string.IsNullOrEmpty(item.Hobby))
            {
                string[] hobbyItems = item.Hobby.Split(',');
                var hobbyList = await GetUserHobbyAll();
                foreach (string hobby in hobbyItems)
                {
                    if (!string.IsNullOrEmpty(hobby) && !hobbyList.Exists(x => x.Name == hobby.Trim()))
                    {
                        await InsertUserHobby(new MUserHobby { Name = hobby.Trim() });
                    }
                }
            }

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertAccountInfo(item);
        }
        public async Task<ResultInfo> UpdateAccountInfo(MAccountInfo item)
        {
            var acc = await GetAccountInfoByID(item.UID);
            if (acc != null) 
            {
                if (item.Password != acc.Password)
                {
                    item.Password = accHelper.HashPassword(item.Password);
                }
            }

            item.LevelRightName = (await GetUserLevelRightByID(item.LevelRightID.Value)).Name;

            if (!string.IsNullOrEmpty(item.Skill))
            {
                string[] skillItems = item.Skill.Split(',');
                var skillList = await GetUserSkillAll();
                foreach (string skill in skillItems)
                {
                    if (!string.IsNullOrEmpty(skill) && !skillList.Exists(x => x.Name == skill.Trim()))
                    {
                        await InsertUserSkill(new MUserSkill { Name = skill.Trim() });
                    }
                }
            }
            if (!string.IsNullOrEmpty(item.Hobby))
            {
                string[] hobbyItems = item.Hobby.Split(',');
                var hobbyList = await GetUserHobbyAll();
                foreach (string hobby in hobbyItems)
                {
                    if (!string.IsNullOrEmpty(hobby) && !hobbyList.Exists(x => x.Name == hobby.Trim()))
                    {
                        await InsertUserHobby(new MUserHobby { Name = hobby.Trim() });
                    }
                }
            }

            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateAccountInfo(item);
        }
        public async Task<ResultInfo> DeleteAccountInfo(MAccountInfo item)
        {
            return await repository.DeleteAccountInfo(item);
        }

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB()
        {
            return repository.GetUserLevelRightDB(x => true);
        }
        public async Task<List<MUserLevelRight>> GetUserLevelRightAll()
        {
            return await repository.GetUserLevelRightAll(x => true);
        }
        public async Task<MUserLevelRight> GetUserLevelRightByID(int id)
        {
            return (await repository.GetUserLevelRightAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetUserLevelRightSelectItemList(bool setDefault = false)
        {
            var result = await GetUserLevelRightAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem { Value = null, Text = string.Empty });
            }
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item)
        {           
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertUserLevelRight(item);
        }
        public async Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateUserLevelRight(item);
        }
        public async Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item)
        {
            return await repository.DeleteUserLevelRight(item);
        }

        #endregion

        #region Status

        public IQueryable<MStatus> GetStatusDB()
        {
            return repository.GetStatusDB(x => true);
        }
        public async Task<List<MStatus>> GetStatusAll()
        {
            return await repository.GetStatusAll(x => true);
        }
        public async Task<MStatus> GetStatusByID(int id)
        {
            return (await repository.GetStatusAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetStatusSelectItemList(bool setDefault = false)
        {
            var result = await GetStatusAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem { Value = null, Text = string.Empty });
            }
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertStatus(MStatus item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertStatus(item);
        }
        public async Task<ResultInfo> UpdateStatus(MStatus item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateStatus(item);
        }
        public async Task<ResultInfo> DeleteStatus(MStatus item)
        {
            return await repository.DeleteStatus(item);
        }

        #endregion

        #region Type

        public IQueryable<MType> GetTypeDB()
        {
            return repository.GetTypeDB(x => true);
        }
        public async Task<List<MType>> GetTypeAll()
        {
            return await repository.GetTypeAll(x => true);
        }
        public async Task<MType> GetTypeByID(int id)
        {
            return (await repository.GetTypeAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetTypeSelectItemList(bool setDefault = false)
        {
            var result = await GetTypeAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem { Value = null, Text = string.Empty });
            }
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertType(MType item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertType(item);
        }
        public async Task<ResultInfo> UpdateType(MType item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateType(item);
        }
        public async Task<ResultInfo> DeleteType(MType item)
        {
            return await repository.DeleteType(item);
        }

        #endregion

        #region PublicHoliday

        public IQueryable<MPublicHoliday> GetPublicHolidayDB()
        {
            return repository.GetPublicHolidayDB(x => true);
        }
        public async Task<List<MPublicHoliday>> GetPublicHolidayAll()
        {
            return await repository.GetPublicHolidayAll(x => true);
        }
        public async Task<MPublicHoliday> GetPublicHolidayByID(int id)
        {
            return (await repository.GetPublicHolidayAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<ResultInfo> InsertPublicHoliday(MPublicHoliday item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertPublicHoliday(item);
        }
        public async Task<ResultInfo> UpdatePublicHoliday(MPublicHoliday item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdatePublicHoliday(item);
        }
        public async Task<ResultInfo> DeletePublicHoliday(MPublicHoliday item)
        {
            return await repository.DeletePublicHoliday(item);
        }

        #endregion

        #region UserSkill

        public IQueryable<MUserSkill> GetUserSkillDB()
        {
            return repository.GetUserSkillDB(x => true);
        }
        public async Task<List<MUserSkill>> GetUserSkillAll()
        {
            return await repository.GetUserSkillAll(x => true);
        }
        public async Task<MUserSkill> GetUserSkillByID(int id)
        {
            return (await repository.GetUserSkillAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetUserSkillSelectItemList(bool setDefault = false)
        {
            var result = await GetUserSkillAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem { Value = null, Text = string.Empty });
            }
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertUserSkill(MUserSkill item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertUserSkill(item);
        }
        public async Task<ResultInfo> UpdateUserSkill(MUserSkill item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateUserSkill(item);
        }
        public async Task<ResultInfo> DeleteUserSkill(MUserSkill item)
        {
            return await repository.DeleteUserSkill(item);
        }

        #endregion

        #region UserHobby

        public IQueryable<MUserHobby> GetUserHobbyDB()
        {
            return repository.GetUserHobbyDB(x => true);
        }
        public async Task<List<MUserHobby>> GetUserHobbyAll()
        {
            return await repository.GetUserHobbyAll(x => true);
        }
        public async Task<MUserHobby> GetUserHobbyByID(int id)
        {
            return (await repository.GetUserHobbyAll(x => x.UID == id)).FirstOrDefault();
        }
        public async Task<List<SelectListItem>> GetUserHobbySelectItemList(bool setDefault = false)
        {
            var result = await GetUserHobbyAll();

            var list = new List<SelectListItem>();
            if (!setDefault)
            {
                list.Add(new SelectListItem { Value = null, Text = string.Empty });
            }
            foreach (var item in result)
            {
                list.Add(new SelectListItem
                {
                    Value = item.UID.ToString(),
                    Text = item.Name
                });
            }
            return list;
        }
        public async Task<ResultInfo> InsertUserHobby(MUserHobby item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = accHelper.GetName();

            return await repository.InsertUserHobby(item);
        }
        public async Task<ResultInfo> UpdateUserHobby(MUserHobby item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = accHelper.GetName();

            return await repository.UpdateUserHobby(item);
        }
        public async Task<ResultInfo> DeleteUserHobby(MUserHobby item)
        {
            return await repository.DeleteUserHobby(item);
        }

        #endregion
    }
}
