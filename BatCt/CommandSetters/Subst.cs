using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Subst : BatCt.CommandSetter
    {
        public Subst(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            radioButton3_CheckedChanged(null, null);

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            if (radioButton2.Checked)
            {  Comment = radioButton2.Text;
                sb.Append("/D ");
                sb.Append(maskedTextBox1.Text);
            }
            else if (radioButton1.Checked)
            {
                  Comment = radioButton1.Text;
                sb.Append(maskedTextBox2.Text);
                sb.Append(" ");
                sb.Append(textBox1.Text);

            }
            else
            {
                Comment = radioButton3.Text;
            }

            lbPreview.Text = sb.ToString();
        }
    }
}
