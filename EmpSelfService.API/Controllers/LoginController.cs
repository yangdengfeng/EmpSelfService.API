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
    /// 登陆控制器
    /// </summary>
    public class LoginController : ApiController
    {

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t用户名\t密码</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 3, "LoginController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var flag = new GlobalBLL().UserLogin(result.Value[1], result.Value[2]);
                return JsonHelper.StringToJson(!flag ? CodeModel.QueryFail : CodeModel.SUCCESS);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.AccountController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }
    }
}
