
#ifdef JMJDLL_EXPORTS
#define JMJDLL_API __declspec(dllexport)
#else
#define JMJDLL_API __declspec(dllimport)
#endif

#include "Struct.h"

extern "C" JMJDLL_API void		 __stdcall	FS_GetDllVersion(char *Version);
extern "C" JMJDLL_API TSZTHsmHDL __stdcall	FS_Connect(char *ipAddr, int port);//与加密机建立连接
extern "C" JMJDLL_API void		 __stdcall	FS_DisConnect(TSZTHsmHDL hdl);//与加密机断开连接
extern "C" JMJDLL_API int		 __stdcall	FS_Authen_Conversation(TSZTHsmHDL hdl, char* indata);//会话密钥认证
extern "C" JMJDLL_API int		 __stdcall	FS_GenerateConversationMAC(TSZTHsmHDL hdl, char* indata, char* outdata);//生成会话MAC
extern "C" JMJDLL_API int		 __stdcall	FS_TAC_Verify(TSZTHsmHDL hdl, unsigned char CardType, unsigned char *recdata);
extern "C" JMJDLL_API int		 __stdcall	FS_A_CPU_Increase(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_Increase(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_A_CPU_Authen(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_A_CPU_EncryptData_GenerateMac(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_EncryptData_GenerateMac(TSZTHsmHDL hSzt, char *indata, char *outdata);

extern "C" JMJDLL_API int		__stdcall	SZT_A_CPU_EncryptData_GenerateMac(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		__stdcall	SZT_RF_SIM_EncryptData_GenerateMac(TSZTHsmHDL hSzt, char *indata, char *outdata);

extern "C" JMJDLL_API int		 __stdcall  FS_RF_SIM_Authen(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_A_CPU_WR(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_WR(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_fnAuthenInit(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_fnAuthen1(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_fnAuthen2(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_fnRead(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_fnWrite(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_Apply_Sum(TSZTHsmHDL hdl, char* outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_Reload_Sum(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_Dispense_Sum(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_Set_Hsm(TSZTHsmHDL hdl, char *HsmID, char *output);
extern "C" JMJDLL_API int		 __stdcall	FS_Get_Hsm(TSZTHsmHDL hdl, char *HsmID, char *AuthID, char *AuthEndTime, char *AuthRemainMoney);
extern "C" JMJDLL_API int		 __stdcall	FS_Generate_Sum(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_Generate_Sum_ToFile(TSZTHsmHDL hdl, char *indata, char *pTerminalID, char *AuthFileNamePath);
extern "C" JMJDLL_API int		 __stdcall	FS_Verify_Tac_ALL(TSZTHsmHDL hdl, RecTac TacRec);
extern "C" JMJDLL_API int		 __stdcall	FS_OnlineOperate_C(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_UnionLongConnSJL06Cmd(TSZTHsmHDL hdl, char *reqStr, int lenOfReqStr, char *resStr, int sizeOfResStr);

extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_CreateMAC1(TSZTHsmHDL hSzt, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_CheckMAC2(TSZTHsmHDL hSzt, char *indata);

//indata数据个数：
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall	FS_Verify_Tac_ACPU(TSZTHsmHDL hdl, 
															   int KeyID, 
															   int ScatterNum, 
															   char *AppCardNo, 
															   char *TAC, 
															   char *indata);


//MAC计算及短信密钥加解密
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_CreateMACOrEncryptDecryptData(TSZTHsmHDL hSzt, char *indata, char *outdata);

//================add by chengliang(泉州专用函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC_QuanZhou(TSZTHsmHDL hdl, char *LS_Data, char *indata, int DataLen, char *MAC);
extern "C" JMJDLL_API int		 __stdcall	FS_CreateMAC_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC1_QuanZhou(TSZTHsmHDL hdl, char *indata);
extern "C" JMJDLL_API int		 __stdcall	FS_GreateMAC2_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_QuanZhou(TSZTHsmHDL hdl, unsigned char *recdata);//recdata数据格式：96字节交易记录数据
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_QuanZhou(TSZTHsmHDL hdl, char *AppCardNo, char *TAC, char *indata);
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTAC_QuanZhou(TSZTHsmHDL hdl, unsigned char *recdata, char *outdata);//生成TAC
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTACNew_QuanZhou(TSZTHsmHDL hdl, char *AppCardNo, char *indata, char *outdata);//生成TAC
extern "C" JMJDLL_API int		 __stdcall	FS_EncryptDecryptData_QuanZhou(TSZTHsmHDL hdl, char *EncDecFlag, char* LS_Data, char *indata, int DataLen, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_GreatePaymentMAC1_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckPaymentMAC2_QuanZhou(TSZTHsmHDL hdl, char *indata);

//================add by chengliang(龙岩专用函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC_LongYan(TSZTHsmHDL hdl, char *LS_Data, char *indata, int DataLen, char *MAC);
extern "C" JMJDLL_API int		 __stdcall	FS_CreateMAC_LongYan(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC1AndGreateMAC2_LongYan(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_LongYan(TSZTHsmHDL hdl, unsigned char *recdata);//recdata数据格式：128字节交易记录数据
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_LongYan(TSZTHsmHDL hdl, char *AppCardNo, char *TAC, char *indata);
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTAC_LongYan(TSZTHsmHDL hdl, unsigned char *recdata, char *outdata);//生成TAC
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTACNew_LongYan(TSZTHsmHDL hdl, char *AppCardNo, char *indata, char *outdata);//生成TAC
extern "C" JMJDLL_API int		 __stdcall	FS_EncryptDecryptData_LongYan(TSZTHsmHDL hdl, char *EncDecFlag, char* LS_Data, char *indata, int DataLen, char *outdata);

//================add by chengliang(惠州加密机函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_GetCreditKey_HuiZhou(TSZTHsmHDL hdl, char *indata, char *outdata);//计算充值密钥

//================add by chengliang 20120911(焦作加密机函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_EncryDecryptData_JiaoZuo(TSZTHsmHDL hdl, int EncDecFlag, char *custom, int KeyID, int ScatterNum, char *scatterData, char *indata, int inDataLen, char *outdata);//加解密数据
extern "C" JMJDLL_API int		 __stdcall	FS_PlainKeyData_JiaoZuo(TSZTHsmHDL hdl, char *custom, int KeyID, int ScatterNum, char *scatterData, char *outdata);//用指定的次主密钥分散数据
extern "C" JMJDLL_API int		 __stdcall  FS_PlainkeyGenerateMac_JiaoZuo(TSZTHsmHDL hdl, char *custom, int  MACType, int KeyID, int MACAlgType, int ScatterNum, char *scatterData, char *MACIVData, char *Data, int DataLen, char *outdata);//分散次主密钥产生MAC
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_JiaoZuo(TSZTHsmHDL hdl, unsigned char *recdata);//recdata数据格式：128字节交易记录数据
//CardMediaType：2：ACPU卡	3：M1卡
//TradeType：	 2：充值	9：消费
//ACPU卡充值indata数据格式：4*2字节电子钱包新余额(充值后) + 2*2字节电子钱包交易序号(充值1前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//ACPU卡消费、M1卡消费充值indata数据格式：4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_JiaoZuo(TSZTHsmHDL hdl, int CardMediaType, char *AppCardNo, char *TAC, char *indata);

extern "C" JMJDLL_API int       __stdcall   FS_VerifyTACUpdate_JiaoZuo(TSZTHsmHDL hdl, 
                                                                            int CardMediaType, 
                                                                            char *AppCardNo, 
                                                                            char *ATS_CardNo, 
                                                                            char *TAC, 
                                                                            char *indata);
//================add by chengliang 20130218(包头加密机函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_ChangeCardType_BT(TSZTHsmHDL hdl, char *indata, char *outdata);


//================Start of 张活林 on 2013-8-1 12:12 v2222(宜春加密机函数)==================
extern "C" JMJDLL_API int		 __stdcall	FS_LinkCryptServer_YiChun(TSZTHsmHDL hdl, char *password, char *cck_iv);
extern "C" JMJDLL_API int		 __stdcall	FS_EncryDecryptData_YiChun(TSZTHsmHDL hdl, int EncDecFlag, char *custom, int KeyID, int ScatterNum, char *scatterData, char *indata, int inDataLen, char *outdata);//加解密数据
extern "C" JMJDLL_API int		 __stdcall	FS_PlainKeyData_YiChun(TSZTHsmHDL hdl, char *custom, int KeyID, int ScatterNum, char *scatterData, char *outdata);//用指定的次主密钥分散数据
extern "C" JMJDLL_API int		 __stdcall  FS_PlainkeyGenerateMac_YiChun(TSZTHsmHDL hdl, char *custom, int  MACType, int KeyID, int MACAlgType, int ScatterNum, char *scatterData, char *MACIVData, char *Data, int DataLen, char *outdata);//分散次主密钥产生MAC

extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_YiChun(TSZTHsmHDL hdl, unsigned char *recdata);//recdata数据格式：128字节交易记录数据
//充值indata数据格式：0000000000000000 + 4*2字节电子钱包新余额 + 2*2字节电子钱包交易序号(加一前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
//消费indata数据格式：0000000000000000 + 4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_YiChun(TSZTHsmHDL hdl, int TradeType, char *AppCardNo, char *TAC, char *indata);


/**************************************************************************
 * 函 数 名: FS_VerifyTACNew_ChongQing      
 * 功能描述:    
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:  CardMediaType：2：ACPU卡  3：M1卡
               KeyID 加密机密钥索引
               ScatterNum 分散次数
               AppCardNo分散因子
               TAC 值
               indata
                //ACPU卡充值indata数据格式：4*2字节电子钱包新余额(充值后) + 2*2字节电子钱包交易序号(充值1前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
                //ACPU卡消费、M1卡消费充值indata数据格式：4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_ChongQing(TSZTHsmHDL hdl, 
                                                                    int CardMediaType, 
                                                                    int KeyID,
                                                                    int ScatterNum,
                                                                    char *AppCardNo, 
                                                                    char *TAC, 
                                                                    char *indata);


/**************************************************************************
 * 函 数 名:  FS_EncryDecryptData_Sjj1310      
 * 功能描述:  3DES加解密  
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:         
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                calc_id     算法ID；0：加密 1：解密
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
                pMACIVData  MAC初始数据    
                DataLen     数据明文长度(2字节可视字符)
                Data        数据明文  
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1310(TSZTHsmHDL hdl, 
                                                          int EncDecFlag, // 加解密标识0：加密 1：解密
                                                          char *custom, 
                                                          int KeyID, 
                                                          int ScatterNum, 
                                                          char *scatterData, 
                                                          char *indata, 
                                                          int inDataLen, 
                                                          char *outdata);

/**************************************************************************
 * 函 数 名: FS_PlainKeyData_Sjj1310      
 * 功能描述: 导出密钥   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:       
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
 * 输出参 数:   
                分散后的数据 outdata
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1310(TSZTHsmHDL hdl, 
                                                        char *custom, 
                                                        int KeyID, 
                                                        int ScatterNum, 
                                                        char *scatterData, 
                                                        char *outdata);

/**************************************************************************
 * 函 数 名: FS_PlainkeyGenerateMac_Sjj1310      
 * 功能描述: 分散次主密钥产生MAC   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:     
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                calc_id     算法ID；0：加密 1：解密
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
                pMACIVData  MAC初始数据    
                DataLen     数据明文长度(2字节可视字符)
                Data        数据明文   
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainkeyGenerateMac_Sjj1310(TSZTHsmHDL hdl, 
                                                                        char *custom, 
                                                                        int  MACType,
                                                                        int  KeyID,
                                                                        int  MACAlgType,
                                                                        int  ScatterNum,
                                                                        char *scatterData,
                                                                        char *MACIVData,
                                                                        char *Data,
                                                                        int  DataLen,
                                                                        char *outdata);   


/**************************************************************************
 * 函 数 名: FS_VerifyTACNew_Sjj1310      
 * 功能描述:    
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:  CardMediaType：2：ACPU卡  3：M1卡
               KeyID 加密机密钥索引
               ScatterNum 分散次数
               AppCardNo分散因子
               TAC 值
               indata
                //ACPU卡充值indata数据格式：4*2字节电子钱包新余额(充值后) + 2*2字节电子钱包交易序号(充值1前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
                //ACPU卡消费、M1卡消费充值indata数据格式：4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
                //m1 交易类型1 + 终端交易序列号(大端)3 + 城市代码2 + 卡唯一代码4 + 卡类型1 + 交易前额(小端)3 + 交易金额(小端)3 + 交易时间7 + 卡计数器(大端)2 + 终端代码4 + "8000"
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTACNew_Sjj1310(TSZTHsmHDL hdl, 
                                                            int CardMediaType, 
                                                            int KeyID,
                                                            int ScatterNum,
                                                            char *AppCardNo, 
                                                            char *TAC, 
                                                            char *indata);                                                                        


/**************************************************************************
 * 函 数 名: FS_LinkCryptServer_Sjj1313      
 * 功能描述: Sjj1313加密机初始化
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:         
                sckConn     与加密机连接Socket标识
  
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_LinkCryptServer_Sjj1313(TSZTHsmHDL hdl, 
                                                                    char *password, 
                                                                    char *cck_iv);


/**************************************************************************
 * 函 数 名: FS_PlainKeyData_Sjj1313      
 * 功能描述: Sjj1313导出密钥   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:       
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
 * 输出参 数:   
                分散后的数据 outdata
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1313(TSZTHsmHDL hdl, 
                                                                char *custom, 
                                                                int KeyID, 
                                                                int ScatterNum, 
                                                                char *scatterData, 
                                                                char *outdata);       


/**************************************************************************
 * 函 数 名:  FS_EncryDecryptData_Sjj1313      
 * 功能描述:  3DES加解密  
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:         
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                calc_id     算法ID；0：加密 1：解密
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
                pMACIVData  MAC初始数据    
                DataLen     数据明文长度(2字节可视字符)
                Data        数据明文  
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1313(TSZTHsmHDL hdl, 
                                                                  int EncDecFlag, // 加解密标识0：加密 1：解密
                                                                  char *custom, 
                                                                  int KeyID, 
                                                                  int ScatterNum, 
                                                                  char *scatterData, 
                                                                  char *indata, 
                                                                  int inDataLen, 
                                                                  char *outdata);                                                          


/**************************************************************************
 * 函 数 名: FS_VerifyTACNew_Sjj1313      
 * 功能描述:    
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:  CardMediaType：2：ACPU卡  3：M1卡
               KeyID 加密机密钥索引
               ScatterNum 分散次数
               AppCardNo分散因子
               TAC 值
               indata
                //ACPU卡充值indata数据格式：4*2字节电子钱包新余额(充值后) + 2*2字节电子钱包交易序号(充值1前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
                //ACPU卡消费、M1卡消费充值indata数据格式：4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
                //m1 交易类型1 + 终端交易序列号(大端)3 + 城市代码2 + 卡唯一代码4 + 卡类型1 + 交易前额(小端)3 + 交易金额(小端)3 + 交易时间7 + 卡计数器(大端)2 + 终端代码4 + "8000"
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTACNew_Sjj1313(TSZTHsmHDL hdl, 
                                                            int CardMediaType, 
                                                            int KeyID,
                                                            int ScatterNum,
                                                            char *AppCardNo, 
                                                            char *TAC, 
                                                            char *indata);


//-------------------------------------------------------------------------------------------------------
/**************************************************************************
 * 函 数 名:  FS_EncryDecryptData_Sjj1309      
 * 功能描述:  3DES加解密  
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:         
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                calc_id     算法ID；0：加密 1：解密
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
                pMACIVData  MAC初始数据    
                DataLen     数据明文长度(2字节可视字符)
                Data        数据明文  
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1309(TSZTHsmHDL hdl, 
                                                          int EncDecFlag, // 加解密标识0：加密 1：解密
                                                          char *custom, 
                                                          int KeyID, 
                                                          int ScatterNum, 
                                                          char *scatterData, 
                                                          char *indata, 
                                                          int inDataLen, 
                                                          char *outdata);

/**************************************************************************
 * 函 数 名: FS_PlainKeyData_Sjj1309      
 * 功能描述: 导出密钥   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:       
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
 * 输出参 数:   
                分散后的数据 outdata
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1309(TSZTHsmHDL hdl, 
                                                        char *custom, 
                                                        int KeyID, 
                                                        int ScatterNum, 
                                                        char *scatterData, 
                                                        char *outdata);

/**************************************************************************
 * 函 数 名: FS_PlainkeyGenerateMac_Sjj1309      
 * 功能描述: 分散次主密钥产生MAC   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:     
                sckConn     与加密机连接Socket标识
                KeyID       加密机内保护密钥编号(8字节可视字符)
                calc_id     算法ID；0：加密 1：解密
                ScatterNum  离散次数(1字节可视字符)
                scatterData 离散因子数据(离散次数*16字节可视字符)
                pMACIVData  MAC初始数据    
                DataLen     数据明文长度(2字节可视字符)
                Data        数据明文   
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainkeyGenerateMac_Sjj1309(TSZTHsmHDL hdl, 
                                                                        char *custom, 
                                                                        int  MACType,
                                                                        
                                                                        int  KeyID,
                                                                        
                                                                        int  MACAlgType,
                                                                        
                                                                        int  ScatterNum,
                                                                        char *scatterData,
                                                                        
                                                                        char *MACIVData,
                                                                        
                                                                        char *Data,
                                                                        int  DataLen,
                                                                        
                                                                        char *outdata);   


/**************************************************************************
 * 函 数 名: FS_VerifyTACNew_Sjj1309      
 * 功能描述:    
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:  CardMediaType：2：ACPU卡  3：M1卡
               KeyID 加密机密钥索引
               ScatterNum 分散次数
               AppCardNo分散因子
               TAC 值
               indata
                //ACPU卡充值indata数据格式：4*2字节电子钱包新余额(充值后) + 2*2字节电子钱包交易序号(充值1前) + 4*2字节交易金额 + 1*2字节交易类型 + 6*2字节终端代号 + 7*2字节交易日期时间
                //ACPU卡消费、M1卡消费充值indata数据格式：4*2交易金额 + 1*2交易类型标识 + 6*2终端机编号 + 4*2终端交易序号(SAM卡交易序号) + 7*2字节交易日期时间
                //m1 交易类型1 + 终端交易序列号(大端)3 + 城市代码2 + 卡唯一代码4 + 卡类型1 + 交易前额(小端)3 + 交易金额(小端)3 + 交易时间7 + 卡计数器(大端)2 + 终端代码4 + "8000"
 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTACNew_Sjj1309(TSZTHsmHDL hdl, 
                                                            int CardMediaType, 
                                                            int KeyID,
                                                            int ScatterNum,
                                                            char *AppCardNo, 
                                                            char *TAC, 
                                                            char *indata); 


/**************************************************************************
 * 函 数 名:  FS_RSA_GeneratePEMKeys_Sjj1310      
 * 功能描述:  生成非对称对
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:         
                hdl         与加密机连接Socket标识
                Bits        密钥长度，如1024,1152
                e_bits      指数长度，如1,3
                pKeyIndex   密钥索引01-20，99表示不保存新生成密钥

 * 输出参 数:         
                pRSA_PUBLIC_KEY  公钥
                pPrivateLen      私钥长度
                pPrivate         加密后的私钥
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GeneratePEMKeys_Sjj1310(TSZTHsmHDL hdl, 
                                                                int Bits,  
                                                                int e_bits, 
                                                                const char *pKeyIndex,
                                                                R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY,
                                                                unsigned int *pPrivateLen,
                                                                unsigned char *pPrivate);

/**************************************************************************
 * 函 数 名:  FS_RSA_GetPublic_Sjj1310      
 * 功能描述:  从私钥获取公钥
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:      
                hdl         与加密机连接Socket标识
                pKeyIndex   密钥索引

                PrivateLen  私钥长度
                pPrivate    私钥数据
                
 * 输出参 数:         
                pRSA_PUBLIC_KEY        公钥
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GetPublic_Sjj1310(TSZTHsmHDL hdl, 
                                                            const char *pKeyIndex,
                                                            unsigned int PrivateLen,
                                                            const unsigned char *pPrivate,
                                                            R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY);



                                                                
/**************************************************************************
 * 函 数 名:  FS_RSA_GetPrivate_Sjj1310      
 * 功能描述:  导出非对称私钥
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:      
                hdl         与加密机连接Socket标识
                pKeyIndex   密钥索引

                PrivateLen  私钥长度
                pPrivate    私钥数据
                
 * 输出参 数:         
                pRSA_PUBLIC_KEY        公钥
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GetPrivate_Sjj1310(TSZTHsmHDL hdl, 
                                                        const char *pKeyIndex,
                                                        
                                                        unsigned int PrivateLen,
                                                        const unsigned char *pPrivate,
                                                        
                                                        R_RSA_PRIVATE_KEY *pRSA_PRIVATE_KEY);







/**************************************************************************
 * 函 数 名:  FS_RSA_PrivateDecrypt_Sjj1310      
 * 功能描述:  私钥解密
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:      
                hdl         与加密机连接Socket标识
                pKeyIndex   密钥索引

                PrivateLen  私钥长度
                pPrivate    私钥数据

                in_len      输入数据长度
                pInput      输入数据          
                
 * 输出参 数:         
                pOutlen     输出数据长度
                pOut        输出数据
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_PrivateDecrypt_Sjj1310(TSZTHsmHDL hdl, 
                                                                    const char *pKeyIndex, 
                                                
                                                                    unsigned int PrivateLen,
                                                                    const unsigned char *pPrivate,
                                                                    
                                                                    unsigned int in_len,
                                                                    const unsigned char *pInput,

                                                                    unsigned int *pOutlen,    
                                                                    unsigned char *pOut);




/**************************************************************************
 * 函 数 名:  FS_RSA_PrivateDecrypt_Sjj1310      
 * 功能描述:  私钥解密
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:      
                hdl     与加密机连接Socket标识
                pKeyIndex   密钥索引

                pRSA_PUBLIC_KEY     公钥数据

                in_len      输入数据长度
                pInput      输入数据                     
                
 * 输出参 数:         
                pOutlen     输出数据长度
                pOut        输出数据
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_PublicEncrypt_Sjj1310(TSZTHsmHDL hdl, 
                                                                    const char *pKeyIndex, 
                                                
                                                                    const R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY,
                                                                    
                                                                    unsigned int in_len,
                                                                    const unsigned char *pInput,

                                                                    unsigned int *pOutlen,    
                                                                    unsigned char *pOut);

/**************************************************************************
 * 函 数 名: FS_VerifyTC_Sjj1309      
 * 功能描述: 验证电子现金的TC值   
 * 调用:          
 * 被调用:      
 * Table Accessed: 
 * Table Updated:  
 * 输入参 数:  
               KeyID 加密机密钥索引: 196
               Scatter Num 分散次数: 1
               AppCard No分散因子:
               tag【5A】采用PAN+右补F的方式存放“3104950012345678901f”，tag【5F34】设置为0x01，分散因子应是0x5001234567890101；
               
               TAC 值:  9F26(8byte)
               pInDev : 终端输入 9f02(6byte)..9f03(6byte)..9f1a(2byte)..95(5byte)..5f2a(2byte)..9a(3byte)..9c(1byte)..9f37(4byte)
               pInCard  卡片输入 82(2byte)..9F36(2byte)..9F10(4byte)

 * 输出参 数:         
 * 返 回 值:  成功: 0, 失败: 其他     
 * 创建人:    张活林  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTC_Sjj1309(TSZTHsmHDL hdl, 
                                                        int KeyID,
                                                        int ScatterNum,
                                                        char *AppCardNo, 
                                                        char *TAC, 
                                                        char *pInDev,
                                                        char *pInCard);                                                                    

