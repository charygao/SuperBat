using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Format : BatCt.CommandSetter
    {
        public Format(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            checkBox2_CheckedChanged(null, null);
            Comment = command.CommandDiscription;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox2.Checked;
            comboBox2.Enabled = checkBox5.Checked;


            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(maskedTextBox1.Text);
            sb.Append(" /FS:");
            sb.Append(comboBox1.Text);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? string.Format("/V:{0} ", textBox1.Text) : "");
            sb.Append(checkBox3.Checked ? "/C " : "");
            sb.Append(checkBox4.Checked ? "/X " : "");
            sb.Append(checkBox5.Checked ? string.Format("/A:{0} ", comboBox2.Text) : "");
            sb.Append(checkBox6.Checked ? "/Q " : "");
            lbPreview.Text = sb.ToString();
        }
    }
}
