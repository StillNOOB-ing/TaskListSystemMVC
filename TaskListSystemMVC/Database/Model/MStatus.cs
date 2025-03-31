using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("Status")]
    public partial class MStatus : BaseTable<MStatus>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
