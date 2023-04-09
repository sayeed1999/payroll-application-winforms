using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Payroll.Service
{
    public class Employee
    {
        Connection conn = new Connection();
        int count = 0;

        public DataTable GetAllEmployees()
        {
            conn.dataGet("Select * from Employee");
            DataTable dt = new DataTable();
            conn.sda.Fill(dt);
            count = dt.Rows.Count;
            return dt;
        }

        private bool IfEmployeeExists(string email)
        {
            conn.dataGet("Select 1 from employee where email = '" + email + "'");
            DataTable dt = new DataTable();
            conn.sda.Fill(dt);
            return (dt.Rows.Count > 0);
        }

        public bool CreateEmployee(string name, string email, string mobile, string filename, byte[] file)
        {
            if (IfEmployeeExists(email))
            {
                throw new Exception("Employee already exists!");
            }

            conn.dataSend("Insert into Employee (EmpId, Name, Email, Mobile, FileName, ImageData) values (" + (count + 1) + ", '" + name + "','" + email + "','" + mobile + "','" + filename + "','" + file + "')");
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
            return true;
        }

    }
}
