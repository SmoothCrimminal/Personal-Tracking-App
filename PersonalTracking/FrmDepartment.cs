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

namespace PersonalTracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the name field");
            }

            else
            {
                DEPARTMENT department = new DEPARTMENT();
                if (!isUpdated)
                {
                    department.DepartamentName = txtDepartment.Text;
                    BLL.DepartmentBLL.AddDepartment(department);
                    MessageBox.Show("Department added!");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        department.ID = detail.ID;
                        department.DepartamentName = txtDepartment.Text;
                        DepartmentBLL.UpdateDepartment(department);
                        MessageBox.Show("Department updated!");
                        this.Close();
                    }

                }
            }
        }
        public bool isUpdated = false;
        public DEPARTMENT detail = new DEPARTMENT();
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdated)
                txtDepartment.Text = detail.DepartamentName;
        }
    }
}
