using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Date : BatCt.CommandSetter
    {
        public Date(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);


            if (radioButton1.Checked)
            {
                Comment = "显示当前日期";
            }
            else
            {
                Comment = "修改当前日期";
                sb.Append(" ");
                sb.Append(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            }



            lbPreview.Text = sb.ToString();
        }

        private void Date_Load(object sender, EventArgs e)
        {
            radioButton1_CheckedChanged(null, null);
        }
    }
}
