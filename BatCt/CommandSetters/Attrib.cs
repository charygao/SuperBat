using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Attrib : BatCt.CommandSetter
    {
        public Attrib(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Enabled = radioButton1.Checked;
            checkBox2.Enabled = button2.Enabled = textBox2.Enabled = radioButton2.Checked;


            StringBuilder sb = new StringBuilder();            
            sb.Append(Command.CommandName);
            sb.Append(" ");

            //只读
            sb.Append(checkBox3.Checked ? "+" : "-");
            sb.Append("R ");

            //存档
            sb.Append(checkBox6.Checked ? "+" : "-");
            sb.Append("A ");

            //系统
            sb.Append(checkBox4.Checked ? "+" : "-");
            sb.Append("S ");

            //隐藏
            sb.Append(checkBox5.Checked ? "+" : "-");
            sb.Append("H ");

            //文件夹或文件
            sb.Append("\"");
            sb.Append(radioButton1.Checked ? textBox1.Text : textBox2.Text);
            sb.Append("\" ");
            //处理文件夹
            if (radioButton2.Checked)
            {
                sb.Append("/S ");
                //递归子文件夹
                if (checkBox2.Checked)
                {
                    sb.Append("/D");
                }

            }

            Comment = "设置磁盘文件/文件夹属性";
            lbPreview.Text = sb.ToString();
        }

        private void Attrib_Load(object sender, EventArgs e)
        {
            radioButton1_CheckedChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

    }
}
