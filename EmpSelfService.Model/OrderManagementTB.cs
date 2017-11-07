using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 订单类
    /// </summary>
    public class OrderManagementTB
    {
        private string _ORDER_ID;

        public string ORDER_ID
        {
            get { return _ORDER_ID; }
            set { _ORDER_ID = value; }
        }

        private string _ORDER_NO;
        /// <summary>
        /// 业务订单编号
        /// </summary>
        public string ORDER_NO
        {
            get { return _ORDER_NO; }
            set { _ORDER_NO = value; }
        }
        private string _ORDER_TYPE;
        /// <summary>
        /// 订单类型
        /// </summary>
        public string ORDER_TYPE
        {
            get { return _ORDER_TYPE; }
            set { _ORDER_TYPE = value; }
        }
        private string _TRADE_DATE;
        /// <summary>
        ///交易时间
        /// </summary>
        public string TRADE_DATE
        {
            get { return _TRADE_DATE; }
            set { _TRADE_DATE = value; }
        }
        private string _CARD_NO;
        /// <summary>
        /// 卡号
        /// </summary>
        public string CARD_NO
        {
            get { return _CARD_NO; }
            set { _CARD_NO = value; }
        }
        private string _CARD_TYPE;
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CARD_TYPE
        {
            get { return _CARD_TYPE; }
            set { _CARD_TYPE = value; }
        }
        private string _PHY_CARD_TYPE;
        /// <summary>
        /// 物理类型
        /// </summary>
        public string PHY_CARD_TYPE
        {
            get { return _PHY_CARD_TYPE; }
            set { _PHY_CARD_TYPE = value; }
        }
        private string _RECHARGE_MONEY;
        /// <summary>
        /// 卡余额
        /// </summary>
        public string RECHARGE_MONEY
        {
            get { return _RECHARGE_MONEY; }
            set { _RECHARGE_MONEY = value; }
        }
        private string _RECHARGE_INDEX;
        /// <summary>
        /// 充值卡序号
        /// </summary>
        public string RECHARGE_INDEX
        {
            get { return _RECHARGE_INDEX; }
            set { _RECHARGE_INDEX = value; }
        }

        private string _TERMINAL_ID;
        /// <summary>
        /// 充值终端编号
        /// </summary>
        public string TERMINAL_ID
        {
            get { return _TERMINAL_ID; }
            set { _TERMINAL_ID = value; }
        }
        private string _RECHARGE_TIME;
        /// <summary>
        /// 充值时间
        /// </summary>
        public string RECHARGE_TIME
        {
            get { return _RECHARGE_TIME; }
            set { _RECHARGE_TIME = value; }
        }
        private string _ORDER_MONEY;
        /// <summary>
        /// 订单金额
        /// </summary>
        public string ORDER_MONEY
        {
            get { return _ORDER_MONEY; }
            set { _ORDER_MONEY = value; }
        }
        private string _STATUS;
        /// <summary>
        /// 订单状态 
        /// </summary>
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        private string _REFUNDFLAG;
        /// <summary>
        /// 退款状态
        /// </summary>
        public string REFUNDFLAG
        {
            get { return _REFUNDFLAG; }
            set { _REFUNDFLAG = value; }
        }
        private string _SETTFLAG;
        /// <summary>
        /// 结算状态
        /// </summary>
        public string SETTFLAG
        {
            get { return _SETTFLAG; }
            set { _SETTFLAG = value; }
        }
        private string _DATATYPE;
        /// <summary>
        /// 支付方式
        /// </summary>
        public string DATATYPE
        {
            get { return _DATATYPE; }
            set { _DATATYPE = value; }
        }
        private string _NFC_LOCK_TIME;
        /// <summary>
        /// 锁定时间
        /// </summary>
        public string NFC_LOCK_TIME
        {
            get { return _NFC_LOCK_TIME; }
            set { _NFC_LOCK_TIME = value; }
        }
        private string _NFC_END_TIME;
        /// <summary>
        /// 完成时间
        /// </summary>
        public string NFC_END_TIME
        {
            get { return _NFC_END_TIME; }
            set { _NFC_END_TIME = value; }
        }
        private string _ERROR_CODE;
        /// <summary>
        /// 错误编码
        /// </summary>
        public string ERROR_CODE
        {
            get { return _ERROR_CODE; }
            set { _ERROR_CODE = value; }
        }
        private string _SETT_DATE;
        /// <summary>
        /// 结算日期
        /// </summary>
        public string SETT_DATE
        {
            get { return _SETT_DATE; }
            set { _SETT_DATE = value; }
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
        private string _CREATION_TIME;
        /// <summary>
        /// 生成时间
        /// </summary>
        public string CREATION_TIME
        {
            get { return _CREATION_TIME; }
            set { _CREATION_TIME = value; }
        }

        //支付-----------------
        private int _PAYORDER_ID;

        public int PAYORDER_ID
        {
            get { return _PAYORDER_ID; }
            set { _PAYORDER_ID = value; }
        }
        private string _PAY_ORDER_NO;
        /// <summary>
        /// 支付订单编号
        /// </summary>
        public string PAY_ORDER_NO
        {
            get { return _PAY_ORDER_NO; }
            set { _PAY_ORDER_NO = value; }
        }
        private string _PAY_MONEY;
        /// <summary>
        /// 支付金额
        /// </summary>
        public string PAY_MONEY
        {
            get { return _PAY_MONEY; }
            set { _PAY_MONEY = value; }
        }

        private string _PAY_LOCK_TIME;
        /// <summary>
        /// 支付锁定时间
        /// </summary>
        public string PAY_LOCK_TIME
        {
            get { return _PAY_LOCK_TIME; }
            set { _PAY_LOCK_TIME = value; }
        }
        private string _PAY_END_TIME;
        /// <summary>
        /// 支付结束时间
        /// </summary>
        public string PAY_END_TIME
        {
            get { return _PAY_END_TIME; }
            set { _PAY_END_TIME = value; }
        }
        //-------------------------------
        //退款-----------------------------
        private string _RE_ORDER_NO;
        /// <summary>
        /// 退款订单号
        /// </summary>
        public string RE_ORDER_NO
        {
            get { return _RE_ORDER_NO; }
            set { _RE_ORDER_NO = value; }
        }

        private string _REFOUND_MONEY;
        /// <summary>
        /// 退款金额
        /// </summary>
        public string REFOUND_MONEY
        {
            get { return _REFOUND_MONEY; }
            set { _REFOUND_MONEY = value; }
        }
        private string _REFUND_LOCK_TIME;
        /// <summary>
        /// 退款锁定时间
        /// </summary>
        public string REFUND_LOCK_TIME
        {
            get { return _REFUND_LOCK_TIME; }
            set { _REFUND_LOCK_TIME = value; }
        }
        private string _REFUND_END_TIME;
        /// <summary>
        /// 退款完成时间
        /// </summary>
        public string REFUND_END_TIME
        {
            get { return _REFUND_END_TIME; }
            set { _REFUND_END_TIME = value; }
        }
        //----------------------------


    }

    public class BATZFBCENTERRESULTTB
    {
        private string _SETTDATE;

        public string SETTDATE
        {
            get { return _SETTDATE; }
            set { _SETTDATE = value; }
        }

        private string _SHOPID;

        public string SHOPID
        {
            get { return _SHOPID; }
            set { _SHOPID = value; }
        }
        private string _SHOPNAME;

        public string SHOPNAME
        {
            get { return _SHOPNAME; }
            set { _SHOPNAME = value; }
        }
        private string _TXNCNT;

        public string TXNCNT
        {
            get { return _TXNCNT; }
            set { _TXNCNT = value; }
        }
        private string _TXNTKCNT;

        public string TXNTKCNT
        {
            get { return _TXNTKCNT; }
            set { _TXNTKCNT = value; }
        }
        private string _TXNAMT;

        public string TXNAMT
        {
            get { return _TXNAMT; }
            set { _TXNAMT = value; }
        }
        private string _TXNSSAMT;

        public string TXNSSAMT
        {
            get { return _TXNSSAMT; }
            set { _TXNSSAMT = value; }
        }
        private string _TXNYHAMT;

        public string TXNYHAMT
        {
            get { return _TXNYHAMT; }
            set { _TXNYHAMT = value; }
        }
        private string _TXNSJYHAMT;

        public string TXNSJYHAMT
        {
            get { return _TXNSJYHAMT; }
            set { _TXNSJYHAMT = value; }
        }
        private string _TXNCARDAMT;

        public string TXNCARDAMT
        {
            get { return _TXNCARDAMT; }
            set { _TXNCARDAMT = value; }
        }
        private string _TXNFWAMT;

        public string TXNFWAMT
        {
            get { return _TXNFWAMT; }
            set { _TXNFWAMT = value; }
        }
        private string _TXNFRAMT;

        public string TXNFRAMT
        {
            get { return _TXNFRAMT; }
            set { _TXNFRAMT = value; }
        }
        private string _TXNSSJEAMT;

        public string TXNSSJEAMT
        {
            get { return _TXNSSJEAMT; }
            set { _TXNSSJEAMT = value; }
        }
    }


    public class OM_ZFBORDERCENTERMANAGEMENT_TB
    {
        private string _SETTDATE;//结算时间

        public string SETTDATE
        {
            get { return _SETTDATE; }
            set { _SETTDATE = value; }
        }
        private string _ZFBTRADE_NO;//支付宝交易号

        public string ZFBTRADE_NO
        {
            get { return _ZFBTRADE_NO; }
            set { _ZFBTRADE_NO = value; }
        }
        private string _TRADE_NO;//订单号

        public string TRADE_NO
        {
            get { return _TRADE_NO; }
            set { _TRADE_NO = value; }
        }
        private string _TRADE_TYPE;//交易类型

        public string TRADE_TYPE
        {
            get { return _TRADE_TYPE; }
            set { _TRADE_TYPE = value; }
        }
        private string _TRADE_NAME;//交易名称

        public string TRADE_NAME
        {
            get { return _TRADE_NAME; }
            set { _TRADE_NAME = value; }
        }
        private string _CREATE_TIME;//创建时间

        public string CREATE_TIME
        {
            get { return _CREATE_TIME; }
            set { _CREATE_TIME = value; }
        }
        private string _END_TIME;//完成时间

        public string END_TIME
        {
            get { return _END_TIME; }
            set { _END_TIME = value; }
        }
        private string _SHOPID;//门店编号

        public string SHOPID
        {
            get { return _SHOPID; }
            set { _SHOPID = value; }
        }
        private string _SHOPNAME;//门店名称

        public string SHOPNAME
        {
            get { return _SHOPNAME; }
            set { _SHOPNAME = value; }
        }
        private string _POSOPRID;//操作员编号

        public string POSOPRID
        {
            get { return _POSOPRID; }
            set { _POSOPRID = value; }
        }
        private string _TERMINALID;//终端代号

        public string TERMINALID
        {
            get { return _TERMINALID; }
            set { _TERMINALID = value; }
        }
        private string _ZFZH;//对方账户名称

        public string ZFZH
        {
            get { return _ZFZH; }
            set { _ZFZH = value; }
        }
        private string _TXNVALUE;//订单金额

        public string TXNVALUE
        {
            get { return _TXNVALUE; }
            set { _TXNVALUE = value; }
        }
        private string _TXNAMT;//商家实收金额

        public string TXNAMT
        {
            get { return _TXNAMT; }
            set { _TXNAMT = value; }
        }
        private string _ZFHB;//支付宝红包

        public string ZFHB
        {
            get { return _ZFHB; }
            set { _ZFHB = value; }
        }
        private string _JFB;//集分宝

        public string JFB
        {
            get { return _JFB; }
            set { _JFB = value; }
        }
        private string _ZFBYH;//支付宝优惠

        public string ZFBYH
        {
            get { return _ZFBYH; }
            set { _ZFBYH = value; }
        }
        private string _SJYH;//商家优惠

        public string SJYH
        {
            get { return _SJYH; }
            set { _SJYH = value; }
        }
        private string _JHXJE;//劵核销金额

        public string JHXJE
        {
            get { return _JHXJE; }
            set { _JHXJE = value; }
        }
        private string _JMC;//劵名称卡

        public string JMC
        {
            get { return _JMC; }
            set { _JMC = value; }
        }
        private string _SJHBXF;//商户红包消费

        public string SJHBXF
        {
            get { return _SJHBXF; }
            set { _SJHBXF = value; }
        }
        private string _CARDXFAMT;//卡消费

        public string CARDXFAMT
        {
            get { return _CARDXFAMT; }
            set { _CARDXFAMT = value; }
        }
        private string _RTNMONEYNO;//退款批次号

        public string RTNMONEYNO
        {
            get { return _RTNMONEYNO; }
            set { _RTNMONEYNO = value; }
        }
        private string _FWFY;//服务费用

        public string FWFY
        {
            get { return _FWFY; }
            set { _FWFY = value; }
        }
        private string _FENRUN;//分润

        public string FENRUN
        {
            get { return _FENRUN; }
            set { _FENRUN = value; }
        }
        private string _REMRMK;//备注

        public string REMRMK
        {
            get { return _REMRMK; }
            set { _REMRMK = value; }
        }
    }
}
