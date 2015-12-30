using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Convert : BatCt.CommandSetter
    {
        public Convert(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(maskedTextBox1.Text);
            sb.Append(" /FS:NTFS ");

            sb.Append(checkBox2.Checked ? "/V " : "");
            sb.Append(checkBox3.Checked ? string.Format("/VCvtArea:{0} ", textBox1.Text) : "");
            sb.Append(checkBox4.Checked ? "/NoSecurity  " : "");
            sb.Append(checkBox5.Checked ? "/X " : "");

            lbPreview.Text = sb.ToString();
        }

        private void textBox1_TextChanged(object sender, MaskInputRejectedEventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void Convert_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
            Comment = Command.CommandDiscription;
        }
    }
}
