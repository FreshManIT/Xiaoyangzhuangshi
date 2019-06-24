using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshCommonUtility.Dapper;
using FreshCommonUtility.SqlHelper;
using xyzs.common.EnumBusiness;
using xyzs.model.DatabaseModel;

namespace xyzs.dataaccess
{
    public class ProcedureHistoryData : BaseData<long, ProcedureHistoryModel>
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="procedureCode"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ProcedureHistoryModel> GetModels(string procedureCode, long userId, int pageIndex, int pageSize)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");

            if (!string.IsNullOrEmpty(procedureCode))
            {
                where.Append(" and ProcedureCode= @ProcedureCode ");
            }
            if (userId > -1)
            {
                where.Append(" and CustomerId=@CustomerId ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                ProcedureCode = procedureCode,
                CustomerId = userId
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.GetListPaged<ProcedureHistoryModel>(pageIndex, pageSize, where.ToString(), " Sort desc ", param)?.ToList();
            }
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string procedureCode, long userId)
        {
            var where = new StringBuilder(" where IsDel=@IsDel ");

            if (!string.IsNullOrEmpty(procedureCode))
            {
                where.Append(" and ProcedureCode= @ProcedureCode ");
            }

            if (userId > -1)
            {
                where.Append(" and CustomerId=@CustomerId ");
            }
            var param = new
            {
                IsDel = FlagEnum.HadZore.GetHashCode(),
                ProcedureCode = procedureCode,
                CustomerId = userId
            };
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                return conn.RecordCount<ProcedureHistoryModel>(where.ToString(), param);
            }
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="saveModel"></param>
        public void SaveModel(ProcedureHistoryModel saveModel)
        {
            if (saveModel == null) return;
            using (var conn = SqlConnectionHelper.GetOpenConnection())
            {
                if (saveModel.Id < 1)
                {
                    //新增
                    conn.Insert<long, ProcedureHistoryModel>(saveModel);
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
    }
}
