using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Find : BatCt.CommandSetter
    {
        public Find(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            checkBox2_CheckedChanged(null, null);
            Comment = command.CommandDiscription;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox1.Text = openFileDialog1.FileName;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/V " : "");
            sb.Append(checkBox3.Checked ? "/C " : "");
            sb.Append(checkBox4.Checked ? "/N " : "");
            sb.Append(checkBox5.Checked ? "/I " : "");
            sb.Append(checkBox6.Checked ? "/OFF " : "");

            sb.Append(" \"");
            sb.Append(textBox2.Text);
            sb.Append("\" ");
            sb.Append(textBox1.Text);

            lbPreview.Text = sb.ToString();
        }

    }
}
