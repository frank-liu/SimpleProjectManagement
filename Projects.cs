using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class Projects : Form
    {
        private SqlProcedures sqlProcedures;
        private ListViewColumnSorter lvColumnSorter;

        public Projects()
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();

            lvColumnSorter = new ListViewColumnSorter();
            this.lvProjects.ListViewItemSorter = lvColumnSorter;
            this.lvProjects.SetSortIcon(0, SortOrder.Ascending);
        }

        private void Projects_Load(object sender, EventArgs e)
        {
            LoadTabs();
            LoadProjects();
        }

        private void LoadTabs()
        {
            sqlProcedures.LoadStatusTabs(tabProjects);
        }

        private void LoadProjects()
        {
            sqlProcedures.LoadProjectsListView(lvProjects, tabProjects);
        }

        private void tabProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabProjects.TabPages.Count > 0)
            {
                LoadProjects();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabProjects.TabPages.Clear();
            tabProjects.TabPages.Add("All");

            LoadTabs();
            LoadProjects();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditProject addProject = new AddEditProject(true);
            addProject.Text = "Add new Project";
            addProject.ShowDialog();

            // refresh
            LoadProjects();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvProjects.Items.Count > 0)
            {
                // ukoliko se pritisne botun edit, a na listi nije nista selektirano.
                if (lvProjects.SelectedItems.Count < 1) 
                {
                    MessageBox.Show("Please select an item", "Cannot edit item", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
                else
                {
                    AddEditProject editProj = new AddEditProject(false);
                    DataRow dr = sqlProcedures.GetProject(Convert.ToInt32(lvProjects.SelectedItems[0].Text));

                    editProj.txtProjectId.Text = Validator.ConvertFromDBVal<int>(dr["ProjectID"]).ToString();
                    editProj.txtProjectName.Text = Validator.ConvertFromDBVal<string>(dr["ProjectName"]);
                    editProj.cmbOwner.SelectedValue = Validator.ConvertFromDBVal<int>(dr["EmployeeID"]);
                    editProj.cmbPriority.SelectedValue = Validator.ConvertFromDBVal<int>(dr["PriorityID"]);
                    editProj.cmbStatus.SelectedValue = Validator.ConvertFromDBVal<int>(dr["StatusID"]);
                    editProj.txtNotes.Text = Validator.ConvertFromDBVal<string>(dr["Notes"]);

                    DateTime? startDate = Validator.ConvertFromDBVal<DateTime?>(dr["StartDate"]);
                    if (startDate != null)
                    {
                        editProj.txtStartDate.Text = Convert.ToDateTime(startDate).ToString("d");
                        editProj.dtpStartDate.Value = Convert.ToDateTime(startDate);
                    }

                    DateTime? endDate = Validator.ConvertFromDBVal<DateTime?>(dr["EndDate"]);
                    if (endDate != null)
                    {
                        editProj.txtEndDate.Text = Convert.ToDateTime(endDate).ToString("d");
                        editProj.dtpEndDate.Value = Convert.ToDateTime(endDate);
                    }

                    editProj.Text = "Edit Project";
                    editProj.ShowDialog();

                    // refresh
                    LoadProjects();
                }
            }
            else
            {
                MessageBox.Show("No Projects found", "Unable to edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvProjects.SelectedItems.Count != 0)
            {
                DataTable dt = sqlProcedures.GetProjectsJoinedTasks(Convert.ToInt32(lvProjects.SelectedItems[0].Text));

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot delete Project! There are Tasks assigned to this Project." +
                    "You first need to delete all Tasks that are assigned to this Project and then delete the Project",
                    "Cannot delete item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (MessageBox.Show("Are you sure you want to remove: " + lvProjects.Items[
                    lvProjects.FocusedItem.Index].SubItems[1].Text, "Removing Department", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    sqlProcedures.RemoveProject(Convert.ToInt32(lvProjects.SelectedItems[0].Text));
                    lvProjects.SelectedItems[0].Remove();
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Cannot delete item", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void lvProjects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvColumnSorter.SortColumn)
            {
                if (lvColumnSorter.Order == SortOrder.Ascending)
                {
                    lvColumnSorter.Order = SortOrder.Descending;
                    this.lvProjects.SetSortIcon(e.Column, SortOrder.Descending);
                }
                else
                {
                    lvColumnSorter.Order = SortOrder.Ascending;
                    this.lvProjects.SetSortIcon(e.Column, SortOrder.Ascending);
                }
            }
            else
            {
                lvColumnSorter.SortColumn = e.Column;
                lvColumnSorter.Order = SortOrder.Ascending;
                this.lvProjects.SetSortIcon(e.Column, SortOrder.Ascending);
            }

            this.lvProjects.Sort();
        }
    }
}