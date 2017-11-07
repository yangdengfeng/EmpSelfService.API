/* ==============================================================================
 * 功能描述：IDBHelper
 * 公司名称: 雄帝
 * 创 建 者：xlx
 * 创建日期：2015/3/12 21:12:07
 * ==============================================================================*/
using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Collections;
using System.Data.OracleClient;

namespace EmpSelfService.DAL.DBEntity
{
    /// <summary>
    ///数据存取组件接口
    /// </summary>
    public interface IDbHelper : IDisposable
    {
        void Open();
        void Close();
        void BeginTrans(); //事务开始
        void CommitTrans(); //事务提交
        void RollbackTrans();//事务回滚
        OracleTransaction CreateTransaction();
        void AssignParameter(object[] Params, IDbCommand cmd);

        #region run sql command

        int GetReturn(string commandText);
        string GetReturnStr(string commandText);
        int RunCommandRowCount(string commandText);
        int RunCommand(string commandText);
        int RunCommand(string commandText, OracleTransaction transaction);
        int RunCommand(string commandText, object[] prams);
        void RunCommand(string commandText, out IDataReader dataReader);
        void RunCommand(string commandText, out DataTable dataTable);
        void RunCommand(string commandText, out DataSet dataSet);
        void RunCommand(string commandText, out DataSet dataSet, OracleTransaction transaction);
        void RunCommand(string commandText, string tableName, out DataSet dataSet);
        void RunCommand(string commandText, string tableName, int pintPageNum, int pintPageSize, out DataSet dataSet);
        void RunCommand(string commandText, string tableName, object[] prams, out DataSet dataSet);
        void RunCommand(string commandText, object[] prams, out IDataReader dataReader);
        #endregion

        #region run stored procedure

        int RunProc(string procName, object[] prams);
        int RunProc(string procName);
        void RunProc(string procName, out IDataReader dataReader);
        void RunProc(string procName, object[] prams, out DataSet dataSet);
        void RunProc(string procName, object[] prams, out DataTable dataTable);
        void RunProc(string procName, object[] prams, string tableName, out DataSet dataSet);
        void RunProc(string procName, object[] prams, out IDataReader dataReader);

        #endregion

        #region execute a Command object
        /// <summary>
        /// 执行某一个命令对象
        /// </summary>
        /// <param name="cmdp"></param>
        /// <returns></returns>
        int ExecuteCommand(IDbCommand cmdp);

        /// <summary>
        /// 执行某一个命令对象，并返回只读程序集
        /// </summary>
        /// <param name="cmdp"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        void ExecuteCommand(IDbCommand cmdp, out IDataReader dataReader);

        /// <summary>
        /// 执行一个命令对象，返回DataSet资料集合
        /// </summary>
        /// <param name="cmdp"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        void ExecuteCommand(IDbCommand cmdp, out DataSet dataSet);

        /// <summary>
        /// 执行一个命令对象，返回一个带名称的DataSet资料集合
        /// </summary>
        /// <param name="cmdp"></param>
        /// <param name="tableName"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        void ExecuteCommand(IDbCommand cmdp, string tableName, out DataSet dataSet);

        #endregion

        #region "update dataset"

        DataSet UpdateDb(string commandText, DataSet ds);
        DataSet UpdateDb(string commandText, DataSet ds, int tblIndex);
        DataTable UpdateDb(string commandText, DataTable dt);
        DataSet UpdateDb(ArrayList commandText, DataSet ds);
        ArrayList UpdateDb(ArrayList commandText, ArrayList dss);
        #endregion

        void RunProcedure(string procName, OracleParameter[] prams);
    }
}
