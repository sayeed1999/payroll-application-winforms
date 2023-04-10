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

        public DataTable Select(
            string table, 
            List<Filter> fields
        ) {
            try
            {
                connection();
                string sql = $"select * from {table}";
                bool whereAdded = false;
                foreach (Filter field in fields)
                {
                    if (!whereAdded)
                    {
                        whereAdded = true;
                        sql += " where ";
                    }
                    else
                        sql += " and ";
                    
                    sql += $"{field.Key} {field.Operator} '{field.Value}'";
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
                    sql += " ( ";
                else
                    sql += ", ";
                sql += $"{key}";
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

        public string Update(string table, List<Filter> filters, List<Tuple<string, object>> values)
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
            foreach (Filter filter in filters)
            {
                if (count > 0) sql += " and";
                else sql += " where";
                sql += $" {filter.Key} {filter.Operator} '{filter.Value}'";
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

        public string Delete(string table, List<Filter> filters)
        {
            string sql = $"delete from {table}";

            int count = 0;
            foreach (Filter filter in filters)
            {
                if (count > 0) sql += " and";
                else sql += " where";
                sql += $" {filter.Key} {filter.Operator} '{filter.Value}'";
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
