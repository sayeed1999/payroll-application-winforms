using Payroll.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                "[User]", new List<Tuple<string, string>>()
            );
            return dt;
        }

        public bool Login(string username, string password)
        {
            DataTable dt = conn.Select(
                "[User]", new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("username", username), 
                    new Tuple<string, string>("password", password)
                });
            return (dt.Rows.Count > 0);
        }

        public bool Register(string name, string email, string username, string password, string role, DateTime dob, string address)
        {
            conn.dataSend("Insert into [User] (Name, Email, Username, Password, Role, DOB, Address) values ('" + name + "','" + email + "','" + username + "','" + password + "','" + role + "','" + dob.ToString("dd/MM/yyyy") + "','" + address + "')");
            return String.IsNullOrEmpty(conn.pkk);
        }

        // returns the no. of rows updated
        public int UpdateUser(string email, string username, string name, DateTime dob, string role, string address)
        {
            DataTable dt = new DataTable();
            conn.dataSend("UPDATE [User] SET Username ='" + username + "', Name ='" + name + "', DOB ='" + dob.ToString("dd/MM/yyyy") + "', Role ='" + role + "', Address ='" + address + "' where Email = '" + email + "'");
            conn.sda.Fill(dt);
            return dt.Rows.Count;
        }
        
        public bool ChangePassword(string username, string newPassword)
        {
            conn.dataSend("UPDATE [User] SET Password ='" + newPassword + "' where Username = '" + username + "'");
            return String.IsNullOrEmpty(conn.pkk);
        }

        public int DeleteUser(string email, string username)
        {
            DataTable dt = new DataTable();
            conn.dataSend("DELETE from [User] where Username = '" + username + "' or Email ='" + email + "'");
            conn.sda.Fill(dt);
            return dt.Rows.Count;
        }
    }
}
