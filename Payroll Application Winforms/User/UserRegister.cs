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
            }
        }
    }
}
