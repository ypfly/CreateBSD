using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using PlugInSample_AutoBSD;
using System.Net;
using System.Threading;
using System.IO;
using Stimulsoft.Report;



/* OrBit的C#插件示例
 * 采用VS-2008-SP1编写
 * 版本号V10.20
 * 本插件适用于OrBit-Browser V10.20　或以上的版本
 * 提供了各种必要的接口(属性、函数(方法\过程))，开发者可以在此基础上进行改编或完善
 * 由:深圳OrBit Systems Inc. OrBit Team提供
 * 发布日期 2009年1月1日
 * 最后修改 2009年11月15日
 */

namespace PlugInSample_AutoBSD
{
    public partial class UserControl1 : UserControl
    {  //类初始化

        #region ////以下定义OrBit插件与浏览器进行交互的接口变量

        //基本接口变量
        public string PlugInCommand;  //插件的命令ID
        public string PlugInName; //插件的名字(能随语言ID改变)
        public string LanguageId; //语言ID(0-英,1-简,2-繁...8)
        public string ParameterString; //参数字串
        public string RightString; //权限字串(1-Z)
        public string OrBitUserId; //OrBit用户ID
        public string OrBitUserName; //用户名
        public string ApplicationName; //应用系统
        public string ResourceId; //资源(电脑)ID
        public string ResourceName; //资源名
        public bool IsExitQuery; //在插件窗体退出是询问是否要退出，用于提醒数据状态已改变。
        public string UserTicket; //经浏览器认证后的加密票，调用某些WCF服务时需要使用

        //单独调试数据库应用时需用到参数
        public string DatabaseServer; //数据库服务器
        public string DatabaseName;//数据库名
        public string DatabaseUser;//数据库用户
        public string DatabasePassword; //密码

        //各服务器的地址
        public string WcfServerUrl; // WCF或Webservice服务的路径
        public string DocumentServerURL; //文档服务器URL
        public string PluginServerURL;//插件服务器URL
        public string RptReportServerURL; //水晶报表服务器URL

        //回传给浏览器的元对象信息
        public string MetaDataName = "No metadata"; //元对象名
        public string MetaDataVersion = "0.00"; //元对象版本
        public string UIMappingEngineVersion = "0.00"; //UIMapping版本号

        //事件日志类型枚举--1.普通事件，2警告，3错误，4严重错误 ,5表字段变更 ,6其它
        public enum EventLogType { Normal = 1, Warning = 2, Error = 3, FatalError = 4, TableChanged = 5, Other = 6 };

        #endregion

        #region ////以下定义OrBit插件与浏览器交互的接口函数

