
#ifdef JMJDLL_EXPORTS
#define JMJDLL_API __declspec(dllexport)
#else
#define JMJDLL_API __declspec(dllimport)
#endif

#include "Struct.h"

extern "C" JMJDLL_API void		 __stdcall	FS_GetDllVersion(char *Version);
extern "C" JMJDLL_API TSZTHsmHDL __stdcall	FS_Connect(char *ipAddr, int port);//����ܻ���������
extern "C" JMJDLL_API void		 __stdcall	FS_DisConnect(TSZTHsmHDL hdl);//����ܻ��Ͽ�����
extern "C" JMJDLL_API int		 __stdcall	FS_Authen_Conversation(TSZTHsmHDL hdl, char* indata);//�Ự��Կ��֤
extern "C" JMJDLL_API int		 __stdcall	FS_GenerateConversationMAC(TSZTHsmHDL hdl, char* indata, char* outdata);//���ɻỰMAC
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

//indata���ݸ�����
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall	FS_Verify_Tac_ACPU(TSZTHsmHDL hdl, 
															   int KeyID, 
															   int ScatterNum, 
															   char *AppCardNo, 
															   char *TAC, 
															   char *indata);


//MAC���㼰������Կ�ӽ���
extern "C" JMJDLL_API int		 __stdcall	FS_RF_SIM_CreateMACOrEncryptDecryptData(TSZTHsmHDL hSzt, char *indata, char *outdata);

