using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace F2FPayDll.Model
{
    /// <summary>
    /// GoodsInfo 的摘要说明
    /// </summary>
    public class GoodsInfo
    {
        /// <summary>
        /// 商品的编号
        /// </summary>
        public string goods_id { get; set; }

        /// <summary>
        /// 支付宝定义的统一商品编号 特殊可选
        /// </summary>
        public string alipay_goods_id { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// 商品单价，单位为元
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 商品类目 特殊可选
        /// </summary>
        public string goods_category { get; set; }

        /// <summary>
        /// 商品描述信息 特殊可选
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GoodsInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
    }
}