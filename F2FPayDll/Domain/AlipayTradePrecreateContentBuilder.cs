using System;
using System.Collections.Generic;
using System.Web;
using F2FPayDll.Model;


namespace F2FPayDll.Domain
{
    /// <summary>
    /// AlipayTradePrecreateContentBuilder 的摘要说明
    /// </summary>
    public class AlipayTradePrecreateContentBuilder : JsonBuilder
    {
        /// <summary>
        /// 商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        public string out_trade_no {get;set;}

        /// <summary>
        /// 卖家支付宝用户ID 2088------
        /// </summary>
        public string seller_id {get;set;}

        /// <summary>
        /// 订单总金额
        /// </summary>
        public string total_amount { get; set; }

        /// <summary>
        /// 可打折金额 可选
        /// </summary>
        public string discountable_amount { get; set; }

        /// <summary>
        /// 不可打折金额 可选
        /// </summary>
        public string undiscountable_amount { get; set; }

        /// <summary>
        /// 买家支付宝账号 可选
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 对交易或商品的描述 可选
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 商品详细说明 可选
        /// </summary>
        public List<GoodsInfo> goods_detail{get;set;}

        /// <summary>
        /// 商户操作员编号 可选
        /// </summary>
        public string operator_id { get; set; }

        /// <summary>
        /// 商户门店编号 可选
        /// </summary>
        public string store_id { get; set; }

        /// <summary>
        /// 商户机具终端编号 可选
        /// </summary>
        public string terminal_id { get; set; }

        /// <summary>
        /// 业务扩展参数 可选
        /// </summary>
        public ExtendParams extend_params { get; set; }

        public string time_expire { get; set; }

        public AlipayTradePrecreateContentBuilder()
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