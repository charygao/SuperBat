using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Replace : BatCt.CommandSetter
    {
        public Replace(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            checkBox7_CheckedChanged(null, null);
            Comment = command.CommandDiscription;
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

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox6.Enabled = checkBox7.Enabled = !checkBox5.Checked;
            checkBox5.Enabled = !checkBox6.Checked;


            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/P " : "");
            sb.Append(checkBox3.Checked ? "/R " : "");
            sb.Append(checkBox4.Checked ? "/W " : "");


            sb.Append(checkBox5.Checked ? "/A " : "");
            sb.Append(checkBox6.Checked ? "/S " : "");
            sb.Append(checkBox6.Checked ? "/U " : "");

            sb.Append(textBox1.Text);

            sb.Append(" ");
            sb.Append(textBox2.Text);
            lbPreview.Text = sb.ToString();

        }
    }
}
