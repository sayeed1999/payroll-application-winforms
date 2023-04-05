using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            conn.dataGet("Select * from [User] where Username = '" + txtUsername.Text + "' and Password = '" + txtPassword.Text + "'");
            DataTable dt = new DataTable();
            conn.sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username & Password.");
            }
        }

        private void changePasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            User.ChangePassword form = new User.ChangePassword();
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}
