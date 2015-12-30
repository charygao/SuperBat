using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Driverquery : BatCt.CommandSetter
    {
        public Driverquery(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void Driverquery_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "TABLE";
            checkBox2_CheckedChanged(null, null);
            Comment = Command.CommandDiscription;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = checkBox2.Checked;
            comboBox1.Enabled = checkBox6.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            //远程
            if (checkBox2.Checked)
            {
                sb.Append("/S \"");
                sb.Append(textBox1.Text);

                sb.Append("\" /U \"");
                sb.Append(textBox2.Text);

                sb.Append("\" /P \"");
                sb.Append(textBox3.Text);
                sb.Append("\" ");
            }

            if (checkBox6.Checked)
            {
                sb.Append("/FO ");
                sb.Append(comboBox1.Text);
                sb.Append(" ");
            }

            sb.Append(checkBox3.Checked ? "/NH " : "");
            sb.Append(checkBox4.Checked ? "/SI " : "");
            sb.Append(checkBox5.Checked ? "/V " : "");


            lbPreview.Text = sb.ToString();
        }
    }
}
