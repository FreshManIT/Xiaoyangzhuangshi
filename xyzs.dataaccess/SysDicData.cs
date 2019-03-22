using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshCommonUtility.Dapper;
using FreshCommonUtility.SqlHelper;
using xyzs.common.EnumBusiness;
using xyzs.model.DatabaseModel;

namespace xyzs.dataaccess
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class SysDicData
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="lable"></param>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<SysdictModel> GetModels(string lable, string type, int pageIndex, int pageSize)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");
            if (!string.IsNullOrEmpty(lable))
            {
                where.Append(" and Lable like @Lable ");
            }

            if (!string.IsNullOrEmpty(type))
            {
                where.Append(" and Type> @Type ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                Lable = "%" + lable + "%",
                Type = type
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.GetListPaged<SysdictModel>(pageIndex, pageSize, where.ToString(), null, param)?.ToList();
            }
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string lable, string type)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");
            if (!string.IsNullOrEmpty(lable))
            {
                where.Append(" and Lable like @Lable ");
            }

            if (!string.IsNullOrEmpty(type))
            {
                where.Append(" and Type> @Type ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                Lable = "%" + lable + "%",
                Type = type
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.RecordCount<Syscontent>(where.ToString(), param);
            }
        }
    }
}
