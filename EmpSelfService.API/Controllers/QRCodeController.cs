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
    /// 生成支付二维码控制器
    /// </summary>
    public class QRCodeController : ApiController
    {
        /// <summary>
        /// 生成支付二维码
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t设备终端编号\t交易类型\t交易金额\t交易方式\t卡号\t卡类型</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Post(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 7, "QRCodeController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                var resultQr = new QRCodeBLL().CreateQrUrl(terminalNo, rt[3], rt[2], rt[4], rt[5], rt[6]);
                
                return !resultQr ? JsonHelper.StringToJson(resultQr.Info) : JsonHelper.DataTableToJson(CodeModel.SUCCESS, resultQr.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.QRCodeController.Post", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            } 
        }

        /// <summary>
        /// 二维码支付状态查询 
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t交易订单号\t交易方式（1:WeChat 2:AliPay）</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 3, "QRCodeController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);
                var rt = result.Value;
                var resultQr = new QRCodeBLL().GetPayState(rt[1], rt[2]);
                return !resultQr ? JsonHelper.StringToJson(resultQr.Info) : JsonHelper.DataTableToJson(CodeModel.SUCCESS, resultQr.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.QRCodeController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }
    }
}