        /// <summary>
        /// 执行浏览器的命令
        /// </summary>
        /// <param name="command">命令ID</param>
        public void RunCommand(string command)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("RunCommand", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { command });
            }
            catch
            {

            }
        }
        /// <summary>
        /// 将信息送往浏览器状态条
        /// </summary>
        /// <param name="Message">信息</param>
        public void SendToStatusBar(string Message)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("SendToStatusBar", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { Message });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 更改浏览器进度条
        /// </summary>
        /// <param name="Maximum">最大值</param>
        /// <param name="Value">当前值</param>        
        public void ChangeProgressBar(int Maximum, int Value)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("ChangeProgressBar", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { Maximum, Value });
            }
            catch
            {

            }
        }



        //更新一个由客户端传回的记录集
        public bool UpdateDataSetBySQL(DataSet DataSetWithSQL, string SQLString)
        {
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                            ";Initial Catalog=" + DatabaseName +
                            ";password=" + DatabasePassword +
                            ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {

                        conn.ConnectionString = ConnectionString;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(SQLString, conn);
                        SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(da);
                        da.Update(DataSetWithSQL.Tables[0]);
                        conn.Close();
                        return true;
                    }
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    bool resultR = true;
                    Object[] myArgs = new object[] { DataSetWithSQL, SQLString };
                    resultR = (bool)type.InvokeMember("UpdateDataSetBySQL", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    return resultR;

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        /// <summary>
        /// 通用执行存储过程程序.
        /// </summary>
        /// <param name="SQLCmd">传入的SqlCommand对象</param>
        /// <param name="ReturnDataSet">执行存储过程后返回的数据集</param>
        /// <param name="ReturnValue">执行存储过程的返回值</param>
        /// <returns>将SQLCmd执行后的参数刷新并传回，主要返回存储过程中的out参数</returns>
        public SqlCommand RunStoredProcedure(SqlCommand SQLCmd, out DataSet ReturnDataSet, out int ReturnValue)
        {
            ReturnValue = 0;
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                            ";Initial Catalog=" + DatabaseName +
                            ";password=" + DatabasePassword +
                            ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SQLCmd.Connection = conn;
                        SQLCmd.CommandType = CommandType.StoredProcedure;
                        SQLCmd.CommandTimeout = conn.ConnectionTimeout;
                        SQLCmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                        SQLCmd.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = SQLCmd;

                        DataSet ds = new DataSet("WCFSQLDataSet");
                        adapter.Fill(ds, "WCFSQLDataSet");

                        ReturnDataSet = ds;
                        conn.Close();
                        ReturnValue = (int)SQLCmd.Parameters["RETURN_VALUE"].Value;
                        return SQLCmd;
                    }
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;

                    SqlCommand cmd = new SqlCommand();
                    DataSet rds = new DataSet();

                    ReturnDataSet = null;

                    Object[] myArgs = new object[] { SQLCmd, ReturnDataSet, ReturnValue };
                    cmd = (SqlCommand)type.InvokeMember("RunStoredProcedure", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    ReturnValue = (int)myArgs[2];
                    ReturnDataSet = (DataSet)myArgs[1];
                    return cmd;

                }
            }
            catch (Exception er)
            {
                ReturnDataSet = null;
                MessageBox.Show(er.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /// <summary>
        /// 执行一个指定的SQL字串，并返回一个记录集
        /// 在浏览器下执行时，直接调用浏览器的WCF服务器来传送记录集
        /// </summary>
        /// <param name="SQLString">SQL字串</param>
        /// <returns>返回的记录集</returns>
        public DataSet GetDataSetWithSQLString(string SQLString)
        {
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                                        ";Initial Catalog=" + DatabaseName +
                                        ";password=" + DatabasePassword +
                                        ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = SQLString;

                        comm.CommandType = CommandType.Text;
                        comm.CommandTimeout = conn.ConnectionTimeout;

                        DataSet ds = new DataSet("SQLDataSet");
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = comm;
                        adapter.Fill(ds, "SQLDataSet");

                        conn.Close();
                        return ds;
                    }
                }
                else
                {　//在浏览器下运行时，直接调用浏览器的GetDataSetWithSQLString,
                    //通过WCF服务器返回记录集

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    DataSet ds = new DataSet("SQLDataSet");
                    ds = (DataSet)type.InvokeMember("GetDataSetWithSQLString", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, new object[] { SQLString });
                    return ds;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #region // GetUIText为浏览器提供的语言切换函数，可以用它返回控件或提示信息不同语种的内容。
        // GetUIText提供了几种形式的重载，以方便使用

        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载4
        /// </summary>
        /// <param name="module">模块名</param>
        /// <returns></returns>
        public String GetUIText(string module)
        {
            string s = GetUIText("[Public Text]", module, "", "").Trim();
            if (s == string.Empty)
            {
                return module;
            }
            else
            {
                return s;
            }

        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载3
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string module, string defaultText)
        {
            string s = GetUIText("[Public Text]", module, "", defaultText).Trim();
            if (s == string.Empty)
            {
                return defaultText;
            }
            else
            {
                return s;
            }
        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载2
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="control">控件对象</param>
        /// <returns></returns>
        public string GetUIText(string module, Control control)
        {
            string s = GetUIText(this.ParentForm.Text, module, control.Name, control.Text);
            if (s == string.Empty)
            {
                return control.Text;
            }
            else
            {
                return s;
            }

        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载1
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="controlName">控件名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string module, string controlName, string defaultText)
        {
            string s = GetUIText(this.ParentForm.Text, controlName, defaultText);
            if (s == string.Empty)
            {
                return defaultText;
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// 返回控件或提示信息不同语种的内容-基本型式
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <param name="module">模块名</param>
        /// <param name="controlName">控件名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string owner, string module, string controlName, string defaultText)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                string s = (string)type.InvokeMember("GetUIText", BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Instance, null, obj, new object[] { owner, module, controlName, defaultText });
                if (s != null)
                {
                    return s;
                }
                else
                {
                    return defaultText;
                }
            }
            catch
            {
                return defaultText;
            }

        }
        #endregion

        #endregion



        /// <summary>
        /// 插件入口,由.NET自动生成
        /// </summary>
        public UserControl1()
        {
            InitializeComponent();　//插件控件布局(.NET的默认过程)
            initializeVariable(); //插件变量初始化
            SwitchUI();　　//切换语言
        }

        /// <summary>
        /// 本私有函数对插件各接口变量进行初始化，赋予默认值
        /// 调试环境下这些值不变，通过浏览器执行时，
        /// 这些变量将会根据系统环境被重新赋值。
        /// </summary>
        private void initializeVariable()
        {
            PlugInCommand = "MYPGN";
            PlugInName = "我的插件";
            LanguageId = "0";  //(0-英,1-简,2-繁...8)
            ParameterString = "";
            RightString = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            OrBitUserId = "DEVUSER";
            OrBitUserName = "调试者";
            ApplicationName = "DEBUG";
            ResourceId = "RES0000XXX";
            ResourceName = "YourPC";

            //这里需要根据实际的数据库环境进行改写
            DatabaseServer = "33.0.1.1";
            DatabaseName = "OrBitXIJY";
            DatabaseUser = "sa";
            DatabasePassword = "@@orbit";

            WcfServerUrl = "http://33.0.1.4/WCFService";
            DocumentServerURL = ""; //文档服务器URL
            PluginServerURL = "http://henryx61/Plug-in/";//插件服务器URL
            RptReportServerURL = "http://henryx61/RptExamples/"; //水晶报表服务器URL

            UserTicket = "";
            IsExitQuery = false;
        }
        /// <summary>
        /// 计时器
        /// </summary>
        System.Windows.Forms.Timer t;

        EntityData _EDlotsn1 = null, _EDlotsn2 = null;
        MyDelegate md;
        string pcName;
        PrintMRZ pMRZ;
        ftpinfo FTP;
        /// <summary>
        /// 插件加载事件，由.NET自动生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            tsbtnStart.Enabled = false;
            FTP = new ftpinfo();
            md = new MyDelegate();
            pMRZ = new PrintMRZ();
            //   refreshOrBitIVariable(); //显示接口变量
            setControlsRight(RightString); //设定界面上各控件的权限
            GetFtpInfo();
            GetLotSn();
            GetIPSN(DGVLotSN1);
            t = new System.Windows.Forms.Timer();
            t.Interval = 10000;
            t.Tick += new System.EventHandler(T_Tic_StatCreateBSD);
            t.Start();

        }

        /// <summary>
        /// 线程打印标识单
        /// </summary>
        //private void StartThread(object obj)
        //{
        //    MyObject mobj = (MyObject)obj;
        //    if (mobj.ed != null)
        //    {
        //        GetLotsnPCSBoxNum(ref mobj.ed);
        //        int pcs = mobj.ed.PunchPCS - (mobj.ed.box_pcs * mobj.ed.BoxNum);
        //        if (pcs >= mobj.ed.box_pcs)
        //        {
        //            md.SetUIControlValue(richTextBox1, mobj.ed, "即将生产" + pcs / mobj.ed.box_pcs + "个标识单", true);
        //            DataSet ds = GetDataSetWithSQLString(string.Format(sqlhelp.PrintBSD, mobj.dgv.Rows[0].Cells["IdentityDocumentsId"].Value.ToString(),
        //                                                            OrBitUserId,
        //                                                            pcName, pcs / mobj.ed.box_pcs));
        //            if (ds == null)
        //                return;
        //            if (ds.Tables[ds.Tables.Count - 1].Rows[0]["Return Value"].ToString() == "-1")
        //            {
        //                md.SetUIControlValue(richTextBox1, mobj.ed, ds.Tables[ds.Tables.Count - 1].Rows[0]["@I_ReturnMessage"].ToString(), false);
        //            }
        //            else
        //            {
        //                string FTPid = ds.Tables[0].Rows[0][0].ToString();
        //                string FtpDirectory = ds.Tables[0].Rows[0][1].ToString();
        //                ds.Tables.RemoveAt(0);
        //                ds.Tables.Remove(ds.Tables[ds.Tables.Count - 1]);
        //                if (pMRZ.DownloadFtp(mobj.ed.Filepath, FTPid + mobj.i, FTP.ftpDIR + "/" + FtpDirectory + "/" + FTPid, FTP.ftpip, FTP.ftpUNM, FTP.ftpPWD) == -2)
        //                    md.SetUIControlValue(richTextBox1, mobj.ed, "标签文件下载失败", true);
        //                else
        //                    md.SetUIControlValue(richTextBox1, mobj.ed, pMRZ.printStimulsoftReports(FTPid + mobj.i, ds, false), true);
        //                GetIPSN(DGVLotSN1);
        //            }
        //        }

        //        md.SetUIControlValue(new object[] { mobj.dgv, richTextBox1 }, mobj.ed);
        //    }




        //}




        private void StartThread(object obj)
        {
            List<MyObject> mobj = (List<MyObject>)obj;

            int pcs = 0;

            foreach (var item in mobj)
            {
                GetLotSn();

                if (_EDlotsn1 != null && item.ed.lotsn == _EDlotsn1.lotsn)
                {
                    item.ed = _EDlotsn1;
                }
                if (_EDlotsn2 != null && item.ed.lotsn == _EDlotsn2.lotsn)
                {
                    item.ed = _EDlotsn2;
                }
                if (DGVLotSN1.Columns["随工单号"] != null && item.dgv.Rows[0].Cells["随工单号"].Value.ToString() == DGVLotSN1.Rows[0].Cells["随工单号"].Value.ToString())
                {
                    item.dgv = DGVLotSN1;
                }

                if (dgvLotSN2.Columns["随工单号"] != null && item.dgv.Rows[0].Cells["随工单号"].Value.ToString() == dgvLotSN2.Rows[0].Cells["随工单号"].Value.ToString())
                {
                    item.dgv = dgvLotSN2;
                }
                if (item.ed == null)
                    continue;
                pcs = item.ed.PunchPCS - (item.ed.box_pcs * item.ed.BoxNum);
                if (item.ed.isover && pcs > 0)
                {
                    int printBoxNum = 0;
                    string _msg = "";
                    if (item.ed.box_pcs <= 0)
                        continue;
                    if (pcs % item.ed.box_pcs > 0)
                    {
                        printBoxNum = pcs / item.ed.box_pcs + 1;
                        _msg = "即将生产" + printBoxNum + "个标识单,包含零头箱";
                    }
                    else
                    {
                        printBoxNum = pcs / item.ed.box_pcs;
                        _msg = "即将生产" + printBoxNum + "个标识单";
                    }
                      printBSD(item, printBoxNum, _msg);
                }
                if (pcs >= item.ed.box_pcs && !item.ed.isover)
                {
                    int printBoxNum = 0;
                    string _msg = "";
                    if (item.ed.box_pcs <= 0)
                        continue;

                    printBoxNum = pcs / item.ed.box_pcs;
                    _msg = "即将生产" + printBoxNum + "个标识单";

                      printBSD(item, printBoxNum, _msg);
                    #region 根据随工单打印
                    //DataSet ds = GetDataSetWithSQLString(string.Format(sqlhelp.PrintBSD, item.dgv.Rows[0].Cells["IdentityDocumentsId"].Value.ToString(),
                    //                                                OrBitUserId,
                    //                                                pcName, printBoxNum));
                    //md.SetUIControlValue(richTextBox1, item.ed, _msg, true);
                    //if (ds == null)
                    //    return;
                    //if (ds.Tables[ds.Tables.Count - 1].Rows[0]["Return Value"].ToString() == "-1")
                    //{
                    //    md.SetUIControlValue(richTextBox1, item.ed, ds.Tables[ds.Tables.Count - 1].Rows[0]["@I_ReturnMessage"].ToString(), false);
                    //}
                    //else
                    //{

                    //    string FTPid = ds.Tables[0].Rows[0][0].ToString();
                    //    string FtpDirectory = ds.Tables[0].Rows[0][1].ToString();
                    //    ds.Tables.RemoveAt(0);
                    //    ds.Tables.Remove(ds.Tables[ds.Tables.Count - 1]);
                    //    if (pMRZ.DownloadFtp(item.ed.Filepath, FTPid + item.i, FTP.ftpDIR + "/" + FtpDirectory + "/" + FTPid, FTP.ftpip, FTP.ftpUNM, FTP.ftpPWD) == -2)
                    //        md.SetUIControlValue(richTextBox1, item.ed, "标签文件下载失败", true);
                    //    else
                    //    {
                    //        md.SetUIControlValue(richTextBox1, item.ed, pMRZ.printStimulsoftReports(FTPid + item.i, ds, false), true);
                    //        item.ed.BoxNum += printBoxNum;
                    //    }
                    //}
                    #endregion
                } // 打印

                if (_EDlotsn1 != null && item.ed.lotsn == _EDlotsn1.lotsn && _EDlotsn1.isover)
                {
                    md.SetUIControlValue(new object[] { item.dgv, richTextBox1 }, _EDlotsn1);
                    _EDlotsn1 = new EntityData();
                }

                if (_EDlotsn2 != null && item.ed.lotsn == _EDlotsn2.lotsn && _EDlotsn2.isover)
                {
                    _EDlotsn2 = new EntityData();
                }
                md.SetUIControlValue(new object[] { item.dgv, richTextBox1 }, item.ed);
                // GetLotSn();
                GetIPSN(DGVLotSN1);
               
            }

        }

        void printBSD(MyObject m, int PrintBoxNum, string msg)
        {
            //return;
            DataSet ds = GetDataSetWithSQLString(string.Format(sqlhelp.PrintBSD, m.dgv.Rows[0].Cells["IdentityDocumentsId"].Value.ToString(),
                                                                    OrBitUserId,
                                                                    pcName, PrintBoxNum));
            md.SetUIControlValue(richTextBox1, m.ed, msg, true);
            if (ds == null)
                return;
            if (ds.Tables[ds.Tables.Count - 1].Rows[0]["Return Value"].ToString() == "-1")
            {
                md.SetUIControlValue(richTextBox1, m.ed, ds.Tables[ds.Tables.Count - 1].Rows[0]["@I_ReturnMessage"].ToString(), false);
            }
            else
            {

                string FTPid = ds.Tables[0].Rows[0][0].ToString();
                string FtpDirectory = ds.Tables[0].Rows[0][1].ToString();
                ds.Tables.RemoveAt(0);
                ds.Tables.Remove(ds.Tables[ds.Tables.Count - 1]);
                if (pMRZ.DownloadFtp(m.ed.Filepath, FTPid + m.i, FTP.ftpDIR + "/" + FtpDirectory + "/" + FTPid, FTP.ftpip, FTP.ftpUNM, FTP.ftpPWD) == -2)
                    md.SetUIControlValue(richTextBox1, m.ed, "标签文件下载失败", true);
                else
                {
                    md.SetUIControlValue(richTextBox1, m.ed, pMRZ.printStimulsoftReports(FTPid + m.i, ds, false), true);
                    m.ed.BoxNum += PrintBoxNum;
                }

            }
        }
        /// <summary>
        /// 获取机台当前的随工单
        /// </summary>
        void GetLotSn()
        {
            pcName = "012-098";//
            Dns.GetHostName();//获取机台名称

            string sql = string.Format(sqlhelp.GetLotSN, pcName);
            DataSet ds = GetDataSetWithSQLString(sql);
            if (ds.Tables[ds.Tables.Count - 1].Rows[0]["Return Value"].ToString() == "-1")
            {
                md.SetUIControlValue(richTextBox1, null, ds.Tables[ds.Tables.Count - 1].Rows[0]["@I_ReturnMessage"].ToString(), false);
                return;
            }
            else
            {
                //DGVLotSN1.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows[0][1].ToString() != "-2")
                {
                    md.SetDGVUIClic(new object[] { DGVLotSN1 }, ds.Tables[0], "");
                    if (_EDlotsn1 == null)
                        _EDlotsn1 = new EntityData();

                    if (_EDlotsn1.lotsn != "" && _EDlotsn1.lotsn != ds.Tables[0].Rows[0]["随工单号"].ToString())
                    {
                        DataSet ds1 = GetOLDLotSNInfo(_EDlotsn1);
                        _EDlotsn1 = SeveEntityData(ds1.Tables[0]);
                        _EDlotsn1.isover = true;

                        md.SetDGVIsyc(new object[] { DGVLotSN1, label2 }, ds1.Tables[0], true, "");
                    }//换随工单后查询一次原随工单的落料数
                    else
                    {
                        _EDlotsn1 = SeveEntityData(ds.Tables[0]);
                        _EDlotsn1.isover = false;
                        md.SetDGVIsyc(new object[] { DGVLotSN1, label2 }, ds.Tables[0], true, "");
                    }
                }

                if (ds.Tables.Count > 2)
                {
                    //判断随工单在哪里
                    if (ds.Tables[1].Rows[0][1].ToString() != "-2")
                    {
                        if (_EDlotsn2 == null)
                            _EDlotsn2 = new EntityData();

                        if (_EDlotsn2.lotsn != "" && _EDlotsn2.lotsn != ds.Tables[1].Rows[0]["随工单号"].ToString())
                        {
                            DataSet ds1 = GetOLDLotSNInfo(_EDlotsn2);
                            _EDlotsn2 = SeveEntityData(ds1.Tables[0]);
                            _EDlotsn2.isover = true;

                            md.SetDGVIsyc(new object[] { dgvLotSN2, label2 }, ds1.Tables[0], true, "");

                        }
                        else
                        {
                            _EDlotsn2 = SeveEntityData(ds.Tables[1]);
                            _EDlotsn2.isover = false;
                            md.SetDGVIsyc(new object[] { dgvLotSN2, label2 }, ds.Tables[1], true, "");
                        }

                    }



                }
                else
                {

                    //label1.Text = "随工单信息";
                    //label2.Visible = false;
                    //dgvLotSN2.Visible = false;
                    md.SetDGVIsyc(new object[] { dgvLotSN2, label2 }, null, false, "");
                }
            }
        }

        EntityData SeveEntityData(DataTable dt)
        {
            EntityData edt = new EntityData();
            if (dt == null || dt.Rows.Count <= 0)
            {
                return edt;
            }
            edt.lotsn = dt.Rows[0]["随工单号"].ToString();//随工单

            if (dt.Rows[0]["盒数"].ToString() == "")
                edt.BoxNum = 0;
            else
                edt.BoxNum = int.Parse(dt.Rows[0]["盒数"].ToString());//盒子数量


            if (dt.Rows[0]["落料数"].ToString() == "")
            {
                edt.PunchPCS = 0;
            }
            else
                edt.PunchPCS = int.Parse(dt.Rows[0]["落料数"].ToString());//落料数

            if (dt.Rows[0]["标箱数量"].ToString() == "")
            {
                edt.box_pcs = 0;
                md.SetUIControlValue(richTextBox1, null, "请维护随工单[" + dt.Rows[0]["随工单号"].ToString() + "]对应料号的标箱数量！", false);
            }
            else
                edt.box_pcs = int.Parse(dt.Rows[0]["标箱数量"].ToString());//获取标箱数量
            return edt;
        }

        /// <summary>
        /// 获取上一个工单的落料情况
        /// </summary>
        /// <param name="ed"></param>
        DataSet GetOLDLotSNInfo(EntityData ed)
        {
            if (ed == null)
                return null;
            if (ed.lotsn == "")
                return null;
            string sql = string.Format(sqlhelp.GetoldLotsn, ed.lotsn);
            return GetDataSetWithSQLString(sql);


        }

        /// <summary>
        /// SwitchUI为OrBit浏览器提供的统一语言切换函数,
        /// 能根据浏览器的语言环境切换界面语言
        /// 如无多语言切换需求，本函数可以置空。
        /// 如有多语言切换需求，请自行完善本函数的内容
        /// 此函数不可改名或删除，否则将导致浏览器无法对其切换语言。
        /// </summary>
        public void SwitchUI()
        {

            //tsbtnAprove.ToolTipText = GetUIText("Aprove");
            //tsbtnFind.ToolTipText = GetUIText("Find");
            //tsbtnPrint.ToolTipText = GetUIText("Print");
            //tsbtnImport.ToolTipText = GetUIText("Import data");
            //tsbtnExport.ToolTipText = GetUIText("Export date");
            //tsbtnExit.ToolTipText = GetUIText("Exit");

            //groupBox1.Text = GetUIText("Interface variables");
            //label1.Text = GetUIText("PlugInCommand");
            //label2.Text = GetUIText("PlugInName");
            //label3.Text = GetUIText("LanguageId");
            //label4.Text = GetUIText("ParameterString");
            //label5.Text = GetUIText("RightString");
            //label6.Text = GetUIText("OrBitUserId");

            //label7.Text = GetUIText("OrBitUserName");
            //label8.Text = GetUIText("ApplicationName");
            //label9.Text = GetUIText("ResourceId");
            //label12.Text = GetUIText("ResourceName");
            //label11.Text = GetUIText("IsExitQuery");
            //label10.Text = GetUIText("UserTicket");

            //label15.Text = GetUIText("DatabaseServer");
            //label14.Text = GetUIText("DatabaseName");
            //label13.Text = GetUIText("DatabaseUser");

            //label15.Text = GetUIText("DatabaseServer");
            //label14.Text = GetUIText("DatabaseName");
            //label13.Text = GetUIText("DatabaseUser");

            //label18.Text = GetUIText("WcfServerUrl");
            //label17.Text = GetUIText("DocumentServerURL");
            //label16.Text = GetUIText("PluginServerURL");
            //label19.Text = GetUIText("RptReportServerURL");


            //groupBox2.Text = GetUIText("Run command");

            //label20.Text = GetUIText("Command");
            //button1.Text = GetUIText("Run");

            //groupBox3.Text = GetUIText("Send message to status bar");
            //label21.Text = GetUIText("Message");
            //button2.Text = GetUIText("Send");

            //groupBox7.Text = GetUIText("Exit mode");
            //button8.Text = GetUIText("Direct exit");
            //button9.Text = GetUIText("Remind exit");

            //groupBox4.Text = GetUIText("Change progress bar");

            //groupBox8.Text = GetUIText("Use rights string");
            //button12.Text = GetUIText("Create");
            //button11.Text = GetUIText("Modify");
            //button10.Text = GetUIText("Delete");
            //button13.Text = GetUIText("A right");


            //groupBox5.Text = GetUIText("Write into event log");
            //label22.Text = GetUIText("Event type");
            //label23.Text = GetUIText("Event");
            //button6.Text = GetUIText("Write");

            //groupBox6.Text = GetUIText("Run SQL string");
            //label24.Text = GetUIText("SQL string");
            //button7.Text = GetUIText("Run");

            //textBox3.Text = LanguageId;
            //button15.Text = GetUIText("Update dataset by sql string");
            //button14.Text = GetUIText("Run stored procedure: _LsTest");


        }

        /// <summary>
        /// setControlsRight函数用于设定屏幕控件的权限
        /// 通过权限字串来控制控件的.Visible属性
        /// </summary>
        private void setControlsRight(string rightString)
        { //设定界面上各控件的权限

            //button11.Visible = false;
            //button12.Visible = false;
            //button13.Visible = false;
            //button10.Visible = false;

            //if (rightString== null || rightString.Trim() == string.Empty) return;

            ////新增权限
            //if (rightString.IndexOf("1") > -1) button12.Visible = true;
            ////修改权限
            //if (rightString.IndexOf("2") > -1) button11.Visible = true;
            ////删除权限
            //if (rightString.IndexOf("3") > -1) button10.Visible = true;

            ////自定义权限A
            //if (rightString.IndexOf("A") > -1) button13.Visible = true;


        }

        /// <summary>
        /// PluginUnload函数为OrBit浏览器关闭窗体时触发的过程,可以用于示范各种对象
        /// 此函数不可改名或删除
        /// 但里面内容允许修改。
        /// </summary>
        public void PluginUnload()
        {
            t.Stop();
            if (th != null)
                th.Abort();
            if (t2 != null)
                t2.Abort();


        }

        //演示如何退出插件
        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            t.Stop();
            if (th != null)
                th.Abort();
            if (t2 != null)
                t2.Abort();
            this.ParentForm.Close();

        }

        //演示执行一个命令
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    RunCommand(textBox20.Text);
        //}





        //演示修改浏览器进度条
        private void button3_Click(object sender, EventArgs e)
        {
            ChangeProgressBar(0, 0);
        }
        //演示修改浏览器进度条
        private void button4_Click(object sender, EventArgs e)
        {
            ChangeProgressBar(100, 50);
        }
        //演示修改浏览器进度条
        private void button5_Click(object sender, EventArgs e)
        {
            ChangeProgressBar(100, 100);
        }

        //演示即时修改权限
        //private void textBox5_TextChanged(object sender, EventArgs e)
        //{
        //    setControlsRight(textBox5.Text.ToUpper().Trim());
        //}
        //演示写入事件日志
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    WriteToEventLog(textBox22.Text, textBox23.Text);
        //    SendToStatusBar(GetUIText("Your event has been write into event log!"));
        //}

        //演示获取记录集或是执行SQL命令
        //private void button7_Click(object sender, EventArgs e)
        //{
        //    dataGridView1.DataSource = null;
        //    DataSet ds = new DataSet();
        //    ds = GetDataSetWithSQLString(textBox24.Text);
        //    if (ds != null && ds.Tables.Count >0) 
        //    {
        //        dataGridView1.DataSource = ds.Tables[0] ;
        //    }
        //}




        /// <summary>
        /// 获取当前随工单的标识单
        /// </summary>
        /// <param name="sender"></param>
        private void GetIPSN(DataGridView sender)
        {
            if (sender.RowCount <= 0)
                return;

            if (sender.Columns["IdentityDocumentsId"] == null)
                return;
            string idIdentityDocumentsId = sender.Rows[0].Cells["IdentityDocumentsId"].Value.ToString();
            DataSet ds = GetDataSetWithSQLString(string.Format(sqlhelp.GetIdentitysn, idIdentityDocumentsId));
            string msg = "已打印标识单：" + ds.Tables[0].Rows.Count + "个";
            md.SetDGVUIClic(new object[] { DGVIPSN, label4 }, ds.Tables[0], msg);


        }

        /// <summary>
        /// 获取当前随工单标识单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVLotSN1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Name == "DGVIPSN")
                return;
            GetIPSN(dgv);
        }

        /// <summary>
        /// 获取对应随工单的落料数与标识单个数
        /// </summary>
        /// <param name="ed"></param>
        void GetLotsnPCSBoxNum(ref EntityData ed)
        {
            string sql = string.Format(sqlhelp.GetLotSNPunchPCSBoxNum, ed.lotsn);
            DataSet ds = GetDataSetWithSQLString(sql);
            if (ds.Tables[0].Rows[0]["BoxNum"].ToString() == "")
                ed.BoxNum = 0;
            else
                ed.BoxNum = int.Parse(ds.Tables[0].Rows[0]["BoxNum"].ToString());

            if (ds.Tables[0].Rows[0]["PunchPCS"].ToString() == "")
                ed.PunchPCS = 0;
            else
                ed.PunchPCS = int.Parse(ds.Tables[0].Rows[0]["PunchPCS"].ToString());

            if (ds.Tables[0].Rows[0]["Box_pcs"].ToString() == "")
                ed.box_pcs = 0;
            else
                ed.box_pcs = int.Parse(ds.Tables[0].Rows[0]["Box_pcs"].ToString());
        }


        Thread th;
        void T_Tic_StatCreateBSD(object sender, EventArgs e)
        {
               t.Stop();
            if (_EDlotsn2 != null)
            {
                List<MyObject> lmobj = new List<MyObject>();
                lmobj.Add(new MyObject());
                lmobj.Add(new MyObject());
                lmobj[0].dgv = dgvLotSN2;
                lmobj[0].ed = _EDlotsn2;
                lmobj[0].i = 1;
                lmobj[1].dgv = DGVLotSN1;
                lmobj[1].ed = _EDlotsn1;
                lmobj[0].i = 2;
                // StartThread(lmobj);
                th = new Thread(new ParameterizedThreadStart(StartThread));
                th.Start(lmobj);

            }
            else
            {
                MyObject mobj = new MyObject();
                mobj.ed = _EDlotsn1;
                mobj.dgv = DGVLotSN1;
                mobj.i = 0;
                List<MyObject> lmbj = new List<MyObject>();
                lmbj.Add(mobj);
                // StartThread(lmbj);
                th = new Thread(new ParameterizedThreadStart(StartThread));
                th.Start(lmbj);
            }

        }

        string temp = Environment.GetEnvironmentVariable("TEMP") + "\\";
        private void button1_Click(object sender, EventArgs e)
        {
            if (DGVIPSN.RowCount <= 0)
                return;
            string sn = "";
            for (int i = 0; i < DGVIPSN.RowCount; i++)
            {
                if (DGVIPSN.Rows[i].Selected)
                {
                    sn = DGVIPSN.Rows[i].Cells["id"].Value.ToString();
                }
            }
            if (sn == "")
            {
                md.SetUIControlValue(richTextBox1, null, "请选一个标识单", false);
                return;
            }
            DataTable dt = GetDataSetWithSQLString(sqlhelp.ftpname).Tables[0];
            string ftpname = dt.Rows[0][0].ToString();
            string dir = dt.Rows[0][1].ToString();
            DataSet ds = GetDataSetWithSQLString(string.Format(sqlhelp.ageinPrintBSD, sn));
            if (pMRZ.DownloadFtp(temp, ftpname, FTP.ftpDIR + "/" + dir + "/" + ftpname, FTP.ftpip, FTP.ftpUNM, FTP.ftpPWD) == -2)
            {
                md.SetUIControlValue(richTextBox1, null, "标识单标签下载失败", false);
                return;
            }
            md.SetUIControlValue(richTextBox1, null, pMRZ.printStimulsoftReports(ftpname, ds, false), false);
        }


        bool LB5VISI = false;
        Thread t2;
        private void tSBStop_Click(object sender, EventArgs e)
        {
            t.Stop();
            tSBStop.Enabled = false;
            tsbtnStart.Enabled = true;

            LB5VISI = true;
            t2 = new Thread(setLab5Status);
            t2.Start();
        }

        void setLab5Status()
        {
            while (LB5VISI)
            {
                md.setLab5(label5, "自动打印功能已暂停", true);
                Thread.Sleep(500);
                md.setLab5(label5, "自动打印功能已暂停", false);
                Thread.Sleep(500);
            }
        }

        private void tsbtnStart_Click(object sender, EventArgs e)
        {
            tsbtnStart.Enabled = false;
            tSBStop.Enabled = true;
            t.Start();
            label5.Visible = false;
            LB5VISI = false;
            if (t2 != null)
                t2.Abort();

        }

        /// <summary>
        /// 获取ftp信息
        /// </summary>
        private void GetFtpInfo()
        {
            string strmsg = GetDataSetWithSQLString(sqlhelp.FtpInfo).Tables[0].Rows[0][0].ToString().Replace("[", "").Replace("]", "");
            //[FTP:33.0.1.4,USER:Mesuser,PASSWORD:Mesorbit123,FOLDER:MRZ]
            string[] strq = strmsg.Split(',');
            try
            {

                foreach (var item in strq)
                {
                    if (item.Contains("FTP"))
                    {
                        FTP.ftpip = item.Substring(item.IndexOf(":") + 1, item.Length - (item.IndexOf(":") + 1));
                        continue;
                    }
                    if (item.Contains("USER"))
                    {
                        FTP.ftpUNM = item.Substring(item.IndexOf(":") + 1, item.Length - (item.IndexOf(":") + 1));
                        continue;
                    }
                    if (item.Contains("PASSWORD"))
                    {
                        FTP.ftpPWD = item.Substring(item.IndexOf(":") + 1, item.Length - (item.IndexOf(":") + 1));
                        continue;
                    }
                    if (item.Contains("FOLDER"))
                    {
                        FTP.ftpDIR = item.Substring(item.IndexOf(":") + 1, item.Length - (item.IndexOf(":") + 1));
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
    }
    /// <summary>
    /// 保存Ftp服务器信息
    /// </summary>
    class ftpinfo
    {
        public string ftpip = "";
        public string ftpUNM = "";
        public string ftpDIR = "";
        public string ftpPWD = "";

    }

    class MyObject
    {
        public EntityData ed;
        public DataGridView dgv;
        public int i = 0;
    }
}
