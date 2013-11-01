using System;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class AddEditDepartment : Form
    {
        private bool newDept;
        private SqlProcedures sqlProcedures;
        private int departmentId;

        public AddEditDepartment(bool newDept)
        {
            InitializeComponent();

            this.newDept = newDept;
            sqlProcedures = new SqlProcedures();
        }

        public AddEditDepartment(int id, bool newDept)
            : this(newDept)
        {
            this.departmentId = id;
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            if (Validator.EmptyText(txtDepartment.Text))
            {
                MessageBox.Show("Department is empty! Please enter the name of the Department", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtDepartment.Focus();
            }
            else
            {
                if (newDept == true)
                {
                    sqlProcedures.AddDepartment(txtDepartment.Text, txtFunction.Text);
                }
                else
                {
                    sqlProcedures.EditDepartment(departmentId, txtDepartment.Text, txtFunction.Text);
                }

                this.Close();
            }
        }
    }
}