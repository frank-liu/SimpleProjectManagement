using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class AddEditEmployee : Form
    {
        private SqlProcedures sqlProcedures;
        private DBConnection dbConnection;

        private bool newEmp;

        public AddEditEmployee(bool newEmp)
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();
            dbConnection = new DBConnection();

            this.newEmp = newEmp;
            FillDepartmentComboBox();
        }

        private void FillDepartmentComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetAllDepartments(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["DepartmentName"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "DepartmentName"; 
            cmbDepartment.ValueMember = "DepartmentID";     
            cmbDepartment.SelectedIndex = cmbDepartment.Items.Count - 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validator.EmptyText(txtFirstName.Text))
            {
                MessageBox.Show("First Name is empty! Please enter the First Name", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFirstName.Focus();
            }
            else if (Validator.EmptyText(txtLastName.Text))
            {
                MessageBox.Show("Last Name is empty! Please enter the Last Name", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtLastName.Focus();
            }
            else if (Validator.EmptyText(txtJobTitle.Text))
            {
                MessageBox.Show("Job Title is empty! Please enter the Job Title", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtJobTitle.Focus();
            }
            else if (cmbDepartment.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Department", "Combobox not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DateTime? birthDate = null;
                DateTime? dateHired = null;

                if (!Validator.EmptyText(txtBirthDate.Text))
                {
                    if (!Validator.TextIsDate(txtBirthDate.Text))
                    {
                        MessageBox.Show("Birth Date is not in correct format! Please enter the correct date",
                            "Empty text", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        birthDate = Convert.ToDateTime(txtBirthDate.Text).Date;
                    }
                }

                if (!Validator.EmptyText(txtDateHired.Text))
                {
                    if (!Validator.TextIsDate(txtDateHired.Text))
                    {
                        MessageBox.Show("Date Hired is not in correct format! Please enter the correct date",
                            "Empty text", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        dateHired = Convert.ToDateTime(txtDateHired.Text).Date;
                    }
                }

                if (newEmp == true)
                {
                    sqlProcedures.AddEmployee(txtFirstName.Text, txtLastName.Text, birthDate, txtJobTitle.Text,
                        (int)cmbDepartment.SelectedValue, dateHired, txtEmail.Text, txtBussPhone.Text,
                        txtHomePhone.Text, txtMobilePhone.Text, txtFaxNumber.Text, txtAddress.Text, txtCity.Text,
                        txtStateProvince.Text, txtPostalCode.Text, txtCountry.Text, txtNotes.Text);
                }
                else
                {
                    sqlProcedures.EditEmployee(Convert.ToInt32(txtEmpId.Text), txtFirstName.Text, txtLastName.Text,
                        birthDate, txtJobTitle.Text, (int)cmbDepartment.SelectedValue, dateHired,
                        txtEmail.Text, txtBussPhone.Text, txtHomePhone.Text, txtMobilePhone.Text, txtFaxNumber.Text,
                        txtAddress.Text, txtCity.Text, txtStateProvince.Text, txtPostalCode.Text, txtCountry.Text, 
                        txtNotes.Text);
                }

                this.Close();
            }
        }

        private void dtpDateHired_ValueChanged(object sender, EventArgs e)
        {
            txtDateHired.Text = dtpDateHired.Value.ToString("d");
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            txtBirthDate.Text = dtpBirthDate.Value.ToString("d");
        }
    }
}