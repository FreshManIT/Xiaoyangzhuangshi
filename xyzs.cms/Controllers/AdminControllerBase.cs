using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FreshCommonUtility.Configure;
using FreshCommonUtility.Cookie;
using FreshCommonUtility.DataConvert;
using FreshCommonUtility.Enum;
using FreshCommonUtility.Security;
using Newtonsoft.Json;
using xyzs.common.CustomerAttribute;
using xyzs.common.Unit;
using xyzs.model;
using xyzs.service;

namespace xyzs.cms.Controllers
{
    public abstract class AdminControllerBase : ControllerBase
    {
        private int _userExpiresHours = 10;
        public virtual int UserExpiresHours
        {
            get
            {
                return _userExpiresHours;
            }
            set
            {
                _userExpiresHours = value;
            }
        }

        /// <summary>
        /// 管理父级菜单
        /// </summary>
        public int ManagerId { get; set; }

        /// <summary>
        /// 子菜单项
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 登录后用户信息
        /// </summary>
        public virtual LoginInfoModel CurrentModel
        {
            get
            {
                var u = CookieHelper.GetCookie(StaticFileHelper.UserCookieStr);
                if (string.IsNullOrEmpty(u))
                {
                    return null;
                }
                var data = DesHelper.DesDeCode(AesHelper.AesDecrypt(u));
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                var model = JsonConvert.DeserializeObject<LoginInfoModel>(data);
                if (model != null)
                {
                    return AccountService.GetUserMenuInfo(model);
                }
                return null;
            }
        }

        #region Override controller methods
        /// <summary>
        /// 方法执行前，如果没有登录就调整到Passport登录页面，没有权限就抛出信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region [1、验证是否在服务时间内]

            var startTimeStr = AppConfigurationHelper.GetString("SystemRunStartTime");
            var endTimeStr = AppConfigurationHelper.GetString("SystemRunEndTime");
            //没有配置时间
            if (!string.IsNullOrEmpty(startTimeStr) && !string.IsNullOrEmpty(endTimeStr))
            {
                var startTime = DataTypeConvertHelper.ToDateTime(startTimeStr);
                var endTime = DataTypeConvertHelper.ToDateTime(endTimeStr);

                if (startTime <= new DateTime(1900, 1, 1) || endTime <= new DateTime(1900, 1, 1))
                {
                    filterContext.Result = Request.UrlReferrer != null ? Stop("系统运行时间配置错误！", Request.UrlReferrer.AbsoluteUri) : Content("系统运行时间配置错误！");
                    return;
                }
                startTime = new DateTime(1900, 1, 1, startTime.Hour, startTime.Minute, startTime.Second);
                endTime = new DateTime(1900, 1, 1, endTime.Hour, endTime.Minute, endTime.Second);
                var newTime = DateTime.Now;
                newTime = new DateTime(1900, 1, 1, newTime.Hour, newTime.Minute, newTime.Second);
                if (newTime < startTime || newTime > endTime)
                {
                    filterContext.Result = Request.UrlReferrer != null ? Stop("系统处于维护期！", Request.UrlReferrer.AbsoluteUri) : Content("系统处于维护期！");
                    return;
                }
            }

            #endregion
            var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeIgnoreAttribute), false);
            if (noAuthorizeAttributes.Length > 0)
                return;

            base.OnActionExecuting(filterContext);

            if (CurrentModel == null)
            {
                filterContext.Result = RedirectToAction("Login", "Auth");
                return;
            }

            var permissionAttributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(PermissionAttribute), false).Cast<PermissionAttribute>();
            permissionAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(PermissionAttribute), false).Cast<PermissionAttribute>().Union(permissionAttributes);
            var attributes = permissionAttributes as IList<PermissionAttribute> ?? permissionAttributes.ToList();
            var showBanner = string.Empty;
            if (attributes.Any())
            {
                var hasPermission = true;
                foreach (var attr in attributes)
                {
                    foreach (var permission in attr.Permissions)
                    {
                        if (!CurrentModel.BusinessPermissionList.Contains(permission))
                        {
                            hasPermission = false;
                            break;
                        }
                    }
                }

                if (!hasPermission)
                {
                    if (Request.UrlReferrer != null)
                        filterContext.Result = Stop("没有权限！", Request.UrlReferrer.AbsoluteUri);
                    else
                        filterContext.Result = Content("没有权限！");
                }
                if (attributes.Count > 1)
                {
                    MenuId = attributes[0].Permissions.FirstOrDefault().GetHashCode();
                    ManagerId = attributes[1].Permissions.FirstOrDefault().GetHashCode();
                    showBanner = EnumHelper.GetDescriptionByEnum(attributes[1].Permissions.FirstOrDefault());
                    showBanner += "/" + EnumHelper.GetDescriptionByEnum(attributes[0].Permissions.FirstOrDefault());
                }
                else
                {
                    ManagerId = attributes[0].Permissions.FirstOrDefault().GetHashCode();
                    showBanner = EnumHelper.GetDescriptionByEnum(attributes[0].Permissions.FirstOrDefault());
                }
            }
            ViewBag.ShowBanner = showBanner;
            ViewBag.ManagerId = ManagerId;
            ViewBag.MenuId = MenuId;
        }

        /// <summary>
        /// 方法后执行后注入一些视图数据
        /// </summary>
        /// <param name="filterContext">filter context</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.Contains("Edit") ||
                filterContext.ActionDescriptor.ActionName.Contains("Add"))
                return;

            RenderViewData();
        }

        /// <summary>
        /// 如果是Ajax请求的话，清除浏览器缓存
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();
            }
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// 重写Json方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new VMEJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        /// <summary>
        /// 重写Json方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="jsonRequest"></param>
        /// <returns></returns>
        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new VMEJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }

        /// <summary>
        /// 重写Json方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public new JsonResult Json(object data)
        {
            return new VMEJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region initialization
        /// <summary>
        /// overerire controller initailzie method 
        /// </summary>
        /// <remarks>
        /// all  json control forbidden inherit ContrllerBase 
        /// </remarks>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            try
            {
                InitializeResource();
            }
            catch (Exception)
            {
                CookieHelper.SetCookie(StaticFileHelper.UserCookieStr, string.Empty);
            }
        }

        private void InitializeResource()
        {
            ViewBag.CurrentModel = CurrentModel;
        }
        #endregion
    }
}