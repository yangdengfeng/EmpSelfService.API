/* ==============================================================================
 * 功能名称：OmTerminalStateTB
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/4/20 17:26:15
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpSelfService.Model
{
    public class OmTerminalStateTB
    {
        /// <summary>
        /// CPU编号
        /// </summary>
        public string CpuId { get; set; }

        /// <summary>
        /// 最后连接时间
        /// </summary>
        public string TimeLast { get; set; }

        /// <summary>
        /// 售卡PSAM卡号
        /// </summary>
        public string SKTerminalID { get; set; }

        /// <summary>
        /// 充值PSAM卡号
        /// </summary>
        public string CZTerminalID { get; set; }

        /// <summary>
        /// 售卡设备状态
        /// </summary>
        public string SKState { get; set; }

        /// <summary>
        /// 充值设备状态
        /// </summary>
        public string CZState { get; set; }

        /// <summary>
        /// 发卡器状态
        /// </summary>
        public string CardIssuerState { get; set; }

        /// <summary>
        /// 打印机状态
        /// </summary>
        public string PrinterState { get; set; }

        /// <summary>
        /// 钱箱状态
        /// </summary>
        public string CashboxState { get; set; }

        /// <summary>
        /// UPS使用状态
        /// </summary>
        public string UpsState { get; set; }

        /// <summary>
        /// UPS电量百分比
        /// </summary>
        public string UpsPercentage { get; set; }

        /// <summary>
        /// 本地自助服务软件的版本号
        /// </summary>
        public string SoftwareVer { get; set; }
    }
}
