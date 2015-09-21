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
    public partial class TaskListForm : Form
    {
        public enum TaskTypes
        {
            Flow,
            Issue,
            Task
        }

        private const string FLOW_QUERY_ACTIVE = "SELECT [ID], [Title], [Application], [Flow Name], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Flow' AND [State] <> 'In Produzione' ORDER BY [{0}] {1}";
        private const string ISSUE_QUERY_ACTIVE = "SELECT [ID], [Title], [Issue Type], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Issue' AND [State] <> 'Closed' ORDER BY [{0}] {1}";
        private const string TASK_QUERY_ACTIVE = "SELECT [ID], [Title], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Task' AND [State] <> 'Closed' ORDER BY [{0}] {1}";
        private const string FLOW_QUERY = "SELECT [ID], [Title], [Application], [Flow Name], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Flow' ORDER BY [{0}] {1}";
        private const string ISSUE_QUERY = "SELECT [ID], [Title], [Issue Type], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Issue' ORDER BY [{0}] {1}";
        private const string TASK_QUERY = "SELECT [ID], [Title], [Assigned To], [State] FROM WorkItems WHERE [Team Project] = @Project AND [Work Item Type] = 'Task' ORDER BY [{0}] {1}";

        private const string ASC = "ASC";
        private const string DESC = "DESC";

        private string orderBy = "ID";
        private string direction = ASC;

        private WorkItemStore wis;
        private string project;
        private TaskTypes type;

        public TaskListForm()
        {
            InitializeComponent();
        }

        public TaskListForm(WorkItemStore wis, string project, TaskTypes type)
            : this()
        { 
            this.wis = wis;
            this.project = project;
            this.type = type;

            this.Text = string.Format("Select {0}", type);
        }

        public TaskListForm(WorkItemStore wis, string project, TaskTypes type, bool showClosed)
            : this(wis, project, type)
        {
            checkBoxClosed.Checked = showClosed;
        }

        public int SelectedId
        {
            get;
            private set;
        }

        private void TaskListForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            workItemResultGrid.Rows.Clear();

            string query = string.Empty;

            switch (type)
            {
                case TaskTypes.Flow:
                    query = checkBoxClosed.Checked ? FLOW_QUERY : FLOW_QUERY_ACTIVE;
                    break;
                case TaskTypes.Issue:
                    query = checkBoxClosed.Checked ? ISSUE_QUERY : ISSUE_QUERY_ACTIVE;
                    break;
                default:
                    query = checkBoxClosed.Checked ? TASK_QUERY : TASK_QUERY_ACTIVE;
                    break;
            }

            query = string.Format(query, orderBy, direction);
            workItemResultGrid.LoadFromQuery(new Query(wis, query, new Dictionary<string, string> { { "Project", project } }));
        }

        private void workItemResultGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (workItemResultGrid.SelectedRows.Count == 0)
            {
                SelectedId = -1;
                buttonOK.Enabled = false;
            }
            else
            {
                SelectedId = workItemResultGrid.GetSelectedIds().FirstOrDefault();
                buttonOK.Enabled = true;
            }
        }

        private void workItemResultGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedId = workItemResultGrid.WorkItemCollection[e.RowIndex].Id;

                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
        
        private void checkBoxClosed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                LoadList();
        }

        private void TaskListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                LoadList();
        }

        private void workItemResultGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string column = workItemResultGrid.Columns[e.ColumnIndex].Name;

            if (orderBy != column)
            {
                orderBy = column;
                direction = ASC;
            }
            else
            {
                if (direction == ASC)
                    direction = DESC;
                else
                    direction = ASC;
            }

            LoadList();
        }
    }
}
