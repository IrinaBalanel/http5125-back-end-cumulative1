using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SchoolProject.Models
{   //create Teacher class and describe its parameters from the DB
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTimeOffset HireDate { get; set; }
        public decimal Salary { get; set; }
        public string ClassName { get; set; }
    }
}