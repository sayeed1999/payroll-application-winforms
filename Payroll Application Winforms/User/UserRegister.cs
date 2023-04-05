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
        Connection conn = new Connection();
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
            else if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtUsername, "Username Required");
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Password Required");
            }
            else if (txtPassword.Text.Length < 4)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Password must be of greater than 3 length");
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail, "Email Required");
            }
            else if (string.IsNullOrEmpty(txtDOB.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDOB, "DOB Required");
            }
            else if (txtRole.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRole, "Role Required");
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
                conn.dataSend("Insert into [User] (Name, Email, Username, Password, Role, DOB, Address) values ('" + txtName.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtRole.Text + "','" + txtDOB.Value.ToString("dd/MM/yyyy") + "','" + txtAddress.Text + "')");
                MessageBox.Show("Record saved successfully");
                ClearData();
                LoadData();
            }
        }

        private void LoadData()
        {
            conn.dataGet("select * from dbo.[User]");
            DataTable dataTable = new DataTable();
            conn.sda.Fill(dataTable);
            int count = 0;
            dataGridView1.Rows.Clear();
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
            if (dataGridView1.SelectedRows.Count <= 0) return;

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // update by email
                conn.dataSend("UPDATE [User] SET Username ='"+txtUsername.Text+ "', Name ='" + txtName.Text+ "', DOB ='"+txtDOB.Value.ToString("dd/MM/yyyy")+"', Role ='" + txtRole.Text+"', Address ='" + txtAddress.Text+"' where Email = '"+txtEmail.Text+"'");
                MessageBox.Show("Record updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                LoadData();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                conn.dataSend("DELETE from [User] where Username = '" + txtUsername.Text + "' or Email ='" + txtEmail.Text + "'");
                MessageBox.Show("Record updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                LoadData();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtName.Text.Length > 0)
                {
                    txtUsername.Focus();
                }
                if (txtUsername.Text.Length > 0)
                {
                    txtPassword.Focus();
                }
                if (txtPassword.Text.Length > 0)
                {
                    txtEmail.Focus();
                }
                if (txtEmail.Text.Length > 0)
                {
                    txtRole.Focus();
                }
                if (txtRole.Text.Length > 0)
                {
                    txtAddress.Focus();
                }
                if (txtAddress.Text.Length > 0)
                {
                    btnSave.Focus();
                }

            }
        }
    }
}
