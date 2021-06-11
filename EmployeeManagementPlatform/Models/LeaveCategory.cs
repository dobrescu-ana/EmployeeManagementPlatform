using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("NLeave")]
    public class LeaveCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDTypeLeave { get; set; }

        [MaxLength(30)]
        [Required()]
        public string NameTypeLeave { get; set; }
    }
}