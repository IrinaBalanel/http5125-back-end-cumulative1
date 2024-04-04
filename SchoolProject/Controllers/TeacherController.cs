using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List -> returns a page listing teachers in the system
        public ActionResult List(string SearchKey)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET: /Teacher/Show/{id} -> returns a page with a particular teacher and his parameters
        public ActionResult Show(int id) 
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GET: /Teacher/New/ -> returns a page with a form fields for creating a new teacher in DB
        public ActionResult New()
        { 
           
            return View(); 
        }

        [HttpPost]
        //POST: /Teacher/Create/ -> returns List.cshtml
        public ActionResult Create(string TeacherFirstName, string TeacherLastName, string EmployeeNumber, decimal Salary)
        {
            TeacherDataController TeacherController = new TeacherDataController();

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFirstName = TeacherFirstName;
            NewTeacher.TeacherLastName = TeacherLastName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherController.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        //GET: /Teacher/DeleteConfirm/ -> a webpade which asks the user for confirmation to delete
        public ActionResult DeleteConfirm(int id) 
        {
            TeacherDataController TeacherController = new TeacherDataController();
            Teacher SelectedTeacher = TeacherController.FindTeacher(id);
            
            return View(SelectedTeacher);
        }


        [HttpPost]
        //POST: /Teacher/Delete/{id} -> redirects to List.cshtml view of teachers
        public ActionResult Delete(int id) 
        {
            TeacherDataController TeacherController = new TeacherDataController();
            TeacherController.DeleteTeacher(id);

            return RedirectToAction("List");
        }

    }
}