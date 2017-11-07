/* ==============================================================================
 * 功能名称：ClientFileDAL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/5/16 15:53:45
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Data;
using System.Text;
using EmpSelfService.DAL.DBEntity;

namespace EmpSelfService.DAL
{
    public class ClientFileDAL
    {
        private DBHelper dbHelper = new DBHelper();

        /// <summary>
        /// 查询最新的软件版本信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryTerminalSoftware()
        {
            string strSql = "SELECT FILENAME,FILEVERSION,FILEPATH FROM OMTERMINALSOFTWARETB WHERE NEWEST=1";
            DataTable dtResult = new DataTable();
            dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }

        /// <summary>
        /// 查询设备状态表中记录的设备软件版本和是否更新标识
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <returns></returns>
        public DataTable QueryTerminalState(string cpuId)
        {
            string strSql = "SELECT SOFTWAREVER,SOFTWARERENEW FROM OMTERMINALSTATETB WHERE CPUID = '" + cpuId + "'";
            DataTable dtResult = new DataTable();
            dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }
        /// <summary>
        /// 新设备 保存设备状态
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <returns></returns>
        public bool SaveTerminalState(string cpuId)
        {
            string strSql = "INSERT INTO OMTERMINALSTATETB(CPUID,SOFTWARERENEW)VALUES('" + cpuId + "','1')";
            return DBHelper.DoExecuteNonQuery(strSql, CommandType.Text);
        }

        /// <summary>
        /// 查询基础参数数据
        /// </summary>
        /// <param name="paramType">参数类型</param>
        /// <returns></returns>
        public string QueryBasicParam(string paramType)
        {
            string strSql = "SELECT PARAMETER_VALUE FROM SBBASICPARAMETERTB WHERE PARAMETER_TYPE = '" + paramType + "'";
            return dbHelper.GetReturnStr(strSql);
        }
        /// <summary>
        /// 新设备  保存设备默认参数
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <param name="usecontrol">设备使用控制  0：正常 1：禁用充值 2：禁用售卡 3：禁用充值和售卡 </param>
        /// <param name="sellCom">售卡串口</param>
        /// <param name="rechargeCom">充值串口</param>
        /// <param name="cardissuerCom">发卡器串口</param>
        /// <param name="cashboxCom">纸币器串口</param>
        /// <param name="ledCom">LED灯串口</param>
        /// <param name="printerCom">打印机串口</param>
        /// <param name="printerType">打印机类型 1:热敏打印机 2：针式打印机</param>
        /// <param name="userId">操作员编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool SaveTerminalParam(string cpuId, string usecontrol, string sellCom, string rechargeCom,
            string cardissuerCom, string cashboxCom, string ledCom, string printerCom, string printerType, string userId, string password)
        {
            string strSql =
                "INSERT INTO OMTERMINALPARAMTB" +
                "(CPUID,USECONTROL,SELLCOM,RECHARGECOM,CARDISSUERCOM,CASHBOXCOM,LEDCOM,PRINTERCOM,PRINTERTYPE,VALID,USERID,PASSWORD)" +
                "VALUES" +
                "('" + cpuId + "','" + usecontrol + "','" + sellCom + "','" + rechargeCom + "','" + cardissuerCom + "'," +
                "'" + cashboxCom + "','" + ledCom + "','" + printerCom + "','" + printerType + "',1,'" + userId + "','" + password + "')";
            return DBHelper.DoExecuteNonQuery(strSql, CommandType.Text);
        }

        /// <summary>
        /// 获取密码  
        /// </summary>
        /// <param name="cpuId"></param>
        /// <returns></returns>
        public string QueryTerminalPassword(string cpuId)
        {
            string strSql = "SELECT PASSWORD FROM OMTERMINALPARAMTB WHERE CPUID = '" + cpuId + "'";
            return dbHelper.GetReturnStr(strSql);
        }

        /// <summary>
        /// 获取文件全路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public string QueryClientSoftware(string fileName)
        {
            string strSql = "SELECT FILEPATH FROM OMTERMINALSOFTWARETB WHERE FILENAME = '" + fileName + "'";
            return dbHelper.GetReturnStr(strSql);
        }
    }
}
