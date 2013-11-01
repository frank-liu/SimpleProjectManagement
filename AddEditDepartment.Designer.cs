namespace SimpleProjectManagement
{
    partial class AddEditDepartment
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
            this.bttnSave = new System.Windows.Forms.Button();
            this.bttnExit = new System.Windows.Forms.Button();
            this.txtFunction = new System.Windows.Forms.TextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblFunction = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bttnSave
            // 
            this.bttnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bttnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnSave.ForeColor = System.Drawing.Color.Black;
            this.bttnSave.Image = global::SimpleProjectManagement.Properties.Resources.save;
            this.bttnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bttnSave.Location = new System.Drawing.Point(148, 171);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(80, 24);
            this.bttnSave.TabIndex = 2;
            this.bttnSave.Text = "&Save Item";
            this.bttnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bttnSave.UseVisualStyleBackColor = false;
            this.bttnSave.Click += new System.EventHandler(this.bttnSave_Click);
            // 
            // bttnExit
            // 
            this.bttnExit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bttnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bttnExit.ForeColor = System.Drawing.Color.Black;
            this.bttnExit.Image = global::SimpleProjectManagement.Properties.Resources.exit;
            this.bttnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bttnExit.Location = new System.Drawing.Point(236, 171);
            this.bttnExit.Name = "bttnExit";
            this.bttnExit.Size = new System.Drawing.Size(80, 24);
            this.bttnExit.TabIndex = 3;
            this.bttnExit.Text = "E&xit Form";
            this.bttnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bttnExit.UseVisualStyleBackColor = false;
            this.bttnExit.Click += new System.EventHandler(this.bttnExit_Click);
            // 
            // txtFunction
            // 
            this.txtFunction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunction.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFunction.Location = new System.Drawing.Point(92, 56);
            this.txtFunction.Multiline = true;
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFunction.Size = new System.Drawing.Size(224, 104);
            this.txtFunction.TabIndex = 1;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDepartment.ForeColor = System.Drawing.Color.Black;
            this.txtDepartment.Location = new System.Drawing.Point(92, 19);
            this.txtDepartment.MaxLength = 30;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(224, 22);
            this.txtDepartment.TabIndex = 0;
            // 
            // lblFunction
            // 
            this.lblFunction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunction.ForeColor = System.Drawing.Color.Black;
            this.lblFunction.Location = new System.Drawing.Point(12, 59);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(88, 16);
            this.lblFunction.TabIndex = 48;
            this.lblFunction.Text = "Function:";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Location = new System.Drawing.Point(12, 22);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(88, 16);
            this.lblDepartment.TabIndex = 47;
            this.lblDepartment.Text = "Department:";
            // 
            // AddEditDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnExit;
            this.ClientSize = new System.Drawing.Size(327, 204);
            this.Controls.Add(this.bttnSave);
            this.Controls.Add(this.bttnExit);
            this.Controls.Add(this.txtFunction);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.lblFunction);
            this.Controls.Add(this.lblDepartment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddEditDepartment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Department";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnSave;
        private System.Windows.Forms.Button bttnExit;
        public System.Windows.Forms.TextBox txtFunction;
        public System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.Label lblFunction;
        private System.Windows.Forms.Label lblDepartment;
    }
}