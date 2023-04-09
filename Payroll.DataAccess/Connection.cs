using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DataAccess
{
    public class Connection
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataAdapter sda;
        public string pkk;

        public void connection()
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-PMBUO9G\SQLEXPRESS;Initial Catalog=MiniPayroll;Integrated Security=True");
            conn.Open();
        }

        public void dataSend(string SQL)
        {
            try
            {
                connection();
                cmd = new SqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
                pkk = "";
            }
            catch (Exception ex)
            {
                pkk = "Please check your data";
            }
            conn.Close();
        }

        public void dataGet(string SQL)
        {
            try
            {
                connection();
                sda = new SqlDataAdapter(SQL, conn);
            }
            catch (Exception ex)
            {

            }
            conn.Close();
        }
    }
}
