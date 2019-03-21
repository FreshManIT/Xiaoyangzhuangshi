﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreshCommonUtility.Web;
using xyzs.common.EnumBusiness;
using xyzs.model;
using xyzs.model.DatabaseModel;
using xyzs.service;

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

        /// <summary>
        /// 获取内容信息
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ContentEditPage)]
        public ActionResult GetContentInfo(long id)
        {
            var resultMode = new ResponseBaseModel<Syscontent>
            {
                ResultCode = ResponceCodeEnum.Fail
            };
            var model = new Syscontent();
            if (id < 1)
            {
                resultMode.Message = "响应成功";
                resultMode.ResultCode = ResponceCodeEnum.Success;
                resultMode.Data = model;
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }

            model = new ContentService().GetContentModel(id);
            resultMode.Message = "响应成功";
            resultMode.ResultCode = ResponceCodeEnum.Success;
            resultMode.Data = model;
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [Permission(EnumBusinessPermission.ContentEditPage)]
        public ActionResult AddContentInfo(Syscontent model)
        {
            var resultMode = new ResponseBaseModel<Syscontent>
            {
                ResultCode = ResponceCodeEnum.Fail
            };
            if (string.IsNullOrEmpty(model.Content))
            {
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }

            var server = new ContentService();
            var id = 0L;
            if (model.Id > 0)
            {
                var oldModel = server.GetContentModel(model.Id);
                if (oldModel == null)
                {
                    resultMode.Message = "不存在该内容记录";
                    return Json(resultMode, JsonRequestBehavior.AllowGet);
                }

                oldModel.Content = model.Content;
                oldModel.ContentSource = model.ContentSource;
                oldModel.ContentType = model.ContentType;
                oldModel.Title = model.Title;
                id = server.AddAndUpdateContentInfo(oldModel);
            }
            else
            {
                model.CreateTime = DateTime.Now;
                model.CreateUserId = CurrentModel.UserId;
                model.IsDel = FlagEnum.HadZore.GetHashCode();
                id = server.AddAndUpdateContentInfo(model);
            }
            if (id > 0)
            {
                resultMode.ResultCode = ResponceCodeEnum.Success;
                resultMode.Message = "处理成功";
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            resultMode.Message = "处理失败";
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }
    }
}