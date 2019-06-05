using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace XM.Web.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-6-3
    /// 修改时间：2019-
    /// 功能：文件上传下载
    /// </summary>
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportExcel0()
        {
            if (Request.Files["FileUpload1"] != null && Request.Files["FileUpload1"].ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(Request.Files["FileUpload1"].FileName);
                //string extension = System.IO.Path.GetExtension(fileName);
                string serverPath = string.Format("{0}\\{1}", Server.MapPath("~/Views/zxx"), fileName);
                if (System.IO.File.Exists(serverPath))
                {
                    System.IO.File.Delete(serverPath);
                }
                //新建文件,写入用
                FileStream fs = new FileStream(serverPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
                Request.Files["FileUpload1"].SaveAs(serverPath);

                try
                {
                    ViewBag.Msg = "good";
                    return Json(new { success = true, message = "导入成功！", fileName = fileName });
                }
                catch (Exception ex)
                {
                    ViewBag.Msg = ex.Message;
                    System.IO.File.Delete(serverPath);
                    return Json(new { success = false, message = "导入失败" + ex.Message, fileName = fileName });
                }
            }
            return View("Index");
        }
    }
}