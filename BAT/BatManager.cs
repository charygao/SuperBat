using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BAT.Properties;
using BatCt;
using ICSharpCode.TextEditor;
using System.Diagnostics;
using System.IO;

namespace BAT
{
    public partial class BatManager : Form
    {
        private const string Title = "SuperBat批处理文件生成器";
        private string _documentTitle = "新建批处理文件";
        private bool _isSaved, _isRuning;
        private List<BatCommand> _allComaandList;
        Settings _currentSetting;
        Find _findWindow;
        public string currentPath = null;
        public BatManager()
        {
            InitializeComponent();
        }
        private void BatManager_Load(object sender, EventArgs e)
        {
            InitializePrograme();

            int totalnum = 0;
            String[] args = System.Environment.GetCommandLineArgs();
            foreach (string var in args)
            {
                if (totalnum == 1)
                {
                    textEditorControl1.LoadFile(var);

                    DocumentTitle = textEditorControl1.FileName.Substring(textEditorControl1.FileName.LastIndexOf('\\') + 1);
                    IsSaved = true;
                    textEditorControl1.Document.UndoStack.ClearAll();
                    撤消状态改变(null, null);

                }
                else if (totalnum > 1)
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\BAT.exe", "\"" + var + "\"");
                }
                //MessageBox.Show(currentPath);
                totalnum++;
            }
            totalnum--;

