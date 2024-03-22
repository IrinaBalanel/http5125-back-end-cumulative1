using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;
using MySql.Data.MySqlClient;

namespace SchoolProject.Models
{
    public class SchoolDbContext
    {
        //Readonly properties that should be adjusted in order to connect to the database: properties match the ones from school database

        private static string User { get { return "root"; } }
        
        private static string Password { get { return "root"; } }
       
        private static string Database { get { return "school"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        //String of credentials used to connect to the databse
        protected static string ConnectionString
        {
            get
            {

                return "server = " + Server
                + "; user = " + User
                + "; database = " + Database
                + "; port = " + Port
                + "; password = " + Password
                + "; convert zero datetime = True";
            }
        }

        //Method used to get to the school database
        /// <summary>
        /// Returns a connection to the blog database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext(); 
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>MySqlConnection Object</returns>

        public MySqlConnection AccessDatabase() 
        {
            //MySqlConnection Class used to to create an object
            //the object is a specific connection to school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
            



    }
    
}