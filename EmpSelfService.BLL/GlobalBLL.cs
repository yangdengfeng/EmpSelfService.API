/* ==============================================================================
 * 功能名称：GlobalBLL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：xulx
 * 创建日期：2016/4/22 17:47:33
 * 功能描述：全局操作
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using YKT.Core;
using EmpSelfService.Common;
using EmpSelfService.Model;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    /// <summary>
    /// 全局静态存储GUID
    /// </summary>
    public static class GlobalStaticBLL
    {
        #region 添加接口调用的GUID
        /// <summary>
        /// 添加接口调用的GUID  返回值 0：正常  1：程序出错 2：参数不正常(空) 3：参数数据已使用
        /// </summary>
        /// <param name="interFaceName">接口名称</param>
        /// <param name="guid">GUID</param>
        /// <returns> </returns>
        public static string AddGUIDData(string interFaceName, string guid)
        {
            string strResult = "-1";
            GlobalModel.initialization();
            try
            {
                if (guid == "")//interFaceName == "" ||
                {
                    strResult = "2";
                    return strResult;
                }
                //判断接口名称和GUID是否已存在
                DataRow[] drs = GlobalModel.dtGUID.Select(" InterFaceName = '" + interFaceName + "' and GUID = '" + guid + "'");
                if (drs.Length > 0)
                {
                    strResult = "3";
                    return strResult;
                }
                //满1W条数据清理一次
                if (GlobalModel.dtGUID.Rows.Count > 10000)
                {
                    GlobalModel.dtGUID.Clear();
                }
                //记录接口名称和GUID
                DataRow dr = GlobalModel.dtGUID.NewRow();
                dr["InterFaceName"] = interFaceName;
                dr["GUID"] = guid;
                GlobalModel.dtGUID.Rows.Add(dr);

                strResult = "0";
            }
            catch (Exception ex)
            {
                strResult = "1\t" + ex.Message;
            }

            return strResult;
        }

        #endregion
    }

    public class GlobalBLL
    {
        readonly GlobalDAL _dhDal = new GlobalDAL();

        #region 用户登陆

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool UserLogin(string userId, string pwd)
        {
            try
            {
                var dt = _dhDal.GetUserInfo(userId, pwd);
                return dt != null && dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 用户验证
        /// <summary>
        /// 用户验证 返回值 1：用户不存在 正常情况返回密码
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public string UserCheck(string userId)
        {
            try
            {
                string strResult = "";
                DataTable dt = _dhDal.GetUserInfo(userId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strResult = dt.Rows[0]["PASSWORD"].ToString();
                }
                else
                {
                    strResult = "1";
                }
                return strResult;
            }
            catch (Exception ex)
            {
                //todo 记录错误日志
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 根据CPU编号查找用户信息
        /// <summary>
        /// 根据CPU编号查找用户信息
        /// </summary>
        /// <param name="cpuId"></param>
        /// <returns></returns>
        public ResultBase<DataTable> GetUserByCpuId(string cpuId)
        {
            try
            {
                var dt = _dhDal.GetUserByCpuId(cpuId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["USERID"].ToString()) &&
                        !string.IsNullOrEmpty(dt.Rows[0]["PASSWORD"].ToString()))
                    {
                        return ResultBase<DataTable>.GetSuccess(DtSelectTop(1, dt));
                    }
                    return ResultBase<DataTable>.GetFailure("-1");
                }
                return ResultBase<DataTable>.GetFailure("-1");
            }
            catch (Exception ex)
            {
                //todo 记录错误日志
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region 根据终端号查找用户信息
        /// <summary>
        /// 根据终端号查找用户信息
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <returns></returns>
        public ResultBase<DataTable> GetUserByTerminalNo(string terminalNo)
        {
            try
            {
                var dt = _dhDal.GetUserByTerminalNo(terminalNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["u_id"].ToString()) && 
                        !string.IsNullOrEmpty(dt.Rows[0]["u_password"].ToString()))
                    {
                        return ResultBase<DataTable>.GetSuccess(DtSelectTop(1, dt));
                    }
                    else
                    {
                        return ResultBase<DataTable>.GetFailure("-1");
                    }
                }
                else
                {
                    return ResultBase<DataTable>.GetFailure("-1");
                }
            }
            catch (Exception ex)
            {
                //todo 记录错误日志
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 根据企业编号查找用户信息
        /// <summary>
        /// 根据企业编号查找用户信息
        /// </summary>
        /// <param name="companyId">企业编号</param>
        /// <returns></returns>
        public ResultBase<DataTable> GetUserByCompanyId(string companyId)
        {
            try
            {
                var dt = _dhDal.GetUserByCompanyId(companyId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["u_id"].ToString()) &&
                        !string.IsNullOrEmpty(dt.Rows[0]["u_password"].ToString()))
                    {
                        return ResultBase<DataTable>.GetSuccess(DtSelectTop(1, dt));
                    }
                    else
                    {
                        return ResultBase<DataTable>.GetFailure("-1");
                    }
                }
                else
                {
                    return ResultBase<DataTable>.GetFailure("-1");
                }
            }
            catch (Exception ex)
            {
                //todo 记录错误日志
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region 获取DataTable前几条数据
        /// <summary> 
        /// 获取DataTable前几条数据 
        /// </summary> 
        /// <param name="topItem">前N条数据</param> 
        /// <param name="oDt">源DataTable</param> 
        /// <returns></returns> 
        public static DataTable DtSelectTop(int topItem, DataTable oDt)
        {
            if (oDt.Rows.Count < topItem) return oDt;

            DataTable newTable = oDt.Clone();
            DataRow[] rows = oDt.Select("1=1");
            for (int i = 0; i < topItem; i++)
            {
                newTable.ImportRow((DataRow)rows[i]);
            }
            return newTable;
        }
        #endregion


        #region 根据CPU编号  用户验证和参数验证
        /// <summary>
        /// 用户验证和参数验证
        /// </summary>
        /// <param name="cpuId">Cpu编号</param>
        /// <param name="values">参数数据</param>
        /// <param name="paramsNum">参数个数</param>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        public static ResultBase<string[]> VerifyUserAndParamsByCpu(string cpuId, string values, int paramsNum, string methodName)
        {
            LogHelper.Log(methodName, "Request Encrypt Params", string.Format("terminalNo:{0} values:{1}", cpuId, values));
            string uId;
            string uPwd;
            object obj = CacheHelper.Get(cpuId);
            if (obj != null)
            {
                var dt = (DataTable)obj;
                //uId = dt.Rows[0][0].ToString();
                uPwd = dt.Rows[0][1].ToString();
            }
            else
            {
                var result = new GlobalBLL().GetUserByCpuId(cpuId);
                if (!result)
                {
                    return ResultBase<string[]>.GetFailure(result.Info);
                }
                //uId = result.Value.Rows[0][0].ToString();
                uPwd = result.Value.Rows[0][1].ToString();
            }

            var strParameters = ParaAnalyze(values, paramsNum, uPwd, methodName, cpuId);
            return strParameters.Length == 1 ? ResultBase<string[]>.GetFailure(strParameters[0]) :
                ResultBase<string[]>.GetSuccess(strParameters);
        }

        #endregion

        #region 根据设备终端号  用户验证和参数验证 
        /// <summary>
        /// 用户验证和参数验证
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据</param>
        /// <param name="paramsNum">参数个数</param>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        public static ResultBase<string[]> VerifyUserAndParams(string terminalNo, string values, int paramsNum, string methodName)
        {
            LogHelper.Log(methodName, "Request Encrypt Params", string.Format("terminalNo:{0} values:{1}", terminalNo, values));
            string uId;
            string uPwd;
            object obj = CacheHelper.Get(terminalNo);
            if (obj != null)
            {
                var dt = (DataTable)obj;
                //uId = dt.Rows[0][0].ToString();
                uPwd = dt.Rows[0][1].ToString();
            }
            else
            {
                var result = new GlobalBLL().GetUserByTerminalNo(terminalNo);
                if (!result)
                {
                    return ResultBase<string[]>.GetFailure(result.Info);
                }
                else
                {
                    //uId = result.Value.Rows[0][0].ToString();
                    uPwd = result.Value.Rows[0][1].ToString();
                }
            }

            string[] strParameters = ParaAnalyze(values, paramsNum, uPwd, methodName, terminalNo);
            
            return strParameters.Length == 1 ? ResultBase<string[]>.GetFailure(strParameters[0]) :
                ResultBase<string[]>.GetSuccess(strParameters);
        }


        public static ResultBase<string[]> VerifyUserAndParamsNew(string companyId, string values, int paramsNum, string methodName)
        {
            LogHelper.Log(methodName, "Request Encrypt Params", string.Format("terminalNo:{0} values:{1}", companyId, values));
            string uId;
            string uPwd;
            object obj = CacheHelper.Get(companyId);
            if (obj != null)
            {
                var dt = (DataTable)obj;
                //uId = dt.Rows[0][0].ToString();
                uPwd = dt.Rows[0][1].ToString();
            }
            else
            {
                var result = new GlobalBLL().GetUserByCompanyId(companyId);
                if (!result)
                {
                    return ResultBase<string[]>.GetFailure(result.Info);
                }
                else
                {
                    //uId = result.Value.Rows[0][0].ToString();
                    uPwd = result.Value.Rows[0][1].ToString();
                }
            }

            string[] strParameters = ParaAnalyze(values, paramsNum, uPwd, methodName, companyId);

            return strParameters.Length == 1 ? ResultBase<string[]>.GetFailure(strParameters[0]) :
                ResultBase<string[]>.GetSuccess(strParameters);
        }

        #endregion

        #region 参数解析

        /// <summary>
        /// 参数解析 返回值 -1:程序出错 1：GUID程序出错 2：参数不正常(空) 3：参数数据已使用 4：解析后参数数量错误
        /// </summary>
        /// <param name="values">DES加密过后的字符串</param>
        /// <param name="num">请求的参数数量</param>
        /// <param name="password">DES加密解密密码</param>
        /// <returns></returns>
        public static string[] ParaAnalyze(string values, int num, string password, string methodName, string terminalNo)
        {
            try
            {
                password = xEncrypt.DecryptText(password);
                values = xEncrypt.DecryptText(values.Replace(' ', '+'), password);

                LogHelper.Log(methodName, "Request Decrypt Params", string.Format("terminalNo:{0} values:{1}", terminalNo, values));
                string[] strsParameters = Regex.Split(values, @"\\t", RegexOptions.IgnoreCase);
                if (strsParameters.Length != num)
                    return new string[] { "4" }; //4:解析后参数数量错误

                string guidResult = GlobalStaticBLL.AddGUIDData("", strsParameters[0]);
                if (guidResult != "0") //1：程序出错 2：参数不正常(空) 3：参数数据已使用
                    return new string[] { guidResult };
                return strsParameters;
            }
            catch (Exception ex)
            {
                LogHelper.Log("GlobalBLL.ParaAnalyze", ex, string.Format("values:{0} num:{1} password:{2}", values, num, password));
                return new string[] { "-1" };
            }
        }


        
        #endregion
    }
}
