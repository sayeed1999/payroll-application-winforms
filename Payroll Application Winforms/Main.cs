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
    }
}
