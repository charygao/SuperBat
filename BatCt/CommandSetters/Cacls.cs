using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Cacls : BatCt.CommandSetter
    {
        public Cacls(BatCommand command)
            : base(command)
        {
            InitializeComponent();
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

            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel3.Enabled = radioButton2.Checked;
            checkBox5.Enabled = textBox2.Enabled = checkBox2.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" \"");
            sb.Append(textBox1.Text);
            sb.Append("\" ");

            //更改目录权限
            if (radioButton2.Checked)
            {
                sb.Append("/T ");

                //编辑而不是替换
                if (checkBox2.Checked)
                {
                    Comment = "对指定的文件或者文件夹设置权限，仅对NTFS磁盘系统有效";
                    sb.Append("/E ");
                    //撤销指定用户的访问权限
                    if (checkBox5.Checked)
                    {
                        sb.Append("/R ");
                        sb.Append(textBox2.Text);
                        sb.Append(" ");
                    }


                    //在出现拒绝访问错误时继续。
                    if (checkBox3.Checked)
                    {
                        sb.Append("/C ");
                    }

                    //赋予指定用户访问权限。
                    if (checkBox4.Checked)
                    {
                        sb.Append("/G ");
                        sb.Append(textBox3.Text);
                        sb.Append(";");
                        sb.Append(comboBox1.Text.Length > 1 ? comboBox1.Text[0].ToString() : "R");
                        sb.Append(" ");
                    }

                    //替换指定用户访问权限。
                    if (checkBox6.Checked)
                    {
                        sb.Append("/P ");
                        sb.Append(textBox4.Text);
                        sb.Append(";");
                        sb.Append(comboBox2.Text.Length > 1 ? comboBox2.Text[0].ToString() : "R");
                        sb.Append(" ");
                    }

                    //拒绝指定用户的访问
                    if (checkBox7.Checked)
                    {
                        sb.Append("/D ");
                        sb.Append(textBox5.Text);
                    }
                }
            }

            lbPreview.Text = sb.ToString();


        }

        private void Cacls_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);

        }
    }
}
