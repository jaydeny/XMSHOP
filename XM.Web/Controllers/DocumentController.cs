/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.IO;
using System.Web.Mvc;

namespace XM.Web.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 功能：文件上传下载
    /// </summary>
    public class DocumentController : Controller
    {
        #region view

        /// <summary>
        /// 文件上传下载首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
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
        #endregion
    }
}