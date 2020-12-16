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
using DAL.DAO;

namespace PersonalTracking
{
    public partial class FrmLogin : Form
    {

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginLogic()
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Please fill the UserNo");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Please fill the Password box");
            else
            {
                string password = txtPassword.Text;
                if (Convert.ToInt32(txtUserNo.Text) > 6)
                    password = PasswordEncryption.CreateMD5Hash(password);
                List<EMPLOYEE> employeeList = EmployeeBLL.GetEmployees(Convert.ToInt32(txtUserNo.Text), password);

                if (employeeList.Count == 0)
                    MessageBox.Show("Wrong username or password!");
                else
                {
                    EMPLOYEE employee = new EMPLOYEE();
                    employee = employeeList.First();
                    UserStatic.EmployeeID = employee.ID;
                    UserStatic.UserNo = employee.UserNo;
                    UserStatic.isAdmin = employee.isAdmin;
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.ShowDialog();
                }

            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginLogic();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                loginLogic();
        }
    }
}
