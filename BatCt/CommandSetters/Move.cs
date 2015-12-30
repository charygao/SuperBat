using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Move : BatCt.CommandSetter
    {
        public Move(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            Comment = Command.CommandDiscription;
            textBox2_TextChanged(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(textBox1.Text);
            sb.Append(" ");
            sb.Append(textBox2.Text);

            sb.Append(checkBox2.Checked?"/-Y":"/Y");

            lbPreview.Text = sb.ToString();
        }
    }
}
