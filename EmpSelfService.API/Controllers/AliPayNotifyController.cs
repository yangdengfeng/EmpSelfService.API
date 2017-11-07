using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{
    /// <summary>
    /// 接收支付宝二维码支付通知的控制器
    /// </summary>
    public class AliPayNotifyController : ApiController
    {
        //
        // GET: /AliPayNotify/

        public string Post()
        {
            try
            {
                //todo 接收支付宝的二维码支付通知  并做相应处理
                return "success";
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.QRCodeController.Post", ex);
                return CodeModel.ErrSystem;
            }
        }

    }
}