//================add by chengliang(Ȫ��ר�ú���)==================
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC_QuanZhou(TSZTHsmHDL hdl, char *LS_Data, char *indata, int DataLen, char *MAC);
extern "C" JMJDLL_API int		 __stdcall	FS_CreateMAC_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC1_QuanZhou(TSZTHsmHDL hdl, char *indata);
extern "C" JMJDLL_API int		 __stdcall	FS_GreateMAC2_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_QuanZhou(TSZTHsmHDL hdl, unsigned char *recdata);//recdata���ݸ�ʽ��96�ֽڽ��׼�¼����
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_QuanZhou(TSZTHsmHDL hdl, char *AppCardNo, char *TAC, char *indata);
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTAC_QuanZhou(TSZTHsmHDL hdl, unsigned char *recdata, char *outdata);//����TAC
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTACNew_QuanZhou(TSZTHsmHDL hdl, char *AppCardNo, char *indata, char *outdata);//����TAC
extern "C" JMJDLL_API int		 __stdcall	FS_EncryptDecryptData_QuanZhou(TSZTHsmHDL hdl, char *EncDecFlag, char* LS_Data, char *indata, int DataLen, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_GreatePaymentMAC1_QuanZhou(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckPaymentMAC2_QuanZhou(TSZTHsmHDL hdl, char *indata);

//================add by chengliang(����ר�ú���)==================
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC_LongYan(TSZTHsmHDL hdl, char *LS_Data, char *indata, int DataLen, char *MAC);
extern "C" JMJDLL_API int		 __stdcall	FS_CreateMAC_LongYan(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_CheckMAC1AndGreateMAC2_LongYan(TSZTHsmHDL hdl, char *indata, char *outdata);
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_LongYan(TSZTHsmHDL hdl, unsigned char *recdata);//recdata���ݸ�ʽ��128�ֽڽ��׼�¼����
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_LongYan(TSZTHsmHDL hdl, char *AppCardNo, char *TAC, char *indata);
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTAC_LongYan(TSZTHsmHDL hdl, unsigned char *recdata, char *outdata);//����TAC
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall  FS_CreateTACNew_LongYan(TSZTHsmHDL hdl, char *AppCardNo, char *indata, char *outdata);//����TAC
extern "C" JMJDLL_API int		 __stdcall	FS_EncryptDecryptData_LongYan(TSZTHsmHDL hdl, char *EncDecFlag, char* LS_Data, char *indata, int DataLen, char *outdata);

//================add by chengliang(���ݼ��ܻ�����)==================
extern "C" JMJDLL_API int		 __stdcall	FS_GetCreditKey_HuiZhou(TSZTHsmHDL hdl, char *indata, char *outdata);//�����ֵ��Կ

//================add by chengliang 20120911(�������ܻ�����)==================
extern "C" JMJDLL_API int		 __stdcall	FS_EncryDecryptData_JiaoZuo(TSZTHsmHDL hdl, int EncDecFlag, char *custom, int KeyID, int ScatterNum, char *scatterData, char *indata, int inDataLen, char *outdata);//�ӽ�������
extern "C" JMJDLL_API int		 __stdcall	FS_PlainKeyData_JiaoZuo(TSZTHsmHDL hdl, char *custom, int KeyID, int ScatterNum, char *scatterData, char *outdata);//��ָ���Ĵ�����Կ��ɢ����
extern "C" JMJDLL_API int		 __stdcall  FS_PlainkeyGenerateMac_JiaoZuo(TSZTHsmHDL hdl, char *custom, int  MACType, int KeyID, int MACAlgType, int ScatterNum, char *scatterData, char *MACIVData, char *Data, int DataLen, char *outdata);//��ɢ������Կ����MAC
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_JiaoZuo(TSZTHsmHDL hdl, unsigned char *recdata);//recdata���ݸ�ʽ��128�ֽڽ��׼�¼����
//CardMediaType��2��ACPU��	3��M1��
//TradeType��	 2����ֵ	9������
//ACPU����ֵindata���ݸ�ʽ��4*2�ֽڵ���Ǯ�������(��ֵ��) + 2*2�ֽڵ���Ǯ���������(��ֵ1ǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//ACPU�����ѡ�M1�����ѳ�ֵindata���ݸ�ʽ��4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_JiaoZuo(TSZTHsmHDL hdl, int CardMediaType, char *AppCardNo, char *TAC, char *indata);

extern "C" JMJDLL_API int       __stdcall   FS_VerifyTACUpdate_JiaoZuo(TSZTHsmHDL hdl, 
                                                                            int CardMediaType, 
                                                                            char *AppCardNo, 
                                                                            char *ATS_CardNo, 
                                                                            char *TAC, 
                                                                            char *indata);
//================add by chengliang 20130218(��ͷ���ܻ�����)==================
extern "C" JMJDLL_API int		 __stdcall	FS_ChangeCardType_BT(TSZTHsmHDL hdl, char *indata, char *outdata);


//================Start of �Ż��� on 2013-8-1 12:12 v2222(�˴����ܻ�����)==================
extern "C" JMJDLL_API int		 __stdcall	FS_LinkCryptServer_YiChun(TSZTHsmHDL hdl, char *password, char *cck_iv);
extern "C" JMJDLL_API int		 __stdcall	FS_EncryDecryptData_YiChun(TSZTHsmHDL hdl, int EncDecFlag, char *custom, int KeyID, int ScatterNum, char *scatterData, char *indata, int inDataLen, char *outdata);//�ӽ�������
extern "C" JMJDLL_API int		 __stdcall	FS_PlainKeyData_YiChun(TSZTHsmHDL hdl, char *custom, int KeyID, int ScatterNum, char *scatterData, char *outdata);//��ָ���Ĵ�����Կ��ɢ����
extern "C" JMJDLL_API int		 __stdcall  FS_PlainkeyGenerateMac_YiChun(TSZTHsmHDL hdl, char *custom, int  MACType, int KeyID, int MACAlgType, int ScatterNum, char *scatterData, char *MACIVData, char *Data, int DataLen, char *outdata);//��ɢ������Կ����MAC

extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTAC_YiChun(TSZTHsmHDL hdl, unsigned char *recdata);//recdata���ݸ�ʽ��128�ֽڽ��׼�¼����
//��ֵindata���ݸ�ʽ��0000000000000000 + 4*2�ֽڵ���Ǯ������� + 2*2�ֽڵ���Ǯ���������(��һǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
//����indata���ݸ�ʽ��0000000000000000 + 4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_YiChun(TSZTHsmHDL hdl, int TradeType, char *AppCardNo, char *TAC, char *indata);


/**************************************************************************
 * �� �� ��: FS_VerifyTACNew_ChongQing      
 * ��������:    
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:  CardMediaType��2��ACPU��  3��M1��
               KeyID ���ܻ���Կ����
               ScatterNum ��ɢ����
               AppCardNo��ɢ����
               TAC ֵ
               indata
                //ACPU����ֵindata���ݸ�ʽ��4*2�ֽڵ���Ǯ�������(��ֵ��) + 2*2�ֽڵ���Ǯ���������(��ֵ1ǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
                //ACPU�����ѡ�M1�����ѳ�ֵindata���ݸ�ʽ��4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int		 __stdcall	FS_VerifyTACNew_ChongQing(TSZTHsmHDL hdl, 
                                                                    int CardMediaType, 
                                                                    int KeyID,
                                                                    int ScatterNum,
                                                                    char *AppCardNo, 
                                                                    char *TAC, 
                                                                    char *indata);


/**************************************************************************
 * �� �� ��:  FS_EncryDecryptData_Sjj1310      
 * ��������:  3DES�ӽ���  
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:         
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                calc_id     �㷨ID��0������ 1������
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
                pMACIVData  MAC��ʼ����    
                DataLen     �������ĳ���(2�ֽڿ����ַ�)
                Data        ��������  
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1310(TSZTHsmHDL hdl, 
                                                          int EncDecFlag, // �ӽ��ܱ�ʶ0������ 1������
                                                          char *custom, 
                                                          int KeyID, 
                                                          int ScatterNum, 
                                                          char *scatterData, 
                                                          char *indata, 
                                                          int inDataLen, 
                                                          char *outdata);

/**************************************************************************
 * �� �� ��: FS_PlainKeyData_Sjj1310      
 * ��������: ������Կ   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:       
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
 * ����� ��:   
                ��ɢ������� outdata
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1310(TSZTHsmHDL hdl, 
                                                        char *custom, 
                                                        int KeyID, 
                                                        int ScatterNum, 
                                                        char *scatterData, 
                                                        char *outdata);

/**************************************************************************
 * �� �� ��: FS_PlainkeyGenerateMac_Sjj1310      
 * ��������: ��ɢ������Կ����MAC   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:     
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                calc_id     �㷨ID��0������ 1������
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
                pMACIVData  MAC��ʼ����    
                DataLen     �������ĳ���(2�ֽڿ����ַ�)
                Data        ��������   
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
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
 * �� �� ��: FS_VerifyTACNew_Sjj1310      
 * ��������:    
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:  CardMediaType��2��ACPU��  3��M1��
               KeyID ���ܻ���Կ����
               ScatterNum ��ɢ����
               AppCardNo��ɢ����
               TAC ֵ
               indata
                //ACPU����ֵindata���ݸ�ʽ��4*2�ֽڵ���Ǯ�������(��ֵ��) + 2*2�ֽڵ���Ǯ���������(��ֵ1ǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
                //ACPU�����ѡ�M1�����ѳ�ֵindata���ݸ�ʽ��4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
                //m1 ��������1 + �ն˽������к�(���)3 + ���д���2 + ��Ψһ����4 + ������1 + ����ǰ��(С��)3 + ���׽��(С��)3 + ����ʱ��7 + ��������(���)2 + �ն˴���4 + "8000"
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTACNew_Sjj1310(TSZTHsmHDL hdl, 
                                                            int CardMediaType, 
                                                            int KeyID,
                                                            int ScatterNum,
                                                            char *AppCardNo, 
                                                            char *TAC, 
                                                            char *indata);                                                                        


/**************************************************************************
 * �� �� ��: FS_LinkCryptServer_Sjj1313      
 * ��������: Sjj1313���ܻ���ʼ��
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:         
                sckConn     ����ܻ�����Socket��ʶ
  
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_LinkCryptServer_Sjj1313(TSZTHsmHDL hdl, 
                                                                    char *password, 
                                                                    char *cck_iv);


/**************************************************************************
 * �� �� ��: FS_PlainKeyData_Sjj1313      
 * ��������: Sjj1313������Կ   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:       
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
 * ����� ��:   
                ��ɢ������� outdata
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1313(TSZTHsmHDL hdl, 
                                                                char *custom, 
                                                                int KeyID, 
                                                                int ScatterNum, 
                                                                char *scatterData, 
                                                                char *outdata);       


/**************************************************************************
 * �� �� ��:  FS_EncryDecryptData_Sjj1313      
 * ��������:  3DES�ӽ���  
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:         
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                calc_id     �㷨ID��0������ 1������
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
                pMACIVData  MAC��ʼ����    
                DataLen     �������ĳ���(2�ֽڿ����ַ�)
                Data        ��������  
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1313(TSZTHsmHDL hdl, 
                                                                  int EncDecFlag, // �ӽ��ܱ�ʶ0������ 1������
                                                                  char *custom, 
                                                                  int KeyID, 
                                                                  int ScatterNum, 
                                                                  char *scatterData, 
                                                                  char *indata, 
                                                                  int inDataLen, 
                                                                  char *outdata);                                                          


/**************************************************************************
 * �� �� ��: FS_VerifyTACNew_Sjj1313      
 * ��������:    
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:  CardMediaType��2��ACPU��  3��M1��
               KeyID ���ܻ���Կ����
               ScatterNum ��ɢ����
               AppCardNo��ɢ����
               TAC ֵ
               indata
                //ACPU����ֵindata���ݸ�ʽ��4*2�ֽڵ���Ǯ�������(��ֵ��) + 2*2�ֽڵ���Ǯ���������(��ֵ1ǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
                //ACPU�����ѡ�M1�����ѳ�ֵindata���ݸ�ʽ��4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
                //m1 ��������1 + �ն˽������к�(���)3 + ���д���2 + ��Ψһ����4 + ������1 + ����ǰ��(С��)3 + ���׽��(С��)3 + ����ʱ��7 + ��������(���)2 + �ն˴���4 + "8000"
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-11-15
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
 * �� �� ��:  FS_EncryDecryptData_Sjj1309      
 * ��������:  3DES�ӽ���  
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:         
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                calc_id     �㷨ID��0������ 1������
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
                pMACIVData  MAC��ʼ����    
                DataLen     �������ĳ���(2�ֽڿ����ַ�)
                Data        ��������  
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_EncryDecryptData_Sjj1309(TSZTHsmHDL hdl, 
                                                          int EncDecFlag, // �ӽ��ܱ�ʶ0������ 1������
                                                          char *custom, 
                                                          int KeyID, 
                                                          int ScatterNum, 
                                                          char *scatterData, 
                                                          char *indata, 
                                                          int inDataLen, 
                                                          char *outdata);

/**************************************************************************
 * �� �� ��: FS_PlainKeyData_Sjj1309      
 * ��������: ������Կ   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:       
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
 * ����� ��:   
                ��ɢ������� outdata
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_PlainKeyData_Sjj1309(TSZTHsmHDL hdl, 
                                                        char *custom, 
                                                        int KeyID, 
                                                        int ScatterNum, 
                                                        char *scatterData, 
                                                        char *outdata);

/**************************************************************************
 * �� �� ��: FS_PlainkeyGenerateMac_Sjj1309      
 * ��������: ��ɢ������Կ����MAC   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:     
                sckConn     ����ܻ�����Socket��ʶ
                KeyID       ���ܻ��ڱ�����Կ���(8�ֽڿ����ַ�)
                calc_id     �㷨ID��0������ 1������
                ScatterNum  ��ɢ����(1�ֽڿ����ַ�)
                scatterData ��ɢ��������(��ɢ����*16�ֽڿ����ַ�)
                pMACIVData  MAC��ʼ����    
                DataLen     �������ĳ���(2�ֽڿ����ַ�)
                Data        ��������   
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
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
 * �� �� ��: FS_VerifyTACNew_Sjj1309      
 * ��������:    
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:  CardMediaType��2��ACPU��  3��M1��
               KeyID ���ܻ���Կ����
               ScatterNum ��ɢ����
               AppCardNo��ɢ����
               TAC ֵ
               indata
                //ACPU����ֵindata���ݸ�ʽ��4*2�ֽڵ���Ǯ�������(��ֵ��) + 2*2�ֽڵ���Ǯ���������(��ֵ1ǰ) + 4*2�ֽڽ��׽�� + 1*2�ֽڽ������� + 6*2�ֽ��ն˴��� + 7*2�ֽڽ�������ʱ��
                //ACPU�����ѡ�M1�����ѳ�ֵindata���ݸ�ʽ��4*2���׽�� + 1*2�������ͱ�ʶ + 6*2�ն˻���� + 4*2�ն˽������(SAM���������) + 7*2�ֽڽ�������ʱ��
                //m1 ��������1 + �ն˽������к�(���)3 + ���д���2 + ��Ψһ����4 + ������1 + ����ǰ��(С��)3 + ���׽��(С��)3 + ����ʱ��7 + ��������(���)2 + �ն˴���4 + "8000"
 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTACNew_Sjj1309(TSZTHsmHDL hdl, 
                                                            int CardMediaType, 
                                                            int KeyID,
                                                            int ScatterNum,
                                                            char *AppCardNo, 
                                                            char *TAC, 
                                                            char *indata); 


/**************************************************************************
 * �� �� ��:  FS_RSA_GeneratePEMKeys_Sjj1310      
 * ��������:  ���ɷǶԳƶ�
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:         
                hdl         ����ܻ�����Socket��ʶ
                Bits        ��Կ���ȣ���1024,1152
                e_bits      ָ�����ȣ���1,3
                pKeyIndex   ��Կ����01-20��99��ʾ��������������Կ

 * ����� ��:         
                pRSA_PUBLIC_KEY  ��Կ
                pPrivateLen      ˽Կ����
                pPrivate         ���ܺ��˽Կ
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GeneratePEMKeys_Sjj1310(TSZTHsmHDL hdl, 
                                                                int Bits,  
                                                                int e_bits, 
                                                                const char *pKeyIndex,
                                                                R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY,
                                                                unsigned int *pPrivateLen,
                                                                unsigned char *pPrivate);

/**************************************************************************
 * �� �� ��:  FS_RSA_GetPublic_Sjj1310      
 * ��������:  ��˽Կ��ȡ��Կ
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:      
                hdl         ����ܻ�����Socket��ʶ
                pKeyIndex   ��Կ����

                PrivateLen  ˽Կ����
                pPrivate    ˽Կ����
                
 * ����� ��:         
                pRSA_PUBLIC_KEY        ��Կ
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GetPublic_Sjj1310(TSZTHsmHDL hdl, 
                                                            const char *pKeyIndex,
                                                            unsigned int PrivateLen,
                                                            const unsigned char *pPrivate,
                                                            R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY);



                                                                
/**************************************************************************
 * �� �� ��:  FS_RSA_GetPrivate_Sjj1310      
 * ��������:  �����ǶԳ�˽Կ
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:      
                hdl         ����ܻ�����Socket��ʶ
                pKeyIndex   ��Կ����

                PrivateLen  ˽Կ����
                pPrivate    ˽Կ����
                
 * ����� ��:         
                pRSA_PUBLIC_KEY        ��Կ
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_GetPrivate_Sjj1310(TSZTHsmHDL hdl, 
                                                        const char *pKeyIndex,
                                                        
                                                        unsigned int PrivateLen,
                                                        const unsigned char *pPrivate,
                                                        
                                                        R_RSA_PRIVATE_KEY *pRSA_PRIVATE_KEY);







/**************************************************************************
 * �� �� ��:  FS_RSA_PrivateDecrypt_Sjj1310      
 * ��������:  ˽Կ����
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:      
                hdl         ����ܻ�����Socket��ʶ
                pKeyIndex   ��Կ����

                PrivateLen  ˽Կ����
                pPrivate    ˽Կ����

                in_len      �������ݳ���
                pInput      ��������          
                
 * ����� ��:         
                pOutlen     ������ݳ���
                pOut        �������
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
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
 * �� �� ��:  FS_RSA_PrivateDecrypt_Sjj1310      
 * ��������:  ˽Կ����
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:      
                hdl     ����ܻ�����Socket��ʶ
                pKeyIndex   ��Կ����

                pRSA_PUBLIC_KEY     ��Կ����

                in_len      �������ݳ���
                pInput      ��������                     
                
 * ����� ��:         
                pOutlen     ������ݳ���
                pOut        �������
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-8-1
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_RSA_PublicEncrypt_Sjj1310(TSZTHsmHDL hdl, 
                                                                    const char *pKeyIndex, 
                                                
                                                                    const R_RSA_PUBLIC_KEY *pRSA_PUBLIC_KEY,
                                                                    
                                                                    unsigned int in_len,
                                                                    const unsigned char *pInput,

                                                                    unsigned int *pOutlen,    
                                                                    unsigned char *pOut);

/**************************************************************************
 * �� �� ��: FS_VerifyTC_Sjj1309      
 * ��������: ��֤�����ֽ��TCֵ   
 * ����:          
 * ������:      
 * Table Accessed: 
 * Table Updated:  
 * ����� ��:  
               KeyID ���ܻ���Կ����: 196
               Scatter Num ��ɢ����: 1
               AppCard No��ɢ����:
               tag��5A������PAN+�Ҳ�F�ķ�ʽ��š�3104950012345678901f����tag��5F34������Ϊ0x01����ɢ����Ӧ��0x5001234567890101��
               
               TAC ֵ:  9F26(8byte)
               pInDev : �ն����� 9f02(6byte)..9f03(6byte)..9f1a(2byte)..95(5byte)..5f2a(2byte)..9a(3byte)..9c(1byte)..9f37(4byte)
               pInCard  ��Ƭ���� 82(2byte)..9F36(2byte)..9F10(4byte)

 * ����� ��:         
 * �� �� ֵ:  �ɹ�: 0, ʧ��: ����     
 * ������:    �Ż���  Version: V1111  Date: 2013-11-15
 **************************************************************************/
extern "C" JMJDLL_API int __stdcall FS_VerifyTC_Sjj1309(TSZTHsmHDL hdl, 
                                                        int KeyID,
                                                        int ScatterNum,
                                                        char *AppCardNo, 
                                                        char *TAC, 
                                                        char *pInDev,
                                                        char *pInCard);                                                                    

