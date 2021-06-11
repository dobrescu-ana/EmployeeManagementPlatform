using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPlatform.Models
{
    [Table("Wages")]
    public class Salary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSalary { get; set; }

        [Required]
        [MaxLength(20)]
        public string EmploymentType { get; set; }

        [Required]
        public float BasicSalary { get; set; }

        public float MedicalAllowance { get; set; }

        public float SpecialAllowance { get; set; }

        public float FuelAllowance { get; set; }

        public float ProvidedFund { get; set; }

        public float TaxDeduction { get; set; }

        public float OtherDeduction { get; set; }

        public float TotalDeduction { get; set; }

        [Required]
        public float GrossSalary { get; set; }

        [Required]
        public float NetSalary { get; set; }

        public int IDEmployee { get; set; }
        [ForeignKey("IDEmployee")]
        public Employee employee;
    }
}