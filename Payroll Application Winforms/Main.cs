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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void userRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.UserRegister form = new User.UserRegister();
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        bool close = true;
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close != true) return;
            DialogResult result = MessageBox.Show("Are you sure want to quit?", "Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                close = false;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.ChangePassword form = new User.ChangePassword();
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void employeeRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee.EmployeeRegister form = new Employee.EmployeeRegister();
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}
