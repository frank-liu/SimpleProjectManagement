using System;
using System.Windows.Forms;

namespace SimpleProjectManagement
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            this.KeyPreview = true;
        }

        private void EmployeesForm_Click(object sender, EventArgs e)
        {
            Employees empChild = new Employees();
            empChild.MdiParent = this;
            empChild.Show();
        }

        private void DepartmentsForm_Click(object sender, EventArgs e)
        {
            Departments deptChild = new Departments();
            deptChild.MdiParent = this;
            deptChild.Show();
        }

        private void ProjectsForm_Click(object sender, EventArgs e)
        {
            Projects projChild = new Projects();
            projChild.MdiParent = this;
            projChild.Show();
        }

        private void TasksForm_Click(object sender, EventArgs e)
        {
            Tasks taskChild = new Tasks();
            taskChild.MdiParent = this;
            taskChild.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                EmployeesForm_Click(this, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                DepartmentsForm_Click(this, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                ProjectsForm_Click(this, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                TasksForm_Click(this, null);
            }
        }
    }
}