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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace xyzs.model.DatabaseModel
{
    /// <summary>
    /// procedurehistory table in MySQL5
    /// </summary>
    [Table("procedurehistory")]
    public class ProcedureHistoryModel
    {
        /// <summary>
        /// Id 主键
        /// </summary>
        [Key]
        public Int64 Id { get; set; }
        /// <summary>
        /// ProcedureCode 工序编号
        /// </summary>
        public String ProcedureCode { get; set; }
        /// <summary>
        /// ProcedureName 工序名称
        /// </summary>
        public String ProcedureName { get; set; }
        /// <summary>
        /// CustomerId 客户id
        /// </summary>
        public Int64 CustomerId { get; set; }
        /// <summary>
        /// CustomerName 客户姓名
        /// </summary>
        public String CustomerName { get; set; }
        /// <summary>
        /// ResourceUrl 资源路径
        /// </summary>
        public String ResourceUrl { get; set; }
        /// <summary>
        /// ResourceName 资源名称
        /// </summary>
        public String ResourceName { get; set; }
        /// <summary>
        /// CreateBy 创建人
        /// </summary>
        public String CreateBy { get; set; }
        /// <summary>
        /// CreateTime 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Remarks 备注
        /// </summary>
        public String Remarks { get; set; }
        /// <summary>
        /// IsDel 是否已经删除0：未删除，1：已经删除
        /// </summary>
        public Int32 IsDel { get; set; }
        /// <summary>
        /// Sort 排序值
        /// </summary>
        public Decimal Sort { get; set; }
    }
}