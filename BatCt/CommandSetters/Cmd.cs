using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Cmd : BatCt.CommandSetter
    {
        public Cmd(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox8.Enabled = checkBox9.Enabled = checkBox7.Checked;
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(radioButton1.Checked ? "/A " : "/U ");

            sb.Append(checkBox2.Checked ? "/Q " : "");

            sb.Append(checkBox3.Checked ? "/D " : "");

            sb.Append(checkBox4.Checked ? "/E:ON " : "/E:OFF ");

            sb.Append(checkBox5.Checked ? "/F:ON " : "/F:OFF ");

            sb.Append(checkBox6.Checked ? "/V:ON " : "/V:OFF ");

            //执行命令
            if (checkBox7.Checked)
            {
                sb.Append(checkBox9.Checked ? "/S " : "");
                sb.Append(checkBox8.Checked ? "/C " : "/K ");
                sb.Append(textBox1.Text);
            }

            lbPreview.Text = sb.ToString();

        }

        private void Cmd_Load(object sender, EventArgs e)
        {
            checkBox8_CheckedChanged(null, null);

            Comment = "新建一个新的CMD窗口";
        }
    }
}
