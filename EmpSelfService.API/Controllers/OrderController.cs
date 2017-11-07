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
    /// 订单控制器
    /// </summary>
    public class OrderController : ApiController
    {
        /// <summary>
        ///  生成订单
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t订单编号\t订单类型\t卡号\t卡类型\t卡物理类型\t卡余额\t终端编号\t交易金额\t交易方式\t交易时间</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Post(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 11, "OrderController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                var rst = new OrderBLL().SaveOrder(rt[1], rt[2], rt[3], rt[4], rt[5], rt[6], rt[7], rt[8], rt[9], rt[10]);
                return rst ? JsonHelper.StringToJson1(CodeModel.SUCCESS, rst.Info) : JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.OrderController.Post", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t订单单号\t订单状态\t卡余额\t充值时间\t卡号\t卡类型</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage ModifyOrder(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 7, "ModifyOrder");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                bool flag = new OrderBLL().ModifyOrder(rt[1], rt[2], rt[3], rt[4], rt[5], rt[6]);
                return JsonHelper.StringToJson(!flag ? CodeModel.ErrSystem : CodeModel.SUCCESS);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.OrderController.ModifyOrder", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

        /// <summary>
        /// 获取订单交易明细
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="values">参数数据：唯一标识符\t结算开始日期\t结算结束日期\t售卡终端号\t充值终端号</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage GetOrderTransDetail(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 5, "GetOrderTransDetail");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                var dt = new OrderBLL().SelectOrderTransDetailBy(rt[1], rt[2], rt[3], rt[4]);
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
                LogHelper.Log("EmpSelfService.Api.OrderController.GetOrderTransDetail", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

        /// <summary>
        /// 获取交易失败的订单信息
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <param name="values">参数数据：唯一标识符\t结算开始日期\t结算结束日期\t类型\t售卡终端号\t充值终端号</param>
        /// <returns></returns>
        public HttpResponseMessage GetFailureOrderDetail(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 6, "GetFailureOrderDetail");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                var dt = new OrderBLL().SelectFailureOrderDetailBy(rt[1], rt[2], rt[3], rt[4], rt[5]);
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
                LogHelper.Log("EmpSelfService.Api.OrderController.GetFailureOrderDetail", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

        /// <summary>
        /// 现金支付订单数据统计
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public HttpResponseMessage GetCashOrderData(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParams(terminalNo, values, 5, "GetCashOrderData");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                var rt = result.Value;
                var dt = new OrderBLL().SelectCashOrderData(rt[1], rt[2], rt[3], rt[4]);
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
                LogHelper.Log("EmpSelfService.Api.OrderController.GetCashOrderData", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }
       
    }
}