using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class AddEditProject : Form
    {
        private SqlProcedures sqlProcedures;
        private DBConnection dbConnection;

        private bool newProj;

        private ListViewColumnSorter lvColumnSorter;

        public AddEditProject(bool newProj)
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();
            dbConnection = new DBConnection();

            this.newProj = newProj;

            if (newProj == true)
                tabProject.Controls.Remove(tabProjectTasks);

            FillOwnerComboBox();
            FillPriorityComboBox();
            FillStatusComboBox();

            lvColumnSorter = new ListViewColumnSorter();
            this.lvTasks.ListViewItemSorter = lvColumnSorter;
            this.lvTasks.SetSortIcon(0, SortOrder.Ascending);
        }

        private void FillOwnerComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetAllEmployees(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["FullName"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbOwner.DataSource = dt;
            cmbOwner.DisplayMember = "FullName";       
            cmbOwner.ValueMember = "EmployeeID";       
            cmbOwner.SelectedIndex = cmbOwner.Items.Count - 1;
        }

        private void FillPriorityComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetPriority(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["Priority"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbPriority.DataSource = dt;
            cmbPriority.DisplayMember = "Priority";          
            cmbPriority.ValueMember = "PriorityID";          
            cmbPriority.SelectedIndex = cmbPriority.Items.Count - 1;
        }

        private void FillStatusComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetStatus(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["Status"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = "Status";          
            cmbStatus.ValueMember = "StatusID";          
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;
        }

        private void LoadTasks()
        {
            sqlProcedures.LoadTasksListView(lvTasks, Convert.ToInt32(txtProjectId.Text));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validator.EmptyText(txtProjectName.Text))
            {
                MessageBox.Show("Project Name is empty! Please enter the Project Name", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtProjectName.Focus();
            }
            else if (cmbOwner.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Owner", "Combobox not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmbPriority.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Priority", "Combobox not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmbStatus.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Status", "Combobox not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DateTime? startDate = null;
                DateTime? endDate = null;

                if (!Validator.EmptyText(txtStartDate.Text))
                {
                    if (!Validator.TextIsDate(txtStartDate.Text))
                    {
                        MessageBox.Show("Start Date is not in correct format! Please enter the correct date",
                            "Incorrect format", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        startDate = Convert.ToDateTime(txtStartDate.Text).Date;
                    }
                }

                if (!Validator.EmptyText(txtEndDate.Text))
                {
                    if (!Validator.TextIsDate(txtEndDate.Text))
                    {
                        MessageBox.Show("End Date is not in correct format! Please enter the correct date",
                            "Incorrect format", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        endDate = Convert.ToDateTime(txtEndDate.Text).Date;
                    }
                }

                if (newProj == true)
                {
                    sqlProcedures.AddProject(txtProjectName.Text, (int)cmbOwner.SelectedValue,
                        (int)cmbPriority.SelectedValue, (int)cmbStatus.SelectedValue, startDate,
                        endDate, txtNotes.Text);
                }
                else
                {
                    sqlProcedures.EditProject(Convert.ToInt32(txtProjectId.Text), txtProjectName.Text,
                        (int)cmbOwner.SelectedValue, (int)cmbPriority.SelectedValue, (int)cmbStatus.SelectedValue,
                        startDate, endDate, txtNotes.Text);
                }

                this.Close();
            }
        }

        private void AddEditProject_Load(object sender, EventArgs e)
        {
            if (newProj == false)
                LoadTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditTask addTask = new AddEditTask(true);
            addTask.Text = "Add new Task";
            addTask.cmbProject.SelectedValue = Convert.ToInt32(txtProjectId.Text);
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
                if (MessageBox.Show("Are you sure you want to remove: " + lvTasks.Items[
                    lvTasks.FocusedItem.Index].SubItems[1].Text, "Removing Task", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            txtStartDate.Text = dtpStartDate.Value.ToString("d");
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = dtpEndDate.Value.ToString("d");
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