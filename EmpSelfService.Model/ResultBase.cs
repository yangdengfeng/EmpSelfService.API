using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EmpSelfService.Model
{    
    /// <summary>
    /// 表示执行操作后的结果实体类
    /// </summary>
    [Serializable]
    public class ResultBase
    {
        /// <summary>
        /// 是否成功的标志
        /// </summary>
        [DataMember]
        public bool Flag { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        [DataMember]
        public string ErrCode { get; set; }

        /// <summary>
        /// 说明信息
        /// </summary>
        [DataMember]
        public string Info { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">是否成功的标志</param>
        /// <param name="errCode">错误码</param>
        /// <param name="info">执行过程中产生的信息（一般是错误信息）</param>
        [Obsolete("建议使用GetFailure方法获取实例")]
        public ResultBase(bool flag, string errCode, string info)
            : this(flag, info)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">是否成功的标志</param>
        /// <param name="info">执行过程中产生的信息（一般是错误信息）</param>
        [Obsolete("建议使用GetFailure方法获取实例")]
        public ResultBase(bool flag, string info)
        {
            this.Flag = flag;
            this.Info = info;
        }

        /// <summary>
        /// 构造函数（Flag=true, Info=String.Empty）
        /// </summary>
        public ResultBase()
            : this(true, string.Empty)
        {
        }

        /// <summary>
        /// 表示成功的操作结果
        /// </summary>
        public static ResultBase Success
        {
            get { return new ResultBase() { Flag = true, Info = string.Empty }; }
        }

        /// <summary>
        /// 获得成功实例
        /// </summary>
        /// <returns></returns>
        public static ResultBase GetSuccess(string info = "", string errCode = "")
        {
            return new ResultBase() { Flag = true, ErrCode = errCode, Info = info };
        }

        /// <summary>
        /// 获得失败实例
        /// </summary>
        /// <returns></returns>
        public static ResultBase GetFailure(string info = "")
        {
            return new ResultBase() { Flag = false, Info = info };
        }

        /// <summary>
        /// 表示失败的操作结果
        /// </summary>
        public static ResultBase Failure
        {
            get { return new ResultBase() { Flag = false, Info = string.Empty }; }
        }


        /// <summary>
        /// 将操作结果隐式转换为布尔值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static implicit operator bool(ResultBase result)
        {
            if (result == null)
            {
                return false;
            }
            return result.Flag;
        }


        public override string ToString()
        {
            return String.Format("Flag={0}, Info={1}", Flag, Info);
        }
    }


    /// <summary>
    /// 表示执行操作后的结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResultBase<T> : ResultBase
    {
        /// <summary>
        /// 操作结果的值
        /// </summary>
        [DataMember]
        public T Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">是否成功的标志</param>
        /// <param name="info">执行过程中产生的信息（一般是错误信息）</param>
        public ResultBase(bool flag, string info)
        {
            this.Flag = flag;
            this.Info = info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="errorcode"></param>
        /// <param name="value"></param>
        /// <param name="info"></param>
        public ResultBase(bool flag, string errorcode, T value, string info)
            : base(flag, errorcode, info)
        {
            this.Value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">操作是否成功的标志</param>
        /// <param name="value">操作的结果</param>
        /// <param name="info">说明信息</param>
        public ResultBase(bool flag, T value, string info)
            : base(flag, info)
        {
            Value = value;
        }

        /// <summary>
        /// 构造函数（Info=String.Empty）
        /// </summary>
        /// <param name="flag">操作是否成功的标志</param>
        /// <param name="value">操作的结果</param>
        public ResultBase(bool flag, T value)
            : this(flag, value, String.Empty)
        {
        }


        /// <summary>
        /// 构造函数（Flag=true, Info=String.Empty）
        /// </summary>
        /// <param name="value">操作的结果</param>
        public ResultBase(T value)  : this(true, value)
        {
        }


        /// <summary>
        /// 构造函数（Flag=true, Value=default(T), Info=String.Empty）
        /// </summary>
        public ResultBase() : this(default(T))
        {
        }

        public new static ResultBase<T> GetSuccess(string info = "")
        {
            return new ResultBase<T>() { Flag = true, Info = info };
        }

        public new static ResultBase<T> GetSuccess(string info = "", string errCode = "")
        {
            return new ResultBase<T>() { Flag = true, ErrCode = errCode, Info = info };
        }

        public static ResultBase<T> GetSuccess(T value = default(T), string info = "", string errCode = "")
        {
            return new ResultBase<T>() { Flag = true, ErrCode = errCode, Info = info, Value = value };
        }

        /// <summary>
        /// 获得失败实例
        /// </summary>
        /// <returns></returns>
        public new static ResultBase<T> GetFailure(string info = "")
        {
            return new ResultBase<T>() { Flag = false, Info = info };
        }

        public new static ResultBase<T> Failure
        {
            get { return new ResultBase<T>(false, default(T), string.Empty); }
        }

        public new static ResultBase<T> Success
        {
            get { return new ResultBase<T>(true, default(T), string.Empty); }
        }

        public override string ToString()
        {
            return String.Format("Flag={0}, Value={1}, Info={2}", Flag, Value, Info);
        }
    }

    /// <summary>
    /// 表示执行操作后的结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResultBase<T, TY> : ResultBase<T>
    {

        /// <summary>
        /// 操作结果的值
        /// </summary>
        [DataMember]
        public TY Value2 { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">是否成功的标志</param>
        /// <param name="info">执行过程中产生的信息（一般是错误信息）</param>
        public ResultBase(bool flag, string info)
        {
            this.Flag = flag;
            this.Info = info;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultBase(bool flag, string errorcode, T value, TY value2, string info)
            : base(flag, errorcode, value, info)
        {
            this.Value2 = value2;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="flag">操作是否成功的标志</param>
        /// <param name="value">操作的结果</param>
        /// <param name="value2">结果二</param>
        /// <param name="info">说明信息</param>
        public ResultBase(bool flag, T value, TY value2, string info)
            : base(flag, value, info)
        {
            Value2 = value2;
        }

        /// <summary>
        /// 构造函数（Info=String.Empty）
        /// </summary>
        /// <param name="flag">操作是否成功的标志</param>
        /// <param name="value">操作的结果</param>
        /// <param name="value2">结果二</param>
        public ResultBase(bool flag, T value, TY value2)
            : this(flag, value, value2, String.Empty)
        {
        }


        /// <summary>
        /// 构造函数（Flag=true, Info=String.Empty）
        /// </summary>
        /// <param name="value">操作的结果</param>
        /// <param name="value2">结果二</param>
        public ResultBase(T value, TY value2)
            : this(true, value, value2)
        {
        }


        /// <summary>
        /// 构造函数（Flag=true, Value=default(T), Info=String.Empty）
        /// </summary>
        public ResultBase()
            : this(default(T), default(TY))
        {
        }

        public new static ResultBase<T, TY> Failure
        {
            get { return new ResultBase<T, TY>(false, default(T), default(TY), string.Empty); }
        }

        public new static ResultBase<T, TY> Success
        {
            get { return new ResultBase<T, TY>(true, default(T), default(TY), string.Empty); }
        }

        public override string ToString()
        {
            return String.Format("Flag={0}, Value={1}, Value2={2}, Info={3}", Flag, Value, Value2, Info);
        }
    }
}
