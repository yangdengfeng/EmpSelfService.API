using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using EmpSelfService.Common;
using EmpSelfService.DAL;
using EmpSelfService.Model;

namespace EmpSelfService.BLL
{
    public class OrderBLL
    {
        /// <summary>
        /// 生成业务订单
        /// </summary>
        /// <param name="orderNo">订单编号</param>
        /// <param name="orderType">订单类型</param>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="cardPhysicsType">卡物理类型</param>
        /// <param name="cardBalance">卡余额</param>
        /// <param name="terminalNo">终端编号</param>
        /// <param name="orderAmt">交易金额</param>
        /// <param name="txnWay">交易方式</param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public ResultBase<string> SaveOrder(string orderNo, string orderType, string cardNo, string cardType, string cardPhysicsType,
            string cardBalance, string terminalNo, string orderAmt, string txnWay, string createTime)
        {
            bool flag = false;
            orderNo = (string.IsNullOrWhiteSpace(orderNo) ? WeChatPayApi.GenerateOutTradeNo(terminalNo) : orderNo);
            try
            {
                //todo 订单重复利用 售卡验证  验证该设备是否已有售卡订单
                var order = new OrderManagementTB
                {
                    ORDER_NO = orderNo,
                    ORDER_TYPE = orderType == "1" ? "1" : "2",
                    TRADE_DATE = createTime,
                    CARD_NO = cardNo,
                    CARD_TYPE = cardType,
                    PHY_CARD_TYPE = "3",
                    RECHARGE_MONEY = cardBalance,
                    RECHARGE_INDEX = "",
                    TERMINAL_ID = terminalNo,
                    RECHARGE_TIME = createTime,
                    ORDER_MONEY = orderAmt,
                    STATUS = "0",
                    REFUNDFLAG = "1",
                    SETTFLAG = "0",
                    DATATYPE = txnWay,
                    NFC_LOCK_TIME = createTime,
                    NFC_END_TIME = "",
                    ERROR_CODE = "0",
                    SETT_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    REMARK = "",
                    CREATION_TIME = createTime,
                    PAY_ORDER_NO = WeChatPayApi.GenerateOutPayTradeNo(terminalNo),
                    PAY_LOCK_TIME = createTime,
                    PAY_MONEY = orderAmt
                };

                OrderDAL orderDao = new OrderDAL();
                if (orderDao.SaveOrder(order))
                {
                    flag = true;
                }
                if (orderDao.SaveOrderPay(order))
                {
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("OrderBLL.SaveOrder", ex, txnWay == "0" ? "Cash" : (txnWay == "1" ? "WeiXin" : "AliPay"));
            }

            return new ResultBase<string>(flag, orderNo);
        }


        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="balance">卡余额</param>
        /// <param name="chargeTime">充值时间</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="cardNo">卡号</param>
        /// <returns></returns>
        public bool ModifyOrder(string orderNo, string orderStatus, string balance, string chargeTime,
            string cardType, string cardNo)
        {
            bool b = false;
            try
            {
                OrderDAL orderDao = new OrderDAL();
                string err;
                b = orderDao.UpdateOrder(orderNo, orderStatus, balance, chargeTime, cardNo, cardType, out err);
                //if (string.IsNullOrEmpty(err))
                //    err = "订单更新成功！";

                //LogHelper.Log("ModifyOrder", "更新状态", string.Format("{0:yyyy-MM-dd HH:mm:ss}-{1}-{2}-{3}-{4}", 
                //    DateTime.Now, orderNo, orderStatus, b.ToString(), err));
            }
            catch (Exception ex)
            {
                LogHelper.Log("OrderBLL.ModifyOrder", ex);
            }
            
            return b;
        }


        /// <summary>
        /// 获取订单交易明细表
        /// </summary>
        /// <param name="beginDate">结算开始日期</param>
        /// <param name="endDate">结算结束日期</param>
        /// <param name="sellTerId">售卡终端号</param>
        /// <param name="rechangTerId">充值终端号</param>
        /// <returns></returns>
        public DataTable SelectOrderTransDetailBy(string beginDate, string endDate, string sellTerId, string rechangTerId)
        {
            var dt = new DataTable();
            try
            {
                OrderDAL dal = new OrderDAL();
                dt = dal.SelectOrderTransDetailBy(beginDate, endDate, sellTerId, rechangTerId);
            }
            catch (Exception ex)
            {
                LogHelper.Log("OrderBLL.SelectOrderTransDetailBy", ex);
            }

            return dt;
        }

        /// <summary>
        /// 获取交易失败的订单信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sellTerId"></param>
        /// <param name="rechangTerId"></param>
        /// <returns></returns>
        public DataTable SelectFailureOrderDetailBy(string beginDate, string endDate, string status, string sellTerId, string rechangTerId)
        {
            var dt = new DataTable();
            try
            {
                OrderDAL dal = new OrderDAL();
                dt = dal.SelectFailureOrderDetailBy(beginDate, endDate, status, sellTerId, rechangTerId);
            }
            catch (Exception ex)
            {
                LogHelper.Log("OrderBLL.SelectFailureOrderDetailBy", ex);
            }

            return dt;
        }

        /// <summary>
        /// 现金支付订单数据统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sellTerId"></param>
        /// <param name="rechangTerId"></param>
        /// <returns></returns>
        public DataTable SelectCashOrderData(string beginDate, string endDate, string sellTerId, string rechangTerId)
        {
            var dt = new DataTable();
            try
            {
                OrderDAL dal = new OrderDAL();
                dt = dal.SelectCashOrderData(beginDate, endDate, sellTerId, rechangTerId);
            }
            catch (Exception ex)
            {
                LogHelper.Log("OrderBLL.SelectCashOrderData", ex);
            }

            return dt; 
        }
    }
}
