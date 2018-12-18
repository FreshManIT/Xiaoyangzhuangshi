using xyzs.dataaccess;

namespace xyzs.service
{
    /// <summary>
    /// 数据库连接测试
    /// </summary>
    public class DbLinkTestService
    {
        /// <summary>
        /// 数据库连接测试
        /// </summary>
        public static void DbLink()
        {
            new DbLinkTestData().DbLink();
        }
    }
}
