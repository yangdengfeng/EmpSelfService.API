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
    public class CardMngDAL
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// 将卡操作记录存入数据库
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SaveOperationRecord(PuCardOperationRecordTB obj)
        {
            StringBuilder sb = new StringBuilder();
            //操作前卡数量
            sb.AppendLine(" select * from PUCARDTYPEMNGSTB where SKTERMINALID='" + obj.SKTERMINALID + "'");
            DataTable dt = DBHelper.GetDataTable(sb.ToString(), CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.OLD_CARD_NUMBER = dt.Rows[0]["SURPLUSCARD"].ToString();
            }

            obj.OLD_CARD_NUMBER = string.IsNullOrEmpty(obj.OLD_CARD_NUMBER) ? "0" : obj.OLD_CARD_NUMBER;
            sb.Clear();
            sb.AppendLine(" insert into PUCARDOPERATIONRECORDTB(ID,SKTERMINALID,CZTERMINALID,CARDTYPE_CODE ");
            sb.AppendLine(" ,CARD_OPERATION,CARD_NUMBER,CREATION_USER,CREATION_TIME,REMARK,OLD_CARD_NUMBER) ");
            sb.AppendLine(" values( PUCARDOPERATIONRECORDTB_SEQ.Nextval,'" + obj.SKTERMINALID + "','" + obj.CZTERMINALID +
                          "','" + obj.CARDTYPE_CODE + "','" + obj.CARD_OPERATION + "',");
            sb.AppendLine("'" + obj.CARD_NUMBER + "','" + obj.CREATION_USER + "'," + obj.CREATION_TIME + ",'" +
                          obj.REMARK + "','" + obj.OLD_CARD_NUMBER + "') ");

            return DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);
        }

        /// <summary>
        /// 更新卡库存管理表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateCardTypeMng(PuCardTypeMngTB obj)
        {
            StringBuilder sb = new StringBuilder();
            //查看是否有数据
            sb.AppendLine(" select * from PUCARDTYPEMNGSTB where 1=1 ");
            if (obj.SKTERMINALID != "")
            {
                sb.AppendLine(" and SKTERMINALID='" + obj.SKTERMINALID + "'");
            }
            if (obj.CZTERMINALID != "")
            {
                sb.AppendLine(" and CZTERMINALID='" + obj.CZTERMINALID + "' ");
            }
            DataTable dt = DBHelper.GetDataTable(sb.ToString(), CommandType.Text);
            sb.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.AppendLine(" update PUCARDTYPEMNGSTB set sumstock=replace(sumstock,sumstock,sumstock+'" +
                              obj.SUMSTOCK + "') ");
                sb.AppendLine(" ,SURPLUSCARD=replace(SURPLUSCARD,SURPLUSCARD,SURPLUSCARD+'" + obj.SURPLUSCARD + "') ");
                sb.AppendLine(" ,SUMSOLD=replace(SUMSOLD,SUMSOLD,SUMSOLD+'" + obj.SUMSOLD + "') ");
                sb.AppendLine(" ,UPDATE_USER='" + obj.UPDATE_USER + "',UPDATE_TIME=sysdate ");
                sb.AppendLine(" where 1=1 ");
                if (obj.SKTERMINALID != "")
                {
                    sb.AppendLine(" and SKTERMINALID='" + obj.SKTERMINALID + "'");
                }
                if (obj.CZTERMINALID != "")
                {
                    sb.AppendLine(" and CZTERMINALID='" + obj.CZTERMINALID + "' ");
                }

                return DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);
            }
            else
            {
                sb.AppendLine(
                    " INSERT INTO PUCARDTYPEMNGSTB(ID,SKTERMINALID,CZTERMINALID,CARDTYPE_CODE,SUMSTOCK,SUMSOLD, ");
                sb.AppendLine(
                    "  SURPLUSCARD,CREATION_USER,CREATION_TIME,UPDATE_USER,UPDATE_TIME,REMARK)VALUES(PUAMTTYPEMNGSTB_SEQ.Nextval, ");
                sb.AppendLine(" '" + obj.SKTERMINALID + "','" + obj.CZTERMINALID + "','" + obj.CARDTYPE_CODE + "','" +
                              obj.SUMSTOCK + "',0");
                sb.AppendLine(" ,'" + obj.SURPLUSCARD + "','000001',sysdate,'','','" + obj.REMARK + "') ");

                return DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);
            }
        }

        /// <summary>
        /// 根据终端代号获取终端卡箱信息
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <returns></returns>
        public DataTable SelCardByTerminalNo(string terminalNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select SUMSTOCK,SUMSOLD,SURPLUSCARD,UPDATE_TIME from PUCARDTYPEMNGSTB where 1=1 ");
            if (terminalNo != "")
            {
                strSql.AppendLine(" and SKTERMINALID='" + terminalNo + "'");
            }

            DataTable dtResult;
            _dbHelper.RunCommand(strSql.ToString(), out dtResult);
            return dtResult;
        }

        /// <summary>
        /// 查询卡是否是挂失卡
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public bool SelLossCardByCardNo(string cardno)
        {
            string strSql =
                "select * from xnzxykt.omcardmanagelisttb where status='1' and to_number(card_code)=to_number('" +
                cardno + "')";

            DataTable dtResult;
            _dbHelper.RunCommand(strSql, out dtResult);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 卡置为可疑
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetSusCard(string cardNo, string companyId, string userId)
        {
            string strSql1 = "";
            try
            {
                string remark = "置为可疑";
                string strSql = "select * from OMSUSCARDLISTTB where CARD_CODE='" + cardNo + "'";
                DataTable dtResult;
                _dbHelper.RunCommand(strSql, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    strSql1 = @"update OMSUSCARDLISTTB set STATUS = '1'," +
                              " UPDATE_USER=" + userId + ", " +
                              " COMPANYID='" + companyId + "', " +
                              " REMARK='" + remark + "', " +
                              " UPDATE_TIME=sysdate where CARD_CODE='" + cardNo + "'";
                }
                else
                {
                    strSql1 = @"INSERT INTO OMSUSCARDLISTTB (
                                ID
                                ,Card_Code
                                ,Creation_Time
                                ,Creation_User
                                ,Status
                                ,COMPANYID
                                ,REMARK)
                            VALUES(OMSUSCARDLISTTB_SEQ.nextval
                                ,'{0}',sysdate,{1},'1','{2}','{3}')";

                    strSql1 = string.Format(strSql1, cardNo, userId, companyId, remark);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("SetSusCard", ex);
            }

            return DBHelper.DoExecuteNonQuery(strSql1, CommandType.Text);
        }

        /// <summary>
        /// 删除可疑
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveSusCard(string cardNo, string userId)
        {
            string strSql1 = "";
            try
            {
                string remark = "删除可疑";
                string strSql = "select * from OMSUSCARDLISTTB where CARD_CODE='" + cardNo + "'";
                DataTable dtResult;
                _dbHelper.RunCommand(strSql, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    strSql1 = @"update OMSUSCARDLISTTB set STATUS = '0'," +
                              " UPDATE_USER=" + userId + ", " +
                              //" COMPANYID='" + companyId + "', " +
                              " REMARK='" + remark + "', " +
                              " UPDATE_TIME=sysdate where CARD_CODE='" + cardNo + "'";
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("RemoveSusCard", ex);
            }

            return DBHelper.DoExecuteNonQuery(strSql1, CommandType.Text);
        }

        /// <summary>
        /// 保存从行业上来的交易记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveTxnIndustryRecord(TransRecordModel model)
        {
            string strSql = "";
            try
            {
                strSql = @"insert into TDTXNINDUSTRYRECORD(tac,
                                                                  cardid,
                                                                  txntype,
                                                                  txnsubtype,
                                                                  terminalid,
                                                                  posoprid,
                                                                  txndate,
                                                                  TXNDATESN,
                                                                  TXNTIMESN,
                                                                  posseq,
                                                                  txnamt,
                                                                  txnvalue,
                                                                  txnaftbal,
                                                                  cardcnt,
                                                                  preterminalid,
                                                                  pretxntype,
                                                                  pretxndate,
                                                                  pretxnamt,
                                                                  orgcode,
                                                                  citycode,
                                                                  mediatype,
                                                                  samseq,
                                                                  cardkind,
                                                                  txnappflag,
                                                                  errcode)
                                        values('{0}','{1}',{2},'{3}','{4}','{5}','{6}',{7},{8},{9},{10},
                                        {11},{12},{13},'{14}',{15},'{16}',{17},'{18}','{19}',{20},'{21}','{22}','{23}',{24})";

                strSql = string.Format(strSql, model.TAC,
                    model.CARDNO,
                    Convert.ToInt32(model.TXNTYPE),
                    model.TXNSUBTYPE,
                    model.TERMINALID,
                    model.POSOPRID,
                    model.TXNDATE,
                    Convert.ToInt32(model.TXNDATESN),
                    Convert.ToInt32(model.TXNTIMESN),
                    Convert.ToInt32(model.POSSEQ),
                    Convert.ToInt32(model.TXNAMT),
                    Convert.ToInt32(model.TXNVALUE),
                    Convert.ToInt32(model.TXNAFTBAL),
                    Convert.ToInt32(model.CARDCNT),
                    model.PRETERMINALID,
                    Convert.ToInt32(model.PRETXNTYPE),
                    model.PRETXNDATE,
                    Convert.ToInt32(model.PRETXNAMT),
                    model.ORGCODE,
                    model.CITYCODE,
                    model.MEDIATYPE,
                    Convert.ToInt32(model.SAMSEQ),
                    model.CARDKIND,
                    model.TXNAPPFLAG,
                    Convert.ToInt32(model.ERRCODE)
                );
            }
            catch (Exception ex)
            {
                LogHelper.Log("SaveTxnIndustryRecord", ex);
            }

            bool result = DBHelper.DoExecuteNonQuery(strSql, CommandType.Text);
            LogHelper.Log("SaveTxnIndustryRecord", "SaveTxnIndustryRecord >> " + result, "\r\n" + strSql);
            return result;
        }




        #region 获取数据库TAC验证密钥
        /// <summary>
        /// 验证密钥
        /// </summary>
        /// <param name="cardid">卡号</param>
        /// <returns></returns>
        public DataTable GetCardKeyID(string cardid)
        {
            StringBuilder _sbSql = new StringBuilder();
            try
            {
                _sbSql.Clear();
                _sbSql.Append(" SELECT TACKEY FROM sbacpucardcodetb  where cardno='" + cardid + "'");
                DataTable dt = new DataTable();
                _dbHelper.RunCommand(_sbSql.ToString(), out dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("GetCardKeyID(获取TACKEY失败):" + ex.Message);
            }
        }
        #endregion

    }
}
