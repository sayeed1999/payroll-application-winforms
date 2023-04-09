using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Payroll.DataAccess;

namespace Payroll.Service
{
    public class Employee
    {
        Connection conn = new Connection();
        int count = 0;

        public DataTable GetAllEmployees()
        {
            DataTable dt = conn.Select("[Employee]", new List<Tuple<string, string>>());
            count = dt.Rows.Count;
            return dt;
        }

        private bool IfEmployeeExists(string email)
        {
            DataTable dt = conn.Select(
                "[Employee]", new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("email", email)
                });
            return (dt.Rows.Count > 0);
        }

        public bool CreateEmployee(string name, string email, string mobile, string filename, byte[] file)
        {
            if (IfEmployeeExists(email))
            {
                throw new Exception("Employee already exists!");
            }

            conn.Insert("[Employee]",
                new List<string> { "EmpId", "Name", "Email", "Mobile", "FileName", "ImageData" },
                new List<object> { (count+1).ToString(), name, email, mobile, filename, file }
            );
            count++;
            return true;
        }

        public bool UpdateEmployee(string name, string email, string mobile, string filename, byte[] file)
        {
            conn.dataSend("Update Employee set Name = '" + name + "', Mobile = '" + mobile + "', FileName = '" + filename + "', ImageData = '" + file + "' where email = '" + email + "'");
            return true;
        }

        public bool DeleteEmployee(string email)
        {
            conn.dataSend("delete from Employee where email = '" + email + "'");
            count--;
            return true;
        }

    }
}
