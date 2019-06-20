/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System.Web.Mvc;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class VIPController : Controller
    {
        #region view
        /// <summary>
        /// 返回用户商品详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("GoodsDetails");
        }
        #endregion
    }
}