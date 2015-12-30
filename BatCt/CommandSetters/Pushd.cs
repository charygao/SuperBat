using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Pushd : BatCt.CommandSetter
    {
        public Pushd(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            textBox1_TextChanged(null, null);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(textBox1.Text);
           
            lbPreview.Text = sb.ToString();
        }
    }
}
