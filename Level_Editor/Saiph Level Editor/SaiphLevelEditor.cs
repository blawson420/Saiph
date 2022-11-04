using System;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using System.IO;

namespace Saiph_Level_Editor
{
    public partial class SaiphLevelEditor : Form
    {
        public bool isUnsaved;
        public string saveFile;
        public string saveFileName;
        public bool canClose;
        public SaiphLevelEditor()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, splitContainer1.Panel1, new object[] { true });
            //innitalize enemy type to 0 to prevent errors
            cbEnemyType.Text = "0";
            isUnsaved = false;
            saveFile = "";
            canClose = false;
        }

        #region UI        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //check if user has added an amount of enemies to spawn
            if (nudAmount.Value > 0)
            {
                if (lbNodes.SelectedIndex > -1 && lbNodes.SelectedIndex < lbNodes.Items.Count)
                {
                    int selectedIndex = lbNodes.SelectedIndex;
                    //add spawn node to list
                    Node insertNode = createNode();
                    lbNodes.Items.Insert(selectedIndex += 1, insertNode);
                    lbNodes.SelectedIndex = selectedIndex;
                    isUnsaved = true;
                    canClose = false;
                }
                else
                {
                    //add spawn node to list
                    Node node = createNode();
                    lbNodes.Items.Add(node);
                    lbNodes.SelectedIndex = lbNodes.Items.Count - 1;
                }
            }
            else
            {
                //throw no amount error
                const string errorMessage =
                    "Please verify the amount of enemies you would like to add.";
                const string errorCaption =
                    "Error Detected. Amount of enemies is 0. ";
                MessageBoxButtons errorButtons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //if the selectedindex is out of range
            if (lbNodes.Items.Count == 0)
            {
                //throw no Spawn selected
                const string errorMessage =
                    "There are no spawns points to remove.";
                const string errorCaption =
                    "Error Detected. No Spawn points in. ";
                MessageBoxButtons errorButtons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Error);
            }
            else if (lbNodes.SelectedIndex > -1 && lbNodes.SelectedIndex < lbNodes.Items.Count)
            {
                int selectedIndex = lbNodes.SelectedIndex;
                lbNodes.Items.RemoveAt(selectedIndex);
                isUnsaved = true;
                canClose = false;
                if (selectedIndex == lbNodes.Items.Count)
                {
                    lbNodes.SelectedIndex = selectedIndex - 1;
                }
                else if (selectedIndex == 0)
                {
                    lbNodes.SelectedIndex = 0;
                }
                else
                {
                    lbNodes.SelectedIndex = selectedIndex;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check if user has added an amount of enemies to spawn
            if (nudAmount.Value > 0)
            {
                //add spawn node to list
                Node node = createNode();
                int selectedIndex = lbNodes.SelectedIndex;
                lbNodes.Items.RemoveAt(selectedIndex);
                lbNodes.Items.Insert(selectedIndex, node);
                lbNodes.SelectedIndex = selectedIndex;
                isUnsaved = true;
                canClose = false;

            }
            else
            {
                //throw no amount error
                const string errorMessage =
                    "Please verify the amount of enemies you would like to add.";
                const string errorCaption =
                    "Error Detected. Amount of enemies is 0. ";
                MessageBoxButtons errorButtons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Error);
            }

        }

        private void lbNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbNodes.SelectedIndex > -1 && lbNodes.SelectedIndex < lbNodes.Items.Count)
            {
                //change the attributes to match the selected spawn point
                Node selectedNode = (Node)lbNodes.SelectedItem;
                nudXPosition.Value = (Decimal)selectedNode.GetX();
                nudYPosition.Value = (Decimal)selectedNode.GetY();
                nudWaitTime.Value = (Decimal)selectedNode.GetWait();
                nudInterval.Value = (Decimal)selectedNode.GetInterval();
                nudAmount.Value = selectedNode.GetAmount();
                cbEnemyType.Text = selectedNode.GetEnemy().ToString();
            }
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

            string fileName = "";
            int enemy = 0;
            if (int.TryParse(cbEnemyType.Text, out enemy))
            {
                fileName = "../../Resources/enemy" + enemy + ".png";
            }
            else
            {
                fileName = "../../Resources/enemy0.png";
            }

            Image imag = Image.FromFile(fileName);
            e.Graphics.DrawImage(imag, new Point(Convert.ToInt32(nudXPosition.Value), Convert.ToInt32(nudYPosition.Value)));

        }
        #endregion


        #region FileIO
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFile == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                XmlTextWriter xmlWrite = new XmlTextWriter(saveFile, null);
                xmlWrite.WriteStartElement("Level");
                xmlWrite.WriteWhitespace("\n");
                foreach (Node node in lbNodes.Items)
                {
                    xmlWrite.WriteWhitespace("\t");
                    xmlWrite.WriteStartElement("Spawn ");
                    xmlWrite.WriteStartAttribute("x");
                    xmlWrite.WriteValue(node.GetX());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteStartAttribute("y");
                    xmlWrite.WriteValue(node.GetY());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteStartAttribute("wait");
                    xmlWrite.WriteValue(node.GetWait());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteStartAttribute("interval");
                    xmlWrite.WriteValue(node.GetInterval());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteStartAttribute("enemy");
                    xmlWrite.WriteValue(node.GetEnemy());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteStartAttribute("amount");
                    xmlWrite.WriteValue(node.GetAmount());
                    xmlWrite.WriteEndAttribute();
                    xmlWrite.WriteEndElement();
                    xmlWrite.WriteWhitespace("\n");
                }
                xmlWrite.WriteEndElement();
                xmlWrite.Close();
                SetFormName();
                isUnsaved = false;
                canClose = true;
                
                const string message = "Save was successful.";
                const string caption = "Save Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, caption, buttons);
                
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveFile = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
                saveToolStripMenuItem_Click(sender, e);
                SetFormName();
            }
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaiphLevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUnsaved == true)
            {
                const string errorMessage =
                    "You have unsaved changes. Would you like to save?";
                const string errorCaption =
                    "Unsaved Changes Detected. ";
                MessageBoxButtons errorButtons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (!canClose)
                    {
                        e.Cancel = true;
                    }

                }
                else if (result == DialogResult.No)
                {
                    //do nothing
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (isSaved(sender, e))
            {
                saveFile = "";
                lbNodes.Items.Clear();
                nudXPosition.Value = 300;
                nudYPosition.Value = 300;
                nudWaitTime.Value = 0;
                nudInterval.Value = 0;
                nudAmount.Value = 0;
                cbEnemyType.Text = "0";
                isUnsaved = false;
                canClose = true;
                splitContainer1.Panel1.Invalidate();
                SetFormName();
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved(sender, e))
            {
                using (OpenFileDialog openLevel = new OpenFileDialog())
                {
                    openLevel.InitialDirectory = "../../";
                    openLevel.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                    openLevel.FilterIndex = 1;
                    openLevel.RestoreDirectory = true;
                    if (openLevel.ShowDialog() == DialogResult.OK)
                    {
                        saveFile = openLevel.FileName;
                        try
                        {
                            lbNodes.Items.Clear();
                            XmlDocument doc = new XmlDocument();
                            doc.Load(openLevel.FileName);
                            XmlNodeList spawnList = doc.GetElementsByTagName("Spawn");
                            foreach (XmlNode spawn in spawnList)
                            {
                                Node node = new Node(
                                    float.Parse(spawn.Attributes["x"].Value),
                                    float.Parse(spawn.Attributes["y"].Value),
                                    float.Parse(spawn.Attributes["wait"].Value),
                                    float.Parse(spawn.Attributes["interval"].Value),
                                    Convert.ToInt32(spawn.Attributes["enemy"].Value),
                                    Convert.ToInt32(spawn.Attributes["amount"].Value));
                                lbNodes.Items.Add(node);
                            }
                            SetFormName();
                            isUnsaved = false;
                            canClose = true;
                        }
                        catch (Exception)
                        {
                            const string errorMessage =
                                "Error opening file. ";
                            const string errorCaption =
                                "Error Detected. ";
                            MessageBoxButtons errorButtons = MessageBoxButtons.OK;
                            MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        #endregion
        #region Helper Functions

        //create node
        private Node createNode()
        {
                Node node = new Node((float)nudXPosition.Value, (float)nudYPosition.Value, (float)nudWaitTime.Value, (float)nudInterval.Value, Convert.ToInt32(cbEnemyType.Text), (int)nudAmount.Value);
                return node;
        }
        private bool isSaved(object sender, EventArgs e)
        {
            if (isUnsaved == false)
            {
                return true;
            }
            else
            {
                //throw unsaved changes error
                const string errorMessage =
                    "You have unsaved changes. Would you like to save?";
                const string errorCaption =
                    "Unsaved Changes Detected. ";
                MessageBoxButtons errorButtons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(errorMessage, errorCaption, errorButtons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (isUnsaved == false)
                    {
                        SetFormName();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else if (result == DialogResult.Cancel)
                {
                    //do nothing
                }
            }
            return false;
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Invalidate();
        }

        private void GetFileName()
        {
            if (saveFile != "")
            {
                saveFileName = (saveFile.Split('\\')[saveFile.Split('\\').Length - 1]).Split('.')[0];
            }
            else 
            {
                saveFileName = "Saiph Level Editor";
            }
        
        }
        
        private void SetFormName()
        {
            GetFileName();
            this.Text = saveFileName + " - Saiph Level Editor";
        }

        #endregion
    }
}
