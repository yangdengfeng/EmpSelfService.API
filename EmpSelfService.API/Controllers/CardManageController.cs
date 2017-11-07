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
    /// 卡务管理控制器
    /// </summary>
    public class CardManageController : ApiController
    {

        /// <summary>
        /// 查询是否黑名单卡(自助终端)
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t卡号</param>
        /// <returns></returns>
        public HttpResponseMessage GetLossCard(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 2, "CardManageController_GetLossCard");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                bool flag = new CardMngBLL().SelLossCardByCardNo(rt[1]);
                return JsonHelper.StringToJson(flag ? "0" : "1");
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.CardManageController.GetLossCard", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


        /// <summary>
        /// 查询是否黑名单卡(企业)
        /// </summary>
        /// <param name="companyId">企业编号</param>
        /// <param name="values">参数数据：唯一标识符\t卡号</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string companyId, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParamsNew(companyId, values, 2, "CardManageController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                bool flag = new CardMngBLL().SelLossCardByCardNo(rt[1]);
                return JsonHelper.StringToJson3(CodeModel.SUCCESS, flag ? "0" : "1");
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.CardManageController.GetLossCard", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


    }
}
