using System.Data;
using System.Data.Common;

namespace DoitForFree.DAL
{
    internal class UserDAL
    {
        #region Add
        public int Add(string cmdStr, params DbParameter[] paras)
        {
            return DbHelper.ExecuteNonQuery(cmdStr, paras);
        }
        #endregion

        #region Delete
        public int Delete(string cmdStr, params DbParameter[] paras)
        {
            return DbHelper.ExecuteNonQuery(cmdStr, paras);
        }
        #endregion

        #region Update
        public int Update(string cmdStr, params DbParameter[] paras)
        {
            return DbHelper.ExecuteNonQuery(cmdStr, paras);
        }
        #endregion

        #region Select
        public DataTable Select(string cmdStr, params DbParameter[] paras)
        {
            return DbHelper.ExecuteDateTable(cmdStr, paras);
        }
        #endregion
    }
}
