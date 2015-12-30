using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class At : CommandSetter
    {

        /// <summary>
        /// 构造一个At命令行配置器对话窗
        /// </summary>
        /// <param name="command">传入的命令</param>
        public At(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = radioButton1.Checked;
            checkBox2.Enabled = radioButton3.Checked;
            textBox1.Enabled = radioButton2.Checked;

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            //远程主机
            if (radioButton5.Checked)
            {
                sb.Append(textBox2.Text);
                sb.Append(" ");

                sb2.Append(string.Format("在远程主机{0}上", textBox2.Text));
            }
            else
            {
                sb2.Append("在本机");
            }

            //按名称删除
            if (radioButton2.Checked)
            {
                sb.Append(textBox1.Text);
                sb.Append(" /delete");

                sb2.Append(string.Format("删除计划任务“{0}”", textBox1.Text));
            }

            //全部删除
            if (radioButton3.Checked)
            {
                sb.Append(" /delete");
                sb2.Append("删除全部计划任务");
                //无提示删除
                if (checkBox2.Checked)
                {
                    sb.Append(" /yes ");
                    sb2.Append("（无需用户确认）");
                }
            }

            //新建
            if (radioButton1.Checked)
            {
                sb.Append(textBox6.Text);
                sb.Append(" ");
                sb2.Append("创建计划任务");
                //交互
                if (checkBox3.Checked)
                {
                    sb2.Append("（可交互）");
                    sb.Append("/interactive ");
                }
                //循环执行
                if (radioButton7.Checked)
                {
                    sb.Append("/every:date ");
                    sb.Append(textBox3.Text);
                    sb.Append(" ");
                }
                //下次执行
                if (radioButton8.Checked)
                {
                    sb.Append("/next:date ");
                    sb.Append(textBox5.Text);
                    sb.Append(" ");
                }

                sb.Append(textBox4.Text);
                sb2.Append(string.Format("执行命令{0}", textBox5.Text));
            }

            lbPreview.Text = sb.ToString();
            Comment = sb2.ToString();


        }

        private void At_Load(object sender, EventArgs e)
        {
            radioButton4_CheckedChanged(null, null);
        }



    }
}
