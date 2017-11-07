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
    /// 参数数据控制器
    /// </summary>
    public class BaseParamController : ApiController
    {
        /// <summary>
        /// 获取参数信息
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <param name="values">参数数据：唯一标识符\t参数类型</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string cpuId, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParamsByCpu(cpuId, values, 2, "BaseParamController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var dt = new BaseParamBLL().QueryBaseParamInfo(result.Value[1]);
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
                LogHelper.Log("EmpSelfService.Api.BaseParamController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

       
    }
}
