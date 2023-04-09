using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Service
{
    public class User
    {
        public bool Login(string username, string password)
        {
            Connection conn = new Connection();
            conn.dataGet("Select * from [User] where Username = '" + username + "' and Password = '" + password + "'");
            DataTable dt = new DataTable();
            conn.sda.Fill(dt);
            return (dt.Rows.Count > 0);
        }
    }
}
