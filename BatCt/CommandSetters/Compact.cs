using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Compact : BatCt.CommandSetter
    {
        public Compact(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(radioButton1.Checked ? "/C " : "/U ");
            if (checkBox2.Checked)
            {
                sb.Append("/S");
                if (textBox1.Text.Trim().Length > 0)
                {
                    sb.Append(":\"");
                    sb.Append(textBox1.Text);
                    sb.Append("\"");
                }
                sb.Append(" ");
            }
            sb.Append(checkBox3.Checked ? "/A " : "");
            sb.Append(checkBox4.Checked ? "/I " : "");
            sb.Append(checkBox5.Checked ? "/F " : "");
            sb.Append(checkBox6.Checked ? "/Q " : "");

            if (textBox2.Text.Trim().Length > 0)
            {
                sb.Append(textBox2.Text);
            }
            lbPreview.Text = sb.ToString();
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

        private void Compact_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
            Comment = Command.CommandDiscription;
        }
    }
}
