using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Dir : BatCt.CommandSetter
    {
        public Dir(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            checkBox7.Enabled =
                checkBox8.Enabled =
                checkBox9.Enabled =
                checkBox10.Enabled =
                checkBox2.Enabled =checkBox6.Checked;


                 checkBox20.Enabled =
                checkBox21.Enabled =
                checkBox22.Enabled =
                checkBox23.Enabled =
                checkBox24.Enabled =checkBox13.Checked;

                 checkBox25.Enabled =
                checkBox26.Enabled =
                checkBox27.Enabled = checkBox16.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(textBox1.Text);
            sb.Append(" ");

            if (checkBox6.Checked)
            {
                sb.Append("/A:");
                sb.Append(checkBox7.Checked ? "R" : "-R");
                sb.Append(checkBox8.Checked ? "H" : "-H");
                sb.Append(checkBox9.Checked ? "S" : "-S");
                sb.Append(checkBox10.Checked ? "A" : "-A");
                sb.Append(checkBox2.Checked ? "D " : "-D ");
            }

            sb.Append(checkBox3.Checked ? "/B " : "");
            sb.Append(checkBox4.Checked ? "/C " : "");
            sb.Append(checkBox5.Checked ? "/D " : "");
            sb.Append(checkBox11.Checked ? "/L " : "");
            sb.Append(checkBox12.Checked ? "/N " : "");

            if (checkBox13.Checked)
            {
                sb.Append("/O:");
                sb.Append(checkBox20.Checked ? "N" : "-N");
                sb.Append(checkBox21.Checked ? "S" : "-S");
                sb.Append(checkBox22.Checked ? "E" : "-E");
                sb.Append(checkBox23.Checked ? "D" : "-D");
                sb.Append(checkBox24.Checked ? "G " : "-G ");
            }

            sb.Append(checkBox14.Checked ? "/Q " : "");
            sb.Append(checkBox15.Checked ? "/S " : "");

            if (checkBox16.Checked)
            {
                sb.Append("/T:");
                sb.Append(checkBox25.Checked ? "C" : "-C");
                sb.Append(checkBox26.Checked ? "A" : "-A");
                sb.Append(checkBox27.Checked ? "W " : "-W ");   
            }
            sb.Append(checkBox17.Checked ? "/W " : "");
            sb.Append(checkBox18.Checked ? "/X " : "");
            sb.Append(checkBox19.Checked ? "/4 " : "");
            lbPreview.Text = sb.ToString();
        }

        private void Dir_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
            checkBox27_CheckedChanged(null, null);
        }
    }
}
