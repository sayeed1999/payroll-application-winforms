using Payroll.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Payroll_Application_Winforms.User
{
    public partial class ChangePassword : Form
    {
        Payroll.Service.User userService = new Payroll.Service.User();

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                MessageBox.Show("Invalid attempt!", "Error", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                bool isSuccess = userService.ChangePassword(txtUsername.Text, txtNewPassword.Text);
                if (!isSuccess)
                {
                    MessageBox.Show("Failed!", "Failed", MessageBoxButtons.OK);
                }
                MessageBox.Show("Record updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
