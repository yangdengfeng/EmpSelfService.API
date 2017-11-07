/* ==============================================================================
 * 功能名称：TelStateBLL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/4/21 10:36:42
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{
    public class TelStateController : ApiController
    {
        
        /// <summary>
        /// 保存设备状态信息
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：
        /// 唯一标识符\tCPU编号\t售卡设备ID\t充值设备ID\t售卡设备状态\t充值设备状态\t发卡机状态\t
        /// 打印机状态\t钱箱状态\tUPS使用状态\tUPS电量百分比\t本地自助服务软件的版本号
        /// </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 12, "TelStateController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                OmTerminalStateTB om = new OmTerminalStateTB();
                om.CpuId = rt[1];
                om.TimeLast = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                om.SKTerminalID = rt[2];
                om.CZTerminalID = rt[3];
                om.SKState = rt[4];
                om.CZState = rt[5];
                om.CardIssuerState = rt[6];
                om.PrinterState = rt[7];
                om.CashboxState = rt[8];
                om.UpsState = rt[9];
                om.UpsPercentage = rt[10];
                om.SoftwareVer = rt[11];
                bool flag = new TelStateBLL().SaveTerState(om);
                return JsonHelper.StringToJson(!flag ? CodeModel.ErrSystem : CodeModel.SUCCESS);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.TelStateController.Post", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

    }
}
