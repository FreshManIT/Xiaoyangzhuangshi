using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using FreshCommonUtility.Configure;
using FreshCommonUtility.DataConvert;
using xyzs.common.Unit;
using xyzs.service;

namespace xyzs.web.Controllers
{
    public class ControllerBase : Controller
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public virtual int PageSize => 15;

        protected ContentResult JsonP(string callback, object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return this.Content(string.Format("{0}({1})", callback, json));
        }

        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        public ContentResult RefreshParentTab(string alert = null)
        {
            var script = string.Format("<script>{0}; if (window.opener != null) {{ window.opener.location.reload(); window.opener = null;window.open('', '_self', '');  window.close()}} else {{parent.location.reload(1)}}</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        /// <summary>
        /// 用JS关闭弹窗
        /// </summary>
        /// <returns></returns>
        public ContentResult CloseThickbox()
        {
            return this.Content("<script>top.tb_remove()</script>");
        }

        /// <summary>
        ///  警告并且历史返回
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public ContentResult Back(string notice)
        {
            var content = new StringBuilder("<script>");
            if (!string.IsNullOrEmpty(notice))
                content.AppendFormat("alert('{0}');", notice);
            content.Append("history.go(-1)</script>");
            return this.Content(content.ToString());
        }

        public ContentResult PageReturn(string msg, string url = null)
        {
            var content = new StringBuilder("<script type='text/javascript'>");
            if (!string.IsNullOrEmpty(msg))
                content.AppendFormat("alert('{0}');", msg);
            if (string.IsNullOrWhiteSpace(url))
                url = Request.Url.ToString();
            content.Append("window.location.href='" + url + "'</script>");
            return this.Content(content.ToString());
        }

        /// <summary>
        /// 转向到一个提示页面，然后自动返回指定的页面
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        public ContentResult Stop(string notice, string redirect, bool isAlert = false)
        {
            var content = "<meta http-equiv='refresh' content='1;url=" + redirect + "' /><body style='margin-top:0px;color:red;font-size:24px;'>" + notice + "</body>";

            if (isAlert)
                content = string.Format("<script>alert('{0}'); window.location.href='{1}'</script>", notice, redirect);

            return this.Content(content);
        }

        public virtual void ClearOperater()
        {
            //TODO
        }

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
                }
            }

            #endregion
        }

        /// <summary>
        /// AOP拦截，在Action执行后
        /// </summary>
        /// <param name="filterContext">filter context</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
                RenderViewData();

            this.ClearOperater();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            InitializeStaticResource();

        }

        private void InitializeStaticResource()
        {
            ViewBag.RootNode = AppConfigurationHelper.GetString("ReferenceKey.RootNode") ?? string.Empty;
            var dicSlider = new SysDicService().GetDicByValue("IndexSlider")?.FirstOrDefault();
            var sliderServer = new SysAdvertiseService();
            ViewBag.SliderList = null;
            if (dicSlider != null)
            {
                ViewBag.SliderList = sliderServer.GetList(dicSlider.Id, 1, 10, out _);
            }
        }

        /// <summary>
        /// 产生一些视图数据
        /// </summary>
        protected virtual void RenderViewData()
        {
        }

        /// <summary>
        /// 当前Http上下文信息，用于写Log或其他作用
        /// </summary>
        public WebExceptionContext WebExceptionContext
        {
            get
            {
                var exceptionContext = new WebExceptionContext
                {
                    IP = Fetch.UserIp,
                    CurrentUrl = Fetch.CurrentUrl,
                    RefUrl = (Request == null || Request.UrlReferrer == null) ? string.Empty : Request.UrlReferrer.AbsoluteUri,
                    IsAjaxRequest = (Request == null) ? false : Request.IsAjaxRequest(),
                    FormData = (Request == null) ? null : Request.Form,
                    QueryData = (Request == null) ? null : Request.QueryString,
                    RouteData = (Request == null || Request.RequestContext == null || Request.RequestContext.RouteData == null) ? null : Request.RequestContext.RouteData.Values
                };

                return exceptionContext;
            }
        }

        /// <summary>
        /// 发生异常写Log
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var e = filterContext.Exception;

            LogException(e, this.WebExceptionContext);
        }

        protected virtual void LogException(Exception exception, WebExceptionContext exceptionContext = null)
        {
            //do nothing!
        }
    }

    public class WebExceptionContext
    {
        public string IP { get; set; }
        public string CurrentUrl { get; set; }
        public string RefUrl { get; set; }
        public bool IsAjaxRequest { get; set; }
        public NameValueCollection FormData { get; set; }
        public NameValueCollection QueryData { get; set; }
        public RouteValueDictionary RouteData { get; set; }
    }
}