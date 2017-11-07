/* ==============================================================================
 * 功能名称：GlobalModel
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：xulx
 * 创建日期：2016/4/22 16:15:20
 * 功能描述：全局存储数据
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace EmpSelfService.Model
{
    public static class GlobalModel
    {
        private static DataTable _dtGUID = new DataTable();

        /// <summary>
        /// GUID数据集初始化
        /// </summary>
        /// <returns></returns>
        public static bool initialization()
        {
            if (_dtGUID == null || _dtGUID.Columns.Count <= 0)
            {
                _dtGUID.Columns.Add("InterFaceName", typeof(string));
                _dtGUID.Columns.Add("GUID", typeof(string));
            }
            return true;
        }

        public static DataTable dtGUID
        {
            set { _dtGUID = value; }
            get { return _dtGUID; }
        }

        /// <summary>
        /// 系统用户编号
        /// </summary>
        public static string SystemUserId { get { return ConfigurationManager.AppSettings["ServerName"].ToString(); } }
    }
}
