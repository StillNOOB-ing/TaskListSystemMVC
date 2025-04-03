using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TaskListSystemMVC.Database;
using TaskListSystemMVC.Database.Model;

namespace TaskListSystemMVC.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TDailyTask> DailyTasks { get; set; }
        public DbSet<MAccountInfo> AccountInfos { get; set; }
        public DbSet<MUserSkill> UserSkills { get; set; }
        public DbSet<MUserHobby> UserHobbys { get; set; }
        public DbSet<MUserLevelRight> UserLevelRights { get; set; }
        public DbSet<MStatus> Statuss { get; set; }
        public DbSet<MType> Types { get; set; }
        public DbSet<MPublicHoliday> PublicHolidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            // for view table
            //modelBuilder.Entity<MAccountInfo>(entity =>
            //{
            //    entity.HasNoKey();
            //    entity.ToView("MAccountInfo");
            //});
        }
    }
}
