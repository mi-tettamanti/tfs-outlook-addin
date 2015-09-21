using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing;
using stdole;
using AddIn.Properties;

namespace Reply.Common.CreateTFSTask.AddIn
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        private Outlook.Application application;

        public Ribbon(Outlook.Application application)
        {
            this.application = application;            
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            if (ribbonID == "Microsoft.Outlook.Explorer")
            {
                return GetResourceText("AddIn.Explorer.xml");
            }
            if (ribbonID == "Microsoft.Outlook.Mail.Read")
            {
                return GetResourceText("AddIn.Ribbon.xml");
            }

            return string.Empty;
        }

        #endregion

        #region Ribbon Callbacks
        
        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #region Actions

        public void OnCreateFlow_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            CreateFlow(mail);
        }

        public void OnCreateFlow_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            CreateFlow(mail);
        }

        public void OnCreateIssue_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            CreateIssue(mail);
        }

        public void OnCreateIssue_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            CreateIssue(mail);
        }

        public void OnCreateTask_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            CreateTask(mail);
        }

        public void OnCreateTask_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            CreateTask(mail);
        }

        public void OnAddToFlow_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            AddToFlow(mail);
        }

        public void OnAddToFlow_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            AddToFlow(mail);
        }

        public void OnAddToIssue_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            AddToIssue(mail);
        }

        public void OnAddToIssue_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            AddToIssue(mail);
        }

        public void OnAddToTask_Ribbon(Office.IRibbonControl control)
        {
            Outlook.Inspector window = control.Context;
            Outlook.MailItem mail = window.CurrentItem;

            AddToTask(mail);
        }

        public void OnAddToTask_Context(Office.IRibbonControl control)
        {
            Outlook.Selection selection = control.Context;
            Outlook.MailItem mail = selection[1];

            AddToTask(mail);
        }

        #endregion

        #region Images

        public IPictureDisp GetTfsImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.tfs);
        }

        public IPictureDisp GetWiImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newWi);
        }

        public IPictureDisp GetWiImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newWiSmall);
        }

        public IPictureDisp GetFlowImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newFlow);
        }

        public IPictureDisp GetFlowImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newFlowSmall);
        }

        public IPictureDisp GetTaskImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newTask);
        }

        public IPictureDisp GetTaskImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.newTaskSmall);
        }

        public IPictureDisp GetAddWiImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addWi);
        }

        public IPictureDisp GetAddWiImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addWiSmall);
        }

        public IPictureDisp GetAddFlowImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addFlow);
        }

        public IPictureDisp GetAddFlowImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addFlowSmall);
        }

        public IPictureDisp GetAddTaskImage(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addTask);
        }

        public IPictureDisp GetAddTaskImageSmall(Office.IRibbonControl control)
        {
            return AxHostConverter.ImageToPictureDisp(Resources.addTaskSmall);
        }

        #endregion

        #region BackStage

        public void OnBackstageClosing(object contextObject)
        {
            Settings.Default.Save();
        }

        public void OnServerNameChange(Office.IRibbonControl control, string text)
        {
            Settings.Default.TFSUrl = text;
        }

        public string GetServerName(Office.IRibbonControl control)
        {
            return Settings.Default.TFSUrl;
        }

        public void OnServerTestButton(Office.IRibbonControl control)
        {
            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    MessageBox.Show(string.Format("Connection to {0} Succeeded", Settings.Default.TFSUrl), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Format("Error connecting to {0}", Settings.Default.TFSUrl), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnServerUndoButton(Office.IRibbonControl control)
        {
            Settings.Default.Reload();
            this.ribbon.InvalidateControl("TFSserverEdit");
        }

        #endregion

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        private void CreateFlow(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);
                        
                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var tasktype = project.WorkItemTypes["Flow"];
                        wi = new WorkItem(tasktype);

                        wi.Description = mail.Body;
                        wi.Reason = "New";
                        wi.Title = mail.Subject;

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);

                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }

                        wi.IterationPath = project.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error creating Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateIssue(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);

                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var taskForm = new TaskListForm(wis, project.Name, TaskListForm.TaskTypes.Flow, true);

                        try
                        {
                            var tskResult = taskForm.ShowDialog();

                            if (tskResult != DialogResult.OK)
                                return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error selecting Parent Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var tasktype = project.WorkItemTypes["Issue"];
                        wi = new WorkItem(tasktype);

                        wi.Description = mail.Body;
                        wi.Reason = "New";
                        wi.Title = mail.Subject;
                        wi.Links.Add(new RelatedLink(wis.WorkItemLinkTypes.LinkTypeEnds["Parent"], taskForm.SelectedId));

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);
                            
                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }

                        wi.IterationPath = project.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error creating Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateTask(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);

                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var tasktype = project.WorkItemTypes["Task"];
                        wi = new WorkItem(tasktype);

                        wi.Description = mail.Body;
                        wi.Reason = "New";
                        wi.Title = mail.Subject;

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);

                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }

                        wi.IterationPath = project.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error creating Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToFlow(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);

                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var taskForm = new TaskListForm(wis, project.Name, TaskListForm.TaskTypes.Flow);

                        try
                        {
                            var tskResult = taskForm.ShowDialog();

                            if (tskResult != DialogResult.OK)
                                return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error selecting existing Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        wi = wis.GetWorkItem(taskForm.SelectedId);

                        wi.History = mail.Body;

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);

                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error editing Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Flow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToIssue(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);

                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var taskForm = new TaskListForm(wis, project.Name, TaskListForm.TaskTypes.Issue);

                        try
                        {
                            var tskResult = taskForm.ShowDialog();

                            if (tskResult != DialogResult.OK)
                                return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error selecting existing Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        wi = wis.GetWorkItem(taskForm.SelectedId);

                        wi.History = mail.Body;

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);

                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error editing Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToTask(Outlook.MailItem mail)
        {
            string tempPath = Path.GetTempPath();
            string mailTempPath = Path.Combine(tempPath, MakeValidFileName(mail.Subject) + ".msg");

            try
            {
                if (File.Exists(mailTempPath))
                    File.Delete(mailTempPath);

                mail.SaveAs(mailTempPath, Outlook.OlSaveAsType.olMSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving Mail content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var tfs = new TfsTeamProjectCollection(new Uri(Settings.Default.TFSUrl)))
                {
                    WorkItemStore wis = tfs.GetService(typeof(WorkItemStore)) as WorkItemStore;

                    var projectQuery = from prj in wis.Projects.Cast<Project>()
                                       where prj.HasWorkItemWriteRights
                                       select prj.Name;

                    var projectForm = new SelectProjectForm(projectQuery);

                    try
                    {
                        var pjResult = projectForm.ShowDialog();

                        if (pjResult != DialogResult.OK)
                            return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error selecting Team Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkItem wi = null;

                    try
                    {
                        var project = wis.Projects[projectForm.SelectedProject] as Project;

                        var taskForm = new TaskListForm(wis, project.Name, TaskListForm.TaskTypes.Task);

                        try
                        {
                            var tskResult = taskForm.ShowDialog();

                            if (tskResult != DialogResult.OK)
                                return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error selecting existing Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        wi = wis.GetWorkItem(taskForm.SelectedId);

                        wi.History = mail.Body;

                        wi.Attachments.Add(new Attachment(mailTempPath, "Mail"));

                        foreach (Outlook.Attachment attachment in mail.Attachments)
                        {
                            string fileName = attachment.FileName;
                            int i = 1;

                            while (wi.Attachments.Cast<Attachment>().Where(a => a.Name == fileName).Count() > 0)
                                fileName = string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(attachment.FileName), i++, Path.GetExtension(attachment.FileName));

                            string attachmentPath = Path.Combine(tempPath, fileName);

                            if (File.Exists(attachmentPath))
                                File.Delete(attachmentPath);

                            attachment.SaveAsFile(attachmentPath);

                            wi.Attachments.Add(new Attachment(attachmentPath, string.Format("Mail Attachment: {0}", attachment.DisplayName)));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error editing Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        var wiForm = new WorkItemForm(wi);
                        var wiResult = wiForm.ShowDialog();

                        if (wiResult == DialogResult.OK)
                            wi.Save();

                        wi.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error saving Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to TFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }
    }
}

