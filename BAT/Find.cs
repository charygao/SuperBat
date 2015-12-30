using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.Text.RegularExpressions;

namespace BAT
{
    public partial class Find : Form
    {
        TextEditorControl textbox;
        public Find(TextEditorControl tbox)
        {
            InitializeComponent();
            textbox = tbox;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.Height == panel1.Top + 20)
            {
                this.Height += 400;
            }
            else
            {
                this.Height = panel1.Top + 20;
            }
        }

        private void Find_Load(object sender, EventArgs e)
        {
            this.Height = panel1.Top + 20;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 查找(object sender, EventArgs e)
        {
            TextLocation start;
            ////如果有选择，则取消选择，让光标后移
            //if (textbox.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            //{
            //    //选中区域长度
            //    int length = textbox.ActiveTextAreaControl.SelectionManager.SelectedText.Length;
            //    start = textbox.ActiveTextAreaControl.Caret.Position;
            //    int offSet2 = textbox.Document.PositionToOffset(start) + length;

            //    textbox.ActiveTextAreaControl.SelectionManager.ClearSelection();
            //    textbox.ActiveTextAreaControl.Caret.Position = textbox.Document.OffsetToPosition(offSet2);

               

            //}
            //获取光标位置之后的文本作为查询条件
            start = textbox.ActiveTextAreaControl.Caret.Position;
            int offSet = textbox.Document.PositionToOffset(start);
            string target = textbox.Document.GetText(offSet, textbox.Text.Length - offSet);

            Regex r = new Regex(textBox1.Text);
            var result = r.Matches(target);

            if (result.Count == 0)
            {
                MessageBox.Show("没有找到符合条件的结果，您或许应该把光标移动到开始位置重试一次！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //跳转到
                textbox.ActiveTextAreaControl.JumpTo(0, 0);
                return;
            }

            //定位到第一个查询结果
            var showTarget = result[0];
            //取得相对于文件顶端的偏移位置
            offSet += showTarget.Index;
            //转换偏移为文本起始位
            start = textbox.Document.OffsetToPosition(offSet);

            //计算结束位置
            TextLocation end = textbox.Document.OffsetToPosition(offSet + showTarget.Length);

            //跳转到
            textbox.ActiveTextAreaControl.JumpTo(end.Line, end.Column);
            //选中指定文本
            textbox.ActiveTextAreaControl.SelectionManager.SetSelection(start, end);

            //定位滚动条到合理位置
            //textbox.ActiveTextAreaControl.CenterViewOn(start.Line, -1);
            

        }
    }
}
