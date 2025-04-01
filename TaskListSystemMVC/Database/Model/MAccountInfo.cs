using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("AccountInfo")]
    public partial class MAccountInfo : BaseTable<MAccountInfo>
    {
        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required, StringLength(50)] 
        public string? Username { get; set; }

        [Required, EmailAddress] 
        public string? Email { get; set; }

        [Required, StringLength(255, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)] 
        public string? Password { get; set; }

        [NotMapped, Compare(nameof(Password))] 
        public string? ConfirmPassword { get; set; }

        public bool Active { get; set; }

        public DateTime? LastLoginOn { get; set; }

        public int? LevelRightID { get; set; }    
        public string? LevelRightName { get; set; }
    }
}
