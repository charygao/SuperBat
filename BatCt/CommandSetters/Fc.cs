using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Fc : BatCt.CommandSetter
    {
        public Fc(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void Fc_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
            checkBox12_CheckedChanged(null, null);
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append("");


            sb.Append(checkBox2.Checked ? "/A " : "");
            sb.Append(checkBox3.Checked ? "/B " : "");
            sb.Append(checkBox4.Checked ? "/C " : "");
            sb.Append(checkBox5.Checked ? "/L " : "");
            sb.Append(checkBox6.Checked ? string.Format("/LB{0} ", numericUpDown1.Value) : "");
            sb.Append(checkBox7.Checked ? "/N " : "");
            sb.Append(checkBox8.Checked ? string.Format("/OFF {0} ", textBox3.Text) : "");
            sb.Append(checkBox9.Checked ? "/T " : "");
            sb.Append(checkBox10.Checked ? "/U " : "");
            sb.Append(checkBox11.Checked ? "/W " : "");
            sb.Append(checkBox12.Checked ? string.Format("/{0} ", numericUpDown2.Value) : "");

            sb.Append(textBox1.Text);
            sb.Append(" ");
            sb.Append(textBox2.Text);

            lbPreview.Text = sb.ToString();
        }
    }
}
