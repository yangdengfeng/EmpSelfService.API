using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using EmpSelfService.BLL;
using EmpSelfService.Common;
using EmpSelfService.Model;

namespace EmpSelfService.Api.Controllers
{

    /// <summary>
    /// 客户端文件控制
    /// </summary>
    public class ClientFilesController : ApiController
    {
        /// <summary>
        /// 获取最新客户端文件路径
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <param name="values">当前版本</param>
        /// <returns></returns>
        public HttpResponseMessage GetNewestFilePath(string cpuId, string values)
        {
            try
            {
                ResultBase<string> result = new ClientFilesBLL().GetClientFile(cpuId, values);
                return JsonHelper.StringToJson(CodeModel.SUCCESS, result ? result.Info : result.Value);
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.ClientFilesController.GetNewestFilePath", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="cpuId">CPU编号</param>
        /// <param name="values">参数数据：唯一标识符\t文件名</param>
        /// <returns></returns>
        public HttpResponseMessage GetFile(string cpuId, string values)
        {
            try
            {
                var result = GlobalBLL.VerifyUserAndParamsByCpu(cpuId, values, 2, "ClientFilesController_GetFile");
                if (!result)
                    return JsonHelper.ReturnErrInfo(result.Info);

                string strPath = new ClientFilesBLL().GetFile(result.Value[1]);
                if (string.IsNullOrEmpty(strPath))
                    return JsonHelper.StringToJson(CodeModel.ErrParaData);
                if (!File.Exists(strPath))
                    return JsonHelper.StringToJson(CodeModel.FileNotFound);
                var browser = String.Empty;
                if (HttpContext.Current.Request.UserAgent != null)
                {
                    browser = HttpContext.Current.Request.UserAgent.ToUpper();
                }
                
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                FileStream fileStream = File.OpenRead(strPath);
                httpResponseMessage.Content = new StreamContent(fileStream);
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = 
                        browser.Contains("FIREFOX")
                            ? Path.GetFileName(strPath)
                            : HttpUtility.UrlEncode(Path.GetFileName(strPath))
                };
                string temp = HttpUtility.UrlEncode(Path.GetFileName(strPath));
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                LogHelper.Log("EmpSelfService.Api.ClientFilesController.GetFile", ex);
                return JsonHelper.StringToJson(CodeModel.ErrSystem);
            }
            
        }

    }
}