using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EmpSelfService.Common
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    [DataContract]
    public class CallException
    {
        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public Exception SourceException { get; set; }

        public CallException(System.Exception ex): base()
        {
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            this.SourceException = ex;
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
