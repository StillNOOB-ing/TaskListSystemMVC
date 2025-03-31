using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("UserLevelRight")]
    public partial class MUserLevelRight : BaseTable<MUserLevelRight>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
