using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("DailyTask")]
    public partial class TDailyTask : BaseTable<TDailyTask>
    {        
        [StringLength(50)] public string? ReportByID { get; set; }
        public DateTime? ReportedOn { get; set; }
        [Required] public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        [Required] public int? PICID { get; set; }
        [Required] public int? StatusID { get; set; }
        [Required] public int? TypeID { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime? ScheduledOn { get; set; }
    }
}
