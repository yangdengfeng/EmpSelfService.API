using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{
    /// <summary>
    /// 消费交易记录验证控制器
    /// </summary>
    public class RecordCheckController : ApiController
    {
        private static string JMJIP = ConfigurationManager.AppSettings["JMJIP"].ToString();
        private static int JMJPORT = Convert.ToInt32(ConfigurationManager.AppSettings["JMJPORT"].ToString());

        /// <summary>
        /// 消费交易记录验证(异地卡不验TAC)
        /// </summary>
        /// <param name="terminalNo">企业编号</param>
        /// <param name="values">参数数据：唯一标识符\t交易检验码\t卡号\t交易类型\t...</param>
        /// <returns>JSON code:0(成功),其它 错误编码</returns>
        public HttpResponseMessage Post(string terminalNo, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParamsNew(terminalNo, values, 23, "RecordCheckController_Post");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                #region 校验TAC

                int checkTACFlag = -1;
                TransRecordModel model = ConvTransRecordModel(result.Value);
                if (model.MEDIATYPE == "1" || model.MEDIATYPE == "2")  //本地CPU、电信卡
                {
                    checkTACFlag = CheckTAC_CPU(model);
                }
                else if (model.MEDIATYPE == "3") //交通部
                {
                    checkTACFlag = CheckTAC_JTB(model);
                }
                else if (model.MEDIATYPE == "0") //M1
                {
                    checkTACFlag = CheckTAC_M1(model);
                }
                else
                {
                    checkTACFlag = 4;
                }

                #region 返回结果

                string rt = "";
                if (checkTACFlag == 0)
                {
                    rt = CodeModel.SUCCESS;
                }
                else if (checkTACFlag == 1)
                {
                    rt = CodeModel.TAC_CHECK_FAIL;
                }
                else if (checkTACFlag == 2)
                {
                    rt = CodeModel.CONN_JMJ_FAIL;
                }
                else if (checkTACFlag == 3)
                {
                    rt = CodeModel.CONN_DB_GET_KEY_FAIL;
                }
                else if (checkTACFlag == 4)
                {
                    rt = CodeModel.UNKNOWN_OP_TYPE;
                }
                else if (checkTACFlag == 5)
                {
                    rt = CodeModel.UNKNOWN_TXN_TYPE;
                }
                else
                {
                    rt = CodeModel.ErrSystem;
                }

                #endregion

                #endregion

                //todo 保存消费记录
                model.ERRCODE = rt;
                new CardMngBLL().SaveTxnIndustryRecord(model);

                return JsonHelper.StringToJson3(CodeModel.SUCCESS, rt);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.RecordCheckController.Post", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }


        /// <summary>
        /// 数组转换成交易记录实体
        /// </summary>
        /// <param name="paramStrings"></param>
        /// <returns></returns>
        public TransRecordModel ConvTransRecordModel(string[] paramStrings)
        {
            TransRecordModel model = new TransRecordModel();
            try
            {
                if (paramStrings != null)
                {
                    model.TAC = paramStrings[1];
                    model.CARDNO = paramStrings[2];
                    model.TXNTYPE = paramStrings[3];
                    model.TXNSUBTYPE = paramStrings[4];
                    model.TERMINALID = paramStrings[5];
                    model.POSOPRID = paramStrings[6];
                    model.TXNDATE = paramStrings[7];
                    model.POSSEQ = paramStrings[8];
                    model.TXNAMT = paramStrings[9];
                    model.TXNVALUE = paramStrings[10];
                    model.TXNAFTBAL = paramStrings[11];
                    model.CARDCNT = paramStrings[12];
                    model.PRETERMINALID = paramStrings[13];
                    model.PRETXNTYPE = paramStrings[14];
                    model.PRETXNDATE = paramStrings[15];
                    model.PRETXNAMT = paramStrings[16];
                    model.ORGCODE = paramStrings[17];
                    model.CITYCODE = paramStrings[18];
                    model.MEDIATYPE = paramStrings[19];
                    model.SAMSEQ = paramStrings[20];
                    model.CARDKIND = paramStrings[21];
                    model.TXNAPPFLAG = paramStrings[22];
                    model.TXNDATESN = paramStrings[7].Replace("-", "").Replace(":", "").Replace(" ", "").Trim().Substring(0, 8);
                    model.TXNTIMESN = paramStrings[7].Replace("-", "").Replace(":", "").Replace(" ", "").Trim().Substring(8, 6);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return model;
        }

        /// <summary>
        /// 校验M1卡交易记录TAC
        /// </summary>
        public int CheckTAC_M1(TransRecordModel model)
        {
            int flag = -1;
            try
            {
                string pTacKey = "AAC2703F0AA9D32B73DF2CA3894C6794";
                string tac = model.TAC;
                string txnType = model.TXNTYPE;
                string txnAmt = model.TXNAMT;
                string terminalId = model.TERMINALID;
                string posSeq = model.POSSEQ;
                string txnDateTime = model.TXNDATE.Replace("-", "").Replace(":", "").Replace(" ", "").Trim();

                string indata = "";
                indata += Convert.ToString(int.Parse(txnAmt), 16).PadLeft(8, '0').ToUpper(); //4*2交易金额
                indata += Convert.ToString(int.Parse(txnType), 16).PadLeft(2, '0').ToUpper(); //1*2交易类型标识
                indata += terminalId; //6*2终端机编号
                indata += Convert.ToString(int.Parse(posSeq), 16).PadLeft(8, '0').ToUpper(); //4*2终端交易序号(SAM卡交易序号)
                indata += txnDateTime;  //7*2

                int ret = DLLEntrance.Verify_M1_TAC_XINING(pTacKey, pTacKey.Length, tac, tac.Length, indata,
                    indata.Length);
                if (ret != 0)
                {
                    flag = 1;
                    LogHelper.Log("CheckTAC", "TAC码验证失败(M1)",
                        string.Format("TacKey:{0} TAC:{1} indata:{2}", pTacKey, tac, indata.Trim()));
                }
                else
                {
                    flag = 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("CheckTAC_M1", ex);
            }

            return flag;
        }

        /// <summary>
        /// 校验CPU卡交易记录TAC
        /// </summary>
        public int CheckTAC_CPU(TransRecordModel model)
        {
            int flag = -1;

            int nCardMediaType = 2;
            string tac = model.TAC;
            string appCardNo = model.CARDNO;
            int len = model.CARDNO.Length;
            if (len != 16)
            {
                appCardNo = model.CARDNO.Substring(4, 16);
            }
            model.CARDNO = appCardNo;

            string txnType = model.TXNTYPE;
            string monthCnt = "";
            string numMonth = "";
            string txnAmt = model.TXNAMT;
            string txnAftBal = model.TXNAFTBAL;
            string cardCnt = model.CARDCNT;
            string terminalId = model.TERMINALID;
            string samSeq = model.SAMSEQ;
            string txnDateTime = model.TXNDATE.Replace("-", "").Replace(":", "").Replace(" ", "").Trim();

            try
            {
                int jmjHdl = DLLEntrance.FS_Connect(JMJIP, JMJPORT); //连接加密机 172.16.2.40
                if (jmjHdl < 0)
                {
                    LogHelper.Log("CheckTAC", "验TAC：连接加密机失败", string.Format("IP:{0} Port:{1}", JMJIP, JMJPORT));
                    flag = 2;
                }
                else
                {
                    if (txnType == "15" || txnType == "18" || txnType == "30" || txnType == "31" ||
                        txnType == "32" || txnType == "35" || txnType == "37" || txnType == "38" ||
                        txnType == "42" || txnType == "71" || txnType == "79" || txnType == "35" ||
                        txnType == "40" || txnType == "41") //消费类
                    {
                        string indata = "";
                        if (txnType == "18")
                        {
                            indata +=
                                Convert.ToString(int.Parse(monthCnt) * int.Parse(numMonth), 16).PadLeft(8, '0').ToUpper();
                        }
                        else
                        {
                            indata += Convert.ToString(int.Parse(txnAmt), 16).PadLeft(8, '0').ToUpper(); //4*2交易金额
                        }
                        if (txnType == "30" || txnType == "32")
                        {
                            indata += "09"; //1*2交易类型标识
                        }
                        else
                        {
                            indata += "06"; //1*2交易类型标识
                        }

                        indata += terminalId; //6*2终端机编号
                        indata += Convert.ToString(int.Parse(samSeq), 16).PadLeft(8, '0').ToUpper(); //4*2终端交易序号(SAM卡交易序号)
                        indata += txnDateTime;

                        int ret = DLLEntrance.FS_VerifyTACNew_Sjj1309(jmjHdl, nCardMediaType, 449, 1, appCardNo, tac,
                            indata.Trim());
                        if (ret != 0)
                        {
                            flag = 1;
                            LogHelper.Log("CheckTAC", "TAC码验证失败(CPU)",
                                string.Format("JMJHdl:{0} TAC:{1} CARDNO:{2} indata:{3}", jmjHdl, tac, appCardNo,
                                    indata.Trim()));
                        }
                        else
                        {
                            flag = 0;
                        }
                    }
                    else if (txnType == "01" || txnType == "11" || txnType == "12" || txnType == "13" ||
                             txnType == "16" || txnType == "17" || txnType == "19" || txnType == "69")
                    {
                        string indata = "";
                        //充值
                        indata += Convert.ToString(int.Parse(txnAftBal), 16).PadLeft(8, '0').ToUpper(); //4*2字节电子钱包新余额
                        indata += Convert.ToString(int.Parse(cardCnt), 16).PadLeft(4, '0').ToUpper();
                            //2*2字节电子钱包交易序号(加一前)
                        if (txnType == "17")
                        {
                            indata +=
                                Convert.ToString(int.Parse(monthCnt)*int.Parse(numMonth), 16).PadLeft(8, '0').ToUpper();
                            //4*2交易金额
                        }
                        else
                        {
                            indata += Convert.ToString(int.Parse(txnAmt), 16).PadLeft(8, '0').ToUpper(); //4*2交易金额
                        }
                        indata += "02"; //1*2字节交易类型
                        indata += terminalId; //6*2终端机编号
                        indata += txnDateTime;

                        int ret = DLLEntrance.FS_VerifyTACNew_Sjj1309(jmjHdl, nCardMediaType, 449, 1, appCardNo, tac,
                            indata.Trim());
                        if (ret != 0)
                        {
                            flag = 1;
                            LogHelper.Log("CheckTAC", "TAC码验证失败(CPU)",
                                string.Format("JMJHdl:{0} TAC:{1} CARDNO:{2} indata:{3}", jmjHdl, tac, appCardNo,
                                    indata.Trim()));
                        }
                        else
                        {
                            flag = 0;
                        }
                    }
                    else
                    {
                        flag = 5; //交易类型不存在
                    }

                    DLLEntrance.FS_DisConnect(jmjHdl);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("CheckTAC_CPU", ex);
            }

            return flag;
        }

        /// <summary>
        /// 校验JTB卡交易记录TAC
        /// </summary>
        public int CheckTAC_JTB(TransRecordModel model)
        {
            int flag = -1;

            string tac = model.TAC;
            string appCardNo = model.CARDNO;
            int len = model.CARDNO.Length;
            if (len != 19)
            {
                appCardNo = model.CARDNO.Substring(1, 19);
            }
            model.CARDNO = appCardNo;

            string txnType = model.TXNTYPE;
            string monthCnt = "";
            string numMonth = "";
            string txnAmt = model.TXNAMT;
            string txnAftBal = model.TXNAFTBAL;
            string cardCnt = model.CARDCNT;
            string terminalId = model.TERMINALID;
            string samSeq = model.SAMSEQ;
            string txnDateTime = model.TXNDATE.Replace("-", "").Replace(":", "").Replace(" ", "").Trim();

            try
            {
                string pTacKey = new CardMngBLL().GetCardKeyID(appCardNo); //连接数据库获取密钥
                if (pTacKey == "-1")
                {
                    LogHelper.Log("CheckTAC", "验TAC：连接数据库获取密钥失败", appCardNo);
                    flag = 3;
                }
                else
                {
                    if (txnType == "15" || txnType == "18" || txnType == "30" || txnType == "31" ||
                        txnType == "32" || txnType == "35" || txnType == "37" || txnType == "38" ||
                        txnType == "42" || txnType == "71" || txnType == "79" || txnType == "35" ||
                        txnType == "40" || txnType == "41") //消费类
                    {
                        string indata = "";
                        if (txnType == "18")
                        {
                            indata +=
                                Convert.ToString(int.Parse(monthCnt)*int.Parse(numMonth), 16).PadLeft(8, '0').ToUpper();
                        }
                        else
                        {
                            indata += Convert.ToString(int.Parse(txnAmt), 16).PadLeft(8, '0').ToUpper(); //4*2交易金额
                        }

                        indata += "09"; //1*2交易类型标识
                        indata += terminalId; //6*2终端机编号
                        indata += Convert.ToString(int.Parse(samSeq), 16).PadLeft(8, '0').ToUpper(); //4*2终端交易序号(SAM卡交易序号)
                        indata += txnDateTime;

                        int ret = DLLEntrance.Verify_JTB_TAC_XINING(pTacKey, pTacKey.Length, tac, tac.Length, indata,
                            indata.Length);
                        if (ret != 0)
                        {
                            flag = 1;
                            LogHelper.Log("CheckTAC", "TAC码验证失败(JTB)",
                                string.Format("TacKey:{0} TAC:{1} indata:{2}", pTacKey, tac, indata.Trim()));
                        }
                        else
                        {
                            flag = 0;
                        }
                    }
                    else if (txnType == "01" || txnType == "11" || txnType == "12" || txnType == "13" ||
                             txnType == "16" || txnType == "17" || txnType == "19" || txnType == "69")
                    {
                        string indata = "";
                        indata += Convert.ToString(int.Parse(txnAftBal), 16).PadLeft(8, '0').ToUpper(); //4*2字节电子钱包新余额
                        indata += Convert.ToString(int.Parse(cardCnt), 16).PadLeft(4, '0').ToUpper();
                            //2*2字节电子钱包交易序号(加一前)
                        if (txnType == "17")
                        {
                            indata +=
                                Convert.ToString(int.Parse(monthCnt)*int.Parse(numMonth), 16).PadLeft(8, '0').ToUpper();
                                //4*2交易金额
                        }
                        else
                        {
                            indata += Convert.ToString(int.Parse(txnAmt), 16).PadLeft(8, '0').ToUpper(); //4*2交易金额
                        }
                        indata += "02"; //1*2字节交易类型
                        indata += terminalId; //6*2终端机编号
                        indata += txnDateTime;

                        int ret = DLLEntrance.Verify_JTB_TAC_XINING(pTacKey, pTacKey.Length, tac, tac.Length, indata,
                            indata.Length);
                        if (ret != 0)
                        {
                            flag = 1;
                            LogHelper.Log("CheckTAC", "TAC码验证失败(JTB)",
                                string.Format("TacKey:{0} TAC:{1} indata:{2}", pTacKey, tac, indata.Trim()));
                        }
                        else
                        {
                            flag = 0;
                        }
                    }
                    else
                    {
                        flag = 5; //交易类型不存在
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("CheckTAC_JTB", ex);
            }

            return flag;
        }


    }
}
