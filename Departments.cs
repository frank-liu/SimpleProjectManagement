using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class Departments : Form
    {
        private SqlProcedures sqlProcedures;
        private ListViewColumnSorter lvColumnSorter;

        public Departments()
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();

            lvColumnSorter = new ListViewColumnSorter();
            this.lvDepartments.ListViewItemSorter = lvColumnSorter;
            this.lvDepartments.SetSortIcon(0, SortOrder.Ascending);
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            sqlProcedures.LoadDepartmentListView(lvDepartments);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditDepartment addDept = new AddEditDepartment(true);
            addDept.Text = "Add new Department";
            addDept.ShowDialog();

            LoadDepartments();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvDepartments.Items.Count > 0)
            {
                // ukoliko se pritisne botun edit, a na listi nije nista selektirano.
                if (lvDepartments.SelectedItems.Count < 1) 
                {
                    MessageBox.Show("Please select an item", "Cannot edit item", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
                else
                {
                    AddEditDepartment editDept = new AddEditDepartment(Convert.ToInt32(
                        lvDepartments.SelectedItems[0].Text), false);

                    editDept.txtDepartment.Text = lvDepartments.SelectedItems[0].SubItems[1].Text;
                    editDept.txtFunction.Text = lvDepartments.SelectedItems[0].SubItems[2].Text;
                    editDept.Text = "Edit Department";
                    editDept.ShowDialog();

                    LoadDepartments();
                }
            }
            else
            {
                MessageBox.Show("No Departments found", "Unable to edit", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvDepartments.SelectedItems.Count != 0)
            {
                DataTable dt = sqlProcedures.DepartmentWithEmployees(Convert.ToInt32(
                    lvDepartments.SelectedItems[0].Text));

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot delete Department! There are Employees assigned to this Department." +
                    "You firste need to delete all Employees from Department and then delete the Department",
                    "Cannot delete item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (MessageBox.Show("Are you sure you want to remove: " + lvDepartments.Items[
                    lvDepartments.FocusedItem.Index].SubItems[1].Text, "Removing Department", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    sqlProcedures.RemoveDepartment(Convert.ToInt32(lvDepartments.SelectedItems[0].Text));
                    lvDepartments.SelectedItems[0].Remove();
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Cannot delete item", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvDepartments_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColumnSorter.SortColumn)
            {
                if (lvColumnSorter.Order == SortOrder.Ascending)
                {
                    lvColumnSorter.Order = SortOrder.Descending;
                    this.lvDepartments.SetSortIcon(e.Column, SortOrder.Descending);
                }
                else
                {
                    lvColumnSorter.Order = SortOrder.Ascending;
                    this.lvDepartments.SetSortIcon(e.Column, SortOrder.Ascending);
                }
            }
            else
            {
                lvColumnSorter.SortColumn = e.Column;
                lvColumnSorter.Order = SortOrder.Ascending;
                this.lvDepartments.SetSortIcon(e.Column, SortOrder.Ascending);
            }

            this.lvDepartments.Sort();
        }
    }
}