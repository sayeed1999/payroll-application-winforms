using Payroll.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Payroll.Service
{
    public class User
    {
        Connection conn = new Connection();

        public DataTable GetAllUsers()
        {
            DataTable dt = conn.Select(
                "[User]", new List<Filter> { }
            );
            return dt;
        }

        public bool Login(string username, string password)
        {
            DataTable dt = conn.Select(
                "[User]", new List<Filter>
                {
                    new Filter("username", Operator.Equal, username), 
                    new Filter("password", Operator.Equal, password)
                });
            return (dt.Rows.Count > 0);
        }

        public bool Register(string name, string email, string username, string password, string role, DateTime dob, string address)
        {
            conn.Insert("[User]",
                new List<string> { "name", "email", "username", "password",  "role", "dob", "address" },
                new List<object> { name, email, username, password, role, dob.ToString("dd/MM/yyyy"), address });
            return String.IsNullOrEmpty(conn.pkk);
        }

        // returns the no. of rows updated
        public bool UpdateUser(string email, string username, string name, DateTime dob, string role, string address)
        {
            string error = conn.Update("[User]",
                new List<Filter> { 
                    new Filter("email", Operator.Equal, email),
                },
                new List<Tuple<string, object>> { 
                    new Tuple<string, object>("username", username),
                    new Tuple<string, object>("name", name),
                    new Tuple<string, object>("dob", dob.ToString("dd/MM/yyyy")),
                    new Tuple<string, object>("role", role),
                    new Tuple<string, object>("address", address),
                });
            return error.Length == 0;
        }
        
        public bool ChangePassword(string username, string newPassword)
        {
            string error = conn.Update("[User]",
                new List<Filter> {
                    new Filter("username", Operator.Equal, username),
                },
                new List<Tuple<string, object>> {
                    new Tuple<string, object>("password", newPassword),
                });
            return (error.Length == 0);
        }

        public bool DeleteUser(string email, string username)
        {
            string error = conn.Delete("[User]", new List<Filter>
                {
                    new Filter ("email", Operator.Equal, email)
                });

            return (error.Length == 0);
        }
    }
}
