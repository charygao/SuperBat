using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Chcp : BatCt.CommandSetter
    {
        DataSet pageCodes = new DataSet();

        public Chcp(BatCommand command)
            : base(command)
        {
            InitializeComponent();

            pageCodes.Tables.Add("code");
            pageCodes.Tables["code"].Columns.Add("key", typeof(int));
            pageCodes.Tables["code"].Columns.Add("showText", typeof(string));
        }

        private void Chcp_Load(object sender, EventArgs e)
        {
            radioButton2_CheckedChanged(null, null);

            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding temp = ei.GetEncoding();
                DataRow newRow = pageCodes.Tables["code"].NewRow();
                newRow["key"] = temp.CodePage;
                newRow["showText"] = string.Format("标准页代码{0,-6}{1,-20}(windows页代码{2})", temp.CodePage, ei.Name, temp.WindowsCodePage);
                pageCodes.Tables["code"].Rows.Add(newRow);
            }

            comboBox1.DataSource = pageCodes.Tables["code"];
            comboBox1.ValueMember = "key";
            comboBox1.DisplayMember = "showText";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = radioButton2.Checked;

            StringBuilder sb = new StringBuilder();
            sb.Append(Command.CommandName);
            sb.Append(" ");

            if (radioButton1.Checked)
            {
                Comment = "显示当前系统使用的页代码";
            }

            if (radioButton2.Checked)
            {
                Comment = string.Format("将当前系统使用的页代码修改为{0}", comboBox1.Text);

                sb.Append(comboBox1.SelectedValue.ToString());
            }


            lbPreview.Text = sb.ToString();
        }

    }
}
