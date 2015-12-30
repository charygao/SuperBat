using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Rd : BatCt.CommandSetter
    {
       public Rd(BatCommand command)
            : base(command)
        {
            InitializeComponent();

            checkBox2_CheckedChanged(null, null);
            Comment = command.CommandDiscription;
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
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/S " : "");
            sb.Append(checkBox3.Checked?"/Q ":"");

            sb.Append(textBox1.Text);
            


            lbPreview.Text = sb.ToString();
        }
    }
}