            //默认新建一个文档
            //新建文档(null, null);
        }



        private void InitializePrograme()
        {

            _currentSetting = new Settings();

            //设置高亮模式
            textEditorControl1.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("BAT");
            //textEditorControl1.Encoding = System.Text.Encoding.Default;
            textEditorControl1.Encoding = System.Text.Encoding.ASCII;
            //获取全部内部命令
            _allComaandList = BatCommand.GetInternalCommand();
            //绑定到列表显示命令清单
            FilterCommands(null, null);


            //默认显示边栏、标尺和行号
            ShowRule = bool.Parse(_currentSetting.GetConfigValue(Settings.ConfigItem.显示标尺));
            ShowLineNumber = bool.Parse(_currentSetting.GetConfigValue(Settings.ConfigItem.显示行号));
            ShowLeftBar = bool.Parse(_currentSetting.GetConfigValue(Settings.ConfigItem.显示侧边栏));
            ShowLine80 = bool.Parse(_currentSetting.GetConfigValue(Settings.ConfigItem.显示字宽符));

            Font f = new Font(_currentSetting.GetConfigValue(Settings.ConfigItem.字体),
                float.Parse(_currentSetting.GetConfigValue(Settings.ConfigItem.字号)));

            fontDialog1.Font = textEditorControl1.Font = f;



            //勾选文本发生改变事件
            textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectionChanged += 选中文本发生改变;

            //初始没有运行
            IsRuning = false;

            //有操作被加入到撤销/重做队列中时
            textEditorControl1.Document.UndoStack.OperationPushed += 撤消状态改变;

            CheckForIllegalCrossThreadCalls = false;

        }

        void 撤消状态改变(object sender, ICSharpCode.TextEditor.Undo.OperationEventArgs e)
        {
            toolStripButton3.Enabled = 重复RToolStripMenuItem.Enabled = textEditorControl1.Document.UndoStack.CanRedo;
            toolStripButton2.Enabled = 撤消UToolStripMenuItem.Enabled = textEditorControl1.Document.UndoStack.CanUndo;
        }




        /// <summary>
        /// 改变过滤条件显示命令清单
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FilterCommands(object sender, EventArgs e)
        {
            TreeNode commandsParent = treeView1.Nodes["CMD0"];
            commandsParent.Nodes.Clear();
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                foreach (BatCommand command in _allComaandList)
                {
                    CreateCmd0Node(commandsParent, command);
                }
            }
            else
            {
                //对AllComaandList进行筛选
                foreach (BatCommand command in _allComaandList)
                {
                    if (command.CommandName.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        CreateCmd0Node(commandsParent, command);
                    }
                }

            }

            TreeNode codfParent = treeView1.Nodes["CMD1"];
            codfParent.Nodes.Clear();

            DataTable dt = _currentSetting.GetCodes();
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                foreach (DataRow row in dt.Rows)
                {
                    CreateCmd1Node(codfParent, row);
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["title"].ToString().ToLower().Contains(textBox1.Text.Trim().ToLower()) || row["discription"].ToString().ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        CreateCmd1Node(codfParent, row);
                    }
                }
            }
            treeView1.ExpandAll();
            if (treeView1.SelectedNode == null)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private static void CreateCmd0Node(TreeNode commandsParent, BatCommand command)
        {
            commandsParent.Nodes.Add(new TreeNode
                                         {
                                             Text = command.CommandName,
                                             ToolTipText = command.CommandDiscription,
                                             Tag = command
                                         });
        }

        private void CreateCmd1Node(TreeNode codfParent, DataRow row)
        {
            codfParent.Nodes.Add(new TreeNode
                                     {
                                         Name = row["key"].ToString(),
                                         Text = row["title"].ToString(),
                                         ToolTipText = row["discription"].ToString(),
                                         Tag = row["codeString"],
                                         ContextMenuStrip = contextMenuStrip1
                                     });
        }



        /// <summary>
        /// 是否显示标尺
        /// </summary>
        /// <value><c>true</c> if [show rule]; otherwise, <c>false</c>.</value>
        private bool ShowRule
        {
            get
            {
                return textEditorControl1.ShowHRuler;
            }
            set
            {
                toolStripButton7.Checked = 显示标尺ToolStripMenuItem.Checked = textEditorControl1.ShowHRuler = value;
            }
        }

        /// <summary>
        /// 是否显示行号
        /// </summary>
        /// <value><c>true</c> if [show line number]; otherwise, <c>false</c>.</value>
        private bool ShowLineNumber
        {
            get
            {
                return textEditorControl1.ShowLineNumbers;
            }
            set
            {
                toolStripButton6.Checked = 显示行号ToolStripMenuItem.Checked = textEditorControl1.ShowLineNumbers = value;
            }
        }

        /// <summary>
        /// 是否显示左边栏
        /// </summary>
        /// <value><c>true</c> if [show left bar]; otherwise, <c>false</c>.</value>
        private bool ShowLeftBar
        {
            get
            {
                return !splitContainer1.Panel1Collapsed;
            }
            set
            {
                splitContainer1.Panel1Collapsed = !value;
                显示侧边栏ToolStripMenuItem.Checked = value;
            }
        }

        /// <summary>
        /// 显示每行的80字宽提示线
        /// </summary>     
        private bool ShowLine80
        {
            get
            {
                return textEditorControl1.ShowVRuler;
            }
            set
            {
                toolStripButton10.Checked = 显示80字宽提示线ToolStripMenuItem.Checked = textEditorControl1.ShowVRuler = value;
            }
        }

        /// <summary>
        /// 文档是否已经保存
        /// </summary>
        /// <value><c>true</c> if this instance is saved; otherwise, <c>false</c>.</value>
        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                _isSaved = value;
                Text = string.Format("{0}——{1}{2}", Title, DocumentTitle, value ? string.Empty : " *");
            }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>    
        public bool IsRuning
        {
            get
            {
                return _isRuning;
            }
            set
            {
                _isRuning = value;

                执行EToolStripMenuItem.Enabled = toolStripButton8.Enabled = !value;
                停止SToolStripMenuItem.Enabled = toolStripButton9.Enabled = value;
            }
        }

        /// <summary>
        /// 调试运行的命令行窗口程序进程对象，可以为空
        /// </summary>
        /// <value>The current CMD process.</value>
        public Process CurrentCmdProcess { get; set; }


        /// <summary>
        /// 文档标题
        /// </summary>
        /// <value>The document title.</value>
        private string DocumentTitle
        {
            get
            {
                return _documentTitle;
            }
            set
            {
                _documentTitle = value;
            }
        }


        private void 显示标尺(object sender, EventArgs e)
        {
            ShowRule = !ShowRule;
            _currentSetting.SetConfigValue(Settings.ConfigItem.显示标尺, ShowRule.ToString());
        }

        private void 显示行号(object sender, EventArgs e)
        {
            ShowLineNumber = !ShowLineNumber;
            _currentSetting.SetConfigValue(Settings.ConfigItem.显示行号, ShowLineNumber.ToString());
        }

        private void 显示侧边栏(object sender, EventArgs e)
        {
            ShowLeftBar = !ShowLeftBar;
            _currentSetting.SetConfigValue(Settings.ConfigItem.显示侧边栏, ShowLeftBar.ToString());
        }

        private void 显示80字宽提示线(object sender, EventArgs e)
        {
            ShowLine80 = !ShowLine80;
            _currentSetting.SetConfigValue(Settings.ConfigItem.显示字宽符, ShowLine80.ToString());
        }


        private void 新建文档(object sender, EventArgs e)
        {

            textEditorControl1.ResetText();
            textEditorControl1.Document.CommitUpdate();
            textEditorControl1.Refresh();

            DocumentTitle = "新建批处理文件";
            textEditorControl1.FileName = string.Empty;
            IsSaved = false;
            textEditorControl1.Document.UndoStack.ClearAll();
            撤消状态改变(null, null);
        }

        private void 保存文档(object sender, EventArgs e)
        {
            string savePath;
            if (string.IsNullOrEmpty(textEditorControl1.FileName))
            {
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                savePath = saveFileDialog1.FileName;
            }
            else
            {
                savePath = textEditorControl1.FileName;
            }


            textEditorControl1.SaveFile(savePath);

            DocumentTitle = textEditorControl1.FileName.Substring(textEditorControl1.FileName.LastIndexOf('\\') + 1);
            IsSaved = true;

        }

        private void 文本改变(object sender, EventArgs e)
        {
            IsSaved = false;
        }

        private void 另存(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            textEditorControl1.SaveFile(saveFileDialog1.FileName);

            DocumentTitle = textEditorControl1.FileName.Substring(textEditorControl1.FileName.LastIndexOf('\\') + 1);
            IsSaved = true;
        }

        private void 打开文档(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            textEditorControl1.LoadFile(openFileDialog1.FileName);

            DocumentTitle = textEditorControl1.FileName.Substring(textEditorControl1.FileName.LastIndexOf('\\') + 1);
            IsSaved = true;
            textEditorControl1.Document.UndoStack.ClearAll();
            撤消状态改变(null, null);
        }

        private void 退出程序(object sender, EventArgs e)
        {
            //如果没有保存，提示之
            if (!IsSaved)
            {
                DialogResult result = MessageBox.Show(Resources.BatManager_退出程序_文档已修改_你要保存吗_, Resources.BatManager_退出程序_保存, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    保存文档(null, null);
                }
            }

            _currentSetting.SaveConfigs();

            Application.Exit();
        }

        private void 关闭窗体(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                退出程序(null, null);
            }
        }

        private void 复制(object sender, EventArgs e)
        {
            Clipboard.SetText(textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText);
        }


        private void 选中文本发生改变(object sender, EventArgs e)
        {
            加入个人搜藏LToolStripMenuItem.Enabled =
                加入个人搜藏ToolStripMenuItem.Enabled =
            toolStripMenuItem4.Enabled =
                toolStripMenuItem5.Enabled =
                剪切TToolStripMenuItem.Enabled =
               剪切UToolStripButton.Enabled =
               复制CToolStripButton.Enabled =                          //是否可以复制、剪切
               复制CToolStripMenuItem.Enabled = textEditorControl1.ActiveTextAreaControl.SelectionManager.HasSomethingSelected;
        }

        private void 粘贴(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
            {
                return;
            }
            //获取剪切板文本
            string str = Clipboard.GetText();

            InsertText(str);
            //光标移动到插入点后面
            textEditorControl1.ActiveTextAreaControl.Caret.Position = textEditorControl1.Document.OffsetToPosition(textEditorControl1.ActiveTextAreaControl.Caret.Offset + str.Length);


        }

        private void InsertText(string str)
        {
            //判断是否有选择文本，有则删除之
            if (textEditorControl1.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            }

            //插入光标位置
            textEditorControl1.Document.Insert(textEditorControl1.Document.PositionToOffset(textEditorControl1.ActiveTextAreaControl.Caret.Position), str);


            textEditorControl1.Refresh();
        }

        private void 剪切(object sender, EventArgs e)
        {
            Clipboard.SetText(textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText);
            textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            textEditorControl1.Refresh();
        }

        private void 全选(object sender, EventArgs e)
        {
            TextLocation start = new TextLocation(0, 0);
            TextLocation end = textEditorControl1.Document.OffsetToPosition(textEditorControl1.Document.TextLength);
            textEditorControl1.ActiveTextAreaControl.SelectionManager.SetSelection(start, end);
        }

        private void 注释代码(object sender, EventArgs e)
        {
            //判断是选择了多行，还是一行都没选
            bool mutiLine = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r\n");

            if (mutiLine)
            {
                TextLocation start = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition;
                TextLocation end = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].EndPosition;

                for (int i = start.Line; i < end.Line; i++)
                {
                    //找到行位置所在行第一位
                    TextLocation local = new TextLocation(0, i);
                    //插入注释符号
                    textEditorControl1.Document.Insert(textEditorControl1.Document.PositionToOffset(local), "::");
                }
            }
            else
            {
                //找到光标位置所在行第一位
                TextLocation local = new TextLocation(0, textEditorControl1.ActiveTextAreaControl.Caret.Line);
                //插入注释符号
                textEditorControl1.Document.Insert(textEditorControl1.Document.PositionToOffset(local), "::");
            }

        }

        private void 解除注释(object sender, EventArgs e)
        {
            //判断是选择了多行，还是一行都没选
            bool mutiLine = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r\n");

            if (mutiLine)
            {
                TextLocation start = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition;
                TextLocation end = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].EndPosition;
                for (int j = start.Line; j < end.Line; j++)
                {
                    RemoveComm(j);
                }
            }
            else
            {
                RemoveComm(textEditorControl1.ActiveTextAreaControl.Caret.Line);
            }


        }

        /// <summary>
        /// 按指定的行索引号移除注释符
        /// </summary>
        /// <param name="line">The line.</param>
        private void RemoveComm(int line)
        {
            //按Word集合获取一行，对其遍历，寻找“::”或者"Ram"
            for (int i = 0; i < textEditorControl1.Document.GetLineSegment(line).Words.Count; i++)
            {
                string temp = textEditorControl1.Document.GetLineSegment(line).Words[i].Word;
                if (temp == "::" || temp.ToLower() == "ram")
                {
                    TextLocation local = new TextLocation(
                        textEditorControl1.Document.GetLineSegment(line).Words[i].Offset,
                        line);

                    textEditorControl1.Document.Remove(textEditorControl1.Document.PositionToOffset(local), temp.Length);

                    break;
                }
                //行内的注释符不算
                if (!(temp == string.Empty || temp == "\t" || temp == " "))
                {
                    break;
                }
            }
        }

        private void 启动空cmd窗口(object sender, EventArgs e)
        {
            Process.Start("cmd");
        }


        private void 运行(object sender, EventArgs e)
        {
            //判断其保存状态，保存后运行
            if (!IsSaved)
            {
                string savePath;
                if (string.IsNullOrEmpty(textEditorControl1.FileName))
                {
                    DialogResult result = saveFileDialog1.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                    savePath = saveFileDialog1.FileName;
                }
                else
                {
                    savePath = textEditorControl1.FileName;
                }

                textEditorControl1.SaveFile(savePath);
                DocumentTitle = textEditorControl1.FileName.Substring(textEditorControl1.FileName.LastIndexOf('\\') + 1);
                IsSaved = true;
            }

            //运行保存过的文档
            IsRuning = true;

            CurrentCmdProcess = new Process
                                    {
                                        StartInfo = { FileName = textEditorControl1.FileName },
                                        EnableRaisingEvents = true
                                    };
            CurrentCmdProcess.Exited += 调试运行结束;
            CurrentCmdProcess.Start();

        }



        void 调试运行结束(object sender, EventArgs e)
        {
            CurrentCmdProcess = null;
            IsRuning = false;
        }

        private void 停止(object sender, EventArgs e)
        {
            if (CurrentCmdProcess == null)
            {
                return;
            }

            CurrentCmdProcess.Kill();
            IsRuning = false;
        }

        private void 字体设置(object sender, EventArgs e)
        {
            fontDialog1.Font = textEditorControl1.Font;
            DialogResult result = fontDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            textEditorControl1.Font = fontDialog1.Font;

            _currentSetting.SetConfigValue(Settings.ConfigItem.字号, textEditorControl1.Font.Size.ToString());
            _currentSetting.SetConfigValue(Settings.ConfigItem.字体, textEditorControl1.Font.Name);
        }

        private void 撤销(object sender, EventArgs e)
        {
            textEditorControl1.Document.UndoStack.Undo();
            textEditorControl1.Refresh();
            撤消状态改变(null, null);
        }

        private void 重做(object sender, EventArgs e)
        {
            textEditorControl1.Document.UndoStack.Redo();
            textEditorControl1.Refresh();
            撤消状态改变(null, null);
        }

        private void 插入命令行(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Level != 1)//排除根目录
            {
                return;
            }
            string line;
            //内置命令
            if (e.Node.Tag is BatCommand)
            {
                var setter = ((BatCommand)e.Node.Tag).Setter;
                DialogResult result = setter.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                line = setter.GetCommandLine();

            }
            else //个人收藏
            {
                line = e.Node.Tag.ToString();
            }

            //在插入点插入新的命令行

            InsertText(line);

            textEditorControl1.Focus();

            //光标移动到插入点后面
            textEditorControl1.ActiveTextAreaControl.Caret.Position = textEditorControl1.Document.OffsetToPosition(textEditorControl1.ActiveTextAreaControl.Caret.Offset + line.Length);

        }

        private void 加入个人搜藏(object sender, EventArgs e)
        {
            AddToList atl = new AddToList
                                {
                                    tb_code = { Text = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText }
                                };

            DialogResult result = atl.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            CreateCmd1Node(treeView1.Nodes["CMD1"], _currentSetting.AddCode(atl.tb_title.Text, atl.tb_discription.Text, atl.tb_code.Text));

        }

        private void 查找(object sender, EventArgs e)
        {
            if (_findWindow == null || _findWindow.IsDisposed)
            {
                _findWindow = new Find(textEditorControl1);
            }
            _findWindow.Show();
            _findWindow.Activate();
        }

        private void TreeView1NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Name == null)
            {
                return;
            }

            _currentSetting.RemoveCode(new Guid(treeView1.SelectedNode.Name));

            treeView1.SelectedNode.Remove();
        }

        private void 关于aToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }



  
        private void topmosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            显示侧边栏(sender, e);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //获取拖放的文件地址
                string StrFileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                //将文件内容放入RichTextBox中
                string[] MyPathArray = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string var in MyPathArray)
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\BAT.exe", "\"" + var + "\"");
                }
            }
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
            this.TopMost = true;
        }
    }
}