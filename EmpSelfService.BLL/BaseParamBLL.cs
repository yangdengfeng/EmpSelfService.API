/* ==============================================================================
 * 功能名称：BaseParam
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/5/19 15:47:27
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Data;
using EmpSelfService.Common;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    public class BaseParamBLL
    {
        /// <summary>
        /// 查询基础参数数据
        /// </summary>
        /// <param name="paramType">参数类型</param>
        /// <returns></returns>
        public DataTable QueryBaseParamInfo(string paramType)
        {
            var dt = new DataTable();
            try
            {
                BaseParamDAL dal = new BaseParamDAL();
                dt = dal.QueryBaseParamInfo(paramType);
            }
            catch (Exception ex)
            {
                LogHelper.Log("BaseParamBLL.QueryBaseParamInfo", ex);
            }
            return dt;
        }
    }
}
