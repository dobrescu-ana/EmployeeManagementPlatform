using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("Leaves")]
    public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLeaves { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Reason { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string Destination { get; set; }

        public int IDEmployee { get; set; }
        [ForeignKey("IDEmployee")]
        public virtual Employee employee { get; set; }

        public int IDTypeLeave { get; set; }
        [ForeignKey("IDTypeLeave")]
        public virtual LeaveCategory leaveCategory { get; set; }

    }
}