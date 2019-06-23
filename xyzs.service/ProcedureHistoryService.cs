using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xyzs.common.EnumBusiness;
using xyzs.dataaccess;
using xyzs.model.DatabaseModel;

namespace xyzs.service
{
    public class ProcedureHistoryService
    {
        /// <summary>
        /// 数据服务
        /// </summary>
        private ProcedureHistoryData _dataAccess = new ProcedureHistoryData();

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="procedureCode"></param>
        /// <param name="indexPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ProcedureHistoryModel> GetList(string procedureCode, int indexPage, int pageSize, out int count)
        {
            count = _dataAccess.GetCount(procedureCode);
            return _dataAccess.GetModels(procedureCode, indexPage, pageSize);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids"></param>
        public void DelModel(List<long> ids)
        {
            if (ids == null || ids.Count < 1) return;
            if (ids.Count == 1)
            {
                _dataAccess.DelModel(ids[0]);
            }
        }

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProcedureHistoryModel Get(long id)
        {
            var data = _dataAccess.Get(id);
            if (data == null || data.IsDel == FlagEnum.HadOne.GetHashCode()) return null;
            return data;
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="saveModel"></param>
        public void SaveModel(ProcedureHistoryModel saveModel)
        {
            _dataAccess.SaveModel(saveModel);
        }
    }
}
