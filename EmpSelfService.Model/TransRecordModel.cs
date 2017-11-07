using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpSelfService.Model
{
    /// <summary>
    /// 交易记录模型
    /// </summary>
    public class TransRecordModel
    {
        public string TAC { get; set; }
        public string CARDNO { get; set; }
        public string TXNTYPE { get; set; }
        public string TXNSUBTYPE { get; set; }
        public string TERMINALID { get; set; }
        public string POSOPRID { get; set; }
        public string TXNDATE { get; set; }
        public string TXNDATESN { get; set; }
        public string TXNTIMESN { get; set; }
        public string POSSEQ { get; set; }
        public string TXNAMT { get; set; }
        public string TXNVALUE { get; set; }
        public string TXNAFTBAL { get; set; }
        public string CARDCNT { get; set; }
        public string PRETERMINALID { get; set; }
        public string PRETXNTYPE { get; set; }
        public string PRETXNDATE { get; set; }
        public string PRETXNAMT { get; set; }
        public string ORGCODE { get; set; }
        public string CITYCODE { get; set; }
        public string MEDIATYPE { get; set; }
        public string SAMSEQ { get; set; }
        public string CARDKIND { get; set; }
        public string TXNAPPFLAG { get; set; }
        public string ERRCODE { get; set; }
    }
}
