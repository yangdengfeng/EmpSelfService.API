using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.Common;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    public class NetWorkBLL
    {

        /// <summary>
        /// 查询网点信息
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public DataTable SelectNetWorkInfoBy(string cityCode)
        {
            var dt = new DataTable();
            try
            {
                NetWorkDAL dal = new NetWorkDAL();
                dt = dal.SelectNetWorkInfoBy(cityCode);
            }
            catch (Exception ex)
            {
                LogHelper.Log("NetWorkBLL.SelectNetWorkInfoBy", ex);
            }

            return dt;
        }
    }
}
