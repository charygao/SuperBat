using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Color : BatCt.CommandSetter
    {
        public Color(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void Color_Load(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
            Comment = "改变命令行窗口的颜色";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            sb.Append(comboBox2.Text);
            sb.Append(comboBox1.Text);

            lbPreview.Text = sb.ToString();
        }
    }
}
