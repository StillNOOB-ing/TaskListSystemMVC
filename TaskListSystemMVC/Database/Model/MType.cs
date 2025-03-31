﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskListSystemMVC.Database.Model
{
    [Table("Type")]
    public partial class MType : BaseTable<MType>
    {        
        [Required, StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
