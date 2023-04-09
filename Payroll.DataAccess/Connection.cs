using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable Select(
            string table, 
            List<Tuple<string, string>> fields
        ) {
            try
            {
                connection();
                string sql = $"select * from {table}";
                bool whereAdded = false;
                foreach (Tuple<string, string> field in fields)
                {
                    if (!whereAdded)
                    {
                        whereAdded = true;
                        sql += $" where {field.Item1} = '{field.Item2}'";
                    }
                    else
                    {
                        sql += $" and {field.Item1} = '{field.Item2}'";
                    }
                }
                sda = new SqlDataAdapter(sql, conn);
            }
            catch (Exception ex)
            {

            }
            conn.Close();

            // preparing result set
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            return dataTable;
        }

        // only string supported currently
        public DataTable Insert(string table, List<string> keys, List<object> values)
        {
            if (keys.Count != values.Count)
                throw new Exception("sql query is not valid!");

            string sql = $"insert into {table}";

            int count = 0;
            foreach (string key in keys)
            {
                if (count == 0)
                {
                    sql += $" ( {key}";
                }
                else
                {
                    sql += $", {key}";
                }
                count++;
            }
            if (count > 0) sql += " ) values ( ";

            count = 0;
            foreach (string value in values)
            {
                if (count > 0) sql += ",";
                sql += $" '{value}'";
                count++;
            }
            if (count > 0) sql += " )";

            try
            {
                connection();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                pkk = "";
            }
            catch (Exception ex)
            {
                pkk = "Please check your data";
            }
            conn.Close();

            // preparing result set
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            return dataTable;
        }

        public string Update(string table, List<Tuple<string, string>> filters, List<Tuple<string, object>> values)
        {

            string sql = $"update {table}";

            int count = 0;
            foreach (Tuple<string, object> key in values)
            {
                if (count == 0) sql += " set";
                else sql += " ,";
                sql += $" {key.Item1} = '{key.Item2}'";
                count++;
            }

            count = 0;
            foreach (Tuple<string, string> value in filters)
            {
                if (count > 0) sql += " and";
                else sql += " where";
                sql += $" {value.Item1} = '{value.Item2}'";
                count++;
            }

            try
            {
                connection();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                pkk = "";
            }
            catch (Exception ex)
            {
                pkk = "Please check your data";
            }
            conn.Close();

            // preparing result set
            return pkk;
        }

    }
}
