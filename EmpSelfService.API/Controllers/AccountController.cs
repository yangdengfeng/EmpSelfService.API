using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{
    /// <summary>
    /// 自助终端帐号信息控制器
    /// </summary>
    public class AccountController : ApiController
    {

        /// <summary>
        /// 获取自助终端帐号信息 (注：一个设备终端号24小时内只允许获取一次)
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo)
        {
            try
            {
                object obj = CacheHelper.Get(terminalNo);
                if (obj != null)
                {
                    //获取配置，是否限制时间
                    string limitTerm = ConfigurationManager.AppSettings["LimitTerm"];
                    if (limitTerm == "0")
                    {
                        return JsonHelper.DataTableToJson((DataTable) obj);
                    }
                    else
                    {
                        LogHelper.Log("GetAccountInfo", "24小时内重复获取", string.Format("TerminalNo:{0}", terminalNo));
                        return JsonHelper.StringToJson(CodeModel.BeyondLimited); 
                    }
                }
                
                var result = new GlobalBLL().GetUserByTerminalNo(terminalNo);
                if (!result)
                {
                    LogHelper.Log("GetAccountInfo", "获取用户信息失败", string.Format("TerminalNo:{0}", terminalNo));
                    return JsonHelper.ReturnErrInfo(result.Info);
                }

                //用户信息存入缓存（24小时）
                CacheHelper.Insert(terminalNo, result.Value, DateTime.Now.AddMinutes(24 * 60));
                LogHelper.Log("GetAccountInfo", "缓存用户信息", string.Format("u_id:{0} u_password:{1} terminalNo:{2} netName:{3}",
                    result.Value.Rows[0][0], result.Value.Rows[0][1], terminalNo, result.Value.Rows[0][2]));

                return JsonHelper.DataTableToJson(result.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.AccountController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


        /// <summary>
        /// 获取企业帐号信息 (注：一个企业编号24小时内只允许获取一次)
        /// </summary>
        /// <param name="companyId">企业编号</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage GetAccount(string companyId)
        {
            try
            {
                object obj = CacheHelper.Get(companyId);
                if (obj != null)
                {
                    //获取配置，是否限制时间
                    string limitTerm = ConfigurationManager.AppSettings["LimitTerm"];
                    if (limitTerm == "0")
                    {
                        return JsonHelper.DataTableToJson((DataTable)obj);
                    }
                    else
                    {
                        LogHelper.Log("GetAccountInfo", "24小时内重复获取", string.Format("companyId:{0}", companyId));
                        return JsonHelper.StringToJson(CodeModel.BeyondLimited);
                    }
                }

                var result = new GlobalBLL().GetUserByCompanyId(companyId);
                if (!result)
                {
                    LogHelper.Log("GetAccountInfo", "获取用户信息失败", string.Format("companyId:{0}", companyId));
                    return JsonHelper.ReturnErrInfo(result.Info);
                }

                //用户信息存入缓存（24小时）
                CacheHelper.Insert(companyId, result.Value, DateTime.Now.AddMinutes(24 * 60));
                LogHelper.Log("GetAccountInfo", "缓存用户信息", string.Format("u_id:{0} u_password:{1} companyId:{2} companyName:{3}",
                    result.Value.Rows[0][0], result.Value.Rows[0][1], companyId, result.Value.Rows[0][2]));

                return JsonHelper.DataTableToJson(result.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.AccountController.GetAccount", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

    }
}
