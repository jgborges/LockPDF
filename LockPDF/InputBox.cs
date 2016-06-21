using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockPDF
{
    public partial class InputBox : Form
    {
        public static string Show(IWin32Window owner, string title, string caption, string @default)
        {
            InputBox frm = new InputBox();
            frm.Text = title;
            frm.label.Text = caption;
            frm.textBox.Text = @default ?? "";
            if (string.IsNullOrEmpty(@default) == false)
                frm.textBox.SelectAll();

            frm.StartPosition = owner != null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;

            if (frm.ShowDialog(owner) == DialogResult.OK)
            {
                return frm.textBox.Text;
            }
            else
            {
                return null;
            }
        }

        private InputBox()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.Modal == false)
                this.Close();
        }
    }
}
