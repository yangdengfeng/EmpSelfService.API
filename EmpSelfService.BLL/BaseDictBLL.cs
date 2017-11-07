using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.Common;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    public class BaseDictBLL
    {

        /// <summary>
        /// 根据属性类型获取字典信息
        /// </summary>
        /// <param name="itemKind"></param>
        /// <returns></returns>
        public DataTable SelectBaseDictInfoBy(string itemKind)
        {
            var dt = new DataTable();
            try
            {
                BaseDictDAL dal = new BaseDictDAL();
                dt = dal.SelectBaseDictInfoBy(itemKind);
            }
            catch (Exception ex)
            {
                LogHelper.Log("BaseDictBLL.SelectBaseDictInfoBy", ex);
            }

            return dt;
        }

        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBaseDictInfo()
        {
            var dt = new DataTable();
            try
            {
                BaseDictDAL dal = new BaseDictDAL();
                dt = dal.SelectBaseDictInfo();
            }
            catch (Exception ex)
            {
                LogHelper.Log("BaseDictBLL.SelectBaseDictInfo", ex);
            }

            return dt;
        }
    }
}
