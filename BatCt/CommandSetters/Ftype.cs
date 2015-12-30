using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Ftype : BatCt.CommandSetter
    {
        public Ftype(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            radioButton1_CheckedChanged(null, null);
            Comment = command.CommandDiscription;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            if (radioButton2.Checked)
            {
                sb.Append(textBox1.Text);
            }

            lbPreview.Text = sb.ToString();
        }
    }
}
