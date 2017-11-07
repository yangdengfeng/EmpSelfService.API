using System;
using System.Collections.Generic;
using System.Data;

using F2FPayDll.Business;
using F2FPayDll.Domain;
using F2FPayDll.Model;
using EmpSelfService.Model;
using EmpSelfService.Common;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    public class QRCodeBLL
    {
        private QRCodeDAL dalQRCode = new QRCodeDAL();

        #region 生成二维码  生成对应的订单
        /// <summary>
        /// 生成扫码支付模式url
        /// </summary>
        /// <param name="terminalId">设备终端编号</param>
        /// <param name="txnAmt">交易金额</param>
        /// <param name="txnType">交易类型</param>
        /// <param name="txnWay">交易方式 1:WeChat  2:AliPay</param>
        /// <param name="cardId">卡号</param>
        /// <param name="cardType">卡类型</param>
        public ResultBase<DataTable> CreateQrUrl(string terminalId, string txnAmt, string txnType, string txnWay, string cardId, string cardType)
        {
            string tradeNo = string.Empty;
            string payNo = string.Empty;
            string qrUrl = string.Empty;
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("URL");
            dtResult.Columns.Add("TradeNo");
            //dtResult.Columns.Add("PayNo");
            DataRow dr = dtResult.NewRow();
            NativePayBLL nativePay = new NativePayBLL();
            try
            {
                if (txnWay == "1") //微信
                {
                    var b = false;
                    qrUrl = GetPayWeChatQRCodeData(txnType, txnAmt, terminalId, cardId, cardType, out b, out tradeNo);
                    dr["URL"] = qrUrl;
                    dr["TradeNo"] = tradeNo;
                    dtResult.Rows.Add(dr);
                    return b
                        ? ResultBase<DataTable>.GetSuccess(dtResult)
                        : ResultBase<DataTable>.GetFailure(CodeModel.ErrCreateQrCode);
                }
                else if (txnWay == "2") //支付宝
                {
                    ResultEnum resultEnum = GetPayAliQRCodeData(txnType, txnAmt, terminalId, cardId, cardType, out tradeNo, out qrUrl);
                    switch (resultEnum)
                    {
                        case ResultEnum.SUCCESS:
                            dr["URL"] = qrUrl;
                            dr["TradeNo"] = tradeNo;
                            dtResult.Rows.Add(dr);
                            return ResultBase<DataTable>.GetSuccess(dtResult);
                        case ResultEnum.FAILED:
                            return ResultBase<DataTable>.GetFailure(CodeModel.ErrCreateQrCode);
                        case ResultEnum.UNKNOWN:
                            return ResultBase<DataTable>.GetFailure(CodeModel.ErrSystem);
                            //return ResultBase<DataTable>.GetFailure(precreateResult.response == null
                            //    ? CodeModel.ErrConfigOrNetWork
                            //    : CodeModel.ErrSystem);
                    }
                }
                else
                {
                    return ResultBase<DataTable>.GetFailure(CodeModel.UnknownPayMethod);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("PayBLL.CreateQrUrl", ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(qrUrl))
                {
                    LogHelper.Log("PayBLL", "支付url", string.Format("{0}_PAY_URL:{1}", txnWay == "2" ? "AliPay" : "WeChat", qrUrl));
                }
            }

            return ResultBase<DataTable>.GetFailure(CodeModel.ErrSystem);
        }

        #region 生成微信二维码
        /// <summary>
        /// 生成微信支付的二维码数据
        /// </summary>
        /// <param name="txntype">交易类型</param>
        /// <param name="txnAmt">交易金额</param>
        /// <param name="terminalid">设备ID</param>
        /// <param name="cardid">卡号</param>
        /// <param name="cardtype">卡类型</param>
        /// <param name="b">返回值  是否成功</param>
        /// <param name="tradeNo">交易单号</param>
        /// <returns></returns>
        private string GetPayWeChatQRCodeData(string txntype, string txnAmt, string terminalid, string cardid, string cardtype,
            out bool b, out string tradeNo)
        {
            b = false;
            string strOrderType = string.Empty;
            WeChatPayData data = new WeChatPayData();
            if (txntype == "1")
            {
                data.SetValue("body", "西宁一卡通—自助售卡");//商品描述
                data.SetValue("attach", "ZZZD");//附加数据
                strOrderType = "1";
            }
            else
            {
                data.SetValue("body", "西宁一卡通—自助充值");//商品描述
                data.SetValue("attach", "ZZZD");//附加数据
                strOrderType = "2";
            }

            string strOrderNo = WeChatPayApi.GenerateOutTradeNo(terminalid);
            data.SetValue("out_trade_no", strOrderNo);//随机字符串
            //data.SetValue("total_fee", 1);//测试环境 总金额
            data.SetValue("total_fee", int.Parse(txnAmt));//生成环境 订单总金额，单位为分
            data.SetValue("time_start", DateTime.Now.AddSeconds(-220).ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddSeconds(90).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "XNST");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            if (txnAmt == "1000")
            {
                data.SetValue("product_id", "100000001");//商品ID
            }
            else if (txnAmt == "2000")
            {
                data.SetValue("product_id", "100000002");//商品ID
            }
            else if (txnAmt == "5000")
            {
                data.SetValue("product_id", "100000003");//商品ID
            }
            else if (txnAmt == "10000")
            {
                data.SetValue("product_id", "100000004");//商品ID
            }
            else
            {
                data.SetValue("product_id", "999999999");//商品ID
            }
            string msg;
            WeChatPayData result = WeChatPayApi.UnifiedOrder(data, out msg);//调用统一下单接口  
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            if (msg == "")
            {
                b = CreateOrders(strOrderNo, strOrderType, cardid, cardtype, txnAmt, terminalid, "1", txnAmt);
            }
            tradeNo = strOrderNo;
            return url;
        }

        /// <summary>
        /// 参数数组转换为url格式
        /// </summary>
        /// <param name="map">参数名与参数值的映射表</param>
        /// <returns>URL字符串</returns>
        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// 创建业务订单和支付订单
        /// </summary>
        /// <param name="orderNo">业务订单号</param>
        /// <param name="payType">订单类型 1:售卡 2:充值</param>
        /// <param name="cardId">卡号</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="txnAmt">订单金额</param>
        /// <param name="terminalId">设备终端号</param>
        /// <param name="orderType">订单类型 0:现金 1:微信 2:支付宝 3:银行卡</param>
        /// <param name="rechargeAmt">充值金额</param>
        /// <returns></returns>
        private bool CreateOrders(string orderNo, string orderType, string cardId, string cardType, string txnAmt, string terminalId, 
            string payType,  string rechargeAmt)
        {
            OrderInfoModel orderInfo = new OrderInfoModel();
            orderInfo.ORDER_NO = orderNo;
            orderInfo.ORDER_TYPE = orderType;
            orderInfo.ORDER_MONEY = txnAmt;
            orderInfo.TRADE_DATE = "sysdate";
            orderInfo.CARD_NO = cardId;
            orderInfo.CARD_TYPE = cardType;
            orderInfo.PHY_CARD_TYPE = "3";//CPU 卡
            orderInfo.RECHARGE_MONEY = rechargeAmt;
            orderInfo.RECHARGE_INDEX = "0";
            orderInfo.TERMINAL_ID = terminalId;
            orderInfo.STATUS = "0";
            orderInfo.REFUNDFLAG = "0";
            orderInfo.DATATYPE = payType;
            orderInfo.NFC_LOCK_TIME = "sysdate";
            orderInfo.ERROR_CODE = "0";
            orderInfo.SETT_DATE = DateTime.Now.ToString("yyyyMMdd");
            orderInfo.SETTFLAG = "0";
            orderInfo.CREATION_TIME = "sysdate";
            orderInfo.PAY_ORDER_NO = WeChatPayApi.GenerateOutPayTradeNo(terminalId);//
            orderInfo.PAY_LOCK_TIME = "sysdate";
            orderInfo.PAY_MONEY = txnAmt;

            dalQRCode.CreateOrders(orderInfo);//新增订单
            return true;
        }
        #endregion

        #region 生成支付宝二维码
        /// <summary>
        /// 生成支付宝支付的二维码数据
        /// </summary>
        /// <param name="txnType">交易类型</param>
        /// <param name="txnAmt">交易金额</param>
        /// <param name="terminalId">设备ID</param>
        /// <param name="cardId">卡号</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="tradeNo">交易单号</param>
        /// <param name="qrData"></param>
        /// <returns></returns>
        private ResultEnum GetPayAliQRCodeData(string txnType, string txnAmt, string terminalId, string cardId, string cardType,
            out string tradeNo, out string qrData)
        {
            tradeNo = string.Empty;
            qrData = string.Empty;
            try
            {
                AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(txnType, txnAmt, terminalId);
                //商户接收异步通知的地址 如果需要接收扫码支付异步通知，请调用另外一个方法
                string notify_url = "";    //http://218.17.253.186:8804/Business/notify_url.aspx
                IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(AliPayConfig.serverUrl,
                    AliPayConfig.appId,
                    AliPayConfig.merchant_private_key, AliPayConfig.version,
                    AliPayConfig.sign_type, AliPayConfig.alipay_public_key, AliPayConfig.charset);
                AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, notify_url);
                switch (precreateResult.Status)
                {
                    case ResultEnum.SUCCESS:
                        qrData = precreateResult.response.QrCode;
                        tradeNo = builder.out_trade_no;
                        CreateOrders(tradeNo, txnType == "1" ? "1" : "2", cardId, cardType, txnAmt, terminalId, "2",
                            txnAmt);
                        break;
                }
                return precreateResult.Status;
            }
            catch (Exception ex)
            {
                LogHelper.Log("GetPayAliQRCodeData", ex);
                return ResultEnum.UNKNOWN;
            }
        }

        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(string txnType, string txnAmt, string terminalid)
        {
            //线上联调时，请输入真实的外部订单号。
            string out_trade_no = WeChatPayApi.GenerateOutTradeNo(terminalid);//卡号+时间
            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            builder.out_trade_no = out_trade_no;

            builder.total_amount = "0.01";//测试使使用的金额
            //builder.total_amount = (double.Parse(txnAmt) / 100).ToString("0.00");//生产时使用的金额
            builder.undiscountable_amount = "0";
            builder.operator_id = terminalid;
            builder.terminal_id = terminalid;
            builder.subject = "扫码支付";
            builder.time_expire = DateTime.Now.AddSeconds(90).ToString("yyyy-MM-dd HH:mm:ss"); //超时时间
            builder.body = "西宁一卡通自助售卡充值业务";
            builder.store_id = string.Format("XN_T_{0}", terminalid);    //很重要的参数，可以用作之后的营销     
            builder.seller_id = AliPayConfig.pid;       //可以是具体的收款账号。

            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = txnType;//用来存放是充值还是售卡交易类型，1：售卡；11：充值
            if (txnType == "1")
            {
                goods.goods_name = "售卡";
            }
            else if (txnType == "11")
            {
                goods.goods_name = "充值";
            }
            else
            {
                goods.goods_name = txnType;
            }
            goods.price = "0.01";//测试使使用的金额
            //goods.price = (double.Parse(txnAmt) / 100).ToString("0.00");//生产时使用的金额
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;
            //扩展参数
            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;
        }
        #endregion

        #endregion


        #region 查询支付状态
        /// <summary>
        /// 查询支付状态
        /// </summary>
        /// <param name="OrderNo">交易订单号</param>
        /// <param name="txnWay">交易方式  1:WeChat  2:AliPay</param>
        /// <returns></returns>
        public ResultBase<DataTable> GetPayState(string OrderNo, string txnWay)
        {
            try
            {
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("PayState");
                DataRow dr = dtResult.NewRow();
                OrderInfoModel orderInfo = new OrderInfoModel();
                orderInfo.STATUS = "";
                if (txnWay == "1")
                {
                    #region 微信支付订单查询
                    WeChatPayData data = new WeChatPayData();
                    data.SetValue("out_trade_no", OrderNo);
                    WeChatPayData result = WeChatPayApi.OrderQuery(data);
                    string strTradeState = result.GetValue("trade_state").ToString();
                    if (strTradeState == "NOTPAY")
                    {
                        dtResult.Rows.Add(dr["PayState"] = ("NOTPAY"));
                        LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "NOTPAY", OrderNo));
                    }
                    else if (strTradeState == "SUCCESS")
                    {
                        orderInfo.STATUS = "4";
                        dtResult.Rows.Add(dr["PayState"] = ("SUCCESS"));
                        LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "SUCCESS", OrderNo));  
                    }
                    else
                    {
                        orderInfo.STATUS = "3";
                        dtResult.Rows.Add(dr["PayState"] = ("FAILURE"));
                        LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "FAILURE", OrderNo));
                    }
                    #endregion
                }
                else if (txnWay == "2")
                {
                    #region 支付宝支付状态查询
                    IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(AliPayConfig.serverUrl, AliPayConfig.appId, AliPayConfig.merchant_private_key, AliPayConfig.version,
                     AliPayConfig.sign_type, AliPayConfig.alipay_public_key, AliPayConfig.charset);
                    AlipayF2FQueryResult queryResult = serviceClient.tradeQuery(OrderNo);
                    if (queryResult.Status == ResultEnum.SUCCESS)
                    {
                        if (queryResult.response.TradeStatus == "TRADE_SUCCESS")
                        {
                            orderInfo.STATUS = "4";
                            dtResult.Rows.Add(dr["PayState"] = ("SUCCESS"));
                            LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "SUCCESS", OrderNo));
                        }
                        else
                        {
                            orderInfo.STATUS = "3";
                            dtResult.Rows.Add(dr["PayState"] = ("FAILURE"));
                            LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "FAILURE", OrderNo));
                        }
                    }
                    else //if (queryResult.Status == ResultEnum.FAILED)
                    {
                        dtResult.Rows.Add(dr["PayState"] = ("NOTPAY"));
                        LogHelper.Log("PayBLL", "查支付状态", string.Format("PAY_State:{0};ORDER_NO:{1}", "NOTPAY", OrderNo) + ";交易不存在");
                    }
                    #endregion
                }
                else
                {
                    return ResultBase<DataTable>.GetFailure(CodeModel.UnknownPayMethod);
                }

                //if (orderInfo.STATUS != "")
                //{
                //    orderInfo.ORDER_NO = OrderNo;
                //    //orderInfo.NFC_END_TIME = "sysdate";
                //    orderInfo.PAY_END_TIME = "sysdate";
                //    dalQRCode.UpdateOrders(orderInfo);
                //}

                return ResultBase<DataTable>.GetSuccess(dtResult);
            }
            catch (Exception ex)
            {
                LogHelper.Log("PayBLL.GetPayState", ex);
                return ResultBase<DataTable>.GetFailure(CodeModel.ErrSystem);
            }
        }

        #endregion
    }
}
