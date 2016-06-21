namespace LockPDF
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
            this.buttonLock = new System.Windows.Forms.Button();
            this.checkBoxPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxPrintHD = new System.Windows.Forms.CheckBox();
            this.checkBoxCopy = new System.Windows.Forms.CheckBox();
            this.checkBoxComments = new System.Windows.Forms.CheckBox();
            this.checkBoxForm = new System.Windows.Forms.CheckBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxContents = new System.Windows.Forms.CheckBox();
            this.checkBoxReplace = new System.Windows.Forms.CheckBox();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.buttonContextMenu = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelReplace = new System.Windows.Forms.Label();
            this.checkBoxCopyScreen = new System.Windows.Forms.CheckBox();
            this.checkBoxExtraction = new System.Windows.Forms.CheckBox();
            this.checkBoxRandom = new System.Windows.Forms.CheckBox();
            this.textBoxOwnerPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserPwd = new System.Windows.Forms.TextBox();
            this.checkBoxUserPwd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonLock
            // 
            this.buttonLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLock.Location = new System.Drawing.Point(106, 396);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(199, 23);
            this.buttonLock.TabIndex = 0;
            this.buttonLock.Text = "Lock my PDF";
            this.buttonLock.UseVisualStyleBackColor = true;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // checkBoxPrint
            // 
            this.checkBoxPrint.AutoSize = true;
            this.checkBoxPrint.Location = new System.Drawing.Point(11, 154);
            this.checkBoxPrint.Name = "checkBoxPrint";
            this.checkBoxPrint.Size = new System.Drawing.Size(88, 17);
            this.checkBoxPrint.TabIndex = 1;
            this.checkBoxPrint.Text = "Allow printing";
            this.checkBoxPrint.UseVisualStyleBackColor = true;
            this.checkBoxPrint.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxPrintHD
            // 
            this.checkBoxPrintHD.AutoSize = true;
            this.checkBoxPrintHD.Location = new System.Drawing.Point(11, 177);
            this.checkBoxPrintHD.Name = "checkBoxPrintHD";
            this.checkBoxPrintHD.Size = new System.Drawing.Size(129, 17);
            this.checkBoxPrintHD.TabIndex = 1;
            this.checkBoxPrintHD.Text = "Allow high-def printing";
            this.checkBoxPrintHD.UseVisualStyleBackColor = true;
            this.checkBoxPrintHD.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxCopy
            // 
            this.checkBoxCopy.AutoSize = true;
            this.checkBoxCopy.Location = new System.Drawing.Point(11, 200);
            this.checkBoxCopy.Name = "checkBoxCopy";
            this.checkBoxCopy.Size = new System.Drawing.Size(91, 17);
            this.checkBoxCopy.TabIndex = 1;
            this.checkBoxCopy.Text = "Allow copying";
            this.checkBoxCopy.UseVisualStyleBackColor = true;
            this.checkBoxCopy.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxComments
            // 
            this.checkBoxComments.AutoSize = true;
            this.checkBoxComments.Location = new System.Drawing.Point(172, 200);
            this.checkBoxComments.Name = "checkBoxComments";
            this.checkBoxComments.Size = new System.Drawing.Size(102, 17);
            this.checkBoxComments.TabIndex = 1;
            this.checkBoxComments.Text = "Allow comments";
            this.checkBoxComments.UseVisualStyleBackColor = true;
            this.checkBoxComments.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxForm
            // 
            this.checkBoxForm.AutoSize = true;
            this.checkBoxForm.Location = new System.Drawing.Point(172, 223);
            this.checkBoxForm.Name = "checkBoxForm";
            this.checkBoxForm.Size = new System.Drawing.Size(100, 17);
            this.checkBoxForm.TabIndex = 1;
            this.checkBoxForm.Text = "Allow form filling";
            this.checkBoxForm.UseVisualStyleBackColor = true;
            this.checkBoxForm.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(11, 23);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(230, 20);
            this.textBoxFile.TabIndex = 2;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(11, 131);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(103, 17);
            this.checkBoxAll.TabIndex = 1;
            this.checkBoxAll.Text = "Allow everything";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxContents
            // 
            this.checkBoxContents.AutoSize = true;
            this.checkBoxContents.Location = new System.Drawing.Point(172, 177);
            this.checkBoxContents.Name = "checkBoxContents";
            this.checkBoxContents.Size = new System.Drawing.Size(115, 17);
            this.checkBoxContents.TabIndex = 1;
            this.checkBoxContents.Text = "Allow modifications";
            this.checkBoxContents.UseVisualStyleBackColor = true;
            this.checkBoxContents.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxReplace
            // 
            this.checkBoxReplace.AutoSize = true;
            this.checkBoxReplace.Location = new System.Drawing.Point(12, 320);
            this.checkBoxReplace.Name = "checkBoxReplace";
            this.checkBoxReplace.Size = new System.Drawing.Size(118, 17);
            this.checkBoxReplace.TabIndex = 1;
            this.checkBoxReplace.Text = "Replace current file";
            this.checkBoxReplace.UseVisualStyleBackColor = true;
            this.checkBoxReplace.Click += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.Location = new System.Drawing.Point(12, 343);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDefault.TabIndex = 1;
            this.checkBoxDefault.Text = "Save as default settings";
            this.checkBoxDefault.UseVisualStyleBackColor = true;
            // 
            // buttonContextMenu
            // 
            this.buttonContextMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonContextMenu.Location = new System.Drawing.Point(180, 339);
            this.buttonContextMenu.Name = "buttonContextMenu";
            this.buttonContextMenu.Size = new System.Drawing.Size(125, 23);
            this.buttonContextMenu.TabIndex = 0;
            this.buttonContextMenu.Text = "Enable Context Menu";
            this.buttonContextMenu.UseVisualStyleBackColor = true;
            this.buttonContextMenu.Click += new System.EventHandler(this.buttonContextMenu_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 396);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(269, 380);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(35, 13);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "About";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "LockPDF by Jonas Borges - Version 1.0.0";
            // 
            // buttonFile
            // 
            this.buttonFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFile.Location = new System.Drawing.Point(245, 21);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(60, 24);
            this.buttonFile.TabIndex = 5;
            this.buttonFile.Text = "Open";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
            this.openFileDialog.Title = "Load PDF";
            // 
            // labelReplace
            // 
            this.labelReplace.AutoSize = true;
            this.labelReplace.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelReplace.Location = new System.Drawing.Point(133, 321);
            this.labelReplace.Name = "labelReplace";
            this.labelReplace.Size = new System.Drawing.Size(164, 13);
            this.labelReplace.TabIndex = 6;
            this.labelReplace.Text = "(a new locked file will be created)";
            // 
            // checkBoxCopyScreen
            // 
            this.checkBoxCopyScreen.AutoSize = true;
            this.checkBoxCopyScreen.Location = new System.Drawing.Point(11, 223);
            this.checkBoxCopyScreen.Name = "checkBoxCopyScreen";
            this.checkBoxCopyScreen.Size = new System.Drawing.Size(124, 17);
            this.checkBoxCopyScreen.TabIndex = 1;
            this.checkBoxCopyScreen.Text = "Allow screen readers";
            this.checkBoxCopyScreen.UseVisualStyleBackColor = true;
            this.checkBoxCopyScreen.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxExtraction
            // 
            this.checkBoxExtraction.AutoSize = true;
            this.checkBoxExtraction.Location = new System.Drawing.Point(172, 154);
            this.checkBoxExtraction.Name = "checkBoxExtraction";
            this.checkBoxExtraction.Size = new System.Drawing.Size(132, 17);
            this.checkBoxExtraction.TabIndex = 1;
            this.checkBoxExtraction.Text = "Allow page extractions";
            this.checkBoxExtraction.UseVisualStyleBackColor = true;
            this.checkBoxExtraction.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxRandom
            // 
            this.checkBoxRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRandom.AutoSize = true;
            this.checkBoxRandom.Checked = true;
            this.checkBoxRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRandom.Location = new System.Drawing.Point(239, 87);
            this.checkBoxRandom.Name = "checkBoxRandom";
            this.checkBoxRandom.Size = new System.Drawing.Size(66, 17);
            this.checkBoxRandom.TabIndex = 1;
            this.checkBoxRandom.Text = "Random";
            this.checkBoxRandom.UseVisualStyleBackColor = true;
            this.checkBoxRandom.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // textBoxOwnerPwd
            // 
            this.textBoxOwnerPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOwnerPwd.Enabled = false;
            this.textBoxOwnerPwd.Location = new System.Drawing.Point(11, 85);
            this.textBoxOwnerPwd.Name = "textBoxOwnerPwd";
            this.textBoxOwnerPwd.Size = new System.Drawing.Size(225, 20);
            this.textBoxOwnerPwd.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Owner password";
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.IndianRed;
            this.labelError.Location = new System.Drawing.Point(9, 46);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(255, 13);
            this.labelError.TabIndex = 6;
            this.labelError.Text = "Error: could not load permitions. Click here to unlock.";
            this.labelError.Visible = false;
            this.labelError.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "PDF File to Lock";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(9, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(258, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Use this password to change permissions on the PDF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(12, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Use this password to prevent users from opening the PDF";
            // 
            // textBoxUserPwd
            // 
            this.textBoxUserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserPwd.Enabled = false;
            this.textBoxUserPwd.Location = new System.Drawing.Point(12, 268);
            this.textBoxUserPwd.Name = "textBoxUserPwd";
            this.textBoxUserPwd.Size = new System.Drawing.Size(292, 20);
            this.textBoxUserPwd.TabIndex = 10;
            this.textBoxUserPwd.TextChanged += new System.EventHandler(this.textBoxUserPwd_TextChanged);
            // 
            // checkBoxUserPwd
            // 
            this.checkBoxUserPwd.AutoSize = true;
            this.checkBoxUserPwd.Location = new System.Drawing.Point(12, 251);
            this.checkBoxUserPwd.Name = "checkBoxUserPwd";
            this.checkBoxUserPwd.Size = new System.Drawing.Size(97, 17);
            this.checkBoxUserPwd.TabIndex = 9;
            this.checkBoxUserPwd.Text = "User Password";
            this.checkBoxUserPwd.UseVisualStyleBackColor = true;
            this.checkBoxUserPwd.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 431);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUserPwd);
            this.Controls.Add(this.checkBoxUserPwd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelReplace);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.textBoxOwnerPwd);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.checkBoxDefault);
            this.Controls.Add(this.checkBoxRandom);
            this.Controls.Add(this.checkBoxReplace);
            this.Controls.Add(this.checkBoxContents);
            this.Controls.Add(this.checkBoxForm);
            this.Controls.Add(this.checkBoxComments);
            this.Controls.Add(this.checkBoxExtraction);
            this.Controls.Add(this.checkBoxCopyScreen);
            this.Controls.Add(this.checkBoxCopy);
            this.Controls.Add(this.checkBoxPrintHD);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.checkBoxPrint);
            this.Controls.Add(this.buttonContextMenu);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lock PDF - Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.CheckBox checkBoxPrint;
        private System.Windows.Forms.CheckBox checkBoxPrintHD;
        private System.Windows.Forms.CheckBox checkBoxCopy;
        private System.Windows.Forms.CheckBox checkBoxComments;
        private System.Windows.Forms.CheckBox checkBoxForm;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.CheckBox checkBoxContents;
        private System.Windows.Forms.CheckBox checkBoxReplace;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private System.Windows.Forms.Button buttonContextMenu;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelReplace;
        private System.Windows.Forms.CheckBox checkBoxCopyScreen;
        private System.Windows.Forms.CheckBox checkBoxExtraction;
        private System.Windows.Forms.CheckBox checkBoxRandom;
        private System.Windows.Forms.TextBox textBoxOwnerPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUserPwd;
        private System.Windows.Forms.CheckBox checkBoxUserPwd;
    }
}

