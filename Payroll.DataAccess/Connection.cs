﻿using System;
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
    }
}
