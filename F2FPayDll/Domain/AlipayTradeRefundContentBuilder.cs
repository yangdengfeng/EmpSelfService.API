using System;
using System.Collections.Generic;
using System.Web;


namespace F2FPayDll.Domain
{
    /// <summary>
    /// AlipayTradeRefundContentBuilder 的摘要说明
    /// </summary>
    public class AlipayTradeRefundContentBuilder : JsonBuilder
    {

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public string refund_amount { get; set; }

        /// <summary>
        /// 标识一次退款请求，同一笔交易多次退款需要保证唯一，如需部分退款，则此参数必传
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string refund_reason { get; set; }


        public AlipayTradeRefundContentBuilder()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }


    }
}