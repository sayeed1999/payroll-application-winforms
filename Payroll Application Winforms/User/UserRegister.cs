using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms.User
{
    public partial class UserRegister : Form
    {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {
            LoadData();
            //this.ActiveControl = this;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ClearData()
        {
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtRole.SelectedIndex = -1;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            txtDOB.Value = DateTime.Now;
        }

        private bool Validation()
        {
            bool result = false;    

            if (string.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtName, "Name Required");
            }
            else
            {
                result = true;
            }

            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Connection conn = new Connection();
                conn.dataSend("Insert into [User] (Name, Email, Username, Password, Role, DOB, Address) values ('" + txtName.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtRole.Text + "','" + txtDOB.Value.ToString("dd/MM/yyyy") + "','" + txtAddress.Text + "')");
                MessageBox.Show("Record saved successfully");
                ClearData();
                LoadData();
            }
        }

        private void LoadData()
        {
            Connection conn = new Connection();
            conn.dataGet("select * from dbo.[User]");
            DataTable dataTable = new DataTable();
            conn.sda.Fill(dataTable);
            int count = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells["Sl"].Value = ++count;
                dataGridView1.Rows[index].Cells["Name"].Value = row["Name"].ToString();
                dataGridView1.Rows[index].Cells["Username"].Value = row["Username"].ToString();
                dataGridView1.Rows[index].Cells["Email"].Value = row["Email"].ToString();
                dataGridView1.Rows[index].Cells["Name"].Value = row["Name"].ToString();
                dataGridView1.Rows[index].Cells["Role"].Value = row["Role"].ToString();
                dataGridView1.Rows[index].Cells["Address"].Value = row["Address"].ToString();
                dataGridView1.Rows[index].Cells["DOB"].Value = String.IsNullOrEmpty(row["DOB"].ToString()) ? "" : Convert.ToDateTime(row["DOB"].ToString()).ToString("dd/MM/yyyy");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtName.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            txtUsername.Text = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
            txtEmail.Text = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
            txtDOB.Text = dataGridView1.SelectedRows[0].Cells["DOB"].Value.ToString();
            txtRole.Text = dataGridView1.SelectedRows[0].Cells["Role"].Value.ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
}
