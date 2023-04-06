using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms.Employee
{
    public partial class EmployeeRegister : Form
    {
        string fileName;

        public EmployeeRegister()
        {
            InitializeComponent();
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "JPEG|*.jpg",
                ValidateNames = true,
                Multiselect = false,
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    pictureLabel.Text = fileName;
                    pictureBox.Image = Image.FromFile(fileName);
                }
            }
        }

        private void btnRemovePicture_Click(object sender, EventArgs e)
        {
            fileName = null;
            pictureLabel.Text = "Not selected";
            pictureBox.Image = null;
        }
    }
}
