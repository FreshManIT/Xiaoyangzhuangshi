using xyzs.dataaccess;
using xyzs.model.DatabaseModel;

namespace xyzs.service
{
    /// <summary>
    /// 内容服务
    /// </summary>
    public class ContentService
    {
        /// <summary>
        /// 获取单个的菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Syscontent GetContentModel(long id)
        {
            if (id < 1)
            {
                return null;
            }

            return new SysContentData().GetContentModel(id);
        }

        /// <summary>
        /// 添加内容信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long AddAndUpdateContentInfo(Syscontent model)
        {
            if (model == null) return 0;
            return new SysContentData().AddAndUpdateContentInfo(model);
        }
    }
}
