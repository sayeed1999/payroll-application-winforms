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
        Payroll.Service.User _userService = new Payroll.Service.User();

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
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isSuccess = _userService.Login(username, password);
            if (isSuccess)
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
