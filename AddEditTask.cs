using System;
using System.Data;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class AddEditTask : Form
    {
        private SqlProcedures sqlProcedures;
        private DBConnection dbConnection;

        private bool newTask;

        public AddEditTask(bool newTask)
        {
            InitializeComponent();

            sqlProcedures = new SqlProcedures();
            dbConnection = new DBConnection();

            this.newTask = newTask;

            FillProjectComboBox();
            FillAssignedToComboBox();
            FillPriorityComboBox();
            FillStatusComboBox();
        }

        private void FillProjectComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetAllProjects(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["ProjectName"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbProject.DataSource = dt;
            cmbProject.DisplayMember = "ProjectName";   
            cmbProject.ValueMember = "ProjectID";       
            cmbProject.SelectedIndex = cmbProject.Items.Count - 1;
        }

        private void FillAssignedToComboBox()
        {
            DataTable dt = dbConnection.FillDataTable(sqlProcedures.GetAllEmployees(), null);
            DataRow emptyRow = dt.NewRow();
            emptyRow["FullName"] = "--- select ---";
            dt.Rows.Add(emptyRow);

            cmbAssignedTo.DataSource = dt;
            cmbAssignedTo.DisplayMember = "FullName";       
            cmbAssignedTo.ValueMember = "EmployeeID";       
            cmbAssignedTo.SelectedIndex = cmbAssignedTo.Items.Count - 1;
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
            cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validator.EmptyText(txtTaskName.Text))
            {
                MessageBox.Show("Task Name is empty! Please enter the Task Name", "Empty text",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTaskName.Focus();
            }
            else if (cmbProject.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Project", "Combobox not selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmbAssignedTo.Text == "--- select ---")
            {
                MessageBox.Show("You need to select Assigned to", "Combobox not selected",
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
                DateTime? dueDate = null;

                if (!Validator.EmptyText(txtStartDate.Text))
                {
                    if (!Validator.TextIsDate(txtStartDate.Text))
                    {
                        MessageBox.Show("Start Date is not in correct format! Please enter the correct date",
                            "Empty text", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        startDate = Convert.ToDateTime(txtStartDate.Text).Date;
                    }
                }

                if (!Validator.EmptyText(txtDueDate.Text))
                {
                    if (!Validator.TextIsDate(txtDueDate.Text))
                    {
                        MessageBox.Show("Due Date is not in correct format! Please enter the correct date",
                            "Empty text", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        dueDate = Convert.ToDateTime(txtDueDate.Text).Date;
                    }
                }


                if (newTask == true)
                {
                    sqlProcedures.AddTask(txtTaskName.Text, (int)cmbProject.SelectedValue, 
                        (int)cmbAssignedTo.SelectedValue, (int)cmbPriority.SelectedValue, 
                        (int)cmbStatus.SelectedValue, startDate, dueDate, txtDescription.Text);
                }
                else
                {
                    sqlProcedures.EditTask(Convert.ToInt32(txtTaskId.Text), txtTaskName.Text, 
                        (int)cmbProject.SelectedValue, (int)cmbAssignedTo.SelectedValue, 
                        (int)cmbPriority.SelectedValue, (int)cmbStatus.SelectedValue, startDate, 
                        dueDate, txtDescription.Text);
                }

                this.Close();
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            txtStartDate.Text = dtpStartDate.Value.ToString("d");
        }

        private void dtpDueDate_ValueChanged(object sender, EventArgs e)
        {
            txtDueDate.Text = dtpDueDate.Value.ToString("d");
        }
    }
}