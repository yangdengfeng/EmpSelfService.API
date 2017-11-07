/* ==============================================================================
 * 功能名称：TelStateDAL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/4/17 9:46:53
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using EmpSelfService.DAL.DBEntity;
using EmpSelfService.Model;

namespace EmpSelfService.DAL
{
    public class TelStateDAL
    {
        private DBHelper dbHelper = new DBHelper();
        private StringBuilder sbSql = new StringBuilder();

        /// <summary>
        /// 获取设备状态信息
        /// </summary>
        /// <param name="CpuId">CPU编号</param>
        /// <returns></returns>
        public DataTable GetTelState(string CpuId)
        {
            DataTable dtResult = new DataTable();
            try
            {
                sbSql.Clear();
                sbSql.AppendLine("SELECT * FROM OMTERMINALSTATETB WHERE CPUID = '" + CpuId + "'");
                dbHelper.RunCommand(sbSql.ToString(), out dtResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + ";SQL:" + sbSql.ToString());
            }
            return dtResult;
        }

        /// <summary>
        /// 保存设备状态信息
        /// </summary>
        /// <param name="om">设备实体</param>
        /// <param name="TableName">表名称</param>
        public void SaveTelState(OmTerminalStateTB om, string TableName)
        {
            try
            {
                sbSql.Clear();
                if (TableName == "OMTERMINALSTATETB")
                {
                    sbSql.AppendLine("INSERT INTO " + TableName + " (CPUID,TIMELAST,SKTERMINALID,");
                    sbSql.AppendLine("CZTERMINALID,SKSTATE,CZSTATE,CARDISSUERSTATE,PRINTERSTATE,CASHBOXSTATE,UPSSTATE,UPSPERCENTAGE,SOFTWAREVER) ");
                    sbSql.AppendLine("VALUES('" + om.CpuId + "',TO_DATE('" + om.TimeLast + "','YYYY-MM-DD HH24:MI:SS'),");
                    sbSql.AppendLine(" '" + om.SKTerminalID + "',");
                    sbSql.AppendLine(" '" + om.CZTerminalID + "',");
                    sbSql.AppendLine(" '" + om.SKState + "',");
                    sbSql.AppendLine(" '" + om.CZState + "',");
                    sbSql.AppendLine(" '" + om.CardIssuerState + "',");
                    sbSql.AppendLine(" '" + om.PrinterState + "',");
                    sbSql.AppendLine(" '" + om.CashboxState + "',");
                    sbSql.AppendLine(" '" + om.UpsState + "',");
                    sbSql.AppendLine(" '" + om.UpsPercentage + "',");
                    sbSql.AppendLine(" '" + om.SoftwareVer + "')");
                }
                else {
                    sbSql.AppendLine("INSERT INTO " + TableName + " (CPUID,TIMELAST,SKTERMINALID,");
                    sbSql.AppendLine("CZTERMINALID,SKSTATE,CZSTATE,CARDISSUERSTATE,PRINTERSTATE,CASHBOXSTATE,UPSSTATE,UPSPERCENTAGE) ");
                    sbSql.AppendLine("VALUES('" + om.CpuId + "',TO_DATE('" + om.TimeLast + "','YYYY-MM-DD HH24:MI:SS'),");
                    sbSql.AppendLine(" '" + om.SKTerminalID + "',");
                    sbSql.AppendLine(" '" + om.CZTerminalID + "',");
                    sbSql.AppendLine(" '" + om.SKState + "',");
                    sbSql.AppendLine(" '" + om.CZState + "',");
                    sbSql.AppendLine(" '" + om.CardIssuerState + "',");
                    sbSql.AppendLine(" '" + om.PrinterState + "',");
                    sbSql.AppendLine(" '" + om.CashboxState + "',");
                    sbSql.AppendLine(" '" + om.UpsState + "',");
                    sbSql.AppendLine(" '" + om.UpsPercentage + "')");
                }
                
                dbHelper.RunCommand(sbSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + ";SQL:" + sbSql.ToString());
            }
        }

        /// <summary>
        /// 修改设备状态信息
        /// </summary>
        /// <param name="om"></param>
        /// <param name="TableName"></param>
        public void ModifyTelState(OmTerminalStateTB om, string TableName)
        {
            try
            {
                sbSql.Clear();
                sbSql.AppendLine(" UPDATE " + TableName + " ");
                sbSql.AppendLine(" SET TIMELAST = TO_DATE('" + om.TimeLast + "','YYYY-MM-DD HH24:MI:SS'),");
                sbSql.AppendLine(" SKTERMINALID = '" + om.SKTerminalID + "',");
                sbSql.AppendLine(" CZTERMINALID = '" + om.CZTerminalID + "',");
                sbSql.AppendLine(" SKSTATE = '" + om.SKState + "',");
                sbSql.AppendLine(" CZSTATE = '" + om.CZState + "',");
                sbSql.AppendLine(" CARDISSUERSTATE = '" + om.CardIssuerState + "',");
                sbSql.AppendLine(" PRINTERSTATE = '" + om.PrinterState + "',");
                sbSql.AppendLine(" CASHBOXSTATE = '" + om.CashboxState + "',");
                sbSql.AppendLine(" UPSSTATE = '" + om.UpsState + "',");
                sbSql.AppendLine(" UPSPERCENTAGE = '" + om.UpsPercentage + "',");
                sbSql.AppendLine(" SOFTWAREVER = '" + om.SoftwareVer + "' ");
                sbSql.AppendLine(" WHERE CPUID = '" + om.CpuId + "'");
                dbHelper.RunCommand(sbSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + ";SQL:" + sbSql.ToString());
            }
        }
    }
}
