using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class GpResult : BatCt.CommandSetter
    {
        public GpResult(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            checkBox3_CheckedChanged(null, null);
            Comment = Command.CommandDiscription;
            comboBox1.Text = "USER";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = checkBox2.Checked;
            textBox4.Enabled = comboBox1.Text == "USER";

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            //远程
            if (checkBox2.Checked)
            {
                sb.Append("/S \"");
                sb.Append(textBox1.Text);

                sb.Append("\" /U \"");
                sb.Append(textBox2.Text);

                sb.Append("\" /P \"");
                sb.Append(textBox3.Text);
                sb.Append("\" ");
            }


            if (comboBox1.Text == "USER")
            {
                sb.Append("/SCOPE USER /USER ");
                sb.Append(textBox4.Text);
                sb.Append(" ");
            }
            else
            {
                sb.Append("/SCOPE COMPUTER ");
            }
            sb.Append(checkBox3.Checked ? "/Z " : "/V ");
            lbPreview.Text = sb.ToString();
        }
    }
}
