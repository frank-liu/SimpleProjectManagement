using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class Employees : Form
    {
        private SqlProcedures sqlProcedures;
        private ListViewColumnSorter lvColumnSorter;

        public Employees()
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();

            lvColumnSorter = new ListViewColumnSorter();
            this.lvEmployees.ListViewItemSorter = lvColumnSorter;
            this.lvEmployees.SetSortIcon(0, SortOrder.Ascending);
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            LoadTabs();
            LoadEmployees();
        }

        private void LoadTabs()
        {
            sqlProcedures.LoadDepartmentTabs(tabDepartment);
        }

        private void LoadEmployees()
        {
            sqlProcedures.LoadEmployeesListView(lvEmployees, tabDepartment);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabDepartment.TabPages.Clear();
            tabDepartment.TabPages.Add("All");

            LoadTabs();
            LoadEmployees();
        }

        private void tabDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabDepartment.TabPages.Count > 0)
            {
                LoadEmployees();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvEmployees.SelectedItems.Count != 0)
            {
                DataTable dt1 = sqlProcedures.EmployeeOwnerOnProject(Convert.ToInt32(
                    lvEmployees.SelectedItems[0].Text));

                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot delete Employee! There are Projects where Employee is owner.",
                    "Cannot delete item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                DataTable dt2 = sqlProcedures.EmployeeAssignedToTask(Convert.ToInt32(
                    lvEmployees.SelectedItems[0].Text));

                if (dt2.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot delete Employee! There are Tasks where Employee is assigned to.",
                    "Cannot delete item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }


                if (MessageBox.Show("Are you sure you want to remove: " + lvEmployees.Items[
                    lvEmployees.FocusedItem.Index].SubItems[1].Text, "Removing Employee", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    sqlProcedures.RemoveEmployee(Convert.ToInt32(lvEmployees.SelectedItems[0].Text));
                    lvEmployees.SelectedItems[0].Remove();
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Cannot delete item", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tabDepartment.TabPages.Count <= 1)
            {
                MessageBox.Show("No Departments entered! You cannot add Employee without one Department created", 
                    "Cannot add Employee", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AddEditEmployee addEmp = new AddEditEmployee(true);
                addEmp.Text = "Add new Employee";
                addEmp.ShowDialog();

                // refresh
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvEmployees.Items.Count > 0)
            {
                // ukoliko se pritisne botun edit, a na listi nije nista selektirano.
                if (lvEmployees.SelectedItems.Count < 1) 
                {
                    MessageBox.Show("Please select an item", "Cannot edit item", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
                else
                {
                    AddEditEmployee editEmp = new AddEditEmployee(false);
                    DataRow dr = sqlProcedures.GetEmployee(Convert.ToInt32(lvEmployees.SelectedItems[0].Text));

                    editEmp.txtEmpId.Text = Validator.ConvertFromDBVal<int>(dr["EmployeeID"]).ToString(); 
                    editEmp.txtFirstName.Text = Validator.ConvertFromDBVal<string>(dr["FirstName"]);      
                    editEmp.txtLastName.Text = Validator.ConvertFromDBVal<string>(dr["LastName"]);        
                    editEmp.txtJobTitle.Text = Validator.ConvertFromDBVal<string>(dr["JobTitle"]);        
                    editEmp.txtEmail.Text = Validator.ConvertFromDBVal<string>(dr["EmailAddress"]);
                    editEmp.txtNotes.Text = Validator.ConvertFromDBVal<string>(dr["Notes"]);

                    DateTime? birthDate = Validator.ConvertFromDBVal<DateTime?>(dr["BirthDate"]);
                    if (birthDate != null)
                    {
                        editEmp.txtBirthDate.Text = Convert.ToDateTime(birthDate).ToString("d");
                        editEmp.dtpBirthDate.Value = Convert.ToDateTime(birthDate);
                    }

                    DateTime? dateHired = Validator.ConvertFromDBVal<DateTime?>(dr["DateHired"]);
                    if (dateHired != null)
                    {
                        editEmp.txtDateHired.Text = Convert.ToDateTime(dateHired).ToString("d");
                        editEmp.dtpDateHired.Value = Convert.ToDateTime(dateHired);
                    }
                
                    // department
                    editEmp.cmbDepartment.SelectedValue = Validator.ConvertFromDBVal<int>(dr["DepartmentID"]);

                    // phone numbers
                    editEmp.txtBussPhone.Text = Validator.ConvertFromDBVal<string>(dr["BusinessPhone"]);
                    editEmp.txtHomePhone.Text = Validator.ConvertFromDBVal<string>(dr["HomePhone"]);
                    editEmp.txtMobilePhone.Text = Validator.ConvertFromDBVal<string>(dr["MobilePhone"]);
                    editEmp.txtFaxNumber.Text = Validator.ConvertFromDBVal<string>(dr["FaxNumber"]);

                    // address
                    editEmp.txtAddress.Text =  Validator.ConvertFromDBVal<string>(dr["Address"]);
                    editEmp.txtCity.Text = Validator.ConvertFromDBVal<string>(dr["City"]);
                    editEmp.txtStateProvince.Text = Validator.ConvertFromDBVal<string>(dr["StateProvince"]);
                    editEmp.txtPostalCode.Text = Validator.ConvertFromDBVal<string>(dr["PostalCode"]);
                    editEmp.txtCountry.Text = Validator.ConvertFromDBVal<string>(dr["Country"]);

                    editEmp.Text = "Edit Employee";
                    editEmp.ShowDialog();

                    // refresh
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("No Employees found", "Unable to edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvEmployees_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColumnSorter.SortColumn)
            {
                if (lvColumnSorter.Order == SortOrder.Ascending)
                {
                    lvColumnSorter.Order = SortOrder.Descending;
                    this.lvEmployees.SetSortIcon(e.Column, SortOrder.Descending);
                }
                else
                {
                    lvColumnSorter.Order = SortOrder.Ascending;
                    this.lvEmployees.SetSortIcon(e.Column, SortOrder.Ascending);
                }
            }
            else
            {
                lvColumnSorter.SortColumn = e.Column;
                lvColumnSorter.Order = SortOrder.Ascending;
                this.lvEmployees.SetSortIcon(e.Column, SortOrder.Ascending);
            }

            this.lvEmployees.Sort();
        }
    }
}