﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
