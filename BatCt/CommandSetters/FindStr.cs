using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class FindStr : BatCt.CommandSetter
    {
        public FindStr(BatCommand command)
            : base(command)
        {
            InitializeComponent();

            checkBox19_CheckedChanged(null, null);
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

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox14.Checked;
            textBox4.Enabled = checkBox15.Checked;
            textBox5.Enabled = checkBox16.Checked;
            textBox6.Enabled = checkBox17.Checked;
            textBox7.Enabled = checkBox18.Checked;
            textBox8.Enabled = checkBox19.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/B " : "");
            sb.Append(checkBox3.Checked ? "/E " : "");
            sb.Append(checkBox4.Checked ? "/L " : "");
            sb.Append(checkBox5.Checked ? "/R " : "");
            sb.Append(checkBox6.Checked ? "/S " : "");
            sb.Append(checkBox7.Checked ? "/I " : "");
            sb.Append(checkBox8.Checked ? "/X " : "");
            sb.Append(checkBox9.Checked ? "/V " : "");
            sb.Append(checkBox10.Checked ? "/N " : "");
            sb.Append(checkBox11.Checked ? "/M " : "");
            sb.Append(checkBox12.Checked ? "/O " : "");
            sb.Append(checkBox13.Checked ? "/P " : "");

            sb.Append(checkBox14.Checked ? string.Format("/OFF {0} ", textBox3.Text) : "");
            sb.Append(checkBox15.Checked ? string.Format("/A: {0} ", textBox4.Text) : "");
            sb.Append(checkBox16.Checked ? string.Format("/F: {0} ", textBox5.Text) : "");
            sb.Append(checkBox17.Checked ? string.Format("/C: {0} ", textBox6.Text) : "");
            sb.Append(checkBox18.Checked ? string.Format("/G: {0} ", textBox7.Text) : "");
            sb.Append(checkBox19.Checked ? string.Format("/D: {0} ", textBox8.Text) : "");


            sb.Append(" \"");
            sb.Append(textBox2.Text);
            sb.Append("\" ");
            sb.Append(textBox1.Text);


            lbPreview.Text = sb.ToString();
        }
    }
}
