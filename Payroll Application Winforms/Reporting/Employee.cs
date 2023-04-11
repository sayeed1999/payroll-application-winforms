using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms.Reporting
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeDs.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.employeeDs.Employee);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reps.Emp_Rep emp_Rep = new Reps.Emp_Rep();
            emp_Rep.SetDataSource(this.employeeDs);
            this.crystalReportViewer1.ReportSource = emp_Rep;
        }
    }
}
