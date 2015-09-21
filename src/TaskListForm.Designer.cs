namespace Reply.Common.CreateTFSTask.AddIn
{
    partial class TaskListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskListForm));
            this.workItemResultGrid = new Microsoft.TeamFoundation.WorkItemTracking.Controls.WorkItemResultGrid();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxClosed = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.workItemResultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // workItemResultGrid
            // 
            this.workItemResultGrid.AllowUserToAddRows = false;
            this.workItemResultGrid.AllowUserToDeleteRows = false;
            this.workItemResultGrid.AllowUserToOrderColumns = true;
            this.workItemResultGrid.AllowUserToResizeRows = false;
            this.workItemResultGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workItemResultGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.workItemResultGrid.BackgroundColor = System.Drawing.Color.White;
            this.workItemResultGrid.BoldDirtyItems = false;
            this.workItemResultGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.workItemResultGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.workItemResultGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.workItemResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.workItemResultGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.workItemResultGrid.DisplayIsDirtyColumn = false;
            this.workItemResultGrid.DisplayRowHeader = false;
            this.workItemResultGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.workItemResultGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.workItemResultGrid.Location = new System.Drawing.Point(12, 12);
            this.workItemResultGrid.MultiSelect = false;
            this.workItemResultGrid.Name = "workItemResultGrid";
            this.workItemResultGrid.PauseBeforeFill = 500;
            this.workItemResultGrid.ReadOnly = true;
            this.workItemResultGrid.ResultOptions = null;
            this.workItemResultGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.workItemResultGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.workItemResultGrid.RowHeadersVisible = false;
            this.workItemResultGrid.RowHeadersWidth = 35;
            this.workItemResultGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.workItemResultGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.workItemResultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.workItemResultGrid.Size = new System.Drawing.Size(760, 509);
            this.workItemResultGrid.SortBackcolor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.workItemResultGrid.SortForecolor = System.Drawing.Color.Black;
            this.workItemResultGrid.StretchLastColumn = false;
            this.workItemResultGrid.TabIndex = 0;
            this.workItemResultGrid.ValueProvider = null;
            this.workItemResultGrid.VirtualMode = true;
            this.workItemResultGrid.VirtualThumbTracking = false;
            this.workItemResultGrid.WorkItemCollection = null;
            this.workItemResultGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.workItemResultGrid_CellContentDoubleClick);
            this.workItemResultGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.workItemResultGrid_ColumnHeaderMouseClick);
            this.workItemResultGrid.SelectionChanged += new System.EventHandler(this.workItemResultGrid_SelectionChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(616, 527);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(697, 527);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxClosed
            // 
            this.checkBoxClosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxClosed.AutoSize = true;
            this.checkBoxClosed.Location = new System.Drawing.Point(12, 531);
            this.checkBoxClosed.Name = "checkBoxClosed";
            this.checkBoxClosed.Size = new System.Drawing.Size(120, 17);
            this.checkBoxClosed.TabIndex = 3;
            this.checkBoxClosed.Text = "Show Closed Tasks";
            this.checkBoxClosed.UseVisualStyleBackColor = true;
            this.checkBoxClosed.CheckedChanged += new System.EventHandler(this.checkBoxClosed_CheckedChanged);
            // 
            // TaskListForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.checkBoxClosed);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.workItemResultGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "TaskListForm";
            this.Text = "TaskListForm";
            this.Load += new System.EventHandler(this.TaskListForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TaskListForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.workItemResultGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.TeamFoundation.WorkItemTracking.Controls.WorkItemResultGrid workItemResultGrid;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxClosed;
    }
}