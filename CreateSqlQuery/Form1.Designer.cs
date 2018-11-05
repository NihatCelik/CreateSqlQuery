namespace CreateSqlQuery
{
    partial class Form1
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
            this.radioInsert = new System.Windows.Forms.RadioButton();
            this.radioUpdate = new System.Windows.Forms.RadioButton();
            this.radioDelete = new System.Windows.Forms.RadioButton();
            this.txtSqlConnectionString = new System.Windows.Forms.TextBox();
            this.txtSqlQuery = new System.Windows.Forms.TextBox();
            this.btnCreateSqlQuery = new System.Windows.Forms.Button();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkIsObject = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // radioInsert
            // 
            this.radioInsert.AutoSize = true;
            this.radioInsert.Checked = true;
            this.radioInsert.Location = new System.Drawing.Point(12, 12);
            this.radioInsert.Name = "radioInsert";
            this.radioInsert.Size = new System.Drawing.Size(51, 17);
            this.radioInsert.TabIndex = 0;
            this.radioInsert.TabStop = true;
            this.radioInsert.Text = "Insert";
            this.radioInsert.UseVisualStyleBackColor = true;
            // 
            // radioUpdate
            // 
            this.radioUpdate.AutoSize = true;
            this.radioUpdate.Location = new System.Drawing.Point(79, 12);
            this.radioUpdate.Name = "radioUpdate";
            this.radioUpdate.Size = new System.Drawing.Size(60, 17);
            this.radioUpdate.TabIndex = 1;
            this.radioUpdate.Text = "Update";
            this.radioUpdate.UseVisualStyleBackColor = true;
            // 
            // radioDelete
            // 
            this.radioDelete.AutoSize = true;
            this.radioDelete.Location = new System.Drawing.Point(156, 12);
            this.radioDelete.Name = "radioDelete";
            this.radioDelete.Size = new System.Drawing.Size(56, 17);
            this.radioDelete.TabIndex = 2;
            this.radioDelete.Text = "Delete";
            this.radioDelete.UseVisualStyleBackColor = true;
            // 
            // txtSqlConnectionString
            // 
            this.txtSqlConnectionString.Location = new System.Drawing.Point(12, 52);
            this.txtSqlConnectionString.Multiline = true;
            this.txtSqlConnectionString.Name = "txtSqlConnectionString";
            this.txtSqlConnectionString.Size = new System.Drawing.Size(200, 64);
            this.txtSqlConnectionString.TabIndex = 3;
            this.txtSqlConnectionString.Text = "Server=.\\SQLEXPRESS;\r\nDatabase=Solar;\r\nTrusted_Connection=True;";
            // 
            // txtSqlQuery
            // 
            this.txtSqlQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlQuery.Location = new System.Drawing.Point(1, 122);
            this.txtSqlQuery.Multiline = true;
            this.txtSqlQuery.Name = "txtSqlQuery";
            this.txtSqlQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlQuery.Size = new System.Drawing.Size(643, 345);
            this.txtSqlQuery.TabIndex = 6;
            this.txtSqlQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSqlQuery_KeyDown);
            this.txtSqlQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSqlQuery_KeyUp);
            // 
            // btnCreateSqlQuery
            // 
            this.btnCreateSqlQuery.Location = new System.Drawing.Point(360, 94);
            this.btnCreateSqlQuery.Name = "btnCreateSqlQuery";
            this.btnCreateSqlQuery.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSqlQuery.TabIndex = 5;
            this.btnCreateSqlQuery.Text = "Create";
            this.btnCreateSqlQuery.UseVisualStyleBackColor = true;
            this.btnCreateSqlQuery.Click += new System.EventHandler(this.btnCreateSqlQuery_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(218, 96);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(136, 20);
            this.txtTableName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Connectionstring";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Table Name";
            // 
            // checkIsObject
            // 
            this.checkIsObject.AutoSize = true;
            this.checkIsObject.Location = new System.Drawing.Point(221, 13);
            this.checkIsObject.Name = "checkIsObject";
            this.checkIsObject.Size = new System.Drawing.Size(57, 17);
            this.checkIsObject.TabIndex = 9;
            this.checkIsObject.Text = "Object";
            this.checkIsObject.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCreateSqlQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 469);
            this.Controls.Add(this.checkIsObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.btnCreateSqlQuery);
            this.Controls.Add(this.txtSqlQuery);
            this.Controls.Add(this.txtSqlConnectionString);
            this.Controls.Add(this.radioDelete);
            this.Controls.Add(this.radioUpdate);
            this.Controls.Add(this.radioInsert);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Sql Query";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioInsert;
        private System.Windows.Forms.RadioButton radioUpdate;
        private System.Windows.Forms.RadioButton radioDelete;
        private System.Windows.Forms.TextBox txtSqlConnectionString;
        private System.Windows.Forms.TextBox txtSqlQuery;
        private System.Windows.Forms.Button btnCreateSqlQuery;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkIsObject;
    }
}

