using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using xyzs.common.EnumBusiness;
using xyzs.model;
using xyzs.model.DatabaseModel;
using xyzs.service;

namespace xyzs.cms.Controllers
{
    /// <summary>
    /// 产品列表控制器
    /// </summary>
    [Permission(EnumBusinessPermission.MaterialProductManage)]
    public class MaterialProductController : AdminControllerBase
    {
        #region [1、产品列表]

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.MaterialProductList)]
        public ActionResult ProductList()
        {
            return View();
        }

        /// <summary>
        /// 获取内容的配置信息
        /// </summary>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.MaterialProductList)]
        public ActionResult GetProductType()
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new SysDicService();
            var data = server.GetAllDict("ProductType");
            resultMode.Data = data?.Select(f => new
            {
                f.Id,
                f.Lable,
                f.Type
            }).Distinct().ToList();
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取产品列表信息
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.MaterialProductList)]
        public ActionResult ResourceListPage(string resourceType = null, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            pageSize = pageSize < 1 ? PageSize : pageSize;
            var server = new MaterialProductService();
            var dataList = server.GetList(resourceType, pageIndex, pageSize, out var count);
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功",
                Data = new { count, dataList }
            };
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存产品列表资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Permission(EnumBusinessPermission.MaterialProductList)]
        public ActionResult SaveResourceInfo(MaterialProductModel model)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new MaterialProductService();
            var saveModel = new MaterialProductModel();
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

            saveModel.Id = model.Id;
            saveModel.IsDel = FlagEnum.HadZore.GetHashCode();
            saveModel.ResourceRemark = model.ResourceRemark;
            saveModel.ProductName = model.ProductName;
            saveModel.ProductResourceUrl = model.ProductResourceUrl;
            saveModel.Sort = model.Sort;
            saveModel.ProductType = model.ProductType;
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
        [Permission(EnumBusinessPermission.MaterialProductList)]
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
            var server = new MaterialProductService();
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
        [Permission(EnumBusinessPermission.ResourceList)]
        public ActionResult GetModel(long id)
        {
            var resultMode = new ResponseBaseModel<dynamic>
            {
                ResultCode = ResponceCodeEnum.Success,
                Message = "响应成功"
            };
            var server = new MaterialProductService();
            var data = server.Get(id);
            resultMode.Data = data;
            return Json(resultMode, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}