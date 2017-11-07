using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EmpSelfService.DAL.DBEntity;
using EmpSelfService.Model;

namespace EmpSelfService.DAL
{
    public class QRCodeDAL
    {
        private DBHelper dbHelper = new DBHelper();

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CreateOrders(OrderInfoModel obj)
        {
            dbHelper.BeginTrans();
            try
            {
                //业务订单
                string strsql = "insert into OM_ORDERMANAGEMENT_TB(ORDER_ID,ORDER_NO,ORDER_TYPE,TRADE_DATE,CARD_NO,CARD_TYPE," +
                    " PHY_CARD_TYPE,RECHARGE_MONEY,RECHARGE_INDEX,TERMINAL_ID,ORDER_MONEY,STATUS,REFUNDFLAG,SETTFLAG,DATATYPE,NFC_LOCK_TIME,ERROR_CODE," +
                    " SETT_DATE,CREATION_TIME) values(OM_ORDERMANAGEMENT_TB_SEQ.NEXTVAL,'" + obj.ORDER_NO + "','" + obj.ORDER_TYPE + "'," + obj.TRADE_DATE + ",'" + obj.CARD_NO + "','" +
                    obj.CARD_TYPE + "','" + obj.PHY_CARD_TYPE + "'," + obj.RECHARGE_MONEY + "," + obj.RECHARGE_INDEX + ",'" + obj.TERMINAL_ID + "'," +
                    obj.ORDER_MONEY + ",'" + obj.STATUS + "','" + obj.REFUNDFLAG + "','" + obj.SETTFLAG + "','" + obj.DATATYPE + "'," + obj.NFC_LOCK_TIME + ",0,'" + obj.SETT_DATE + "'," + obj.CREATION_TIME + ")";

                dbHelper.RunCommand(strsql);
                //支付订单
                strsql = "insert into OM_PAYORDERMANAGEMENT_TB(" +
                "PAYORDER_ID,PAY_ORDER_NO,ORDER_ID,TERMINAL_ID,PAY_MONEY,STATUS,DATATYPE,PAY_LOCK_TIME," +
                "ERROR_CODE,SETT_DATE,CREATION_TIME) values(Om_Payordermanagement_Tb_Seq.Nextval,'" + obj.PAY_ORDER_NO + "','" +
                obj.ORDER_NO + "','" + obj.TERMINAL_ID + "'," + obj.PAY_MONEY + ",'" +
                obj.STATUS + "','" + obj.DATATYPE + "'," + obj.PAY_LOCK_TIME + ",'" + obj.ERROR_CODE + "','" +
                obj.SETT_DATE + "'," + obj.CREATION_TIME + ")";
                dbHelper.RunCommand(strsql);

                dbHelper.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                dbHelper.RollbackTrans();
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 更新业务订单和支付订单的状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateOrders(OrderInfoModel obj)
        {
            dbHelper.BeginTrans();
            try
            {
                //string strsql = "update OM_ORDERMANAGEMENT_TB set STATUS='" + obj.STATUS + "',RECHARGE_TIME= " + obj.RECHARGE_TIME + ",NFC_END_TIME=" + obj.NFC_END_TIME + " where ORDER_NO='" + obj.ORDER_NO + "'";
                ////DBHelper.DoExecuteNonQuery(strsql, CommandType.Text);
                //dbHelper.RunCommand(strsql);
                string strsql = "update OM_PAYORDERMANAGEMENT_TB set STATUS='" + obj.STATUS + "', PAY_END_TIME=" + obj.PAY_END_TIME + " where ORDER_ID='" + obj.ORDER_NO + "'";
                //return DBHelper.DoExecuteNonQuery(strsql, CommandType.Text);
                dbHelper.RunCommand(strsql);
                dbHelper.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                dbHelper.RollbackTrans();
                throw new Exception(ex.ToString());
            }
        }
    }
}
