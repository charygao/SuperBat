using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BatCt.CommandSetters;

namespace BatCt
{

    /// <summary>
    /// 命令对象
    /// </summary>
    public class BatCommand
    {

        public BatCommand(string name)
        {
            this.CommandName = name;
        }


        /// <summary>
        /// 是否内部命令
        /// </summary>
        public bool IsInternal
        {
            get;
            set;
        }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName
        {
            get;
            private set;
        }

        /// <summary>
        /// 命令的功能描述
        /// </summary>
        public string CommandDiscription
        {
            get;
            set;
        }


        private CommandSetter _setter;
        /// <summary>
        /// 命令配置窗口
        /// </summary>
        public CommandSetter Setter
        {
            get
            {
                if (_setter == null)
                {
                    //定义同义词，以处理形如“Cd”和“ChDir”这样的情况
                    string CommandNameW = CommandName;
                    string[][] words = new string[][] 
                    { 
                        new string[]{"CD","CHDIR"},
                        new string[]{"BREAK","CLS"},
                        new string[]{"DEL","ERASE"},
                        new string[]{"BREAK","ENDLOCAL"},                        
                        new string[]{"BOOTCFG","FSUTIL"},
                        new string[]{"BREAK","GOTO"}, 
                        new string[]{"BREAK","HELP"},
                        new string[]{"FOR","IF"},
                        new string[]{"MD","MKDIR"},
                        new string[]{"FOR","MODE"},
                        new string[]{"BOOTCFG","OPENFILES"},
                        new string[]{"BREAK","PAUSE"},
                        new string[]{"BREAK","POPD"},
                        new string[]{"BREAK","REM"},
                        new string[]{"REN","RENAME"},
                        new string[]{"RD","RMDIR"} ,
                        new string[]{"BOOTCFG","SETLOCAL"},
                        new string[]{"FOR","SHUTDOWN"},
                        new string[]{"FOR","SORT"},
                        new string[]{"FOR","START"},
                        new string[]{"FOR","SYSTEMINFO"},
                        new string[]{"FOR","TASKLIST"},
                        new string[]{"FOR","TASKKILL"},
                    };


                    foreach (string[] item in words)
                    {
                        if (CommandName.Equals(item[1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            CommandNameW = item[0];
                        }
                    }


                    //根据命令，通过反射获取相应的对话窗
                    Type t = Type.GetType("BatCt.CommandSetters." + CommandNameW, false, true);

                    //没有与命令相符的窗体时使用默认的DefaultCommandSetter
                    if (t == null)
                    {
                        t = typeof(DefaultCommandSetter);
                    }
                    object dialog = Activator.CreateInstance(t, new object[] { this });

                    _setter = (CommandSetter)dialog;
                }
                return _setter;
            }
        }


        /// <summary>
        /// 用法示例
        /// </summary>
        public string Usege
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已经加载描述和示例等数据
        /// </summary>
        public bool IsLoaded
        {
            get;
            set;
        }
        /// <summary>
        /// 获取内部命令
        /// </summary>
        public static List<BatCommand> GetInternalCommand()
        {
            // 实例一个Process类,启动一个独立进程
            Process p = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",// 设定程序名   
                    Arguments = "/A /c help",
                    UseShellExecute = false,// 关闭Shell的使用
                    RedirectStandardInput = false,// 重定向标准输入
                    RedirectStandardOutput = true,// 重定向标准输出
                    RedirectStandardError = false,  //重定向错误输出
                    CreateNoWindow = true// 设置不显示窗口
                }
            };
            p.Start();
            // 从输出流获取命令执行结果
            string result = p.StandardOutput.ReadToEnd();
            p.Close();


            string[] tmp = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            List<BatCommand> returnValue = new List<BatCommand>();
            int index = 0;

            //剔除第一行和最后3行
            for (int i = 1; i < tmp.Length - 3; i++)
            {
                string[] str = tmp[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (string.IsNullOrEmpty(str[0]))
                {
                    continue;
                }


                switch (str.Length)
                {
                    case 1:
                        returnValue[index - 1].CommandDiscription += str[0];
                        break;
                    case 2:
                        BatCommand bc = new BatCommand(str[0])
                        {
                            IsInternal = true,
                            Usege = null,
                            IsLoaded = false,
                            CommandDiscription = str[1]
                        };
                        returnValue.Add(bc);
                        index++;
                        break;
                    default:
                        StringBuilder sb = new StringBuilder();
                        for (int j = 1; j < str.Length; j++)
                        {
                            sb.Append(str[j]);
                        }
                        BatCommand bc2 = new BatCommand(str[0])
                        {
                            IsInternal = true,
                            Usege = null,
                            IsLoaded = false,
                            CommandDiscription = sb.ToString()
                        };
                        returnValue.Add(bc2);
                        index++;
                        break;

                }

            }

            return returnValue;
        }

        /// <summary>
        /// 获取外部命令
        /// </summary>
        public static List<BatCommand> GetExternalCommand()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取命令用法介绍信息
        /// </summary>
        public void Load()
        {
            //不获取信息的排除字符串
            List<string> exceptString = new List<string>
                (new string[] 
                        { 
                            "wmic", 
                            "eventquery",
                            "pagefileconfig",
                            "sc",
                            "schtasks"
                        });

            const string exceptMessage = "【本程序暂无法显示该命令的用法提示 请自行运行命令行获取该命令的帮助】";

            if (IsInternal)
            {
                if (exceptString.Contains(CommandName.ToLower()))
                {
                    Usege = exceptMessage;
                    return;
                }
                #region 内部命令用help获取
                Process p = new Process
                      {
                          StartInfo =
                          {
                              FileName = "cmd.exe",// 设定程序名
                              Arguments = String.Format("/A /c help {0}", CommandName),
                              UseShellExecute = false,// 关闭Shell的使用
                              RedirectStandardInput = true,// 重定向标准输入
                              RedirectStandardOutput = true,// 重定向标准输出
                              RedirectStandardError = true,  //重定向错误输出
                              CreateNoWindow = true// 设置不显示窗口
                          }
                      };
                p.Start();

                p.StandardInput.WriteLine("\r\n");
                string result = p.StandardOutput.ReadToEnd();

                p.Close();

                string[] tmp = result.Split(new[] { "\r\n" }, StringSplitOptions.None);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    sb.Append(tmp[i]);
                    sb.Append("\r\n");
                }

                IsLoaded = true;
                Usege = sb.ToString();
                #endregion
            }
            else
            {
                //外部命令用  /? 参数获取
            }
        }
    }
}
