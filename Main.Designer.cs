namespace SimpleProjectManagement
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripEmployees = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripDepartments = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripEmployees = new System.Windows.Forms.ToolStripButton();
            this.toolStripDepartments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProjects = new System.Windows.Forms.ToolStripButton();
            this.toolStripTasks = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.recordsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // recordsToolStripMenuItem
            // 
            this.recordsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripEmployees,
            this.menuStripDepartments,
            this.toolStripSeparator1,
            this.menuStripProjects,
            this.menuStripTasks});
            this.recordsToolStripMenuItem.Name = "recordsToolStripMenuItem";
            this.recordsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.recordsToolStripMenuItem.Text = "Records";
            // 
            // menuStripEmployees
            // 
            this.menuStripEmployees.Name = "menuStripEmployees";
            this.menuStripEmployees.Size = new System.Drawing.Size(152, 22);
            this.menuStripEmployees.Text = "Employees";
            this.menuStripEmployees.Click += new System.EventHandler(this.EmployeesForm_Click);
            // 
            // menuStripDepartments
            // 
            this.menuStripDepartments.Name = "menuStripDepartments";
            this.menuStripDepartments.Size = new System.Drawing.Size(152, 22);
            this.menuStripDepartments.Text = "Departments";
            this.menuStripDepartments.Click += new System.EventHandler(this.DepartmentsForm_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // menuStripProjects
            // 
            this.menuStripProjects.Name = "menuStripProjects";
            this.menuStripProjects.Size = new System.Drawing.Size(152, 22);
            this.menuStripProjects.Text = "Projects";
            this.menuStripProjects.Click += new System.EventHandler(this.ProjectsForm_Click);
            // 
            // menuStripTasks
            // 
            this.menuStripTasks.Name = "menuStripTasks";
            this.menuStripTasks.Size = new System.Drawing.Size(152, 22);
            this.menuStripTasks.Text = "Tasks";
            this.menuStripTasks.Click += new System.EventHandler(this.TasksForm_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEmployees,
            this.toolStripDepartments,
            this.toolStripSeparator2,
            this.toolStripProjects,
            this.toolStripTasks});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(584, 55);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripEmployees
            // 
            this.toolStripEmployees.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEmployees.Image = global::SimpleProjectManagement.Properties.Resources.employee;
            this.toolStripEmployees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEmployees.Name = "toolStripEmployees";
            this.toolStripEmployees.Size = new System.Drawing.Size(52, 52);
            this.toolStripEmployees.Text = "Employees";
            this.toolStripEmployees.Click += new System.EventHandler(this.EmployeesForm_Click);
            // 
            // toolStripDepartments
            // 
            this.toolStripDepartments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDepartments.Image = global::SimpleProjectManagement.Properties.Resources.departments;
            this.toolStripDepartments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDepartments.Name = "toolStripDepartments";
            this.toolStripDepartments.Size = new System.Drawing.Size(52, 52);
            this.toolStripDepartments.Text = "Departments";
            this.toolStripDepartments.Click += new System.EventHandler(this.DepartmentsForm_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripProjects
            // 
            this.toolStripProjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripProjects.Image = global::SimpleProjectManagement.Properties.Resources.projects;
            this.toolStripProjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripProjects.Name = "toolStripProjects";
            this.toolStripProjects.Size = new System.Drawing.Size(52, 52);
            this.toolStripProjects.Text = "Projects";
            this.toolStripProjects.Click += new System.EventHandler(this.ProjectsForm_Click);
            // 
            // toolStripTasks
            // 
            this.toolStripTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripTasks.Image = global::SimpleProjectManagement.Properties.Resources.tasks;
            this.toolStripTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTasks.Name = "toolStripTasks";
            this.toolStripTasks.Size = new System.Drawing.Size(52, 52);
            this.toolStripTasks.Text = "Tasks";
            this.toolStripTasks.Click += new System.EventHandler(this.TasksForm_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Simple Project Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStripDepartments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuStripProjects;
        private System.Windows.Forms.ToolStripMenuItem menuStripTasks;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripEmployees;
        private System.Windows.Forms.ToolStripButton toolStripDepartments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripProjects;
        private System.Windows.Forms.ToolStripButton toolStripTasks;
        private System.Windows.Forms.ToolStripMenuItem menuStripEmployees;
    }
}

