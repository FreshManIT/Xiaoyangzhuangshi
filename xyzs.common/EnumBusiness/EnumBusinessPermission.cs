﻿using System.ComponentModel;

namespace xyzs.common.EnumBusiness
{
    public enum EnumBusinessPermission
    {
        //[EnumTitle("[无]", IsDisplay = false)]
        None = 0,
        #region [1、系统设置]
        /// <summary>
        /// 系统设置
        /// </summary>
        [Description("系统设置")]
        SysSetManage = 1001,

        /// <summary>
        /// 菜单权限设置
        /// </summary>
        [Description("菜单权限设置")]
        MenuSet = 1002,

        /// <summary>
        /// 菜单配置
        /// </summary>
        [Description("菜单配置")]
        MenuAdmin = 1003,

        /// <summary>
        /// 管理员列表
        /// </summary>
        [Description("管理员列表")]
        MenuUsers = 1004,

        /// <summary>
        /// 字典管理
        /// </summary>
        [Description("字典管理")]
        SysDicManage = 1005,
        #endregion

        #region [2、内容管理]
        /// <summary>
        /// 内容管理
        /// </summary>
        [Description("内容管理")]
        ContentManage = 2001,

        /// <summary>
        /// 内容编辑
        /// </summary>
        [Description("内容编辑")]
        ContentEditPage = 2002,

        /// <summary>
        /// 内容列表
        /// </summary>
        [Description("内容列表")]
        ContentEditList = 2003,
        #endregion

        #region [3、资源管理]

        /// <summary>
        /// 资源管理
        /// </summary>
        [Description("资源管理")]
        ResourceManage = 3001,

        /// <summary>
        /// 资源列表
        /// </summary>
        [Description("资源列表")]
        ResourceList = 3002,

        #endregion

        #region [4、广告配置]

        /// <summary>
        /// 广告配置
        /// </summary>
        [Description("广告配置")]
        AdvertiseManage = 4001,

        /// <summary>
        /// 广告列表
        /// </summary>
        [Description("广告列表")]
        AdvertiseList = 4002,
        #endregion

        #region [5、留言管理]

        /// <summary>
        /// 留言管理
        /// </summary>
        [Description("留言管理")]
        CustomerCommentManage = 5001,

        /// <summary>
        /// 留言列表
        /// </summary>
        [Description("留言列表")]
        CustomerCommentList = 5002,
        #endregion

        #region [6、产品配置]

        /// <summary>
        /// 产品配置
        /// </summary>
        [Description("产品配置")]
        MaterialProductManage = 6001,

        /// <summary>
        /// 产品列表
        /// </summary>
        [Description("产品列表")]
        MaterialProductList = 6002,
        #endregion

        #region [7、工序记录]

        /// <summary>
        /// 工序记录
        /// </summary>
        [Description("工序记录")]
        ProcedureHistoryManage=7001,
        /// <summary>
        /// 工序记录
        /// </summary>
        [Description("装修工序表")]
        ProcedureHistoryList = 7002,
        /// <summary>
        /// 工序记录
        /// </summary>
        [Description("装修工序存档")]
        ProcedureHistorySelf = 7003,
        #endregion
    }
}
