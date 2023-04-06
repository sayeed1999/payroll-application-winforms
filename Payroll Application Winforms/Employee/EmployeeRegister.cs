using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms.Employee
{
    public partial class EmployeeRegister : Form
    {
        string fileName;
        Connection conn;
        public EmployeeRegister()
        {
            conn = new Connection();
            InitializeComponent();
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
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

        private void keyUpName(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void keyUpMobile(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void keyUpEmail(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPanNo.Focus();
            }
        }

        private void keyUpPanNo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDOB.Focus();
            }
        }

        private void keyUpAddress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private bool IsValid() => !String.IsNullOrEmpty(txtEmail.Text);

        private bool IfEmployeeExists(string email)
        {
            conn.dataGet("Select 1 from employee where email = '" + email + "'");
            DataTable dt = new DataTable();
            conn.sda.Fill(dt);
            return (dt.Rows.Count > 0);
        }

        private byte[] ConvertImageToBinary(Image img)
        {
            if (img == null) return null;
            using(MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                MessageBox.Show("Email Required", "Validation Error", MessageBoxButtons.OK);
                return;
            }

            if (IfEmployeeExists(txtEmail.Text))
            {
                MessageBox.Show("Employee already exists", "Message", MessageBoxButtons.OK);
                return;
            }

            conn.dataSend("Insert into Employee (Name, Email, Mobile, FileName, Image) values ('" + txtName.Text + "','" + txtEmail.Text + "','" + txtMobile.Text + "','"+fileName+"','"+ConvertImageToBinary(pictureBox.Image)+"')");
            MessageBox.Show("Successfully saved in database", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearData();
        }

        private void ClearData()
        {
            txtAddress.Clear();
            txtBankDetails.Clear();
            txtDOB.Value = new DateTime(1999, 1, 1);
            txtEmail.Clear();
            txtEmpId.Clear();
            txtMobile.Clear();
            txtName.Clear();
            txtPanNo.Clear();
        }

        private void EmployeeRegister_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
