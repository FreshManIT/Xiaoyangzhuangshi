using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using FreshCommonUtility.Cookie;
using FreshCommonUtility.Security;
using Newtonsoft.Json;
using xyzs.common.CheckCodeHelper;
using xyzs.common.CustomerAttribute;
using xyzs.common.Unit;
using xyzs.service;

namespace xyzs.cms.Controllers
{
    /// <summary>
    /// 登录操作
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly AccountService _accountService = new AccountService();

        [AuthorizeIgnore]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult Login(string username, string password,string checkcode)
        {
            var code = Session["ValidateCode"]?.ToString();
            if (string.IsNullOrEmpty(code) || !code.Equals(checkcode,StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("error", "验证码错误");
                Session["ValidateCode"] = null;
                return View();
            }
            password = AesHelper.AesEncrypt(password);
            var loginInfo = _accountService.UserLogin(username, password);

            if (loginInfo != null && loginInfo.IsLogin)
            {
                string data = JsonConvert.SerializeObject(loginInfo);
                CookieHelper.SetCookie(StaticFileHelper.UserCookieStr, AesHelper.AesEncrypt(DesHelper.DesEnCode(data)));
                return Redirect(ViewBag.RootNode + "/Home/WelCome");
            }
            ModelState.AddModelError("error", "用户名或密码错误");
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        [AuthorizeIgnore]
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
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            CookieHelper.SetCookie(StaticFileHelper.UserCookieStr, string.Empty);
            return RedirectToAction("Login");
        }

        public ActionResult O404()
        {
            return View("404");
        }
    }
}
