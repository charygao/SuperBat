using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Shift : BatCt.CommandSetter
    {
        public Shift(BatCommand command)
            : base(command)
        {
            InitializeComponent();
            numericUpDown1_ValueChanged(null, null);
            Comment = command.CommandDiscription;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");


            sb.Append(numericUpDown1.Value);



            lbPreview.Text = sb.ToString();
        }
    }
}
