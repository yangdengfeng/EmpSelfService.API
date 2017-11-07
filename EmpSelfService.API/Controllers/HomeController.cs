using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EmpSelfService.Api.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData["Message"] = GetCurrentApplicationVersion();
            return View();
        }

        //获取系统版本
        private string GetCurrentApplicationVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            string versionStr = fvi.FileVersion;
            return versionStr;
        }
    }
}
