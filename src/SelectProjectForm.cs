using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AddIn.Properties;

namespace Reply.Common.CreateTFSTask.AddIn
{
    public partial class SelectProjectForm : Form
    {
        public SelectProjectForm()
        {
            InitializeComponent();
        }

        public SelectProjectForm(IEnumerable<string> projects)
            : this()
        {
            this.comboBoxProjects.Items.AddRange(projects.ToArray());
        }

        public string SelectedProject
        {
            get
            {
                return (comboBoxProjects.SelectedItem ?? string.Empty).ToString();
            }
        }

        private void comboBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProjects.SelectedItem == null)
                buttonOK.Enabled = false;
            else
                buttonOK.Enabled = true;
        }

        private void SelectProjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                Settings.Default.Save();
        }
    }
}
