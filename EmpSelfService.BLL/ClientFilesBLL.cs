/* ==============================================================================
 * 功能名称：ClientFilesBLL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/5/16 11:13:47
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using EmpSelfService.Model;
using EmpSelfService.DAL;
using YKT.Core;

namespace EmpSelfService.BLL
{
    public class ClientFilesBLL
    {
        public ResultBase<string> GetClientFile(string cpuId, string nowVersion)
        {
            bool bResult = false;
            string strFileName = string.Empty, strPassword = string.Empty;
            ClientFileDAL clientFile = new ClientFileDAL();
            DataTable dtSoftware = clientFile.QueryTerminalSoftware();
            if (dtSoftware != null && dtSoftware.Rows.Count > 0)
            {
                strFileName = dtSoftware.Rows[0]["FileName"].ToString();//dtSoftware.Rows[0]["FilePath"] + 
                //查到有最新的版本 才继续以下的操作
                #region 
                DataTable dtTerminalState = clientFile.QueryTerminalState(cpuId);
                //检查设备是否已有设备状态信息
                if (dtTerminalState == null || dtTerminalState.Rows.Count <= 0)
                {
                    //设备第一次连接是没有状态信息的   需要新加默认的设备状态信息
                    clientFile.SaveTerminalState(cpuId);

                    //需要新加默认设备配置信息
                    string strRechargeCom = clientFile.QueryBasicParam("RechargeCom");
                    string strSellCom = clientFile.QueryBasicParam("SellCom");
                    string strCashboxCom = clientFile.QueryBasicParam("CashboxCom");
                    string strCardissuerCom = clientFile.QueryBasicParam("CardissuerCom");
                    string strPrinterCom = clientFile.QueryBasicParam("PrinterCom");
                    string strLedCom = clientFile.QueryBasicParam("LedCom");
                    string strPrinterType = clientFile.QueryBasicParam("PrinterType");
                    string strUserId = clientFile.QueryBasicParam("UserId");
                    strPassword = Common.Common.GetRandomString(8, true, true, true, false, "");
                    strPassword = xEncrypt.EncryptText(strPassword);
                    clientFile.SaveTerminalParam(cpuId, "3", strSellCom, strRechargeCom, strCardissuerCom,
                        strCashboxCom, strLedCom, strPrinterCom, strPrinterType, strUserId, strPassword);
                    bResult = true;
                }
                else 
                {
                    strPassword = clientFile.QueryTerminalPassword(cpuId);
                    if (dtTerminalState.Rows[0]["SOFTWARERENEW"].ToString() == "1" &&
                        nowVersion != dtSoftware.Rows[0]["FileVersion"].ToString())
                    {
                        string[] strSelfVer = nowVersion.Split('.');
                        string[] strSerVer = dtSoftware.Rows[0]["FileVersion"].ToString().Split('.');
                        if (strSelfVer.Length == strSerVer.Length && strSerVer.Length == 4) //版本格式是否匹配  标准格式：0.0.0.0
                        {
                            bResult = true;
                            //for (int i = 0; i < strSelfVer.Length; i++)
                            //{
                            //    if (int.Parse(strSerVer[i]) > int.Parse(strSelfVer[i]))
                            //    {
                            //        bResult = true;
                            //        break;
                            //    }
                            //}
                        }
                    }
                }
                #endregion
            }
            //strFileName = strFileName.Replace("\\", "|");
            return new ResultBase<string>(bResult, "{\"FileName\":\"\",\"Password\":\"" + strPassword + "\"}", "{\"FileName\":\"" + strFileName + "\",\"Password\":\"" + strPassword + "\"}");
        }

        /// <summary>
        /// 获取解密后的文件路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public string GetFile(string fileName)
        {
            string strResult = new ClientFileDAL().QueryClientSoftware(fileName);
            return strResult;
        }
    }
}
