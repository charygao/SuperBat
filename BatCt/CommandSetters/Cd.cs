using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Cd : BatCt.CommandSetter
    {
        public Cd(BatCommand command)
            : base(command)
        {
            InitializeComponent();
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" \"");

            sb.Append(textBox1.Text);
            sb.Append("\" ");

            //跨盘符跳转
            if (checkBox1.Checked)
            {
                sb.Append("/D ");
            }

            lbPreview.Text = sb.ToString();
        }

        private void Cd_Load(object sender, EventArgs e)
        {
            checkBox2_CheckedChanged(null, null);
            Comment = "转到指定路径";
        }
    }
}
