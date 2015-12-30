using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Assoc : CommandSetter
    {

        /// <summary>
        /// 构造一个Assos命令行配置器对话窗
        /// </summary>
        /// <param name="command">传入的命令</param>
        public Assoc(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");



            if (radioButton2.Checked)
            {
                //如果没有以.开头，则加点
                if (!textBox1.Text.StartsWith("."))
                {
                    //补点
                    textBox1.Text = "." + textBox1.Text;
                    //光标后移
                    textBox1.Select(textBox1.Text.Length, 0);
                }
                sb.Append(textBox1.Text);

                this.Comment = string.Format("显示扩展名{0}关联的程序", textBox1.Text);
            }
            else if (radioButton3.Checked)
            {
                //如果没有以.开头，则加点
                if (!textBox3.Text.StartsWith("."))
                {
                    //补点
                    textBox3.Text = "." + textBox3.Text;
                    //光标后移
                    textBox3.Select(textBox3.Text.Length, 0);
                }
                sb.Append(textBox3.Text);
                this.Comment = string.Format("删除程序对扩展名{0}的关联", textBox3.Text.TrimEnd('='));
            }
            else if (radioButton4.Checked)
            {
                //如果没有以.开头，则加点
                if (!textBox2.Text.StartsWith("."))
                {
                    //补点
                    textBox2.Text = "." + textBox2.Text;
                    //光标后移
                    textBox2.Select(textBox2.Text.Length, 0);
                }
                sb.Append(textBox2.Text);
                this.Comment = string.Format("指定扩展名关联程序{0}", textBox2.Text);
            }
            else
            {
                this.Comment = string.Format("显示全部扩展名关联的程序清单");
            }

            lbPreview.Text = sb.ToString();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            //如果没有以=结束，则加等号
            if (!textBox3.Text.EndsWith("="))
            {
                //补点
                textBox3.Text += "=";

            }
        }

        private void Assoc_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
        }
    }
}
