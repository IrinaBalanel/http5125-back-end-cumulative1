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
        /*        public ActionResult Create(string TeacherFirstName, string TeacherLastName, string EmployeeNumber, decimal Salary)
                {
                    TeacherDataController TeacherController = new TeacherDataController();

                        Teacher NewTeacher = new Teacher();
                        NewTeacher.TeacherFirstName = TeacherFirstName;
                        NewTeacher.TeacherLastName = TeacherLastName;
                        NewTeacher.EmployeeNumber = EmployeeNumber;
                        NewTeacher.Salary = Salary;

                        TeacherController.AddTeacher(NewTeacher);

                        return RedirectToAction("List");

                }*/

        public ActionResult Create(Teacher NewTeacher)
        {
            // Check if the data passes the validation from Teacher model.
            // If validation is passed, the form is submitted to the databse and it redirects to List.cshtml
            if (ModelState.IsValid)
            {
                TeacherDataController TeacherController = new TeacherDataController();
                TeacherController.AddTeacher(NewTeacher);
                return RedirectToAction("List");
            }
            else
            {
                // If insertion fails, the form remains the same with error messages
                return View("New");
            }
        }

            //GET: /Teacher/DeleteConfirm/ -> a webpade which asks the user for confirmation to delete
            public ActionResult DeleteConfirm(int id) 
        {
            TeacherDataController TeacherController = new TeacherDataController();
            Teacher SelectedTeacher = TeacherController.FindTeacher(id);
            
            return View(SelectedTeacher);
        }


        [HttpPost]
        //POST: /Teacher/Delete/{teacherid} -> redirects to List.cshtml view of teachers
        public ActionResult Delete(int id) 
        {
            TeacherDataController TeacherController = new TeacherDataController();
            TeacherController.DeleteTeacher(id);
            
            return RedirectToAction("List");
        }

        //GET: /Teacher/Update/{teacherid}
        /// <summary>
        /// webpage asking user to update teacher's information
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/1</example>
        [HttpGet]
        public ActionResult Update(int id) 
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            //goes to /Views/Teacher/Update.cshtml
            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values from user and redirects to the "Teacher Show" page of the updated teacher.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <param name="TeacherFirstName">The updated first name of the teacher</param>
        /// <param name="TeacherLastName">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <param name="Salary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the author.</returns>
        /// <example>
        /// POST : /Teacher/Update/1
        /// POST DATA / REQUEST BODY 
        /// {
        ///     "TeacherFirstName":"Irina"
        ///     "TeacherLastName":"Balanel"
        ///     "EmployeeNumber":"T123"
        ///     "Salary":"30.00"
        /// }
        /// </example>


        [HttpPost]
        //POST: /Teacher/Edit/{teacherid} -> Receiving teacher info to update DB
        public ActionResult Edit(int id, string TeacherFirstName, string TeacherLastName, string EmployeeNumber, decimal Salary)
        {

            TeacherDataController Controller = new TeacherDataController();

            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.TeacherFirstName = TeacherFirstName;
            UpdatedTeacher.TeacherLastName = TeacherLastName;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.Salary = Salary;

            Controller.UpdateTeacher(id, UpdatedTeacher);
            //goes to /Views/Teacher/Show/{teacherid}
            return RedirectToAction("Show/" + id);












            /*SERVER SIDE VALIDATION LOGIC: METHOD NUMBER 1*/

            /*            // Check if the data passes the validation from Teacher model.
                        // If validation is passed, the form is submitted to the databse and it redirects to Edit/teacherId
                        if (ModelState.IsValid)
                        {
                            // Update will be done only if all required fields are provided
                            TeacherDataController Controller = new TeacherDataController();

                            Teacher UpdatedTeacher = new Teacher();
                            UpdatedTeacher.TeacherFirstName = TeacherFirstName;
                            UpdatedTeacher.TeacherLastName = TeacherLastName;
                            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
                            UpdatedTeacher.Salary = Salary;

                            Controller.UpdateTeacher(id, UpdatedTeacher);
                            //goes to /Views/Teacher/Show/{teacherid}
                            return RedirectToAction("Show/" + id);
                        }
                        else
                        {
                            // If insertion fails, the form remains the same with error messages
                            return View("Update");
                        }*/


            /*SERVER SIDE VALIDATION LOGIC: METHOD NUMBER 2*/

            /*            if (string.IsNullOrWhiteSpace(TeacherFirstName) || string.IsNullOrWhiteSpace(TeacherLastName) || string.IsNullOrWhiteSpace(EmployeeNumber) || Salary == 0)
                        {
                            return View("Update");

                        }
                        else
                        {
                            // Update will be done only if all required fields are provided
                            TeacherDataController Controller = new TeacherDataController();

                            Teacher UpdatedTeacher = new Teacher();
                            UpdatedTeacher.TeacherFirstName = TeacherFirstName;
                            UpdatedTeacher.TeacherLastName = TeacherLastName;
                            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
                            UpdatedTeacher.Salary = Salary;

                            Controller.UpdateTeacher(id, UpdatedTeacher);
                            //goes to /Views/Teacher/Show/{teacherid}
                            return RedirectToAction("Show/" + id);
                        }*/

            /*SERVER SIDE VALIDATION LOGIC: METHOD NUMBER 3*/

            /*            if (Salary == null || Salary <= 0)
            {
                ModelState.AddModelError("Salary", "Salary must be greater than zero.");
            }
            if (ModelState.IsValid)
            {
                TeacherDataController Controller = new TeacherDataController();

                Teacher UpdatedTeacher = new Teacher();
                UpdatedTeacher.TeacherFirstName = TeacherFirstName;
                UpdatedTeacher.TeacherLastName = TeacherLastName;
                UpdatedTeacher.EmployeeNumber = EmployeeNumber;
                UpdatedTeacher.Salary = Salary.Value;

                Controller.UpdateTeacher(id, UpdatedTeacher);
                //goes to /Views/Teacher/Show/{teacherid}
                return RedirectToAction("Show/" + id);
            }
            else
            {
                // If insertion fails, the form remains the same with error messages
                return View("Update");
            }*/

        }







    }
}