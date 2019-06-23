using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyzs.common.EnumBusiness;
using xyzs.model;
using xyzs.model.DatabaseModel;
using xyzs.service;

namespace xyzs.cms.Controllers
{
    [Permission(EnumBusinessPermission.ProcedureHistoryManage)]
    public class ProcedureHistoryController : AdminControllerBase
    {
        #region [1、工序记录]
        /// <summary>
        /// 工序记录列表
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult ProcedureHistoryList()
        {
            return View();
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="procedureCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult ProcedureHistoryListPage(string procedureCode = null, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            pageSize = pageSize < 1 ? PageSize : pageSize;
            var server = new ProcedureHistoryService();
            var dataList = server.GetList(procedureCode, pageIndex, pageSize, out var count);
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功",
                Data = new { count, dataList }
            };
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取内容的配置信息
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult GetContentType(string code)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new SysDicService();
            var data = server.GetAllDict(code);
            resultMode.Data = data?.Select(f => new
            {
                f.Id,
                f.Lable,
                f.Type
            }).Distinct().ToList();
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.AdvertiseList)]
        public ActionResult GetAllUser()
        {
            var allUserList = new AccountService().GetSysUsers(1, 10000, out var count);
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            resultMode.Data = allUserList?.Select(f => new
            {
                f.Id,
                f.UserName,
                f.TrueName
            }).Distinct().ToList();
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult SaveProcedureResourceInfo(ProcedureHistoryModel model)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new ProcedureHistoryService();
            var saveModel = new ProcedureHistoryModel();
            if (model == null)
            {
                Debug.WriteLine("请求参数为空");
                resultMode.Message = "保存失败";
                resultMode.ResultCode = ResponceCodeEnum.Fail;
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            if (model.Id > 0)
            {
                saveModel = server.Get(model.Id);
                if (saveModel == null)
                {
                    resultMode.Message = "该记录已经被删除";
                    resultMode.ResultCode = ResponceCodeEnum.Fail;
                    return Json(resultMode, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                saveModel.CreateBy = CurrentModel.Id.ToString();
                saveModel.CreateTime = DateTime.Now;
            }

            if (model.CustomerId > 0)
            {
                var userModel = new AccountService().GetSysUser(model.CustomerId);
                if (userModel == null || userModel.Id < 1)
                {
                    resultMode.Message = "客户不存在";
                    resultMode.ResultCode = ResponceCodeEnum.Fail;
                    return Json(resultMode, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.CustomerName = userModel.UserName;
                }
            }

            if (!string.IsNullOrEmpty(model.ProcedureCode))
            {
                var dicModel = new SysDicService().Get(model.ProcedureCode);
                if (dicModel == null || string.IsNullOrEmpty(dicModel.Id))
                {
                    resultMode.Message = "工序不存在";
                    resultMode.ResultCode = ResponceCodeEnum.Fail;
                    return Json(resultMode, JsonRequestBehavior.AllowGet);
                }

                model.ProcedureName = dicModel.Lable;
            }

            saveModel.Id = model.Id;
            saveModel.IsDel = FlagEnum.HadZore.GetHashCode();
            saveModel.ResourceUrl = model.ResourceUrl;
            saveModel.Sort = model.Sort;
            saveModel.Remarks = model.Remarks;
            saveModel.CustomerId = model.CustomerId;
            saveModel.CustomerName = model.CustomerName;
            saveModel.ProcedureCode = model.ProcedureCode;
            saveModel.ProcedureName = model.ProcedureName;
            saveModel.ResourceName = model.ResourceName;
            try
            {
                server.SaveModel(saveModel);
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                resultMode.Message = "保存失败";
                resultMode.ResultCode = ResponceCodeEnum.Fail;
                resultMode.Data = e.Message;
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult DelResourceModels(List<long> ids)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            if (ids == null || ids.Count < 1)
            {
                return Json(resultMode, JsonRequestBehavior.AllowGet);
            }
            var server = new ProcedureHistoryService();
            try
            {
                server.DelModel(ids);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.ProcedureHistoryList)]
        public ActionResult GetModel(long id)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new ProcedureHistoryService();
            var data = server.Get(id);
            resultMode.Data = data;
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}