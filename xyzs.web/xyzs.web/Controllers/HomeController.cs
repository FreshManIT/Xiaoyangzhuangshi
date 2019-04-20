using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FreshCommonUtility.Configure;
using FreshCommonUtility.Web;
using xyzs.common.CheckCodeHelper;
using xyzs.common.EnumBusiness;
using xyzs.common.Unit;
using xyzs.model;
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
        /// <param name="productType">材料分类</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult ProductList(string productType = null, int page = 1, int pageSize = 8)
        {
            ViewBag.Action = "ProductList";
            var resultModel = new List<MaterialProductModel>();
            #region [获取分类]
            var server = new SysDicService();
            var data = server.GetAllDict("ProductType");
            if (data == null || data.Count < 1)
            {
                return View(resultModel);
            }

            data = data.OrderByDescending(f => f.Sort).ToList();
            var productName = string.Empty;
            if (string.IsNullOrEmpty(productType) || data.FirstOrDefault(f => f.Id == productType) == null)
            {
                var dicModel = data.OrderByDescending(r => r.Sort).FirstOrDefault();
                productType = dicModel?.Id;
                productName = dicModel?.Lable;
            }

            if (string.IsNullOrEmpty(productName))
            {
                productName = data.FirstOrDefault(f => f.Id == productType)?.Lable;
            }
            ViewBag.ProductType = productType;
            ViewBag.ProductTypeName = productName;
            ViewBag.ProductTypeList = data;
            #endregion

            #region [获取产品列表]

            if (page < 1)
            {
                page = 1;
            }
            pageSize = pageSize < 1 ? PageSize : pageSize;
            var materialServer = new MaterialProductService();
            resultModel = materialServer.GetList(productType, page, pageSize, out var count);

            ViewBag.Total = count;
            ViewBag.Page = page;
            ViewBag.PageCount = count / pageSize + (count % pageSize > 0 ? 1 : 0);
            #endregion

            return View(resultModel);
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
        /// 获取验证码
        /// </summary>
        public ActionResult CheckCode()
        {
            var yzm = new YzmHelper();
            yzm.CreateImage();
            var code = yzm.Text;
            Session["ValidateCode"] = code;
            Bitmap img = yzm.Image;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return File(ms.ToArray(), @"image/jpeg");
        }

        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCommentInfo()
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Fail,
                Message = "响应成功"
            };
            var checkcode = System.Web.HttpContext.Current.GetStringFromParameters("checkcode");
            if (string.IsNullOrEmpty(checkcode) || string.IsNullOrEmpty(Session["ValidateCode"]?.ToString()))
            {
                resultMode.Message = "验证码必填";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }

            var oldCode = Session["ValidateCode"];
            Session["ValidateCode"] = null;
            if (!oldCode.Equals(checkcode))
            {
                resultMode.Message = "验证码必填";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            var content = System.Web.HttpContext.Current.GetStringFromParameters("content");
            var createTime = DateTime.Now;
            var customerEmail = System.Web.HttpContext.Current.GetStringFromParameters("email");
            var customerName = System.Web.HttpContext.Current.GetStringFromParameters("username");
            var customerPhone = System.Web.HttpContext.Current.GetStringFromParameters("tel");

            var fw = new FilterWord();
            string str = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var filePath = AppConfigurationHelper.GetString("SensitiveFilePath");
            fw.DictionaryPath = str + filePath;
            fw.SourctText = content;
            content = fw.Filter('*');
            if (string.IsNullOrEmpty(content))
            {
                resultMode.Message = "留言内容不能为空";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            fw.SourctText = customerEmail;
            customerEmail = fw.Filter('*');
            if (string.IsNullOrEmpty(customerEmail) || !RegExp.IsEmail(customerEmail))
            {
                resultMode.Message = "邮箱内容错误";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            fw.SourctText = customerName;
            customerName = fw.Filter('*');
            if (string.IsNullOrEmpty(customerName))
            {
                resultMode.Message = "姓名内容错误";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            fw.SourctText = customerPhone;
            customerPhone = fw.Filter('*');
            if (string.IsNullOrEmpty(customerPhone) || !RegExp.IsMobileNo(customerPhone))
            {
                resultMode.Message = "电话内容错误";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            var commentModel = new CustomercommentModel { Content = content, CreateTime = createTime, CustomerName = customerName, CustomerEmail = customerEmail, CustomerPhone = customerPhone, IsDel = FlagEnum.HadZore.GetHashCode(), HasDeal = FlagEnum.HadZore };
            var server = new CustomerCommentService();
            try
            {
                server.SaveModel(commentModel);
                resultMode.Message = "处理成功";
                resultMode.ResultCode = ResponceCodeEnum.Success;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                resultMode.Message = "系统异常";
            }
            return Json(resultMode, JsonRequestBehavior.AllowGet);
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
            ViewBag.Action = "ArticList";
            var server = new ContentService();
            var model = server.GetContentModel(id);
            var preNextList = server.GetPreNextContent(id);
            ViewBag.Pre = null;
            ViewBag.Next = null;
            if (preNextList != null && preNextList.Count > 0)
            {
                preNextList.ForEach(f =>
                {
                    if (f.Id > model.Id)
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