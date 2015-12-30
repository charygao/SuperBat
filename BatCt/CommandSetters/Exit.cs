using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Exit : BatCt.CommandSetter
    {
        public Exit(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);


            sb.Append(radioButton1.Checked ? " " : "/B ");

            sb.Append(checkBox2.Checked ? numericUpDown1.Value.ToString() : "");

            lbPreview.Text = sb.ToString();
        }

        private void Exit_Load(object sender, EventArgs e)
        {
            Comment = "退出执行";
            radioButton1_CheckedChanged(null, null);
        }
    }
}
