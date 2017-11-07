/* ==============================================================================
 * 功能名称：ErrCodeModel
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：xulx
 * 创建日期：2016/5/4 17:52:43
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    public static class CodeModel
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static string SUCCESS { get { return "0"; } }

        /// <summary>
        /// 系统错误
        /// </summary>
        public static string ErrSystem { get { return "30001"; } }

        /// <summary>
        /// 参数数据错误
        /// </summary>
        public static string ErrParaData { get { return "30002"; } }

        /// <summary>
        /// 参数重复使用
        /// </summary>
        public static string ErrParaRepeat { get { return "30003"; } }

        /// <summary>
        /// 参数数量错误
        /// </summary>
        public static string ErrParaNum { get { return "30004"; } }

        /// <summary>
        /// 用户不存在
        /// </summary>
        public static string ErrParaUser { get { return "30005"; } }

        /// <summary>
        /// 二维码生成失败
        /// </summary>
        public static string ErrCreateQrCode { get { return "30006"; } }

        /// <summary>
        /// 配置或网络异常
        /// </summary>
        public static string ErrConfigOrNetWork { get { return "30007"; } }

        /// <summary>
        /// 未知的支付方式
        /// </summary>
        public static string UnknownPayMethod { get { return "30008"; } }

        /// <summary>
        /// 超出限制 (如：一个设备终端号24小时内只允许获取一次)
        /// </summary>
        public static string BeyondLimited { get { return "30009"; } }

        /// <summary>
        /// 查询失败，无满足条件的数据
        /// </summary>
        public static string QueryFail { get { return "30010"; } }
        /// <summary>
        /// 获取文件失败，文件未找到
        /// </summary>
        public static string FileNotFound{get { return "30011"; }}

        /// <summary>
        /// 连接加密机失败
        /// </summary>
        public static string CONN_JMJ_FAIL { get { return "30012"; } }

        /// <summary>
        /// 连接数据库获取密钥失败
        /// </summary>
        public static string CONN_DB_GET_KEY_FAIL { get { return "30013"; } }

        /// <summary>
        /// 未知的类型
        /// </summary>
        public static string UNKNOWN_OP_TYPE { get { return "30014"; } }

        /// <summary>
        /// 检验TAC失败
        /// </summary>
        public static string TAC_CHECK_FAIL { get { return "2014"; } }

        /// <summary>
        /// 未知的交易类型
        /// </summary>
        public static string UNKNOWN_TXN_TYPE { get { return "2005"; } }
    }


}
