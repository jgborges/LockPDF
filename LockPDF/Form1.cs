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
using iTextSharp;
using System.IO;

namespace LockPDF
{
    public partial class Form1 : Form
    {
        private object sync = new object();

        private string filename = null;

        private string user_pwd = null;
        private string owner_pwd = null;

        public Form1(string file) : this()
        {
            this.filename = file;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetButtonContextTitle();

            if (string.IsNullOrEmpty(filename) == false && File.Exists(filename))
            {
                this.textBoxFile.Width = buttonFile.Right - this.textBoxFile.Left;
                this.buttonFile.Visible = false;
            }

            LoadFile(this.filename);
        }

        private void LoadFile(string file)
        {
            bool hasFile = string.IsNullOrEmpty(file) == false && File.Exists(file);

            PdfPermissions perms = PdfPermissions.All;
            bool wrong_pwd = false, fullPermission = false;

            string pwd = owner_pwd ?? user_pwd ?? null;

            bool readResult = hasFile && 
                Program.ReadPDF(file, pwd, out wrong_pwd, out fullPermission, out perms);

            bool canLock = hasFile && readResult && !wrong_pwd && fullPermission;

            textBoxFile.Text = file;

            labelError.Visible = false;

            this.checkBoxRandom.Enabled = false;
            this.checkBoxRandom.Checked = true;

            this.textBoxUserPwd.Text = "";
            this.checkBoxUserPwd.Enabled = false;
            this.checkBoxUserPwd.Checked = false;

            if (hasFile == false || readResult == false)
            {
                labelError.Text = "Error: could not load file.";
                labelError.Visible = string.IsNullOrEmpty(file) == false;
                textBoxFile.Text = "";
            }
            else if (wrong_pwd)
            {
                labelError.Text = "Error: file is encrypted. Click here to open.";
                labelError.Visible = true;
            }
            else if (fullPermission == false)
            {
                labelError.Text = "Error: permissions are locked. Click here to unlock.";
                labelError.Visible = true;

                this.checkBoxRandom.Enabled = canLock;
                this.checkBoxRandom.Checked = false;
                this.textBoxOwnerPwd.Text = "";

                this.textBoxUserPwd.Text = user_pwd ?? "";
                this.checkBoxUserPwd.Enabled = canLock;
                this.checkBoxUserPwd.Checked = user_pwd != null;
            }
            else
            {
                this.checkBoxRandom.Checked = owner_pwd == null;
                this.checkBoxRandom.Enabled = true;
                this.textBoxOwnerPwd.Text = owner_pwd ?? "";
            }


            bool allowAll = canLock && (perms == PdfPermissions.All);

            lock (sync)
            {
                suspendCheckBoxEvent = true;

                checkBoxPrint.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowDegradedPrinting);
                checkBoxPrintHD.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowPrinting);
                checkBoxCopy.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowCopy);
                checkBoxCopyScreen.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowScreenReaders);
                checkBoxExtraction.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowAssembly);
                checkBoxContents.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowModifyContents);
                checkBoxComments.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowModifyAnnotations);
                checkBoxForm.Checked = allowAll || perms.HasFlag(PdfPermissions.AllowFillIn);

                suspendCheckBoxEvent = false;
            }

            bool permEnabled = canLock;

            checkBoxAll.Enabled = permEnabled;

            checkBoxPrint.Enabled = permEnabled;
            checkBoxPrintHD.Enabled = permEnabled;
            checkBoxCopy.Enabled = permEnabled;
            checkBoxCopyScreen.Enabled = permEnabled;
            checkBoxContents.Enabled = permEnabled;
            checkBoxComments.Enabled = permEnabled;
            checkBoxForm.Enabled = permEnabled;
            checkBoxExtraction.Enabled = permEnabled;

            checkBoxRandom.Enabled = canLock;

            checkBoxUserPwd.Enabled = canLock;

            checkBoxReplace.Enabled = canLock;
            checkBoxDefault.Enabled = canLock;
            buttonLock.Enabled = canLock;

            checkBox_CheckedChanged(checkBoxRandom, null);
            checkBox_CheckedChanged(checkBoxUserPwd, null);
            checkBoxAll_CheckedChanged(checkBoxForm, null); // for update
        }

        private void SetButtonContextTitle()
        {
            this.buttonContextMenu.Text = Program.GetContext() ? "Disable ContextMenu" : "Enable ContextMenu";
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            bool CTRL = ModifierKeys.HasFlag(Keys.Control);
            if (CTRL)
            {
                if (Program.AskReplace(this, filename, true) == false)
                    return;

                string pwd = owner_pwd ?? user_pwd ?? null;

                string error;
                if (Program.UnlockPDF(textBoxFile.Text, pwd, out error))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                if (checkBoxRandom.Checked == false && textBoxOwnerPwd.Text.Length < 3)
                {
                    MessageBox.Show(this, "Plase, set a owner password with at least 3 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxOwnerPwd.Focus();
                    return;
                }

                if (checkBoxUserPwd.Checked && textBoxUserPwd.Text.Length < 3)
                {
                    MessageBox.Show(this, "Plase, set a user password with at least 3 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxUserPwd.Focus();
                    return;
                }

                if (Program.AskReplace(this, textBoxFile.Text, checkBoxReplace.Checked, false) == false)
                    return;

                PdfPermissions permissions = PdfPermissions.All;

                if (checkBoxPrint.Checked)
                    permissions |= PdfPermissions.AllowDegradedPrinting;
                if (checkBoxPrintHD.Checked)
                    permissions |= PdfPermissions.AllowPrinting;
                if (checkBoxCopy.Checked)
                    permissions |= PdfPermissions.AllowCopy;
                if (checkBoxCopyScreen.Checked)
                    permissions |= PdfPermissions.AllowScreenReaders;
                if (checkBoxExtraction.Checked)
                    permissions |= PdfPermissions.AllowAssembly;
                if (checkBoxContents.Checked)
                    permissions |= PdfPermissions.AllowModifyContents;
                if (checkBoxComments.Checked)
                    permissions |= PdfPermissions.AllowModifyAnnotations;
                if (checkBoxForm.Checked)
                    permissions |= PdfPermissions.AllowFillIn;

                string pwdOwner = checkBoxRandom.Checked ? Program.RandomPassword : textBoxOwnerPwd.Text;
                string pwdUser = checkBoxUserPwd.Checked && textBoxUserPwd.Text.Length >= 3 ? textBoxUserPwd.Text : null;

                if (checkBoxDefault.Checked)
                {
                    string pwdOwner_default = checkBoxRandom.Checked == false ? pwdOwner : null;

                    Program.SaveDefault(permissions, checkBoxReplace.Checked, pwdOwner_default, pwdUser);
                }

                string pwd = owner_pwd ?? user_pwd ?? null;

                string error;
                if (Program.LockPDF(textBoxFile.Text, pwd, permissions, pwdOwner, pwdUser, checkBoxReplace.Checked, out error))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }

            if (this.Modal == false)
                this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this.Modal == false)
                this.Close();
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            buttonFile.Enabled = false;
            Application.DoEvents();

            if (sender == labelError && string.IsNullOrEmpty(textBoxFile.Text) == false & File.Exists(textBoxFile.Text))
            {
                string pwd = InputBox.Show(this, "PDF Password", "Please type the owner or user password:", "");

                bool fullPermission;
                if (Program.TryUnlock(textBoxFile.Text, pwd, out fullPermission))
                {
                    if (fullPermission)
                    {
                        this.owner_pwd = pwd;
                    }
                    else
                    {
                        this.user_pwd = pwd;
                    }

                    LoadFile(textBoxFile.Text);
                }
                else
                {
                    MessageBox.Show(this, "Bad password, please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Default;
                    buttonFile.Enabled = true;
                    return;
                }
            }
            if (sender == buttonFile)
            {
                this.owner_pwd = null;
                this.user_pwd = null;

                if (textBoxFile.Text != string.Empty)
                {
                    openFileDialog.FileName = textBoxFile.Text;
                }

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    LoadFile(openFileDialog.FileName);
                }
            }

            Cursor = Cursors.Default;
            buttonFile.Enabled = true;
        }

        private void buttonContextMenu_Click(object sender, EventArgs e)
        {
            if (Program.GetContext())
            {
                Program.DisableContext();
            }
            else
            {
                Program.SetContext();
            }

            SetButtonContextTitle();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBoxUserPwd)
            {
                textBoxUserPwd.Enabled = checkBoxUserPwd.Enabled && checkBoxUserPwd.Checked;
            }
            else if (sender == checkBoxReplace)
            {
                labelReplace.Visible = checkBoxReplace.Checked == false;
            }
            else if (sender == checkBoxRandom)
            {
                textBoxOwnerPwd.Enabled = checkBoxRandom.Enabled && !checkBoxRandom.Checked;

                if (checkBoxRandom.Checked)
                {
                    textBoxOwnerPwd.Text = Program.RandomPassword;
                }
            }
            else if (sender == checkBoxUserPwd)
            {
                textBoxUserPwd.Enabled = checkBoxUserPwd.Checked;

                if (checkBoxUserPwd.Checked && checkBoxUserPwd.Tag is string)
                {
                    checkBoxUserPwd.Text = checkBoxUserPwd.Text as string;
                }
            }
        }

        private bool suspendCheckBoxEvent = false;

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (suspendCheckBoxEvent)
                return;

            lock (sync)
            {
                suspendCheckBoxEvent = true;

                if (sender != checkBoxAll)
                {
                    if (checkBoxPrintHD.Checked && checkBoxPrint.Checked == false)
                    {
                        checkBoxPrint.Checked = true;
                    }

                    bool isAll =
                        checkBoxPrint.Checked && checkBoxPrintHD.Checked &&
                        checkBoxCopy.Checked && checkBoxCopyScreen.Checked &&
                        checkBoxContents.Checked && checkBoxExtraction.Checked &&
                        checkBoxComments.Checked && checkBoxForm.Checked;


                    bool isNone =
                        !checkBoxPrint.Checked && !checkBoxPrintHD.Checked &&
                        !checkBoxCopy.Checked && !checkBoxCopyScreen.Checked &&
                        !checkBoxContents.Checked && !checkBoxExtraction.Checked &&
                        !checkBoxComments.Checked && !checkBoxForm.Checked;

                    checkBoxAll.CheckState = isAll ? CheckState.Checked : isNone ? CheckState.Unchecked : CheckState.Indeterminate;
                }
                else
                {

                    bool isChecked = checkBoxAll.CheckState != CheckState.Unchecked;

                    checkBoxPrint.Checked = isChecked;
                    checkBoxPrintHD.Checked = isChecked;
                    checkBoxCopy.Checked = isChecked;
                    checkBoxCopyScreen.Checked = isChecked;
                    checkBoxExtraction.Checked = isChecked;
                    checkBoxContents.Checked = isChecked;
                    checkBoxComments.Checked = isChecked;
                    checkBoxForm.Checked = isChecked;
                }

                suspendCheckBoxEvent = false;
            }
        }

        private void textBoxUserPwd_TextChanged(object sender, EventArgs e)
        {
            textBoxUserPwd.Tag = textBoxUserPwd.Text;
        }
    }
}
