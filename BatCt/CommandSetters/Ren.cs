using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Ren : BatCt.CommandSetter
    {
        public Ren(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            textBox1_TextChanged(null, null);
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
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textBox2.Text = openFileDialog1.FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

        
            sb.Append(textBox1.Text);

            sb.Append(" ");


            sb.Append(textBox2.Text);

            lbPreview.Text = sb.ToString();
        }
    }
}
