using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    public class Projects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDProject { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "You must enter a project title")]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Required(ErrorMessage = "You must enter a project description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public int IDEmployee { get; set; }
        [ForeignKey("IDEmployee")]
        public virtual Employee employee { get; set; }



    }
}