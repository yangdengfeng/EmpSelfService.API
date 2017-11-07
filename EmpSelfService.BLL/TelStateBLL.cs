/* ==============================================================================
 * 功能名称：TelStateBLL
 * 公司名称: 雄帝科技股份有限公司
 * 创 建 者：lusong
 * 创建日期：2017/4/21 9:06:50
 * 功能描述：
 * ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using EmpSelfService.Common;
using EmpSelfService.Model;
using EmpSelfService.DAL;

namespace EmpSelfService.BLL
{
    public class TelStateBLL
    {
        public bool SaveTerState(OmTerminalStateTB om)
        {
            bool bResult = false;
            try
            {
                TelStateDAL dalTel = new TelStateDAL();
                DataTable dtTel = dalTel.GetTelState(om.CpuId);
                if (dtTel != null && dtTel.Rows.Count > 0)
                {
                    dalTel.ModifyTelState(om, "OMTERMINALSTATETB");
                }
                else
                {
                    dalTel.SaveTelState(om, "OMTERMINALSTATETB");
                }
                //保存设备状态信息历史
                dalTel.SaveTelState(om, "OMTERMINALSTATEHISTB");
                bResult = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log("TelStateBLL.SaveTerState", ex);
            }
            return bResult;
        }
    }
}
