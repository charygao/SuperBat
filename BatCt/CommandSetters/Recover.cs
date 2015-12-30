using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Recover : BatCt.CommandSetter
    {
        public Recover(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            textBox1_TextChanged(null, null);
            Comment = command.CommandDiscription;
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
