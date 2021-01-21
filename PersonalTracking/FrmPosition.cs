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

namespace PersonalTracking
{
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<DEPARTMENT> departmentList = new List<DEPARTMENT>();
        public bool isUpdated = false;
        public PositionDTO detail = new PositionDTO();

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            departmentList = DepartmentBLL.GetDepartments();
            cmbDepartment.DataSource = departmentList;
            cmbDepartment.DisplayMember = "DepartamentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            if (isUpdated)
            {
                txtPosition.Text = detail.Position1;
                cmbDepartment.SelectedValue = detail.DepID;
            }
                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the position name");
            }

            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department");
            }

            else
            {
               if (!isUpdated)
                {
                    POSITION position = new POSITION();
                    position.Position1 = txtPosition.Text;
                    position.DepID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    PositionBLL.AddPosition(position);
                    MessageBox.Show("Position added");
                    txtPosition.Clear();
                    cmbDepartment.SelectedIndex = -1;
                }
                else
                {
                    POSITION position = new POSITION();
                    position.ID = detail.ID;
                    position.Position1 = txtPosition.Text;
                    position.DepID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    bool control = false;
                    if (Convert.ToInt32(cmbDepartment.SelectedValue) != detail.OldDepartmentID)
                        control = true;
                    PositionBLL.UpdatePosition(position, control);
                    MessageBox.Show("Position updated!");
                    this.Close();
                }
            }
        }
    }
}
