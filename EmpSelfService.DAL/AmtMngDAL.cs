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
    public class AmtMngDAL
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// 将钱箱操作记录存入数据库
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SaveOperationRecord(PuAmtOperationRecordTB obj)
        {
            bool b = false;
            StringBuilder strSql = new StringBuilder();

            try
            {
                //操作前金额
                strSql.AppendLine(" select * from PUAMTTYPEMNGTB where CZTERMINALID='" + obj.CZTERMINALID + "'");
                DataTable dt = DBHelper.GetDataTable(strSql.ToString(), CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.SUMSOLD = dt.Rows[0]["SURPLUSAMT"].ToString();
                }

                strSql.Clear();
                strSql.AppendLine(" insert into PUAMTOPERATIONRECORDTB(ID,SKTERMINALID,CZTERMINALID,SUMSOLD,SUMSTOCK ");
                strSql.AppendLine(" ,CREATION_USER,CREATION_TIME,REMARK,AMT_OPERATION) ");
                strSql.AppendLine(" values(PUAMTOPERATIONRECORDTB_SEQ.NEXTVAL,'" + obj.SKTERMINALID + "','" +
                                  obj.CZTERMINALID + "',");
                strSql.AppendLine("'" + (string.IsNullOrWhiteSpace(obj.SUMSOLD) ? "0" : obj.SUMSOLD) + "','" + obj.SUMSTOCK +
                                  "',");
                strSql.AppendLine("'" + obj.CREATION_USER + "'," + obj.CREATION_TIME + ",'" + obj.REMARK + "','" +
                                  obj.AMT_OPERATION + "') ");

                b = DBHelper.DoExecuteNonQuery(strSql.ToString(), CommandType.Text);
                if (!b)
                {
                    LogHelper.Log("UpdateAmtTypeMng", "AmtMngDAL.SaveOperationRecord >> false", "\r\n" + strSql.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return b;
        }



        /// <summary>
        /// 更新钱箱管理表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateAmtTypeMng(PuAmtTypeMngTB obj)
        {
            bool b = false;
            StringBuilder sb = new StringBuilder();

            try
            {
                //查看是否有数据
                sb.AppendLine("select * from PUAMTTYPEMNGTB where 1=1 ");
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
                    sb.AppendLine(" update PUAMTTYPEMNGTB set SUMSOLD=replace(SUMSOLD,SUMSOLD,SUMSOLD+'" + obj.SUMSOLD + "') ");
                    sb.AppendLine(" ,SURPLUSAMT=replace(SURPLUSAMT,SURPLUSAMT,SURPLUSAMT+'" + obj.SURPLUSAMT + "'), ");
                    sb.AppendLine(obj.SURPLUSNUM == "1" ? " SURPLUSNUM = SURPLUSNUM + 1," : "SURPLUSNUM = 0,");
                    sb.AppendLine(" SUMSTOCK=replace(SUMSTOCK,SUMSTOCK,SUMSTOCK+'" + obj.SUMSTOCK + "'), ");
                    sb.AppendLine(" UPDATE_USER='" + obj.UPDATE_USER + "',UPDATE_TIME=sysdate ");
                    sb.AppendLine(" where 1=1 ");
                    if (obj.SKTERMINALID != "")
                    {
                        sb.AppendLine(" and SKTERMINALID='" + obj.SKTERMINALID + "'");
                    }
                    if (obj.CZTERMINALID != "")
                    {
                        sb.AppendLine(" and CZTERMINALID='" + obj.CZTERMINALID + "' ");
                    }

                    b = DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);
                }
                else
                {
                    sb.AppendLine(" INSERT INTO PUAMTTYPEMNGTB(ID,SKTERMINALID,CZTERMINALID,SUMSTOCK,SUMSOLD,SURPLUSAMT,SURPLUSNUM ");
                    sb.AppendLine(obj.CREATION_USER == null ? " ,UPDATE_USER,UPDATE_TIME" : " ,CREATION_USER,CREATION_TIME");
                    sb.AppendLine(" ,REMARK)VALUES( PUAMTTYPEMNGTB_SEQ.NEXTVAL,");
                    sb.AppendLine(" '" + obj.SKTERMINALID + "','" + obj.CZTERMINALID + "','" + obj.SUMSTOCK + "','" + obj.SUMSOLD + "','" + obj.SURPLUSAMT + "',0 ");
                    if (obj.CREATION_USER == null)
                    {
                        sb.AppendLine(" ,'" + obj.UPDATE_USER + "',sysdate");
                    }
                    else
                    {
                        sb.AppendLine(" ,'" + obj.CREATION_USER + "',sysdate");
                    }
                    sb.AppendLine(" ,'" + obj.REMARK + "') ");

                    b = DBHelper.DoExecuteNonQuery(sb.ToString(), CommandType.Text);
                }

                if (!b)
                {
                    LogHelper.Log("UpdateAmtTypeMng", "AmtMngDAL.UpdateAmtTypeMng >> false", "\r\n" + sb.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return b;
        }


        /// <summary>
        /// 根据终端代号获取终端钱箱余额
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <returns></returns>
        public DataTable SelBalanceByTerminalNo(string terminalNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select SUMSTOCK,SUMSOLD,SURPLUSAMT,UPDATE_TIME from PUAMTTYPEMNGTB where 1=1 ");
            if (terminalNo != "")
            {
                strSql.AppendLine(" and CZTERMINALID='" + terminalNo + "'");
            }

            DataTable dtResult;
            _dbHelper.RunCommand(strSql.ToString(), out dtResult);
            return dtResult;
        }
    }
}
