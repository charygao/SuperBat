using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Copy : BatCt.CommandSetter
    {
        public Copy(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/D " : "");
            sb.Append(checkBox3.Checked ? "/V " : "");
            sb.Append(checkBox4.Checked ? "/N " : "");
            sb.Append(checkBox5.Checked ? "/-Y " : "/Y ");
            sb.Append(checkBox6.Checked ? "/Z " : "");

            sb.Append(textBox1.Text);
            sb.Append(" ");
            sb.Append(textBox2.Text);

            lbPreview.Text = sb.ToString();
        }

        private void Copy_Load(object sender, EventArgs e)
        {
            checkBox2_CheckedChanged(null, null);
            Comment = Command.CommandDiscription;
        }
    }
}
