using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("NLogLevel")]
    public class LogLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLogLevel { get; set; }

        [MaxLength(30)]
        [Required()]
        public string LogName { get; set; }
    }
}