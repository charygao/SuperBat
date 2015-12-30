using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Del : BatCt.CommandSetter
    {
        public Del(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox7.Enabled = checkBox9.Enabled = checkBox8.Enabled = checkBox10.Enabled = checkBox6.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/P " : "");
            sb.Append(checkBox3.Checked ? "/F " : "");
            sb.Append(checkBox4.Checked ? "/S " : "");
            sb.Append(checkBox5.Checked ? "/Q " : "");

            if (checkBox6.Checked)
            {
                sb.Append("/A:");
                sb.Append(checkBox7.Checked ? "R " : "-R ");
                sb.Append(checkBox8.Checked ? "H " : "-H ");
                sb.Append(checkBox9.Checked ? "S " : "-S ");
                sb.Append(checkBox10.Checked ? "A " : "-A ");
            }

            sb.Append(textBox1.Text);

            lbPreview.Text = sb.ToString();
        }

        private void Del_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
            checkBox7_CheckedChanged(null, null);
        }
    }
}
