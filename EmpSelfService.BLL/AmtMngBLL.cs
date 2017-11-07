using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.Common;
using EmpSelfService.DAL;
using EmpSelfService.Model;

namespace EmpSelfService.BLL
{
    public class AmtMngBLL
    {
        /// <summary>
        /// 钱箱更新
        /// </summary>
        /// <param name="terminalNo">设备终端号(PSAM卡号)</param>
        /// <param name="flag">增加或减少标识 1：增加 2：减少</param>
        /// <param name="amount">金额</param>
        /// <param name="opUser">操作用户</param>
        /// <returns></returns>
        public bool UpdateAmtTypeMng(string terminalNo, string flag, string amount, string opUser)
        {
            bool isOK = false;
            try
            {
                //余额操作记录入库
                var puAmtOperationRecordTB = new PuAmtOperationRecordTB
                {
                    SKTERMINALID = "",
                    CZTERMINALID = terminalNo,
                    SUMSTOCK =  amount,
                    AMT_OPERATION = flag,
                    CREATION_USER = opUser,
                    CREATION_TIME = "sysdate"
                };

                AmtMngDAL amtMngDal = new AmtMngDAL();
                amtMngDal.SaveOperationRecord(puAmtOperationRecordTB);

                //更新钱箱
                var puAmtTypeMngTB = new PuAmtTypeMngTB
                {
                    SKTERMINALID = "",
                    CZTERMINALID = terminalNo,
                    SUMSTOCK = (flag == "1" ? amount : "0"),
                    SUMSOLD = (flag == "1" ? "0" : "-" + amount),
                    SURPLUSAMT = (flag == "1" ? amount : "-" + amount),
                    SURPLUSNUM = flag,
                    UPDATE_USER = opUser,
                    UPDATE_TIME = "sysdate"
                };

                isOK = amtMngDal.UpdateAmtTypeMng(puAmtTypeMngTB);
            }
            catch (Exception ex)
            {
                LogHelper.Log("AmtMngBLL.UpdateAmtTypeMng", ex);
            }

            return isOK;
        }

        /// <summary>
        /// 根据终端代号获取终端的余额
        /// </summary>
        /// <param name="terminalNO"></param>
        /// <returns></returns>
        public DataTable SelBalanceByTerminalNo(string terminalNO)
        {
            var dt = new DataTable();
            try
            {
                AmtMngDAL amtMngDal = new AmtMngDAL();
                dt = amtMngDal.SelBalanceByTerminalNo(terminalNO);
            }
            catch (Exception ex)
            {
                LogHelper.Log("AmtMngBLL.SelBalanceByTerminalNo", ex);
            }

            return dt;
        }
    }
}
