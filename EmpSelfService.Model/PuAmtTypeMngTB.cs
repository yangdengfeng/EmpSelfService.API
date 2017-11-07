using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 钱箱余额管理表
    /// </summary>
    public class PuAmtTypeMngTB
    {
        private string _ID;
        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _SKTERMINALID;
        /// <summary>
        /// 售卡终端代号
        /// </summary>
        public string SKTERMINALID
        {
            get { return _SKTERMINALID; }
            set { _SKTERMINALID = value; }
        }
        private string _CZTERMINALID;
        /// <summary>
        /// 充值终端代号
        /// </summary>
        public string CZTERMINALID
        {
            get { return _CZTERMINALID; }
            set { _CZTERMINALID = value; }
        }
        private string _SUMSTOCK;
        /// <summary>
        /// 已收入总金额
        /// </summary>
        public string SUMSTOCK
        {
            get { return _SUMSTOCK; }
            set { _SUMSTOCK = value; }
        }
        private string _SUMSOLD;
        /// <summary>
        /// 已取出总金额
        /// </summary>
        public string SUMSOLD
        {
            get { return _SUMSOLD; }
            set { _SUMSOLD = value; }
        }
        private string _SURPLUSAMT;
        /// <summary>
        /// 剩余金额
        /// </summary>
        public string SURPLUSAMT
        {
            get { return _SURPLUSAMT; }
            set { _SURPLUSAMT = value; }
        }
        private string _SURPLUSNUM;
        /// <summary>
        /// 剩余张数
        /// </summary>
        public string SURPLUSNUM
        {
            get { return _SURPLUSNUM; }
            set { _SURPLUSNUM = value; }
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
        /// 修改时间
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
