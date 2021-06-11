using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDTask { get; set; }

        [Required(ErrorMessage = "You must enter the task title")]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public int IDEmployee { get; set; }
        [ForeignKey("IDEmployee")]
        public virtual Employee employee { get; set; }

        [NotMapped]
        public int difference { get; set; }
    }
}