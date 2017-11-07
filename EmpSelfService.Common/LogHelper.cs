using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace EmpSelfService.Common
{
    public class LogHelper
    {
        //在网站根目录下创建日志目录
        public static string path = HttpContext.Current.Request.PhysicalApplicationPath + "\\logs";
        private static string logPath = @"d:\logs\";

        /// <summary>
        /// 日志路径
        /// </summary>
        public static string LogPath
        {
            set { LogHelper.logPath = value; }
        }

        /// <summary>
        /// 服务端异常信息记录文件的调用的方法
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(CallException ex)
        {
            try
            {
                WriteError(ex.MethodName, ex.SourceException.Message, ex.StackTrace);
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 客户端异常信息记录文件的调用的方法
        /// </summary>
        /// <param name="methodName">异常方法名</param>
        /// <param name="ex"></param>
        /// <param name="content"></param>
        public static void Log(string methodName, Exception ex, string content)
        {
            try
            {
                WriteError(methodName, ex.Message, ex.StackTrace, content);
            }
            catch (Exception)
            {

            }
        }

        public static void Log(string methodName, Exception ex)
        {
            try
            {
                WriteError(methodName, ex.Message, ex.StackTrace);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        public static void Log(string infoType, string rowHead, string content, string summary = "")
        {
            try
            {
                // 例如：d:/logs/2016_12/2016-12-05/GetAccountInfo.log
                string logFile = string.Format("{0}.log", infoType);
                //string logFilePath = Path.Combine(logPath, infoType);
                //string filePath = "D:" + "\\" + "Logs" + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                string filePath = path + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                FileHelper.CreatFilePath(filePath);
                StringBuilder logInfo = new StringBuilder();
                logInfo.AppendFormat("[{0}]\r\n", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                logInfo.AppendFormat("{0}:{1}{2}\r\n", rowHead, string.IsNullOrEmpty(summary) ? "" : summary + "\t", content);
                logInfo.AppendFormat("\r\n");
                FileHelper.AppendAllText(string.Format("{0}\\{1}", filePath, logFile), logInfo.ToString());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        ///  <param name="content"></param>
        private static void WriteError(string methodName, string message, string stackTrace, string content = "")
        {
            string infoType = "Error";
            string logFile = string.Format("{0}.log", infoType);
            //string logFilePath = Path.Combine(logPath, infoType);
            //string filePath = "D:" + "\\" + "Logs" + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            string filePath = path + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            FileHelper.CreatFilePath(filePath);
            StringBuilder logInfo = new StringBuilder();
            logInfo.AppendFormat("[{0}]-[{1}]\r\n", DateTime.Now.ToString(), methodName);
            if (!string.IsNullOrEmpty(content))
                logInfo.AppendFormat("{0}\r\n", content);
            logInfo.AppendFormat("[异常信息]：{0}\r\n", message);
            logInfo.AppendFormat("[堆栈信息]：{0}\r\n\r\n", stackTrace);
            FileHelper.AppendAllText(string.Format("{0}\\{1}", filePath, logFile), logInfo.ToString());
        }




        #region 多线程解析文件错误信息日志
        /// <summary>
        /// 多线程解析文件错误信息日志
        /// </summary>
        /// <param name="strLog">日志内容</param>
        /// <param name="fileNameExt"></param>
        public static void WriteLog(string strLog, string fileNameExt)
        {
                                
            string isNeedLog = ConfigurationManager.AppSettings["NeedLog"];
            if (isNeedLog == "1")
            {
                var fileName = DateTime.Now.ToString("yyyyMMdd") + "_" + fileNameExt + ".log";
                strLog = DateTime.Now.ToString("HH:mm:ss") + " " + strLog;
                //string FilePath = System.IO.Directory.GetCurrentDirectory() + "\\Logfiles\\" + System.DateTime.Now.ToString("yyyyMM");
                string filePath = "D:" + "\\" + "LogFiles" + "\\" + DateTime.Now.ToString("yyyy_MM") + "\\" + DateTime.Now.ToString("yyyy_MM_dd");
                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                FileStream fs = new FileStream(filePath + "\\" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter mStreamWriter = new StreamWriter(fs);
                mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
                mStreamWriter.WriteLine(strLog);
                mStreamWriter.Flush();
                mStreamWriter.Close();
                fs.Close();
            }
        }
        #endregion

        #region 写文件内容
        /// <summary>
        /// 多线程解析文件错误信息日志
        /// </summary>
        /// <param name="strContent">文件内容</param>
        /// <param name="filePath">文件路径名称</param>
        /// <param name="fileName"></param>
        public static void WriteFile(string strContent, string filePath, string fileName)
        {
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            FileStream fs = new FileStream(filePath + "\\" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter mStreamWriter = new StreamWriter(fs, Encoding.GetEncoding("gbk"));
            mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
            mStreamWriter.WriteLine(strContent);
            mStreamWriter.Flush();
            mStreamWriter.Close();
            fs.Close();
        }
        #endregion


        /**
         * 向日志文件写入调试信息
         * @param className 类名
         * @param content 写入内容
         */
        public static void Debug(string className, string content)
        {
            if (WeChatPayConfig.LOG_LEVENL >= 3)
            {
                WriteLog("DEBUG", className, content);
            }
        }

        /**
        * 向日志文件写入运行时信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Info(string className, string content)
        {
            if (WeChatPayConfig.LOG_LEVENL >= 2)
            {
                WriteLog("INFO", className, content);
            }
        }

        /**
        * 向日志文件写入出错信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Error(string className, string content)
        {
            if (WeChatPayConfig.LOG_LEVENL >= 1)
            {
                WriteLog("ERROR", className, content);
            }
        }

        /**
        * 实际的写日志操作
        * @param type 日志记录类型
        * @param className 类名
        * @param content 写入内容
        */
        protected static void WriteLog(string type, string className, string content)
        {
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            //向日志文件写入内容
            string write_content = time + " " + type + " " + className + ": " + content;
            mySw.WriteLine(write_content);

            //关闭日志文件
            mySw.Close();
        }
    }
}
