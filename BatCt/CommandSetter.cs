using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BatCt
{
    public partial class CommandSetter : Form
    {
        //todo 完成后删除之，该构造函数仅供开发时使用(删除后所有派生类将不能使用设计器)
        private CommandSetter()
        { InitializeComponent(); }


        BatCommand _command;
        string _comment;

        /// <summary>
        /// 配置的代码注释文本
        /// </summary>
        /// <value>The comment.</value>
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        /// <summary>
        /// 该对话窗用来配置的命令行命令对象
        /// </summary>    
        public BatCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        /// <summary>
        /// 构造一个命令行配置器对话窗
        /// </summary>
        /// <param name="command">传入的命令</param>
        public CommandSetter(BatCommand command)
        {
            InitializeComponent();
            _command = command;
            this.Text += command.CommandName;
            this.lbPreview.Text = this.lbCommandName.Text = command.CommandName;
            this.lbCommandDisctiption.Text = command.CommandDiscription;

            if (!command.IsLoaded)
            {
                command.Load();
            }
            this.tbCommandUseage.Text = command.Usege;

        }

        /// <summary>
        /// 获取命令行文本
        /// </summary>
        /// <returns></returns>
        public virtual string GetCommandLine()
        {
            StringBuilder sb = new StringBuilder();
           // sb.Append("\r\n");
            if (checkBox1.Checked)
            {
                sb.Append("::");
                sb.Append(Comment);
                sb.Append("\r\n");
            }
            sb.Append(lbPreview.Text);
            //sb.Append("\r\n");
            return sb.ToString();
        }

        private void btnTry_Click(object sender, EventArgs e)
        {

            Process p = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = " /c \" " + lbPreview.Text + " \"",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            p.Start();

            string s = p.StandardOutput.ReadToEnd();

            p.StandardInput.WriteLine("exit");
            p.Close();


            Priview pv = new Priview();
            pv.SetText(s);
            pv.ShowDialog();
        }


    }
}
