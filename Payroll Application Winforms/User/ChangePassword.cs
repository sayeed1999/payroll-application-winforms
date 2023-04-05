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
                Connection conn = new Connection();
                // update by email
                conn.dataSend("UPDATE [User] SET Password ='" + txtNewPassword.Text + "' where Username = '" + txtUsername.Text + "'");
                MessageBox.Show("Record updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
