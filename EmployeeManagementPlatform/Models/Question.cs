using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("NQuestion")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDQuestion { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameQuestion { get; set; }
    }
}