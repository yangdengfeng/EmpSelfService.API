using System;
using System.Data;
using System.Net.Http;
using System.Text;

using EmpSelfService.Model;

namespace EmpSelfService.Common
{
    public class JsonHelper
    {
        #region To Json
        public static HttpResponseMessage StringToJson(string code)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("{\"Code\":\"" + code + "\",");
            sbResult.Append("\"Data\":[\"\"]}");
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sbResult.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public static HttpResponseMessage StringToJson(string code, string data)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("{\"Code\":\"" + code + "\",");
            sbResult.Append("\"Data\":[" + data + "]}");
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sbResult.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public static HttpResponseMessage StringToJson1(string code, string orderNo)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("{\"Code\":\"" + code + "\",");
            sbResult.Append("\"Data\":[{\"OrderNo\":\"" + orderNo + "\"}]}");
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sbResult.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public static HttpResponseMessage StringToJson2(string code, string url)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("{\"Code\":\"" + code + "\",");
            sbResult.Append("\"Data\":[{\"URL\":\"" + url + "\"}]}");
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sbResult.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public static HttpResponseMessage StringToJson3(string code, string rt)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("{\"Code\":\"" + code + "\",");
            sbResult.Append("\"Data\":[{\"Result\":\"" + rt + "\"}]}");
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(sbResult.ToString(), Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        public static HttpResponseMessage DataTableToJson(DataTable dt)
        {
            return DataTableToJson("0", dt);
        }

        public static HttpResponseMessage DataTableToJson(string code, DataTable dt)
        {
            String strResult = DataTable2Json(code, dt);
            HttpResponseMessage hrmResult = new HttpResponseMessage { Content = new StringContent(strResult, Encoding.GetEncoding("UTF-8"), "application/json") };
            return hrmResult;
        }

        public static HttpResponseMessage DataSetToJson(string code, DataSet ds)
        {
            String strResult = DataSet2Json(code, ds);
            HttpResponseMessage hrmResult = new HttpResponseMessage { Content = new StringContent(strResult, Encoding.GetEncoding("UTF-8"), "application/json") };
            return hrmResult;
        }

        private static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            string strName = dt.TableName == "" ? "Data" : dt.TableName;
            jsonBuilder.Append("\"" + strName + "\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        private static string DataTable2Json(string code, DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"Code\":\"" + code + "\",");
            jsonBuilder.Append(DataTable2Json(dt));
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        private static string DataSet2Json(string code, DataSet ds)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{\"Code\":\"" + code + "\",");
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable dt = ds.Tables[i];
                json.Append(DataTable2Json(dt));
                if (i != ds.Tables.Count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("}");
            return json.ToString();
        }
        #endregion

        #region 反馈错误信息
        /// <summary>
        /// 反馈错误信息
        /// </summary>
        /// <param name="errCode">错误代码</param>
        /// <returns></returns>
        public static HttpResponseMessage ReturnErrInfo(string errCode)
        {
            if (errCode == "-1" || errCode == "1")
                return StringToJson(CodeModel.ErrSystem);
            else if (errCode == "2")
                return StringToJson(CodeModel.ErrParaData);
            else if (errCode == "3")
                return StringToJson(CodeModel.ErrParaRepeat);
            else if (errCode == "4")
                return StringToJson(CodeModel.ErrParaNum);
            else if (errCode == "5")
                return StringToJson(CodeModel.ErrParaUser);
            else if (errCode == "6")
                return StringToJson(CodeModel.ErrCreateQrCode);
            else if (errCode == "7")
                return StringToJson(CodeModel.ErrConfigOrNetWork);
            else if (errCode == "8")
                return StringToJson(CodeModel.UnknownPayMethod);
            else
                return StringToJson(CodeModel.ErrSystem);
        }
        #endregion
    }
}
