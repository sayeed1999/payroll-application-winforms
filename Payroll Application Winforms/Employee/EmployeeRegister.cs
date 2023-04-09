using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Application_Winforms.Employee
{
    public partial class EmployeeRegister : Form
    {
        Payroll.Service.Employee employeeService = new Payroll.Service.Employee();

        int count = 0;
        string fileName;
        
        public EmployeeRegister()
        {
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

        private bool IsValid()
        {
            bool ret = true;
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.SetError(this.txtEmail, "Email required!");
                ret = false;
            }
            else if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                errorProvider1.SetError(this.txtEmail, "Email not valid!");
                ret = false;
            }
            else
            {
                errorProvider1.Clear();
            }
            return ret;
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
            pictureBox.Image = null;
            pictureLabel.Text = "No image selected";
            fileName = null;
        }

        private void LoadData()
        {
            DataTable dt = employeeService.GetAllEmployees();
            dataGridView2.Rows.Clear();
            foreach(DataRow row in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells["EmpId"].Value = row["EmpId"].ToString();
                dataGridView2.Rows[n].Cells["Name"].Value = row["Name"].ToString();
                dataGridView2.Rows[n].Cells["Email"].Value = row["Email"].ToString();
                dataGridView2.Rows[n].Cells["Mobile"].Value = row["Mobile"].ToString();
                dataGridView2.Rows[n].Cells["File"].Value = row["FileName"].ToString();
            }
        }

        private void EmployeeRegister_Load(object sender, EventArgs e)
        {
            LoadData();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void mouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0) return;

            txtName.Text = dataGridView2.SelectedRows[0].Cells["Name"].Value.ToString();
            txtEmail.Text = dataGridView2.SelectedRows[0].Cells["Email"].Value.ToString();
            txtMobile.Text = dataGridView2.SelectedRows[0].Cells["Mobile"].Value.ToString();
            fileName = dataGridView2.SelectedRows[0].Cells["File"].Value.ToString();
            if (!String.IsNullOrEmpty(fileName))
            {
                pictureBox.Image = Image.FromFile(fileName);
                pictureLabel.Text = fileName;
            }
            else
            {
                pictureBox.Image = null;
                pictureLabel.Text = "No image found";
            }
            txtEmail.ReadOnly = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            try
            {
                bool success = employeeService.CreateEmployee(txtName.Text, txtEmail.Text, txtMobile.Text, fileName, ConvertImageToBinary(pictureBox.Image));
                MessageBox.Show("Successfully saved in database", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            employeeService.UpdateEmployee(txtName.Text, txtEmail.Text, txtMobile.Text, fileName, ConvertImageToBinary(pictureBox.Image));
            MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK);
            ClearData();
            LoadData();
            txtEmail.ReadOnly = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled =  false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            employeeService.DeleteEmployee(txtEmail.Text);
            MessageBox.Show("Record deleted successfully", "Information", MessageBoxButtons.OK);
            ClearData();
            LoadData();
            txtEmail.ReadOnly = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
