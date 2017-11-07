/* ==============================================================================
 * 功能名称：BaseParam
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/5/19 15:47:54
 * 功能描述：
 * ==============================================================================*/
using System.Data;
using EmpSelfService.DAL.DBEntity;

namespace EmpSelfService.DAL
{
    public class BaseParamDAL
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        /// <summary>
        /// 查询基础参数数据
        /// </summary>
        /// <param name="paramType">参数类型</param>
        /// <returns></returns>
        public DataTable QueryBaseParamInfo(string paramType)
        {
            DataTable dtResult = new DataTable();
            if (string.IsNullOrWhiteSpace(paramType))
                return dtResult;
            string strSql =
                "SELECT PARAMETER_TYPE ParameterType,PARAMETER_VALUE ParamterValue FROM SBBASICPARAMETERTB  " +
                " WHERE PARAMETER_TYPE= '" + paramType + "'";
            _dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }
    }
}
