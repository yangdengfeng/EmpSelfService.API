using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.Common;
using EmpSelfService.DAL.DBEntity;
using EmpSelfService.Model;

namespace EmpSelfService.DAL
{
    public class OrderDAL
    {

        /// <summary>
        /// 将业务订单信息存入数据库
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool SaveOrder(OrderManagementTB order)
        {
            bool b = false;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine("select * from OM_ORDERMANAGEMENT_TB where ORDER_NO='" + order.ORDER_NO + "'");
                DataTable dt = DBHelper.GetDataTable(sb.ToString(), CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                    return true;

                sb.Clear();
                sb.AppendLine(
                    " insert into OM_ORDERMANAGEMENT_TB(ORDER_ID,ORDER_NO,ORDER_TYPE,TRADE_DATE,CARD_NO,CARD_TYPE, ");
                sb.AppendLine(
                    " PHY_CARD_TYPE,TERMINAL_ID,ORDER_MONEY,STATUS,REFUNDFLAG,SETTFLAG,DATATYPE,NFC_LOCK_TIME,ERROR_CODE, ");
                sb.AppendLine(" SETT_DATE,CREATION_TIME) values( OM_ORDERMANAGEMENT_TB_SEQ.Nextval,'" + order.ORDER_NO +
                              "','" + order.ORDER_TYPE + "',to_date('" + order.TRADE_DATE + "', 'yyyy-MM-dd hh24:mi:ss'),'" + order.CARD_NO + "' ");
                sb.AppendLine(" ,'" + order.CARD_TYPE + "','" + order.PHY_CARD_TYPE + "','" + order.TERMINAL_ID + "', ");
                sb.AppendLine("'" + order.ORDER_MONEY + "','" + order.STATUS + "','" + order.REFUNDFLAG + "','" +
                              order.SETTFLAG + "','" + order.DATATYPE + "',to_date('" + order.NFC_LOCK_TIME + "', 'yyyy-MM-dd hh24:mi:ss'),0,'" +
                              order.SETT_DATE + "', to_date('" + order.CREATION_TIME + "', 'yyyy-MM-dd hh24:mi:ss')) ");

                b = DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);

                LogHelper.Log("OrderDAL", "OrderDAL.SaveOrder >> " + b.ToString(), "\r\n" + sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return b;
        }


        /// <summary>
        /// 将支付订单信息存入数据库
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool SaveOrderPay(OrderManagementTB order)
        {
            bool b = false;
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("select * from OM_PAYORDERMANAGEMENT_TB where ORDER_ID='" + order.ORDER_NO + "'");
                DataTable dt = DBHelper.GetDataTable(sb.ToString(), CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                    return true;

                sb.Clear();
                sb.AppendLine(
                    " insert into OM_PAYORDERMANAGEMENT_TB(PAYORDER_ID,PAY_ORDER_NO,ORDER_ID,TERMINAL_ID,PAY_MONEY,STATUS,DATATYPE,PAY_LOCK_TIME, ");
                sb.AppendLine(" ERROR_CODE,SETT_DATE,CREATION_TIME) values(OM_PAYORDERMANAGEMENT_TB_SEQ.Nextval,'" +
                              order.PAY_ORDER_NO + "','" + order.ORDER_NO + "','" + order.TERMINAL_ID + "' ");
                sb.AppendLine(" ," + order.PAY_MONEY + ",'" + order.STATUS + "','" + order.DATATYPE + "', to_date('" + order.PAY_LOCK_TIME + "', 'yyyy-MM-dd hh24:mi:ss'),'" + order.ERROR_CODE + "', ");
                sb.AppendLine(" '" + order.SETT_DATE + "',to_date('" + order.CREATION_TIME + "', 'yyyy-MM-dd hh24:mi:ss')) ");

                b = DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);

                LogHelper.Log("OrderDAL", "OrderDAL.SaveOrderPay >> " + b.ToString(), "\r\n" + sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return b;
        }


        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateOrder(string orderNo, string orderStatus, string balance, string chargeTime,
            string cardType, string cardNo, out string err)
        {
            err = "";
            string strRefund = orderStatus == "4" ? "0" : "1";
            //string strRefund = "1";   //测试退款用
            StringBuilder sb = new StringBuilder();

            try
            {
                decimal rechargeMoney = string.IsNullOrEmpty(balance) ? 0 : Convert.ToDecimal(balance);
                DateTime ctime = DateTime.ParseExact(chargeTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                chargeTime = ctime.ToString("yyyy-MM-dd HH:mm:ss");

                sb.AppendLine(" update OM_PAYORDERMANAGEMENT_TB set " +
                              "STATUS='" + orderStatus + "'," +
                              "PAY_END_TIME=to_date('" + chargeTime + "', 'yyyy-MM-dd hh24:mi:ss') " +
                              "where ORDER_ID='" + orderNo + "' ");
                if (!DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text))
                {
                    err = "OM_PAYORDERMANAGEMENT_TB更新失败！";
                }

                LogHelper.Log("OrderDAL", "OrderDAL.UpdatePayOrder >> " + err, "\r\n" + sb.ToString());

                sb.Clear();
                sb.AppendLine(" update OM_ORDERMANAGEMENT_TB set STATUS='" + orderStatus + "'," +
                              "RECHARGE_MONEY=" + rechargeMoney + ",REFUNDFLAG='" + strRefund + "',");
                if (!string.IsNullOrEmpty(cardNo))
                {
                    sb.AppendLine(" CARD_NO='" + cardNo + "',");
                }
                if (!string.IsNullOrEmpty(cardType))
                {
                    sb.AppendLine(" CARD_TYPE='" + cardType + "',");
                }
                sb.AppendLine("RECHARGE_TIME= to_date('" + chargeTime + "', 'yyyy-MM-dd hh24:mi:ss')" +
                    ",NFC_END_TIME=sysdate where ORDER_NO='" + orderNo + "' ");
                if (!DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text))
                {
                    err += " & OM_ORDERMANAGEMENT_TB更新失败！";
                }

                LogHelper.Log("OrderDAL", "OrderDAL.UpdateOrder >> " + err, "\r\n" + sb.ToString());

                if (string.IsNullOrEmpty(err))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT SETT_DATE,SUM(CASE WHEN (ORDER_TYPE=1 AND DATATYPE=0 AND (STATUS=4 OR STATUS=0)) OR (ORDER_TYPE=1 AND DATATYPE<>0 AND STATUS=4) THEN 1 ELSE 0 END)SELLSUCNUM, ");
            sbSql.Append(" SUM(CASE WHEN (ORDER_TYPE=1 AND DATATYPE=0 AND (STATUS=4 OR STATUS=0)) OR (ORDER_TYPE=1 AND DATATYPE<>0 AND STATUS=4) THEN ORDER_MONEY/100 ELSE 0 END)SELLSUCAMT, ");
            sbSql.Append(" SUM(CASE WHEN ORDER_TYPE=1 AND STATUS=3 THEN 1 ELSE 0 END)SELLFAILNUM, ");
            sbSql.Append(" SUM(CASE WHEN ORDER_TYPE=1 AND STATUS=3 THEN ORDER_MONEY/100 ELSE 0 END)SELLFAILAMT, ");
            sbSql.Append(" SUM(CASE WHEN (ORDER_TYPE=2 AND DATATYPE=0 AND (STATUS=4 OR STATUS=0)) OR (ORDER_TYPE=2 AND DATATYPE<>0 AND STATUS=4) THEN 1 ELSE 0 END)RECHANGSUCNUM, ");
            sbSql.Append(" SUM(CASE WHEN (ORDER_TYPE=2 AND DATATYPE=0 AND (STATUS=4 OR STATUS=0)) OR (ORDER_TYPE=2 AND DATATYPE<>0 AND STATUS=4) THEN ORDER_MONEY/100 ELSE 0 END)RECHANGSUCAMT, ");
            sbSql.Append(" SUM(CASE WHEN ORDER_TYPE=2 AND STATUS=3 THEN 1 ELSE 0 END)RECHANGFAILNUM, ");
            sbSql.Append(" SUM(CASE WHEN ORDER_TYPE=2 AND STATUS=3 THEN ORDER_MONEY/100 ELSE 0 END)RECHANGFAILAMT, ");
            sbSql.Append(" SUM(ORDER_MONEY/100)ORDER_MONEY ");
            sbSql.Append("FROM OM_ORDERMANAGEMENT_TB ");
            sbSql.Append("WHERE SETT_DATE>='" + beginDate + "' AND SETT_DATE<='" + endDate + "'");
            sbSql.Append(" AND TERMINAL_ID in ('" + sellTerId + "','" + rechangTerId + "')");
            sbSql.Append(" GROUP BY SETT_DATE ORDER BY SETT_DATE ");

            var dtResult = DBHelper.GetDataTable(sbSql.ToString(), CommandType.Text);
            return dtResult;
        }


        /// <summary>
        /// 获取交易失败的订单信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sellTerId"></param>
        /// <param name="rechangTerId"></param>
        /// <returns></returns>
        public DataTable SelectFailureOrderDetailBy(string beginDate, string endDate, string status, string sellTerId,
            string rechangTerId)
        {

            string strSql = @"select o.order_no ORDERID, o.card_no CARDNO,
                            (case when o.order_type='1' then '售卡' else '充值' end) ORDERTYPE,
                            (case when o.datatype='0' then '现金' when o.datatype='1' then '微信' when o.datatype='2' then '支付宝' else '银行卡' end) DATATYPE,
                            p.pay_money/100 PAYMONEY,
                            o.terminal_id TERMINALID,
                            p.pay_end_time PAYTIME
                            from om_ordermanagement_tb o left join om_payordermanagement_tb p on p.order_id=o.order_no
                            where o.sett_date >= '{0}' and  o.sett_date <= '{1}' ";

            if (status == "0")
            {
                strSql += " and ((o.datatype='0' and o.status='-1') or (o.datatype<>'0' and o.status='0'))";
            }
            else if (status == "3")
            {
                strSql += " and o.status='3'"; 
            }
            strSql = string.Format(strSql, beginDate, endDate, status);
            strSql += " AND o.TERMINAL_ID in ('" + sellTerId + "','" + rechangTerId + "')";
            strSql += " ORDER BY o.Creation_time desc";

            var dtResult = DBHelper.GetDataTable(strSql, CommandType.Text);
            return dtResult;
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
            string strSql = @"select 
                            SUM(CASE WHEN o.DATATYPE='0' THEN o.ORDER_MONEY/100 ELSE 0 END)TOTALAMT,
                            SUM(CASE WHEN o.DATATYPE='0' AND (o.STATUS='4' OR o.STATUS='0') THEN o.ORDER_MONEY/100 ELSE 0 END)SUCAMT,
                            SUM(CASE WHEN o.DATATYPE='0' AND o.STATUS='3' THEN o.ORDER_MONEY/100 ELSE 0 END)FAILAMT
                            from om_ordermanagement_tb o 
                            where o.sett_date >= '{0}' and  o.sett_date <= '{1}'";

            strSql = string.Format(strSql, beginDate, endDate);
            strSql += " AND o.TERMINAL_ID in ('" + sellTerId + "','" + rechangTerId + "')";
            var dtResult = DBHelper.GetDataTable(strSql, CommandType.Text);
            return dtResult;
        }

    }
}
