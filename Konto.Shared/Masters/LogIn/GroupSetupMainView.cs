using Konto.Core.Shared;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Konto.Shared.Masters.LogIn
{
    public partial class GroupSetupMainView : KontoForm
    {
        public GroupSetupMainView()
        {
            InitializeComponent();
            this.FormClosed += GroupSetupMainView_FormClosed;
        }

        private void GroupSetupMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(groupNameKontoTextBoxExt.Text.Trim()) || string.IsNullOrEmpty(databaseKontoTextBoxExt.Text.Trim()))
                {
                    MessageBox.Show("please enter valid input");
                    return;
                }
                string filePath = "DbGroupListFile.xml";
                if (!File.Exists(filePath))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("DBLists");

                        xmlWriter.WriteStartElement("GroupData");
                        xmlWriter.WriteElementString("GroupName", groupNameKontoTextBoxExt.Text.Trim());
                        xmlWriter.WriteElementString("DBName", databaseKontoTextBoxExt.Text.Trim());
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load(filePath);
                    XElement root = xDocument.Element("DBLists");
                    IEnumerable<XElement> rows = root.Descendants("GroupData");
                    XElement firstRow = rows.First();
                    firstRow.AddBeforeSelf(
                       new XElement("GroupData",
                       new XElement("GroupName", groupNameKontoTextBoxExt.Text.Trim()),
                       new XElement("DBName", databaseKontoTextBoxExt.Text.Trim())));
                    xDocument.Save(filePath);//"Test.xml"
                    
                }

                MessageBox.Show("Group Name Updated..");
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                MessageBox.Show(ex.ToString());
                
            }
            
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
