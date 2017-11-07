using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 钱箱操作记录表
    /// </summary>
    public class PuAmtOperationRecordTB
    {
        private string _CZTERMINALID;
        /// <summary>
        /// 充值PSAM卡号
        /// </summary>
        public string CZTERMINALID
        {
            get { return _CZTERMINALID; }
            set { _CZTERMINALID = value; }
        }
        private string _SKTERMINALID;
        /// <summary>
        /// 售卡PSAM卡号
        /// </summary>
        public string SKTERMINALID
        {
            get { return _SKTERMINALID; }
            set { _SKTERMINALID = value; }
        }
        private string _SUMSOLD;
        /// <summary>
        /// 操作前金额
        /// </summary>
        public string SUMSOLD
        {
            get { return _SUMSOLD; }
            set { _SUMSOLD = value; }
        }
        private string _SUMSTOCK;
        /// <summary>
        /// 操作金额
        /// </summary>
        public string SUMSTOCK
        {
            get { return _SUMSTOCK; }
            set { _SUMSTOCK = value; }
        }
        private string _AMT_OPERATION;
        /// <summary>
        /// 操作类型
        /// </summary>
        public string AMT_OPERATION
        {
            get { return _AMT_OPERATION; }
            set { _AMT_OPERATION = value; }
        }
        private string _CREATION_USER;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CREATION_USER
        {
            get { return _CREATION_USER; }
            set { _CREATION_USER = value; }
        }
        private string _CREATION_TIME;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CREATION_TIME
        {
            get { return _CREATION_TIME; }
            set { _CREATION_TIME = value; }
        }
        private string _UPDATE_USER;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UPDATE_USER
        {
            get { return _UPDATE_USER; }
            set { _UPDATE_USER = value; }
        }
        private string _UPDATE_TIME;
        /// <summary>
        /// 修改日期
        /// </summary>
        public string UPDATE_TIME
        {
            get { return _UPDATE_TIME; }
            set { _UPDATE_TIME = value; }
        }
        private string _REMARK;
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
    }
}
