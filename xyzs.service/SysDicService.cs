using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xyzs.dataaccess;
using xyzs.model.DatabaseModel;

namespace xyzs.service
{
    /// <summary>
    /// 数据字典服务
    /// </summary>
    public class SysDicService
    {
        /// <summary>
        /// 数据服务
        /// </summary>
        private SysDicData _dataAccess = new SysDicData();

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <param name="indexPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<SysdictModel> GetList(string label, string type, int indexPage, int pageSize, out int count)
        {
            count = _dataAccess.GetCount(label, type);
            return _dataAccess.GetModels(label, type, indexPage, pageSize);
        }
    }
}
