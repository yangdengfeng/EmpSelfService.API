using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{
    /// <summary>
    /// 钱箱控制器
    /// </summary>
    public class CashBoxController : ApiController
    {
        /// <summary>
        /// 钱箱更新
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t设备终端号\t增加或减少标识\t金额\t操作用户</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage ModifyCashBox(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 5, "ModifyCashBox");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                bool flag = new AmtMngBLL().UpdateAmtTypeMng(rt[1], rt[2], rt[3], rt[4]);
                return JsonHelper.StringToJson(!flag ? CodeModel.ErrSystem : CodeModel.SUCCESS);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.CashBoxController.ModifyCashBox", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


        /// <summary>
        /// 根据终端代号获取终端钱箱余额
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t设备终端号</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 2, "CashBoxController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var dt = new AmtMngBLL().SelBalanceByTerminalNo(result.Value[1]);
                if (dt == null || dt.Rows.Count < 1)
                {
                    return JsonHelper.StringToJson(CodeModel.ErrSystem);
                }
                else
                {
                    return JsonHelper.DataTableToJson(dt);
                } 
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.CashBoxController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


    }
}
