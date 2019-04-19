using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using FreshCommonUtility.Dapper;
using FreshCommonUtility.SqlHelper;
using xyzs.common.EnumBusiness;
using xyzs.model.DatabaseModel;

namespace xyzs.dataaccess
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class MaterialProductData : BaseData<long, MaterialProductModel>
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<MaterialProductModel> GetModels(string type, int pageIndex, int pageSize)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");

            if (!string.IsNullOrEmpty(type))
            {
                where.Append(" and ProductType= @Type ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                Type = type
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.GetListPaged<MaterialProductModel>(pageIndex, pageSize, where.ToString(), " Sort desc ", param)?.ToList();
            }
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string type)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");

            if (!string.IsNullOrEmpty(type))
            {
                where.Append(" and ProductType= @Type ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                Type = type
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.RecordCount<MaterialProductModel>(where.ToString(), param);
            }
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="saveModel"></param>
        public void SaveModel(MaterialProductModel saveModel)
        {
            if (saveModel == null) return;
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                if (saveModel.Id < 1)
                {
                    //新增
                    conn.Insert<long, MaterialProductModel>(saveModel);
                }

                //修改
                conn.Update(saveModel);
            }
        }

        /// <summary>
        /// 删除表记录
        /// </summary>
        /// <param name="id"></param>
        public void DelModel(long id)
        {
            if (id < 1) return;
            var model = Get(id);
            if (model == null) return;
            model.IsDel = FlagEnum.HadOne.GetHashCode();
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                conn.Update(model);
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="ids"></param>
        public void DelModels(List<long> ids)
        {
            if (ids == null || ids.Count < 1) return;
            var sql = @"update materialproduct set IsDel=@IsDel where Id in @Ids";
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                conn.Execute(sql, new { IsDel = FlagEnum.HadOne.GetHashCode(), Ids = ids });
            }
        }
    }
}
