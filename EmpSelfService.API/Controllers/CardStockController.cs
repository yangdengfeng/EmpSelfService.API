using System;
using System.Collections.Generic;
using System.Data;
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
    /// 卡库存控制器
    /// </summary>
    public class CardStockController : ApiController
    {
        /// <summary>
        /// 卡库存更新
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t设备终端号\t增加或减少标识\t数量\t操作用户</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage ModifyCardStock(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 5, "ModifyCardStock");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                bool flag = new CardMngBLL().UpdateCardTypeMng(rt[1], rt[2], rt[3], rt[4]);
                return JsonHelper.StringToJson(!flag ? CodeModel.ErrSystem : CodeModel.SUCCESS);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.CardStockController.ModifyCardStock", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

        /// <summary>
        /// 根据终端代号获取终端卡箱信息
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t设备终端号</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Get(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 2, "CardStockController_Get");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var dt = new CardMngBLL().SelCardByTerminalNo(result.Value[1]);
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
                LogHelper.Log("EmpSelfService.Api.CardStockController.Get", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

    }
}
