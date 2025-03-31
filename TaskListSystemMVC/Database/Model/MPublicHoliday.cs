using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("PublicHoliday")]
    public partial class MPublicHoliday : BaseTable<MPublicHoliday>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Remark { get; set; }
        [Required] public DateTime? StartDate { get; set; }
        [Required] public DateTime? EndDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Day must be greater than zero!")] public int? Day { get; set; }
    }
}
