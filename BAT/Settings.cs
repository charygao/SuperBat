using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using System.IO;
using System.Windows.Forms;

namespace BAT
{
    class Settings
    {
        DataSet settingStore = new DataSet();
        string savePath = Application.StartupPath + "//config.xml";

        public Settings()
        {
            //存放配置的表
            settingStore.Tables.Add("setting", "Michaelyu");
            settingStore.Tables["setting"].Columns.Add("key", typeof(string));
            settingStore.Tables["setting"].Columns.Add("value", typeof(string));


            //存放个人记录的表
            settingStore.Tables.Add("code", "Michaelyu");
            settingStore.Tables["code"].Columns.Add("key", typeof(Guid));
            settingStore.Tables["code"].Columns.Add("title", typeof(string));
            settingStore.Tables["code"].Columns.Add("discription", typeof(string));
            settingStore.Tables["code"].Columns.Add("codeString", typeof(string));


            //判断是否存在配置文件，不存在则建一个
            if (!File.Exists(savePath))
            {
                SetDefault();
            }
            //加载数据
            settingStore.ReadXml(savePath);
        }

        /// <summary>
        /// 全部设为默认配置
        /// </summary>
        public void SetDefault()
        {
            string[][] values = new string[][] 
            {
                new string[]{"显示侧边栏","true"},
                new string[]{"显示行号","true"},
                new string[]{"显示字宽符","true"},
                new string[]{"显示标尺","true"},
                new string[]{"字体","宋体"},
                new string[]{"字号","9"}
            };


            foreach (string[] item in values)
            {
                DataRow newRow = settingStore.Tables["setting"].NewRow();
                newRow["key"] = item[0];
                newRow["value"] = item[1];
                settingStore.Tables["setting"].Rows.Add(newRow);
            }
            settingStore.WriteXml(savePath);
        }


        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetConfigValue(ConfigItem key)
        {
            foreach (DataRow row in settingStore.Tables["setting"].Rows)
            {
                if (row["key"].ToString() == key.ToString())
                {
                    return row["value"].ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 修改配置的值
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetConfigValue(ConfigItem key, string value)
        {
            foreach (DataRow row in settingStore.Tables["setting"].Rows)
            {
                if (row["key"].ToString() == key.ToString())
                {
                    row["value"] = value;
                    return;
                }
            }

        }


        /// <summary>
        /// 添加代码收藏
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="discription">The discription.</param>
        /// <param name="code">The code.</param>
        public DataRow AddCode(string title, string discription, string code)
        {
            DataRow newRow = settingStore.Tables["code"].NewRow();
            newRow["key"] = Guid.NewGuid();
            newRow["title"] = title;
            newRow["discription"] = discription;
            newRow["codeString"] = code;
            settingStore.Tables["code"].Rows.Add(newRow);

            return newRow;

            //settingStore.WriteXml(savePath);
        }

        /// <summary>
        /// 删除一条代码收藏
        /// </summary>
        /// <param name="id">The id.</param>
        public void RemoveCode(Guid id)
        {
            foreach (DataRow row in settingStore.Tables["code"].Rows)
            {
                if ((Guid)row["key"] == id)
                {
                    settingStore.Tables["code"].Rows.Remove(row);
                    return;
                }
            }
        }

        /// <summary>
        /// 获取全部收藏的代码段
        /// </summary>
        /// <returns></returns>
        public DataTable GetCodes()
        {
            return settingStore.Tables["code"];
        }

        /// <summary>
        /// 保存配置的修改
        /// </summary>
        public void SaveConfigs()
        {
            settingStore.WriteXml(savePath);
        }

        public enum ConfigItem
        {
            显示侧边栏,
            显示行号,
            显示字宽符,
            显示标尺,
            字体,
            字号
        }
    }
}
