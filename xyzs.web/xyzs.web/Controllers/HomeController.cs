using System.Web.Mvc;

namespace xyzs.web.Controllers
{
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 材料列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductList()
        {
            return View();
        }

        /// <summary>
        /// 材料详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductDetails()
        {
            return View();
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <returns></returns>
        public ActionResult Message()
        {
            return View();
        }

        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// 地图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Map()
        {
            return PartialView();
        }
    }
}