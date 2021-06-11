using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("Logs")]
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }

        [MaxLength(500)]
        [Required]
        public string LogTitle { get; set; }

        [MaxLength(1000)]
        [Required]
        public string LogDescription { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EventDate { get; set; }
    
        public string LogLevel { get; set; }

        //public int IDEmployee { get; set; }
        //[ForeignKey("IDEmployee")]
        //public Employee Author { get; set; }

        public string Author { get; set; }

        #region CTORS
        public Logs()
        {

        }

        public Logs(string title, string description, DateTime datetime, string loglevel, string author)
        {
            LogTitle = title;
            LogDescription = description;
            EventDate = datetime;
            LogLevel = loglevel;
            Author = author;
        }

        #endregion
    }
}