using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.DAL.DBEntity;

namespace EmpSelfService.DAL
{
    public class NetWorkDAL
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        public DataTable SelectNetWorkInfoBy(string cityCode)
        {
            string strSql = "select rownum, t.* from ( ";
            strSql += " select network_name,network_address,network_tell,networktype,networkregion from omnetworkinfotb where status=1 ";
            if (!string.IsNullOrEmpty(cityCode))
            {
                strSql += " and CityCode= '" + cityCode + "'";
            }

            strSql += " order by network_name)t";

            DataTable dtResult;
            _dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }


    }
}
