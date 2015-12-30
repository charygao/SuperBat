using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Comp : BatCt.CommandSetter
    {
        public Comp(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox7.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);

            sb.Append(" \"");
            sb.Append(textBox1.Text);
            sb.Append("\" ");

            sb.Append(" \"");
            sb.Append(textBox2.Text);
            sb.Append("\" ");

            sb.Append(checkBox2.Checked ? "/D " : "");
            sb.Append(checkBox3.Checked ? "/A " : "");
            sb.Append(checkBox4.Checked ? "/L " : "");
            sb.Append(checkBox5.Checked ? string.Format("/N={0} ", numericUpDown1.Value) : "");
            sb.Append(checkBox6.Checked ? "/C " : "");
            sb.Append(checkBox7.Checked ? string.Format("/OFF {0}", textBox3.Text) : "");
            lbPreview.Text = sb.ToString();
        }

        private void Comp_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
            textBox1_TextChanged(null, null);
        }
    }
}
