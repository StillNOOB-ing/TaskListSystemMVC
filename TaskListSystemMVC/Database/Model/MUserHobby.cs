using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("UserHobby")]
    public partial class MUserHobby : BaseTable<MUserHobby>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
