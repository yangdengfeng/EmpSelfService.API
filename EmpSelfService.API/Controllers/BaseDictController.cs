using System;
using System.Collections.Generic;
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
    /// 字典数据控制器
    /// </summary>
    public class BaseDictController : ApiController
    {

        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <param name="values">参数数据：唯一标识符\t属性类型</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        //public HttpResponseMessage Get(string cpuId, string values)
        //{
        //    try
        //    {
        //        var result = GlobalBLL.VerifyUserAndParamsByCpu(cpuId, values, 2, "BaseDictController_Get");
        //        if (!result)
        //            return JsonHelper.ReturnErrInfo(result.Info);

        //        var dt = new BaseDictBLL().SelectBaseDictInfoBy(result.Value[1]);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            return JsonHelper.DataTableToJson(dt);
        //        }
        //        else
        //        {
        //            return JsonHelper.StringToJson(CodeModel.QueryFail);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log("EmpSelfService.Api.BaseDictController.Get", ex);
        //        return JsonHelper.StringToJson(CodeModel.ErrSystem);
        //    }
        //}

        /// <summary>
        /// 获取字典参数
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string terminalNo)
        {
            try
            {
                var result = new GlobalBLL().GetUserByTerminalNo(terminalNo);
                if (!result)
                {
                    LogHelper.Log("GetBaseDict", "获取用户信息失败", string.Format("TerminalNo:{0}", terminalNo));
                    return JsonHelper.ReturnErrInfo(result.Info);
                }

                var dt = new BaseDictBLL().SelectBaseDictInfo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return JsonHelper.DataTableToJson(dt);
                }
                else
                {
                    return JsonHelper.StringToJson(CodeModel.QueryFail);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.BaseDictController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }
    }
}
