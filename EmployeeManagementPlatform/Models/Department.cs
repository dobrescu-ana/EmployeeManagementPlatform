using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("NDepartment")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDepartment { get; set; }

        [MaxLength(30)]
        [Required()]
        public string DepartmentName { get; set; }

        [MaxLength(1000)]
        [Required()]
        public string Details { get; set; }
    }
}