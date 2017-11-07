using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using F2FPayDll.Model;


namespace F2FPayDll.Business
{
    /// <summary>
    /// AlipayF2FPayResult 的摘要说明
    /// </summary>
    public class AlipayF2FPayResult
    {
        public AlipayF2FPayResult()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public AlipayTradePayResponse response { get; set; }

        public ResultEnum Status
        {
            get
            {

                if (response != null)
                {
                    if (response.Code == ResultCode.SUCCESS)
                    {
                        return ResultEnum.SUCCESS;
                    }
                    else
                        return ResultEnum.FAILED;
                }
                else
                {
                    return ResultEnum.UNKNOWN;
                }
            }

        }


    }
}