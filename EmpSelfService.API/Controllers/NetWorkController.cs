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
    /// 网点控制器
    /// </summary>
    public class NetWorkController : ApiController
    {
        /// <summary>
        /// 获取网点信息
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t城市代码</param> 
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 2, "NetWorkController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var dt = new NetWorkBLL().SelectNetWorkInfoBy(result.Value[1]);
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
                LogHelper.Log("EmpSelfService.Api.NetWorkController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

    }
}
