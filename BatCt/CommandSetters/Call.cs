using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Call : BatCt.CommandSetter
    {
        public Call(BatCommand command)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" \"");

            sb.Append(textBox1.Text);
            sb.Append("\" ");

            sb.Append(textBox2.Text);

            lbPreview.Text = sb.ToString();
        }

        private void Call_Load(object sender, EventArgs e)
        {
            Comment = "调用指定的批处理文件";
        }
    }
}
