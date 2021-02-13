using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace G05_Notepad
{
    public partial class NotepadForm : Form
    {
        public NotepadForm()
        {
            InitializeComponent();
        }

        private FileInfo fileInfo;

        private int searchingPosition = 0;

        #region Event handlers

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavaAsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                txtContent.BackColor = dlgColor.Color;
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Undo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string s = txtSearch.Text;

            searchingPosition = txtContent.Text.IndexOf(s, searchingPosition);

            if (searchingPosition != -1)
            {
                txtContent.Select(searchingPosition, s.Length);
                searchingPosition += s.Length;
            }
            else
            {
                searchingPosition = 0;
            }
            
            
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFont();
        }

        #endregion

        private bool NewFile()
        {
            txtContent.Clear();
            fileInfo = null;
            return true;
        }

        private bool OpenFile()
        {
            DialogResult result = dlgOpen.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileInfo = new FileInfo(dlgOpen.FileName);

                using (StreamReader reader = fileInfo.OpenText())
                {
                    txtContent.Text = reader.ReadToEnd();
                }
            }

            return true;
        }

        private bool SaveFile()
        {
            if (fileInfo != null)
            {
                using (StreamWriter writer = fileInfo.CreateText())
                {
                    writer.Write(txtContent.Text);
                }
                return true;
            }

            DialogResult result = dlgSave.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileInfo = new FileInfo(dlgSave.FileName);
                using (StreamWriter writer = fileInfo.CreateText())
                {
                    writer.Write(txtContent.Text);
                }
            }

            return true;
        }

        private bool SavaAsFile()
        {
            DialogResult result = dlgSave.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileInfo = new FileInfo(dlgSave.FileName);
                using (StreamWriter writer = fileInfo.CreateText())
                {
                    writer.Write(txtContent.Text);
                }
            }

            return true;
        }

        private void ChangeFont()
        {
            DialogResult result = dlgFont.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtContent.Font = dlgFont.Font;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void NotepadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Bye!", "Bye", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
