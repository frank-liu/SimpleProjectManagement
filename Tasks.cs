using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SimpleProjectManagement
{
    public partial class Tasks : Form
    {
        private SqlProcedures sqlProcedures;
        private ListViewColumnSorter lvColumnSorter;

        public Tasks()
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();

            lvColumnSorter = new ListViewColumnSorter();
            this.lvTasks.ListViewItemSorter = lvColumnSorter;
            this.lvTasks.SetSortIcon(0, SortOrder.Ascending);
        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            LoadTabs();
            LoadTasks();
        }

        private void LoadTabs()
        {
            sqlProcedures.LoadStatusTabs(tabTasks);
        }

        private void LoadTasks()
        {
            sqlProcedures.LoadTasksListView(lvTasks, tabTasks);
        }

        private void tabTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabTasks.TabPages.Count > 0)
            {
                LoadTasks();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabTasks.TabPages.Clear();
            tabTasks.TabPages.Add("All");

            LoadTabs();
            LoadTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditTask addTask = new AddEditTask(true);
            addTask.Text = "Add new Task";
            addTask.ShowDialog();

            // refresh
            LoadTasks();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvTasks.Items.Count > 0)
            {
                // ukoliko se pritisne botun edit, a na listi nije nista selektirano.
                if (lvTasks.SelectedItems.Count < 1)
                {
                    MessageBox.Show("Please select an item", "Cannot edit item", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    AddEditTask editTask = new AddEditTask(false);
                    DataRow dr = sqlProcedures.GetTask(Convert.ToInt32(lvTasks.SelectedItems[0].Text));

                    editTask.txtTaskId.Text = Validator.ConvertFromDBVal<int>(dr["TaskID"]).ToString();
                    editTask.txtTaskName.Text = Validator.ConvertFromDBVal<string>(dr["TaskName"]);
                    editTask.cmbProject.SelectedValue = Validator.ConvertFromDBVal<int>(dr["ProjectID"]);
                    editTask.cmbAssignedTo.SelectedValue = Validator.ConvertFromDBVal<int>(dr["EmployeeID"]);
                    editTask.cmbPriority.SelectedValue = Validator.ConvertFromDBVal<int>(dr["PriorityID"]);
                    editTask.cmbStatus.SelectedValue = Validator.ConvertFromDBVal<int>(dr["StatusID"]);
                    editTask.txtDescription.Text = Validator.ConvertFromDBVal<string>(dr["Description"]);

                    DateTime? startDate = Validator.ConvertFromDBVal<DateTime?>(dr["StartDate"]);
                    if (startDate != null)
                    {
                        editTask.txtStartDate.Text = Convert.ToDateTime(startDate).ToString("d");
                        editTask.dtpStartDate.Value = Convert.ToDateTime(startDate);
                    }

                    DateTime? dueDate = Validator.ConvertFromDBVal<DateTime?>(dr["DueDate"]);
                    if (dueDate != null)
                    {
                        editTask.txtDueDate.Text = Convert.ToDateTime(dueDate).ToString("d");
                        editTask.dtpDueDate.Value = Convert.ToDateTime(dueDate);
                    }

                    editTask.Text = "Edit Task";
                    editTask.ShowDialog();

                    // refresh
                    LoadTasks();
                }
            }
            else
            {
                MessageBox.Show("No Tasks found", "Unable to edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count != 0)
            {
                if (MessageBox.Show("Are you sure you want to remove: " + lvTasks.Items[lvTasks.FocusedItem.Index].
                    SubItems[1].Text, "Removing Task", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    sqlProcedures.RemoveTask(Convert.ToInt32(lvTasks.SelectedItems[0].Text));
                    lvTasks.SelectedItems[0].Remove();
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Cannot delete item", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lvTasks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColumnSorter.SortColumn)
            {
                if (lvColumnSorter.Order == SortOrder.Ascending)
                {
                    lvColumnSorter.Order = SortOrder.Descending;
                    this.lvTasks.SetSortIcon(e.Column, SortOrder.Descending);
                }
                else
                {
                    lvColumnSorter.Order = SortOrder.Ascending;
                    this.lvTasks.SetSortIcon(e.Column, SortOrder.Ascending);
                }
            }
            else
            {
                lvColumnSorter.SortColumn = e.Column;
                lvColumnSorter.Order = SortOrder.Ascending;
                this.lvTasks.SetSortIcon(e.Column, SortOrder.Ascending);
            }

            this.lvTasks.Sort();
        }
    }
}