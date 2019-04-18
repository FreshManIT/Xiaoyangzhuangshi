using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using xyzs.model.DatabaseModel;
using xyzs.service;

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
            ViewBag.Action = "Index";
            return View();
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Action = "About";
            return View();
        }

        /// <summary>
        /// 材料列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductList()
        {
            ViewBag.Action = "ProductList";
            return View();
        }

        /// <summary>
        /// 材料详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductDetails()
        {
            ViewBag.Action = "ProductDetails";
            return View();
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <returns></returns>
        public ActionResult Message()
        {
            ViewBag.Action = "Contact";
            return View();
        }

        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Action = "Contact";
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

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult ArticList(int page = 1, int pageSize = 10)
        {
            ViewBag.Action = "ArticList";
            var contentList = GetTypeContents("NewsArticleList", out var total, page, pageSize);
            ViewBag.Total = total;
            ViewBag.Page = page;
            ViewBag.PageCount = total / pageSize + (total % pageSize > 0 ? 1 : 0);
            ViewBag.Url = ViewBag.RootNote + "/Home/ArticList";
            return View(contentList);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="dicValue"></param>
        /// <param name="count">总量</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private List<Syscontent> GetTypeContents(string dicValue, out int count, int page = 1, int pageSize = 10)
        {
            return new ContentService().GetTypeContents(dicValue, out count, page, pageSize);
        }

        /// <summary>
        /// 新闻资讯详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ArticleDetail(int id)
        {
            ViewBag.Action = "ArticleDetail";
            var server = new ContentService();
            var model = server.GetContentModel(id);
            var preNextList = server.GetPreNextContent(id);
            ViewBag.Pre = null;
            ViewBag.Next = null;
            if (preNextList != null && preNextList.Count > 0)
            {
                preNextList.ForEach(f =>
                {
                    if (f.CreateTime > model.CreateTime)
                    {
                        ViewBag.Next = f;
                    }
                    else
                    {
                        ViewBag.Pre = f;
                    }
                });
            }
            return View(model);
        }
    }
}