using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Label : BatCt.CommandSetter
    {
        public Label(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            Comment = Command.CommandDiscription;
            maskedTextBox1_TextChanged(null, null);
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(maskedTextBox1.Text);
            sb.Append(" ");

            sb.Append(textBox1.Text);

            lbPreview.Text = sb.ToString();
        }
    }
}
