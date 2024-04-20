using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
// Use DataAnnotations attribute in the model to specify what data is required
// Create required errors for first name and last name if validation fails

namespace SchoolProject.Models
{   //create Teacher class and describe its parameters from the DB
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string TeacherFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string TeacherLastName { get; set; }

        [Required(ErrorMessage = "Employee number is required")]
        public string EmployeeNumber { get; set; }

        public DateTimeOffset HireDate { get; set; }

        /*[Required(ErrorMessage = "Salary is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than zero.")]*/
        public decimal Salary { get; set; }

        public string ClassName { get; set; }
    }
}