using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class ChkNTFS : BatCt.CommandSetter
    {
        public ChkNTFS(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            //检查磁盘
            if (radioButton1.Checked)
            {
                sb.Append(maskedTextBox1.Text);

                Comment = string.Format("立刻检查磁盘{0}", maskedTextBox1.Text);
            }

            //归位
            if (radioButton2.Checked)
            {
                sb.Append("/D");
                Comment = "磁盘检查所有设置归位";
            }

            //设定倒计时
            if (radioButton3.Checked)
            {
                sb.Append("/T:");
                sb.Append(numericUpDown1.Value);
                Comment = string.Format("检查磁盘前倒计时{0}秒", numericUpDown1.Value);
            }

            //排除驱动器
            if (radioButton4.Checked)
            {
                sb.Append("/X ");
                sb.Append(maskedTextBox2.Text);
                Comment = string.Format("不检查磁盘{0}", maskedTextBox2.Text);
            }

            //启动后检查
            if (radioButton5.Checked)
            {
                sb.Append("/C ");
                sb.Append(maskedTextBox3.Text);
                Comment = string.Format("下次开机检查磁盘{0}", maskedTextBox3.Text);
            }

            lbPreview.Text = sb.ToString();
        }
    }
}
