using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using HTTP5101SchoolDbProject.Models;
using System.Diagnostics;

namespace HTTP5101SchoolDbProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        /// <summary>
        ///Connecting the database using the model 
        /// </summary>
        private SchoolDbContext SchoolDb = new SchoolDbContext();
        //This Controller Will access the teachers table in  schooldb database.
        /// <summary>
        /// Returns a list of Teachers from the database
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers/{searchname}</example>
        /// <returns>
        /// A list of Teachers (first names , last names,salary,Hireddate)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{searchname}")]
        public IEnumerable<Teacher> ListTeachers(string SearchName)
        {
            Debug.WriteLine(SearchName);
            ///Connecting to the mysql database 
            ///create an instance of the connection
            MySqlConnection connection = SchoolDb.AcessDatabase();
            ///opening the connection between the server and database
            connection.Open();
            ///Creating a command to query the database
            MySqlCommand command = connection.CreateCommand();
            ///the sql query
            command.CommandText = "select * from teachers where teacherfname like @searchname or teacherlname like @searchname";
            command.Parameters.AddWithValue("@searchname", "%" + SearchName + "%");
            command.Prepare();
            ///Take the data from the database into an array
            MySqlDataReader ResultSet = command.ExecuteReader();
            ///Creating an empty list of teachers
            List<Teacher> Teachers = new List<Teacher>{};
            ///Loops through the rows in the table
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.TeacherHiredDate = TeacherHiredDate;
                NewTeacher.TeacherSalary = TeacherSalary;

                //Add the Teachers Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            connection.Close();
            ///returns the list of teachers
            return Teachers;
        }

        /// <summary>
        /// Finds an author in the system given an ID
        /// </summary>
        /// <param name="id">The author primary key</param>
        /// <returns>An author object</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = SchoolDb.AcessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand command = Connection.CreateCommand();

            //SQL QUERY
            command.CommandText = "Select * from teachers  join classes on teachers.teacherid = classes.teacherid where classes.teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;
                NewTeacher.TeacherHiredDate = TeacherHiredDate;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.ClassCode = ClassCode;
                NewTeacher.ClassName = ClassName;
            }
            Connection.Close();
            Debug.WriteLine(NewTeacher);
            return NewTeacher;
        }

    }
}
