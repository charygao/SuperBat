using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Echo : BatCt.CommandSetter
    {
        public Echo(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);

            if (radioButton1.Checked)
            {
                sb.Append(" ");
                sb.Append(" on");

                Comment = "开启回显";
            }

            if (radioButton2.Checked)
            {
                sb.Append(" ");
                sb.Append(" off");
                Comment = "关闭回显";
            }

            if (radioButton3.Checked)
            {
                sb.Append(".");
                Comment = "输出空行";
            }

            if (radioButton4.Checked)
            {
                sb.Append(" ");
                sb.Append(textBox1.Text);
                Comment = "输出文本";
            }         

            lbPreview.Text = sb.ToString();
        }
    }
}
