using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 库存卡种类管理表
    /// </summary>
    public class PuCardTypeMngTB
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
        private string _CARDTYPE_CODE;
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CARDTYPE_CODE
        {
            get { return _CARDTYPE_CODE; }
            set { _CARDTYPE_CODE = value; }
        }
        private string _CARDTYPE_NAME;
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CARDTYPE_NAME
        {
            get { return _CARDTYPE_NAME; }
            set { _CARDTYPE_NAME = value; }
        }
        private string _SUMSTOCK;
        /// <summary>
        /// 总存入张数
        /// </summary>
        public string SUMSTOCK
        {
            get { return _SUMSTOCK; }
            set { _SUMSTOCK = value; }
        }
        private string _SUMSOLD;
        /// <summary>
        /// 总售卡张数
        /// </summary>
        public string SUMSOLD
        {
            get { return _SUMSOLD; }
            set { _SUMSOLD = value; }
        }
        private string _SURPLUSCARD;
        /// <summary>
        /// 剩余张数
        /// </summary>
        public string SURPLUSCARD
        {
            get { return _SURPLUSCARD; }
            set { _SURPLUSCARD = value; }
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
