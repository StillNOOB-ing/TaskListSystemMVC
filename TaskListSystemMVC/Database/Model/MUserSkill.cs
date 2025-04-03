using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("UserSkill")]
    public partial class MUserSkill : BaseTable<MUserSkill>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
