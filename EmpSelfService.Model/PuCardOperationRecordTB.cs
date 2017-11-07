using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 库存卡操作记录表
    /// </summary>
    public class PuCardOperationRecordTB
    {
        private string _SKTERMINALID;
        /// <summary>
        /// 售卡PSAM卡号
        /// </summary>
        public string SKTERMINALID
        {
            get { return _SKTERMINALID; }
            set { _SKTERMINALID = value; }
        }
        private string _CZTERMINALID;
        /// <summary>
        /// 充值PSAM卡号
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
        private string _CARD_OPERATION;
        /// <summary>
        /// 卡操作：0：减少 1：增加
        /// </summary>
        public string CARD_OPERATION
        {
            get { return _CARD_OPERATION; }
            set { _CARD_OPERATION = value; }
        }
        private string _OLD_CARD_NUMBER;
        /// <summary>
        /// 操作前卡数量
        /// </summary>
        public string OLD_CARD_NUMBER
        {
            get { return _OLD_CARD_NUMBER; }
            set { _OLD_CARD_NUMBER = value; }
        }
        private string _CARD_NUMBER;
        /// <summary>
        /// 卡数量
        /// </summary>
        public string CARD_NUMBER
        {
            get { return _CARD_NUMBER; }
            set { _CARD_NUMBER = value; }
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
