using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F2FPayDll.Domain
{
    /// <summary>
    /// 为账单查询添加用于构造JSON数据流
    /// </summary>
    public class AlipayBillQueryContentBuilder : JsonBuilder
    {
        /// <summary>
        /// 账单类型
        /// </summary>
        public string bill_type { get; set; }
        /// <summary>
        /// 账单时间
        /// </summary>
        public string bill_date { get; set; }


        public AlipayBillQueryContentBuilder()
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
