using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Reply.Common.CreateTFSTask.AddIn
{
    public partial class WorkItemForm : Form
    {
        public WorkItemForm()
        {
            InitializeComponent();
        }

        public WorkItemForm(WorkItem workItem)
            : this()
        {
            this.workItemFormControl.Item = workItem;
        }

        private void WorkItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((this.DialogResult == DialogResult.OK) && !this.workItemFormControl.Item.IsValid())
            {
                MessageBox.Show(this, "Complete WorkItem before saving.", "WorkItem is not valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
