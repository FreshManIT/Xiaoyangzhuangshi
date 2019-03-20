using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreshCommonUtility.Web;
using xyzs.common.EnumBusiness;
using xyzs.model.DatabaseModel;

namespace xyzs.cms.Controllers
{
    /// <summary>
    /// 内容编辑
    /// </summary>
    [Permission(EnumBusinessPermission.ContentManage)]
    public class ContentController : AdminControllerBase
    {
        /// <summary>
        /// 内容编辑页面
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ContentEditPage)]
        public ActionResult ContentEdit(long id = 0)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}