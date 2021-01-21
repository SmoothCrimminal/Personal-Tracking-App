using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdated = false;
        string imagePath = "";

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartamentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "Position1";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            if (isUpdated)
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtAddress.Text = detail.Address;
                txtPassword.Text = detail.Password;
                txtSalary.Text = detail.Salary.ToString();
                txtUserNo.Text = detail.UserNo.ToString();
                chAdmin.Checked = detail.isAdmin;
                dateTimePicker1.Value = Convert.ToDateTime(detail.Birthday);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                imagePath = Application.StartupPath + "\\Images\\" + detail.ImagePath;
                txtImagePath.Text = imagePath;
                pictureBox1.ImageLocation = imagePath;
            }
        }

        bool comboFull = false;

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepID == departmentID).ToList();
            }
        }

        string fileName = "";

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName; // only file name without full path
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User No is empty!");
            
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("User has no password!");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("User Name is empty!");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("User Surname is empty!");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("User has no salary!");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Select a department!");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Select a position!");
            else
            {
                if (!isUpdated)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                        MessageBox.Show("This user No is already taken!");
                    else
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = PasswordEncryption.CreateMD5Hash(txtPassword.Text);
                        employee.isAdmin = chAdmin.Checked;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Address = txtAddress.Text;
                        employee.Birthday = dateTimePicker1.Value;
                        employee.ImagePath = fileName;
                        EmployeeBLL.AddEmployee(employee);
                        File.Copy(txtImagePath.Text, @"Images\\" + fileName);
                        MessageBox.Show("Employee successfuly added!");
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        chAdmin.Checked = false;
                        txtName.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        txtAddress.Clear();
                        txtImagePath.Clear();
                        pictureBox1.Image = null;
                        comboFull = false;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbPosition.SelectedIndex = -1;
                        comboFull = true;
                        dateTimePicker1.Value = DateTime.Today;
                        fileName = "";
                    }
                }

                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        if (txtImagePath.Text != imagePath)
                        {
                            if (File.Exists(@"Images\\" + detail.ImagePath))
                                File.Delete(@"Images\\" + detail.ImagePath);
                            File.Copy(txtImagePath.Text, @"Images\\" + fileName);
                            employee.ImagePath = fileName;
                        }
                        else
                            employee.ImagePath = detail.ImagePath;
                        employee.ID = detail.EmployeeID;
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Address = txtAddress.Text;
                        employee.Birthday = dateTimePicker1.Value;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Password = txtPassword.Text;
                        employee.isAdmin = chAdmin.Checked;
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        EmployeeBLL.UpdateEmployee(employee);
                        MessageBox.Show("Employee updated!");
                        this.Close();
                    }
                }

                


            }

        }

        bool isUnique = false;

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User No is empty!");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                    MessageBox.Show("This User no is already used!");
                else
                    MessageBox.Show("This User no is not in use");
            }
        }

       
    }
}
