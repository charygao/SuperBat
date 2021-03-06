﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class DiskCopy : BatCt.CommandSetter
    {
        public DiskCopy(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(maskedTextBox1.Text);

            sb.Append(" ");
            sb.Append(maskedTextBox2.Text);

            if (checkBox2.Checked)
            {
                sb.Append("/V");
            }
            lbPreview.Text = sb.ToString();
        }

        private void DiskCopy_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;

            maskedTextBox1_TextChanged(null, null);
        }
    }
}
