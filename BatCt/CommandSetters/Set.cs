using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Set : BatCt.CommandSetter
    {
        public Set(BatCommand command)
            : base(command)
        {
            InitializeComponent();

            radioButton4_CheckedChanged(null, null);

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            if (radioButton1.Checked)
            {
                Comment = "显示环境变量";
            }
            else if (radioButton3.Checked)
            {
                Comment = "删除环境变量";
                sb.Append(textBox3.Text);

            }
            else
            {
                Comment = "设置环境变量";                
                sb.Append(textBox2.Text);
            }
            lbPreview.Text = sb.ToString();
        }
    }
}
