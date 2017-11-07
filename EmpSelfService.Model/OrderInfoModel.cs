using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    public class OrderInfoModel
    {
        private string _ORDER_ID;

        public string ORDER_ID
        {
            get { return _ORDER_ID; }
            set { _ORDER_ID = value; }
        }

        private string _ORDER_NO;
        /// <summary>
        /// 订单编号
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
    }

    public class OM_WXORDERCENTERMANAGEMENT_TB
    {

        private string _SETTDATE;//结算时间
        private string _TXNDATE;//交易时间
        private string _APPID;//公众账号ID
        private string _WXTRADE_NO;//商户号
        private string _SUBWXTRADE_NO;//子商户号
        private string _TERMINALID;//设备号
        private string _TRADE_NO;//微信订单号
        private string _ORDER_NO;//商户订单号
        private string _USER_FLAG;//用户标识
        private string _TRADE_TYPE;//交易类型
        private string _TRADE_FLAG;//交易状态
        private string _REVENUEBANK;//付款银行
        private string _AMT_TYPE;//货币种类
        private string _SUM_AMT;//总金额
        private string _ENTERPRISE_RED_AMT;//企业红包金额
        private string _WXRETURN_NO;//微信退款单号
        private string _SHRETURN_NO;//商户退款单号
        private string _RETURN_AMT;//退款金额
        private string _ENTERPRISE_RED_RETURN_AMT;//企业红包退款金额
        private string _RETURN_TYPE;//退款类型
        private string _RETURN_FLAG;//退款状态
        private string _SPNAME;//商品名称
        private string _SHDATA;//商户数据包
        private string _HANDFREE;//手续费
        private string _RATEAMT;//费率
        /// <summary>
        /// 结算时间
        /// </summary>
        public string SETTDATE
        {
            get { return _SETTDATE; }
            set { _SETTDATE = value; }
        }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string TXNDATE
        {
            get { return _TXNDATE; }
            set { _TXNDATE = value; }
        }

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string APPID
        {
            get { return _APPID; }
            set { _APPID = value; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string WXTRADE_NO
        {
            get { return _WXTRADE_NO; }
            set { _WXTRADE_NO = value; }
        }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SUBWXTRADE_NO
        {
            get { return _SUBWXTRADE_NO; }
            set { _SUBWXTRADE_NO = value; }
        }

        /// <summary>
        /// 设备号
        /// </summary>
        public string TERMINALID
        {
            get { return _TERMINALID; }
            set { _TERMINALID = value; }
        }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TRADE_NO
        {
            get { return _TRADE_NO; }
            set { _TRADE_NO = value; }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string ORDER_NO
        {
            get { return _ORDER_NO; }
            set { _ORDER_NO = value; }
        }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string USER_FLAG
        {
            get { return _USER_FLAG; }
            set { _USER_FLAG = value; }
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TRADE_TYPE
        {
            get { return _TRADE_TYPE; }
            set { _TRADE_TYPE = value; }
        }

        /// <summary>
        /// 交易状态
        /// </summary>
        public string TRADE_FLAG
        {
            get { return _TRADE_FLAG; }
            set { _TRADE_FLAG = value; }
        }

        /// <summary>
        /// 付款银行
        /// </summary>
        public string REVENUEBANK
        {
            get { return _REVENUEBANK; }
            set { _REVENUEBANK = value; }
        }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string AMT_TYPE
        {
            get { return _AMT_TYPE; }
            set { _AMT_TYPE = value; }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public string SUM_AMT
        {
            get { return _SUM_AMT; }
            set { _SUM_AMT = value; }
        }

        /// <summary>
        /// 企业红包金额
        /// </summary>
        public string ENTERPRISE_RED_AMT
        {
            get { return _ENTERPRISE_RED_AMT; }
            set { _ENTERPRISE_RED_AMT = value; }
        }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string WXRETURN_NO
        {
            get { return _WXRETURN_NO; }
            set { _WXRETURN_NO = value; }
        }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string SHRETURN_NO
        {
            get { return _SHRETURN_NO; }
            set { _SHRETURN_NO = value; }
        }

        /// <summary>
        /// 退款金额
        /// </summary>
        public string RETURN_AMT
        {
            get { return _RETURN_AMT; }
            set { _RETURN_AMT = value; }
        }

        /// <summary>
        /// 企业红包退款金额
        /// </summary>
        public string ENTERPRISE_RED_RETURN_AMT
        {
            get { return _ENTERPRISE_RED_RETURN_AMT; }
            set { _ENTERPRISE_RED_RETURN_AMT = value; }
        }


        /// <summary>
        /// 退款类型
        /// </summary>
        public string RETURN_TYPE
        {
            get { return _RETURN_TYPE; }
            set { _RETURN_TYPE = value; }
        }

        /// <summary>
        /// 退款状态
        /// </summary>
        public string RETURN_FLAG
        {
            get { return _RETURN_FLAG; }
            set { _RETURN_FLAG = value; }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string SPNAME
        {
            get { return _SPNAME; }
            set { _SPNAME = value; }
        }

        /// <summary>
        /// 商户数据包
        /// </summary>
        public string SHDATA
        {
            get { return _SHDATA; }
            set { _SHDATA = value; }
        }

        /// <summary>
        /// 手续费
        /// </summary>
        public string HANDFREE
        {
            get { return _HANDFREE; }
            set { _HANDFREE = value; }
        }

        /// <summary>
        /// 费率
        /// </summary>
        public string RATEAMT
        {
            get { return _RATEAMT; }
            set { _RATEAMT = value; }
        }


       

    }

    public class BATWXCENTERRESULTTB
    {
        private string _SETTDATE;
        private string _TXNCNT;
        private string _TXNAMT;
        private string _TXNTKAMT;
        private string _TXNHBTKAMT;
        private string _TXNSXFAMT;

        /// <summary>
        /// 结算时间
        /// </summary>
        public string SETTDATE
        {
            get { return _SETTDATE; }
            set { _SETTDATE = value; }
        }

        /// <summary>
        /// 总交易次数
        /// </summary>
        public string TXNCNT
        {
            get { return _TXNCNT; }
            set { _TXNCNT = value; }
        }

        /// <summary>
        /// 总交易金额
        /// </summary>
        public string TXNAMT
        {
            get { return _TXNAMT; }
            set { _TXNAMT = value; }
        }

        /// <summary>
        /// 总退款金额
        /// </summary>
        public string TXNTKAMT
        {
            get { return _TXNTKAMT; }
            set { _TXNTKAMT = value; }
        }

        /// <summary>
        /// 总红包退款金额
        /// </summary>
        public string TXNHBTKAMT
        {
            get { return _TXNHBTKAMT; }
            set { _TXNHBTKAMT = value; }
        }

        /// <summary>
        /// 手续费
        /// </summary>
        public string TXNSXFAMT
        {
            get { return _TXNSXFAMT; }
            set { _TXNSXFAMT = value; }
        }
    }
}
