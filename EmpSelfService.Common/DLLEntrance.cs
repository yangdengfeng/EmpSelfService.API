using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EmpSelfService.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DLLEntrance
    {
        #region ToolAPI.dll
        ///*===========================================================================
        //**  函数：		Standard_iPb_AscStrToBinArr
        //**  功能：		字符串转数组转为十六进制数组("1234"==>0x12, 0x34)
        //**  入口参数：	char *inData				字符串数据
        //                unsigned int DataLen		数据长度
        //**  出口参数：	unsigned char *outDate		转换后的十六进制数据
        //**  函数返回值	无
        //=============================================================================*/
        //extern "C" TOOLAPI void _stdcall Standard_iPb_AscStrToBinArr(char *inData, unsigned int DataLen, unsigned char *outDate);
        [DllImport("ToolAPI.dll")]
        public static extern byte Standard_iPb_AscStrToBinArr(string inData, int DataLen, byte[] outDate);

        /// <summary>
        /// 定义转账充值附加记录的函数
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordStr"></param>
        /// <returns></returns>
        [DllImport("ToolAPI.dll")]
        public static extern int GetBankInfRecordStr_JiaoZuo(byte[] Record, byte[] RecordStr);

        /// <summary>
        /// 定义客户附加记录的函数
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordStr"></param>
        /// <returns></returns>
        [DllImport("ToolAPI.dll")]
        public static extern int Standard_iPb_GetCustomInf_JiaoZuo(byte[] Record, byte[] RecordStr);

        /// <summary>
        /// 定义考勤交易记录的函数
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordStr"></param>
        /// <returns></returns>
        [DllImport("ToolAPI.dll")]
        public static extern int Standard_iPb_RecordToStr917_JiaoZuo128(byte[] Record, byte[] RecordStr);

        /// <summary>
        /// 定义取交易记录的函数
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordStr"></param>
        /// <returns></returns>
        [DllImport("ToolAPI.dll")]
        public static extern int Standard_iPb_GetRecord_Zhoushan(byte[] Record, byte[] RecordStr);

        /// <summary>
        /// 定义取交易记录的函数
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordStr"></param>
        /// <returns></returns>
        [DllImport("ToolAPI.dll")]
        public static extern int Standard_iPb_GetRecord_JiaoZuo(byte[] Record, byte[] RecordStr);

        //定义取设备状态的函数
        [DllImport("ToolAPI.dll")]
        public static extern int Standard_iPb_215ToStr(byte[] Record, byte[] RecordStr);
        #endregion

        #region 交易文件转换 AnalyzeToo.dll
        /*===========================================================================
        **  函数：		GetDllVersion
        **  功能：		获取版本号
        **  入口参数：	无
        **  出口参数：  char* Version
        **  函数返回值	无
        =============================================================================*/
        [DllImport("AnalyzeTool.dll")]
        public static extern int GetDllVersion(byte[] Version);

        /*===========================================================================
        **  函数：		XD_InitDllNew
        **  功能：		动态库初始化(供入库清分使用)
        **  入口参数：	char * filePathName 模版文件名
        **  出口参数：  无
        **  函数返回值	= ERR_OK			成功
                        = 其他(错误代码)    失败
        =============================================================================*/
        [DllImport("AnalyzeTool.dll")]
        public static extern int XD_InitDllNew(string filePathName);

        /*===========================================================================
        **  函数：		XD_AnalyzeRecordNew
        **  功能：		单笔交易记录解析(供入库清分使用)
        **  入口参数：	unsigned char *Record 单条交易记录
                        unsigned int RecordLen 单笔交易记录长度
                        int appFlag 附加标志
        **  出口参数：  char *recordRsult 返回交易记录结果集
        **  函数返回值	= ERR_OK			成功
                        = 其他(错误代码)    失败
        =============================================================================*/
        [DllImport("AnalyzeTool.dll")]
        public static extern int XD_AnalyzeRecordNew(byte[] Record, int RecordLen, byte[] recordRsult, ref int appFlag);

        /*===========================================================================
        **  函数：		Standard_iPb_CRC_16
        **  功能：		计算CRC_16
        **  入口参数：	unsigned char *BIN	十六进制数组
				        int BinLen			数组大小
        **  出口参数：	char *Str			字符串输出
        **  函数返回值	无
        =============================================================================*/
        [DllImport("AnalyzeTool.dll")]
        public static extern int Standard_iPb_CRC_16(byte[] ptr, int pSize, byte[] pCRC);
        #endregion

        #region 加密机 JMJDLL.dll

        /// <summary>
        /// 获取加密机句柄
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport("JMJDLL.dll")]
        public static extern int FS_Connect(string ipAddr, int port);

        [DllImport("JMJDLL.dll")]
        public static extern void FS_DisConnect(int hdl);

        //充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
        //消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
        //extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_YiChun(TSZTHsmHDL hdl, int CardMediaType, int TradeType, char *AppCardNo, char *TAC, char *indata);
        [DllImport("JMJDLL.dll")]
        public static extern int FS_VerifyTACNew_SJL06E(int hdl, int CardMediaType, int KeyID, int ScatterNum, string AppCardNo, string TAC, string indata);


        //M1卡TAC验证。返回0成功。
        //pTacKey: TAC密钥
        //pTacKeyLen: TAC密钥长度，32
        //pTacData：交易产生的TAC码
        //pTacDataLen：交易产生的TAC码长度，8
        // pVerifyData:计算TAC的数据域，格式：交易金额(4)+交易类型(2) + 终端机编号(6) + 终端交易序号(4) + 时间(7)
        // pVerifyDataLen:计算TAC的数据域的长度，44
        [DllImport("JMJDLL.dll")]
        public static extern int Verify_M1_TAC_XINING(string pTacKey, int pTacKeyLen, string pTacData, int pTacDataLen, string pVerifyData, int pVerifyDataLen);

        //交通部CPU卡TAC验证。返回0成功。
        //pTacKey: TAC密钥
        //pTacKeyLen: TAC密钥长度，32
        //pTacData：交易产生的TAC码
        //pTacDataLen：交易产生的TAC码长度，8
        // pVerifyData:计算TAC的数据域，格式：交易金额(4)+交易类型(2) + 终端机编号(6) + 终端交易序号(4) + 时间(7)。消费交易类型09，充值交易类型02
        // pVerifyDataLen:计算TAC的数据域的长度，44
        [DllImport("JMJDLL.dll")]
        public static extern int Verify_JTB_TAC_XINING(string pTacKey, int pTacKeyLen, string pTacData, int pTacDataLen, string pVerifyData, int pVerifyDataLen);

        [DllImport("JMJDLL.dll")]
        public static extern int FS_SetConnectionString_XINING(string strDbConnString);

        [DllImport("JMJDLL.dll")]
        public static extern int FS_PlainKeyData_XINING(int KeyID, string cardNo, string outdata);

        [DllImport("JMJDLL.dll")]
        public static extern int FS_VerifyTACNew_Sjj1309(int hdl, int CardMediaType, int KeyID, int ScatterNum, string AppCardNo, string TAC, string indata);
        #endregion
    }
}
