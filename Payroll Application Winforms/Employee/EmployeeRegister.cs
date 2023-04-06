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
        int count = 0;
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

            conn.dataSend("Insert into Employee (EmpId, Name, Email, Mobile, FileName, ImageData) values (" + (count + 1) + ", '" + txtName.Text + "','" + txtEmail.Text + "','" + txtMobile.Text + "','"+fileName+"','"+ConvertImageToBinary(pictureBox.Image)+"')");
            MessageBox.Show("Successfully saved in database", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearData();
            LoadData();
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

        private void LoadData()
        {
            conn.dataGet("Select * from Employee");
            DataTable dt = new DataTable();
            count = dt.Rows.Count;
            conn.sda.Fill(dt);
            dataGridView2.Rows.Clear();
            foreach(DataRow row in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells["EmpId"].Value = row["EmpId"].ToString();
                dataGridView2.Rows[n].Cells["Name"].Value = row["Name"].ToString();
                dataGridView2.Rows[n].Cells["Email"].Value = row["Email"].ToString();
                dataGridView2.Rows[n].Cells["Mobile"].Value = row["Mobile"].ToString();
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
            txtEmail.ReadOnly = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.dataSend("Update Employee set name = '"  + txtName.Text + "' where email = '" + txtEmail.Text + "'");
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
            conn.dataSend("delete from Employee where email = '" + txtEmail.Text + "'");
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
