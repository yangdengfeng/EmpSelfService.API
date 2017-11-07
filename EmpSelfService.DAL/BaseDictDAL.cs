using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.DAL.DBEntity;

namespace EmpSelfService.DAL
{
    public class BaseDictDAL
    {

        private readonly DBHelper _dbHelper = new DBHelper();

        public DataTable SelectBaseDictInfoBy(string itemKind)
        {
            string strSql = "SELECT  ITEMNAME,ITEMVALUE FROM SBBASICDICTIONARY ";
            if (!string.IsNullOrEmpty(itemKind))
            {
                strSql += " WHERE ITEMKIND= '" + itemKind + "'";
            }
            strSql += " ORDER BY ITEMORDER";

            DataTable dtResult;
            _dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }

        public DataTable SelectBaseDictInfo()
        {
            string strSql = "SELECT  PARAMETER_TYPE ITEM_NAME,PARAMETER_VALUE ITEM_VALUE FROM SBBASICPARAMETERTB WHERE VALID=1 ";
            
            DataTable dtResult;
            _dbHelper.RunCommand(strSql, out dtResult);
            return dtResult;
        }


    }
}
