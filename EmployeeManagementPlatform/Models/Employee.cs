using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDEmployee { get; set; }

        [MaxLength(20)]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "This field must contain only letters")]
        [Required(ErrorMessage = "You must enter the first name")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "This field must contain only letters")]
        [Required(ErrorMessage = "You must enter the last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must give specific rights to the employee")]
        [Range(0,3,ErrorMessage = "You need to enter a number between 1 and 3")]
        public int IntRight { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfJoin { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage ="You must enter an address")]
        public string Address { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "You must enter the employee phone number")]
        public string PhoneNo { get; set; }

        [Required]
        public bool Gender { get; set; }

        public int IdDepartment { get; set; }
        [ForeignKey("IdDepartment")]
        public virtual Department department { get; set; }

        [Required]
        public string Function { get; set; }

        [MaxLength(500)]
        public string Skills { get; set; }

        [MaxLength(2000)]
        public string Education { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public int AccountStatus { get; set; }

        public int IDQuestion { get; set; }
        [ForeignKey("IDQuestion")]
        public virtual Question question { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name ="Raspuns de securitate")]
        public string Response { get; set; }

    }
}