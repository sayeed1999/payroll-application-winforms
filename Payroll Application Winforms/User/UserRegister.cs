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
        Payroll.Service.User userService = new Payroll.Service.User();

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

        private void LoadData()
        {
            DataTable dt = userService.GetAllUsers();
            int count = 0;
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
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
            bool isSuccess = false;

            if (Validation())
            {
                isSuccess = userService.Register(txtName.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text, txtRole.Text, txtDOB.Value, txtAddress.Text);
            }

            if (isSuccess)
            {
                MessageBox.Show("Record saved successfully");
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("Some error occurred");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // update by email
                int rowCount = userService.UpdateUser(txtEmail.Text, txtUsername.Text, txtName.Text, txtDOB.Value, txtRole.Text, txtAddress.Text);
                if (rowCount == 0)
                {
                    MessageBox.Show("Update Failed!", "Error", MessageBoxButtons.OKCancel);
                    return;
                }
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
                int rowCount = userService.DeleteUser(txtEmail.Text, txtUsername.Text);
                if (rowCount == 0)
                {
                    MessageBox.Show("Delete Failed!", "Error", MessageBoxButtons.OKCancel);
                    return;
                }
                MessageBox.Show("Record updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                LoadData();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
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
