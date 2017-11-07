using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 请求实体类型
    /// </summary>
    [Serializable]
    public class RequestModel
    {
        /// <summary>
        /// 设备终端号
        /// </summary>
        public string TerminalNo { get; set; }

        /// <summary>
        /// 参数数据
        /// </summary>
        public string Values { get; set; }
    }
}
