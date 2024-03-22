using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using Google.Protobuf.WellKnownTypes;
using Mysqlx.Datatypes;
using Org.BouncyCastle.Asn1.Cms;


namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // create a class which allows us to access MySQL school database:
        private SchoolDbContext School = new SchoolDbContext();

        ///<summary>
        /// This controller will access the teachers table in DB and outputs a list of teachers. It also allows a search in teachers database by name and salary
        /// </summary>
        /// <returns>
        /// Returns a list of teachers in given database
        /// </returns>
        /// <example>
        /// GET api/teacherdata/listteachers -> ["Alexander Bennett", "Caitlin Cummings", "Linda Chan"]
        /// </example>

        [HttpGet]
        [Route ("api/teacherdata/listteachers")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey)
        {
            //Open a connection 
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open ();

            // Create the first command and a query which allows a search by name
            MySqlCommand Cmd1 = Conn.CreateCommand ();
            string query1 = "select * from teachers where teacherfname like @SearchKey or teacherlname like @SearchKey";
            
            //Define @SearchKey to avoid SQL misinterpretations //Define @SearchKey
            Cmd1.Parameters.AddWithValue ("@SearchKey", "%"+SearchKey+"%");
            Cmd1.Prepare();

            // Associate sql command to query 
            Cmd1.CommandText = query1;

            //Execute command and store it in in a result set
            MySqlDataReader ResultSet = Cmd1.ExecuteReader ();


            //Create list of teachers
            List<Teacher> Teachers = new List<Teacher>();

            //Loop through the result set to retrive information 

            while (ResultSet.Read ())
            {
                //Retrieve teacher first name, teacher last name and show it in the list
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                string TeacherFirstName = ResultSet["teacherfname"].ToString ();

                string TeacherLastName = ResultSet["teacherlname"].ToString();

                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                DateTimeOffset HireDate = DateTimeOffset.Parse(ResultSet["hiredate"].ToString());

                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                //Create new teacher instance of teacher class
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFirstName = TeacherFirstName;
                NewTeacher.TeacherLastName = TeacherLastName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                Teachers.Add(NewTeacher);
                
            }

            //Close the first data reader before executing the second query
            ResultSet.Close();




            // Create the second command and a query which allows a search by salary
            MySqlCommand Cmd2 = Conn.CreateCommand();
            string query2 = "select * from teachers where salary like @SearchKey";

            //Define @SearchKey to avoid SQL misinterpretations 
            Cmd2.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            Cmd2.Prepare();

            // Associate sql command to query 
            Cmd2.CommandText = query2;

            //Execute command and store it in a result set
            MySqlDataReader ResultSet2 = Cmd2.ExecuteReader();

            //Loop through the result set to retrive information 
            while (ResultSet2.Read())
            {
                //Retrieve teacher first name, teacher last name and show it in the list
                int TeacherId = Convert.ToInt32(ResultSet2["teacherid"]);

                string TeacherFirstName = ResultSet2["teacherfname"].ToString();

                string TeacherLastName = ResultSet2["teacherlname"].ToString();

                string EmployeeNumber = ResultSet2["employeenumber"].ToString();

                DateTimeOffset HireDate = DateTimeOffset.Parse(ResultSet2["hiredate"].ToString());

                decimal Salary = Convert.ToDecimal(ResultSet2["salary"]);

                //Create new teacher instance of teacher class
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFirstName = TeacherFirstName;
                NewTeacher.TeacherLastName = TeacherLastName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                Teachers.Add(NewTeacher);

            }


            //Close connection
            Conn.Close ();

            //Output list of teachers
            return Teachers;
        }




        ///<summary>
        /// Finds a teacher in the system by an id
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object with all the parameters from the teachers database along with classes taught by this particular teacher</returns>
        /// <example>
        /// GET /api/teacherdata/findteacher/1 -> ["Alexander Bennett, ID in the school database: 1, Employee number: T378, Hire date: 8/5/2016 12:00:00 AM -04:00, Salary: $55.30, Courses: Web Application Development"]
        /// </example>


        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //Open connection
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open ();

            //Create a first command and sql query which finds a teacher by id
            MySqlCommand Cmd = Conn.CreateCommand ();
            string query = "select * from teachers where teacherid=" + id;
            Cmd.CommandText = query;

            //Execute the command
            MySqlDataReader ResultSet = Cmd.ExecuteReader ();

            //Create new instance of a teacher class to store a returned resultset
            Teacher NewTeacher = new Teacher();

            //Loop through the result set to retrive information 
            while (ResultSet.Read ())
            {

                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFirstName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLastName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = DateTimeOffset.Parse(ResultSet["hiredate"].ToString());
                NewTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
            }

            //close the first data reader before executing the second query
            ResultSet.Close();

            //Create a second command and sql query which joins teachers table and classes table to show classes taught by this particular teacher

            MySqlCommand Cmd2 = Conn.CreateCommand();
            string query2 = "select classes.classname from teachers join classes on classes.teacherid = teachers.teacherid where teachers.teacherid= " + id;
            Cmd2.CommandText = query2;

            //Execute the command and retrive information
            MySqlDataReader ResultSet2 = Cmd2.ExecuteReader();
            
            //Create a new list of classes
            List<string>Classes = new List<string>();

            //Loop through the resultset and output all classes
            while (ResultSet2.Read())
            {

                string Class = ResultSet2["classname"].ToString();
                Classes.Add(Class);
               
            }


            //Because of the conflict between data types, I added these two lines to make it work:

            //Concatenate the list of class names into a single string
            string concatenatedClassNames = string.Join(", ", Classes);

            // Assign the concatenated string to NewTeacher.ClassName
            NewTeacher.ClassName = concatenatedClassNames;

            //Output all gathered information
            return NewTeacher;
        }


    }
}
