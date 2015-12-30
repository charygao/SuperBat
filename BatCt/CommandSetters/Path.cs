using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Path : BatCt.CommandSetter
    {
        public Path(BatCommand command)
            : base(command)
        {
            InitializeComponent();

            radioButton1_CheckedChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton3.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            if (radioButton1.Checked)
            {
                sb.Append(";");
                Comment = "清除所有搜索路径设置并指示 cmd.exe 只在当前目录中搜索";
            }
            if (radioButton2.Checked)
            {
                Comment = "显示当前路径";
            }
            if (radioButton3.Checked)
            {
                sb.Append(textBox1.Text);
                Comment = "设置一个搜索路径";
            }

            lbPreview.Text = sb.ToString();
        }
    }
}
