/*|========================================================|
  |=============This code is auto by CodeBuilder===========|
  |================ Organization:FreshManIT+  =============|
  |==========Any Question please tell me:FreshManIT========|
  |===https://github.com/FreshManIT/CodeBuilder/issues ====|
  |===============OR Email:qinbocai@sina.cn================|
  |========================================================|
**/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace xyzs.model.DatabaseModel
{
    /// <summary>
    /// materialproduct table in MySQL5
    /// </summary>
    [Table("materialproduct")]
    public class MaterialProductModel
    {
        /// <summary>
        /// Id 主键自增长
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// ProductName 产品名称
        /// </summary>
        public String ProductName { get; set; }
        /// <summary>
        /// ProductType 产品类型，关联字典表中的ProductType类型
        /// </summary>
        public String ProductType { get; set; }
        /// <summary>
        /// ProductResourceUrl 资源链接
        /// </summary>
        public String ProductResourceUrl { get; set; }
        /// <summary>
        /// ResourceRemark 资源备注
        /// </summary>
        public String ResourceRemark { get; set; }
        /// <summary>
        /// CreateBy 创建人
        /// </summary>
        public String CreateBy { get; set; }
        /// <summary>
        /// CreateTime 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// IsDel 是否已经删除0：未删除，1：已经删除
        /// </summary>
        public Int32 IsDel { get; set; }
        /// <summary>
        /// Sort 排序值
        /// </summary>
        public Int32 Sort { get; set; }
    }
}