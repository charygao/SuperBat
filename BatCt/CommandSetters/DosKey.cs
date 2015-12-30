using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class DosKey : BatCt.CommandSetter
    {
        public DosKey(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(checkBox2.Checked ? "/REINSTALL " : "");
            sb.Append(checkBox3.Checked ? string.Format("/LISTSIZE={0} ", numericUpDown1.Value) : "");

            if (checkBox4.Checked)
            {
                sb.Append("/MACROS");
                if (checkBox5.Checked)
                {
                    sb.Append(":ALL ");
                }
                else if (checkBox6.Checked)
                {
                    sb.Append(":exename ");
                }
                else
                {
                    sb.Append(" ");
                }
            }
            sb.Append(checkBox7.Checked ? "/HISTORY " : "");

            if (checkBox8.Checked)
            {
                sb.Append(checkBox9.Checked ? "/INSERT " : "/OVERSTRIKE ");
            }

            sb.Append(checkBox11.Checked ? string.Format("/EXENAME={0} ", textBox2.Text) : "");
            sb.Append(checkBox12.Checked ? string.Format("/MACROFILE={0} ", textBox3.Text) : "");
            sb.Append(checkBox13.Checked ? string.Format("acroname={0} ", textBox4.Text) : "");

            lbPreview.Text = sb.ToString();
        }

        private void DosKey_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
            textBox1_TextChanged(null, null);
        }
    }
}
