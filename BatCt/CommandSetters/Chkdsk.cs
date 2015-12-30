using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Chkdsk : BatCt.CommandSetter
    {
        public Chkdsk(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(textBox1.Text);
            sb.Append(" ");

            //查错
            if (checkBox2.Checked)
            {
                sb.Append("/F ");
            }

            if (checkBox3.Checked)
            {
                sb.Append("/V ");
            }
            if (checkBox4.Checked)
            {
                sb.Append("/R ");
            } 
            if (checkBox5.Checked)
            {
                sb.Append("/L:");
                sb.Append(numericUpDown1.Text);
                sb.Append(" ");
            }

            if (checkBox6.Checked)
            {
                sb.Append("/X ");
            }
            if (checkBox7.Checked)
            {
                sb.Append("/I ");
            }
            if (checkBox8.Checked)
            {
                sb.Append("/C");
            }

            lbPreview.Text = sb.ToString();
        }

        private void Chkdsk_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
            Comment = "在磁盘上检查错误";
        }
    }
}
