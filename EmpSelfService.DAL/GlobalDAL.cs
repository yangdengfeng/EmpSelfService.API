/* ==============================================================================
 * 功能名称：GlobalDAL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：xulx
 * 创建日期：2016/5/11 14:46:18
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EmpSelfService.DAL.DBEntity;

namespace EmpSelfService.DAL
{
    public class GlobalDAL
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly StringBuilder _sbSql = new StringBuilder();

        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string userId)
        {
            try
            {
                _sbSql.Clear();
                _sbSql.Append("SELECT U_PASSWORD PASSWORD FROM SMUSERTB WHERE U_ID='" + userId + "'");
                DataTable dtResult = new DataTable();
                _dbHelper.RunCommand(_sbSql.ToString(), out dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserInfo(根据卡号查卡信息):" + ex.Message);
            }
        }
        #endregion

        #region 是否存在用户
        /// <summary>
        /// 是否存在用户
        /// </summary>
        public DataTable GetUserInfo(string userId, string pwd)
        {
            try
            {
                _sbSql.Clear();
                _sbSql.Append("SELECT * FROM SMUSERTB WHERE U_ID='" + userId + "' and U_PASSWORD='" + pwd + "'");
                DataTable dtResult;
                _dbHelper.RunCommand(_sbSql.ToString(), out dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserInfo(查询用户信息失败):" + ex.Message);
            }
        }
        #endregion

        #region 根据CPU编号查找用户信息
        /// <summary>
        /// 根据CPU编号查找用户信息
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <returns></returns>
        public DataTable GetUserByCpuId(string cpuId)
        {
            try
            {
                _sbSql.Clear();
                _sbSql.Append("SELECT USERID,PASSWORD FROM OMTERMINALPARAMTB WHERE CPUID = '" + cpuId + "'");
                DataTable dtResult;
                _dbHelper.RunCommand(_sbSql.ToString(), out dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserByTerminalNo(根据CPU编号查找用户信息):" + ex.Message);
            }
        }
        #endregion

        #region 根据终端号查找用户信息
        /// <summary>
        /// 根据终端号查找用户信息
        /// </summary>
        /// <param name="terminalNo">设备终端号</param>
        /// <returns></returns>
        public DataTable GetUserByTerminalNo(string terminalNo)
        {
            try
            {
                _sbSql.Clear();
                _sbSql.Append("select s.u_id,s.u_password,t.component_name from ");
                _sbSql.Append(" (select * from OMBASICCOMPONENTTB where component_id in (select father_id from OMBASICCOMPONENTTB where status=1 and type_code=5 and segment2= '" + terminalNo + "')) t");
                _sbSql.Append(" left join  smusertb s on s.component_id=t.component_id");
                _sbSql.Append(" where (s.status='Y' or s.status='1')  and t.status=1");
                DataTable dtResult;
                _dbHelper.RunCommand(_sbSql.ToString(), out dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserByTerminalNo(根据终端号查找用户信息):" + ex.Message);
            }
        }
        #endregion

        #region 根据企业编号查找用户信息
        /// <summary>
        /// 根据企业编号查找用户信息
        /// </summary>
        /// <param name="companyId">企业编号</param>
        /// <returns></returns>
        public DataTable GetUserByCompanyId(string companyId)
        {
            try
            {
                _sbSql.Clear();
                _sbSql.Append("select sm.u_id,sm.u_password,om.component_name from ombasiccomponenttb om left join smusertb sm on om.component_id=sm.component_id ");
                _sbSql.Append(" where om.component_code= '" + companyId + "' and om.type_code=1 and om.status='1' and sm.status='1'");
                DataTable dtResult;
                _dbHelper.RunCommand(_sbSql.ToString(), out dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserByCompanyId(根据企业编号查找用户信息):" + ex.Message);
            }
        }
        #endregion

    }
}
