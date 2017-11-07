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
    /// 可疑卡号
    /// </summary>
    public class SusCardController : ApiController
    {

        /// <summary>
        /// 可疑卡号(添加/去掉)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="values">参数数据：唯一标识符\t卡号\t类型</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string companyId, string values)
        {
            bool b = false;
            try
            {
                var result = GlobalBLL.VerifyUserAndParamsNew(companyId, values, 3, "SusCardController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = new GlobalBLL().GetUserByCompanyId(companyId);
                if (rt)
                {
                    CardMngBLL bll = new CardMngBLL();
                    if (result.Value[2] == "1")      //添加可疑卡号
                    {
                        b = bll.SetSusCard(result.Value[1], companyId, rt.Value.Rows[0][0].ToString());
                    }
                    else if (result.Value[2] == "0")   //去掉可疑卡号
                    {
                        b = bll.RemoveSusCard(result.Value[1], rt.Value.Rows[0][0].ToString());
                    }
                }

                return JsonHelper.StringToJson3(CodeModel.SUCCESS, b ? "0" : "1" );
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.SusCardController.Post", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }
    }
}
