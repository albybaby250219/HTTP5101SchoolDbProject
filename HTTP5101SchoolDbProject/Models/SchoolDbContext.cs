using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//The cnnection  to mysql
using MySql.Data.MySqlClient;

namespace HTTP5101SchoolDbProject.Models
{
    //Creating a model to connect the schooldb database
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        ///Method to use the database
        /// <summary>
        /// Returns a connection to the schooldb database
        /// </summary> 
        /// <returns>Returns a MySqlConnection object</returns>
        /// <example>private SchoolDbContext SchoolDb = new SchoolDbContext();
                  /// MySqlConnection Conn = SchoolDb.AccessDatabase();
       /// </example>
        public MySqlConnection AcessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}