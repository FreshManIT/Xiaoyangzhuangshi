using FreshCommonUtility.SqlHelper;
using FreshCommonUtility.Dapper;
using xyzs.model.DatabaseModel;

namespace xyzs.dataaccess
{
    /// <summary>
    /// 内容数据服务类
    /// </summary>
    public class SysContentData
    {
        /// <summary>
        /// 根据id获取内容信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Syscontent GetContentModel(long id)
        {
            if (id < 1) return null;
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.Get<Syscontent>(id);
            }
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long AddAndUpdateContentInfo(Syscontent model)
        {
            if (model == null) return 0;
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                if (model.Id > 0)
                {
                    return conn.Update(model);
                }
                return conn.Insert<long,Syscontent>(model);
            }
        }
    }
}
