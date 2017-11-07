using System;
using System.Collections.Generic;
using Aop.Api.Response;

namespace Aop.Api.Request
{
    /// <summary>
    /// AOP API: alipay.data.bill.downloadurl.get
    /// </summary>
    public class AlipayDataBillDownloadurlGetRequest : IAopRequest<AlipayDataBillDownloadurlGetResponse>
    {
        /// <summary>
        /// 账单时间：日账单格式为yyyy-MM-dd,月账单格式为yyyy-MM
        /// </summary>
        public string BillDate { get; set; }

        /// <summary>
        /// 账单类型，目前支持的类型有：air
        /// </summary>
        public string BillType { get; set; }

        /// <summary>
        /// edit by hnh 增加用来存储JSON格式字符
        /// 业务请求接口的参数体，JSON格式，具体包含的内容参见各个接口的请求参数
        /// </summary>
        public string BizContent { get; set; }

        #region IAopRequest Members
        private string apiVersion = "1.0";
		private string terminalType;
		private string terminalInfo;
        private string prodCode;
		private string notifyUrl;

		public void SetNotifyUrl(string notifyUrl){
            this.notifyUrl = notifyUrl;
        }

        public string GetNotifyUrl(){
            return this.notifyUrl;
        }

        public void SetTerminalType(String terminalType){
			this.terminalType=terminalType;
		}

    	public string GetTerminalType(){
    		return this.terminalType;
    	}

    	public void SetTerminalInfo(String terminalInfo){
    		this.terminalInfo=terminalInfo;
    	}

    	public string GetTerminalInfo(){
    		return this.terminalInfo;
    	}

        public void SetProdCode(String prodCode){
            this.prodCode=prodCode;
        }

        public string GetProdCode(){
            return this.prodCode;
        }

        public string GetApiName()
        {
            //return "alipay.data.bill.downloadurl.get";
            return "alipay.data.dataservice.bill.downloadurl.query";
        }

        public void SetApiVersion(string apiVersion){
            this.apiVersion=apiVersion;
        }

        public string GetApiVersion(){
            return this.apiVersion;
        }

        //public IDictionary<string, string> GetParameters()
        //{
        //    AopDictionary parameters = new AopDictionary();
        //    parameters.Add("bill_date", this.BillDate);
        //    parameters.Add("bill_type", this.BillType);
        //    return parameters;
        //}

        public IDictionary<string, string> GetParameters()
        {
            AopDictionary parameters = new AopDictionary();
            parameters.Add("biz_content", this.BizContent);
            return parameters;
        }

        #endregion
    }
}
