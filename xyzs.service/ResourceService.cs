using System.Collections.Generic;
using xyzs.common.EnumBusiness;
using xyzs.dataaccess;
using xyzs.model.DatabaseModel;

namespace xyzs.service
{
    /// <summary>
    /// 资源管理服务
    /// </summary>
    public class ResourceService
    {
        /// <summary>
        /// 数据服务
        /// </summary>
        private ResourceData _dataAccess = new ResourceData();

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="type"></param>
        /// <param name="indexPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<SysresourceModel> GetList(ResourceTypeEnum type, int indexPage, int pageSize, out int count)
        {
            count = _dataAccess.GetCount(type);
            return _dataAccess.GetModels(type, indexPage, pageSize);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        public void DelModel(long id)
        {
            _dataAccess.DelModel(id);
        }

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysresourceModel Get(long id)
        {
            var data = _dataAccess.Get(id);
            if (data == null || data.IsDel == FlagEnum.HadOne.GetHashCode()) return null;
            return data;
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="saveModel"></param>
        public void SaveModel(SysresourceModel saveModel)
        {
            _dataAccess.SaveModel(saveModel);
        }
    }
}
