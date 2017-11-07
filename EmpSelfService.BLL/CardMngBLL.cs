using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using EmpSelfService.Common;
using EmpSelfService.DAL;
using EmpSelfService.Model;

namespace EmpSelfService.BLL
{
    public class CardMngBLL
    {
        /// <summary>
        /// 卡库存更新
        /// </summary>
        /// <param name="terminalNo">设备终端号(PSAM卡号)</param>
        /// <param name="flag">增加或减少标识 1：增加 2：减少</param>
        /// <param name="num">数量</param>
        /// <param name="opUser">操作用户</param>
        /// <returns></returns>
        public bool UpdateCardTypeMng(string terminalNo, string flag, string num, string opUser)
        {
            bool isOK = false;
            try
            {
                //卡操作记录入库
                var cardOperationTB = new PuCardOperationRecordTB
                {
                    SKTERMINALID = terminalNo,
                    CZTERMINALID = "",
                    CARDTYPE_CODE = ConfigurationManager.AppSettings["CardType"].ToString(),
                    CARD_OPERATION = flag,
                    CARD_NUMBER = num,
                    CREATION_USER = opUser,
                    CREATION_TIME = "sysdate"
                };

                CardMngDAL cardMngDal = new CardMngDAL();
                if (!cardMngDal.SaveOperationRecord(cardOperationTB))
                {
                    LogHelper.Log("UpdateCardTypeMng", "卡操作记录入库失败", string.Format("{0:yyyy-MM-dd HH:mm:ss}-{1}", DateTime.Now, terminalNo));
                }

                //更新卡库存
                var cardTypeMngTB = new PuCardTypeMngTB
                {
                    SKTERMINALID = terminalNo,
                    CZTERMINALID = "",
                    CARDTYPE_CODE = ConfigurationManager.AppSettings["CardType"].ToString(),
                    SUMSTOCK = (flag == "1" ? num : "0"),
                    SUMSOLD = (flag == "1" ? "0" : "-" + num),
                    SURPLUSCARD = (flag == "1" ? num : "-" + num),
                    UPDATE_USER = opUser,
                    UPDATE_TIME = "sysdate"
                };
                if (!cardMngDal.UpdateCardTypeMng(cardTypeMngTB))
                {
                    LogHelper.Log("UpdateCardTypeMng", "更新卡库存失败", string.Format("{0:yyyy-MM-dd HH:mm:ss}-{1}", DateTime.Now, terminalNo));
                }
                else
                {
                    isOK = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("CardMngBLL.UpdateCardTypeMng", ex);
            }

            return isOK;
        }


        /// <summary>
        /// 根据终端代号获取终端的卡片信息
        /// </summary>
        /// <param name="terminalNO"></param>
        /// <returns></returns>
        public DataTable SelCardByTerminalNo(string terminalNO)
        {
            var dt = new DataTable();
            try
            {
                CardMngDAL dal = new CardMngDAL();
                dt = dal.SelCardByTerminalNo(terminalNO);
            }
            catch (Exception ex)
            {
                LogHelper.Log("CardMngBLL.SelCardByTerminalNo", ex);
            }

            return dt;
        }

        /// <summary>
        /// 查询卡是否是挂失卡
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public bool SelLossCardByCardNo(string cardno)
        {
            return  new CardMngDAL().SelLossCardByCardNo(cardno);
        }


        public bool SetSusCard(string cardNo, string componentId, string userId)
        {
            return new CardMngDAL().SetSusCard(cardNo, componentId, userId);
        }

        public bool RemoveSusCard(string cardNo, string userId)
        {
            return new CardMngDAL().RemoveSusCard(cardNo, userId);
        }

        /// <summary>
        /// 保存从行业上来的交易记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveTxnIndustryRecord(TransRecordModel model)
        {
            return new CardMngDAL().SaveTxnIndustryRecord(model);
        }

        /// <summary>
        /// 获取交通部卡面TAC密钥
        /// </summary>
        /// <param name="cardid"></param>
        /// <returns></returns>
        public string GetCardKeyID(string cardid)
        {
            string CardKey = "-1";
            try
            {
                CardMngDAL dal = new CardMngDAL();
                DataTable dsTxnFileInfo = dal.GetCardKeyID(cardid);
                if (dsTxnFileInfo != null && dsTxnFileInfo.Rows.Count > 0) //文件已入库
                {
                    CardKey = dsTxnFileInfo.Rows[0]["TACKEY"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return CardKey;
        }
    }
}
