using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

using YKT.Core;
using EmpSelfService.Common;


namespace EmpSelfService.DAL.DBEntity
{
    public class DBHelper
    {
        private string _mstrCon;//连接字符串
        private const string MstrDbType = "ORACLE";
        private IDbHelper _mconn;

        private static string _dbConStr;//数据库连接串

        /// <summary>
        /// 创建数据库对象
        /// </summary>
        public DBHelper()
		{
            _mstrCon = GetDbConString();

			this.DbFactory();
		}

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <returns></returns>
        private string GetDbConString()
        {
            if (!string.IsNullOrEmpty(_dbConStr))
            {
                return _dbConStr;
            }
            else
            {
                string ServerName = ConfigurationManager.AppSettings["ServerName"];//定义数据库服务器服务名或IP地址
                string DataBaseName = ConfigurationManager.AppSettings["DataBaseName"];//定义数据库名称
                string UID = xEncrypt.DecryptText(ConfigurationManager.AppSettings["UID"]);//定义数据库访问登陆用户名
                string PWD = xEncrypt.DecryptText(ConfigurationManager.AppSettings["PWD"]);//定义数据库访问密码
                _dbConStr = "Data Source=" + DataBaseName + ";User Id=" + UID + ";Password=" + PWD + ";Pooling=true;Min Pool Size=0;Max Pool Size=5000";

                return _dbConStr;
            }
        }

		#region   open or close database
		/// <summary>
		/// 打开数据库
		/// </summary>
		public void Open() 
		{
			this._mconn.Open();
		}
		/// <summary>
		/// 关闭数据库
		/// </summary>
		public void Close() 
		{
			this._mconn.Close();
		}

		/// <summary>
		/// 释放连接资源
		/// </summary>
		public void Dispose()
		{
			this._mconn.Dispose();
		}
		#endregion

		/// <summary>
		/// 数据存取对象创建工厂
		/// </summary>
		/// <returns></returns>
		private void DbFactory()
		{
			switch(MstrDbType)
			{
                //case "SQL":
                //    _mconn = new SqlServerDB();//SqlServerDB
                //    break;
                case "ORACLE":
                    _mconn = new  OracleDb(_dbConStr);//OracleDB
                    break;
                //default:
                //    //默认Oracle的数据库
                //    _mconn  = new OracleDb(_dbConStr);//OracleDB
                //    break;
			}			
		}

		/// <summary>
		/// 事务开始
		/// </summary>
		public virtual void BeginTrans() 
		{ 
			_mconn.BeginTrans();
		} 

		/// <summary>
        /// 事务提交
		/// </summary>
		public virtual void CommitTrans() 
		{ 
   			_mconn.CommitTrans();
		} 

		/// <summary>
        /// 事务回滚
		/// </summary>
		public virtual void RollbackTrans() 
		{ 
			_mconn.RollbackTrans();
		}

        /// <summary>
        /// 产生一个事务并开始 
        /// </summary>
        /// <returns>返回此事物</returns>
        public OracleTransaction CreateTransaction()
        {
            OracleTransaction transaction= _mconn.CreateTransaction();
            return transaction;
        }

		#region run sql command
		/// <summary>
		/// 执行一条Sql命令
		/// </summary>
		/// <param name="commandText">要执行的sql命令语句</param>
		/// <returns>返回值1表示命令，0表示命令执行失败</returns>
		public virtual int GetReturn(string commandText) 
		{ 
			return this._mconn.GetReturn(commandText);
		}
		/// <summary>
		/// 执行一条SQL命令，返回要查询的某一个字段值
		/// </summary>
		/// <param name="commandText">执行的查询命令行</param>
		/// <returns></returns>
		public virtual string GetReturnStr(string commandText) 
		{ 
			return this._mconn.GetReturnStr(commandText);
		}

        /// <summary>
        /// Run SQL command.
        /// </summary>
        /// <param name="commandText">Command of SQL.</param>
        /// <returns>0=Command Execute Successful Falg.</returns>
        public virtual int RunCommandRowCount(string commandText)
        {
            return this._mconn.RunCommandRowCount(commandText);
        }

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <returns>0=Command Execute Successful Falg.</returns>
		public virtual int RunCommand(string commandText) 
		{ 
			return this._mconn.RunCommand(commandText);
		}

        /// <summary>
        /// Run SQL command.
        /// </summary>
        /// <param name="commandText">Command of SQL.</param>
        /// <param name="transaction">Transaction</param>
        /// <returns>0=Command Execute Successful Falg.</returns>
        public virtual int RunCommand(string commandText, OracleTransaction transaction)
        {
            return this._mconn.RunCommand(commandText, transaction);
        }

		/// <summary>
		/// Run SQL command with Parameters.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
        /// <param name="commandParams">Command params.</param>
		/// <returns>Command Execute Successful Falg.</returns>
		public virtual int RunCommand(string commandText, object[] commandParams) 
		{
			return this._mconn.RunCommand(commandText,commandParams);
			
		}

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <param name="dataReader">Return result of Command.</param>
		public virtual void RunCommand(string commandText, out IDataReader dataReader) 
		{
			 this._mconn.RunCommand(commandText,out dataReader);
		}

		/// <summary>
		/// run sql command return DataSet
		/// </summary>
		/// <param name="commandText">sql command text</param>
        /// <param name="dataTable">a dataTable of the sql run result</param>
		public virtual  void RunCommand(string commandText,out DataTable dataTable)
		{
			this._mconn.RunCommand(commandText,out dataTable);
		}

		/// <summary>
		/// run sql command return DataSet
		/// </summary>
		/// <param name="commandText">sql command text</param>
		/// <param name="dataSet">a dataset of the sql run result</param>
		public virtual void RunCommand(string commandText,out DataSet dataSet)
		{
			this._mconn.RunCommand(commandText,out dataSet);
		}

        /// <summary>
        /// run sql command return DataSet
        /// </summary>
        /// <param name="commandText">sql command text</param>
        /// <param name="dataSet">a dataset of the sql run result</param>
        public virtual void RunCommand(string commandText, out DataSet dataSet,OracleTransaction transaction)
        {
            this._mconn.RunCommand(commandText, out dataSet, transaction);
        }

		/// <summary>
		/// run sql command return a DataSet
		/// </summary>
		/// <param name="commandText">sql command </param>
		/// <param name="tableName">return table name </param>
		/// <param name="dataSet">the sql execute result datatable</param>
		public virtual void RunCommand(string commandText,string tableName,out DataSet dataSet)
		{
			this._mconn.RunCommand(commandText,tableName,out dataSet);
		}

		/// <summary>
		/// run sql command  
		/// </summary>
		/// <param name="commandText">sql command</param>
		/// <param name="tableName">sql table name</param>
		/// <param name="pintPageNum">the current page num </param>
		/// <param name="pintPageSize">the page size </param>
		/// <param name="dataSet">return dataset </param>
		public virtual void RunCommand(string commandText,string tableName,int pintPageNum,int pintPageSize,out DataSet dataSet)
		{
			//cal the start data record 
			 this._mconn.RunCommand(commandText,tableName,pintPageNum,pintPageSize,out dataSet);
		}

		/// <summary>
		/// run sql command return the special page DataSet
		/// </summary>
		/// <param name="commandText">sql command </param>
		/// <param name="tableName">sql table name</param>
		/// <param name="prams">sql parameters</param>
		/// <param name="dataSet"></param>
		public virtual void RunCommand(string commandText,string tableName,object[] prams,out DataSet dataSet)
		{
			 this._mconn.RunCommand(commandText,tableName,prams,out dataSet);
		}

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <param name="prams">Command params.</param>
		/// <param name="dataReader">Return result of Command.</param>
		public virtual void RunCommand(string commandText, object[] prams, out IDataReader dataReader) 
		{
			 this._mconn.RunCommand(commandText,prams,out dataReader);
		}
		#endregion
		
		#region "Run stored procedure "
		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <returns>Stored procedure return value.</returns>
		public virtual int RunProc(string procName, object[] prams) 
		{
			return this._mconn.RunProc(procName,prams);
		}

		/// <summary>
		/// run stored procedure
		/// </summary>
		/// <param name="procName">stored procedure name</param>
		/// <returns>int stored procedure  return value</returns>
		public virtual int RunProc(string procName) 
		{
			return this._mconn.RunProc(procName);
		}
		
		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public virtual void RunProc(string procName, out IDataReader dataReader) 
		{
			this._mconn.RunProc(procName, out dataReader);
		}
		
		/// <summary>
		/// return dataset
		/// </summary>
		/// <param name="procName">the procedure name </param>
		/// <param name="prams">sql parameters value </param>
		/// <param name="dataSet">return a dataset</param>
		public virtual void RunProc(string procName,object[] prams,out DataSet dataSet)
		{
			this._mconn.RunProc(procName,prams, out dataSet);
		}

		public virtual void RunProc(string procName,object[] prams,out DataTable dataTable)
		{
			this._mconn.RunProc(procName,prams, out dataTable);
		}

		public virtual void RunProc(string procName,object[] prams,string tableName,out DataSet dataSet)
		{
			this._mconn.RunProc(procName,prams,tableName, out dataSet);
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public  virtual void RunProc(string procName, object[] prams, out IDataReader dataReader) 
		{
			this._mconn.RunProc(procName, prams,out dataReader);
		}
		#endregion

		#region execute a Command object
		/// <summary>
		/// 执行一个命令对象
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <returns>返回1成功，0失败</returns>
		public virtual int ExecuteCommand(IDbCommand cmdp)
		{
			return this._mconn.ExecuteCommand(cmdp);
		}
		/// <summary>
		/// 执行一个命令对象，返回只读的资料集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <param name="dataReader">返回的只读的资料集合</param>
		/// <returns></returns>
		public virtual void ExecuteCommand(IDbCommand cmdp,out IDataReader dataReader)
		{
			 this._mconn.ExecuteCommand(cmdp,out dataReader);
		}
		/// <summary>
		/// 执行一个命令对象，返回DataSet数据集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <param name="dataSet">返回的DataSet数据集合</param>
		/// <returns></returns>
		public virtual void ExecuteCommand(IDbCommand cmdp,out DataSet dataSet)
		{
			 this._mconn.ExecuteCommand(cmdp,out dataSet);
		}
		/// <summary>
		/// 执行一个命令对象，返回待名称的DataSet数据集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
        /// <param name="tableName">sql table name</param>
		/// <param name="dataSet">要返回的数据集合</param>
		/// <returns></returns>
		public virtual void ExecuteCommand(IDbCommand cmdp,string tableName,out DataSet dataSet)
		{
			this._mconn.ExecuteCommand(cmdp,tableName,out dataSet);
		}
		#endregion

		#region "update dataset"
		/// <summary>
		/// 执行一条Sql命令，对该Sql命令返回的DataSet数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sql命令语句</param>
		/// <param name="ds">要更新的数据集合</param>
		/// <returns>更新成功后的DataSet</returns>
		public DataSet UpdateDb(string commandText, DataSet  ds)
		{
			return this._mconn.UpdateDb(commandText, ds);

		}

		/// <summary>
		/// 执行一条Sql命令，把该Sql命令返回的DataSet数据集合的某个表的纪录更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sql命令语句</param>
		/// <param name="ds">要更新的数据集合</param>
		/// <param name="tblIndex">要更新的某个表的索引</param>
		/// <returns></returns>
		public DataSet UpdateDb(string commandText, DataSet  ds,int tblIndex)
		{
			return this._mconn.UpdateDb(commandText,ds,tblIndex);
		}


		/// <summary>
		///执行一条Sql命令，对该Sql命令返回的DataTable数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sql命令行</param>
		/// <param name="dt">要更新的数据集合</param>
		/// <returns></returns>
		public DataTable UpdateDb(string commandText, DataTable  dt) 
		{
			return this._mconn.UpdateDb(commandText,dt);
		}

		/// <summary>
		/// 执行Sql命令集合，对该Sql命令集合返回的DataSet数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sql命令集合</param>
		/// <param name="ds">数据集合DataSet</param>
		/// <returns>数据集合</returns>
		public DataSet UpdateDb(ArrayList commandText,DataSet ds)
		{
			return this._mconn.UpdateDb(commandText,ds);

		}
		
		/// <summary>
		///  执行一条Sql命令集合，对该Sql命令返回的DataSet数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sqlSelect命令集合</param>
		/// <param name="dss">返回的DataSet资料集合</param>
		/// <returns>更新后的数据集合</returns>
		public ArrayList UpdateDb(ArrayList commandText,ArrayList dss)
		{
			return this._mconn.UpdateDb(commandText,dss);

		}
		#endregion
        
		#region DBType Enum

		public enum DbType
		{
			Sql			= 1	,
			Oracle		= 2,
			Other		= 3
		}

		#endregion EntityModule Enum

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程的名称</param>
        /// <param name="parameters">存储过程的参数</param>
        public virtual void RunProcedure(string storedProcName, OracleParameter[] parameters)
        {
            this._mconn.RunProcedure(storedProcName, parameters);
        }


        /// <summary>
        /// 执行增删改T-SQL语句，发挥执行成功与否
        /// </summary>
        /// <param name="strsql">T-SQL语句或存储过程名称</param>
        /// <param name="cmdType">T-SQL语句类别</param>
        /// <param name="param">参数列表</param>
        /// <returns>返回值</returns>
        public static bool DoExecuteNonQuery(string strsql, CommandType cmdType, params OracleParameter[] param)
        {
            using (OracleConnection conn = new OracleConnection(_dbConStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand(strsql, conn);
                    comm.CommandType = cmdType;
                    if (param.Length > 0)
                        comm.Parameters.AddRange(param);

                    int flag = comm.ExecuteNonQuery();
                    return flag > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    LogHelper.Log("DBHelper.DoExecuteNonQuery", ex, "执行失败 >> " + strsql);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询T-SQL语句，返回SqlDataReader对象
        /// </summary>
        /// <param name="strsql">T-SQL语句或存储过程名称</param>
        /// <param name="commType">T-SQL语句类型</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static OracleDataReader GetSqlDataReader(string strsql, CommandType commType, params OracleParameter[] param)
        {
            OracleConnection conn = new OracleConnection(_dbConStr);
            OracleDataReader sdr = null;
            OracleCommand comm = null;
            try
            {
                conn.Open();
                comm = new OracleCommand();
                comm.Connection = conn;
                comm.CommandType = commType;
                comm.CommandText = strsql;
                if (param.Length > 0)
                    comm.Parameters.AddRange(param);

                sdr = comm.ExecuteReader(CommandBehavior.CloseConnection);
                return sdr;
            }
            catch (Exception ex)
            {
                LogHelper.Log("DBHelper.GetSqlDataReader", ex, "执行失败 >> " + strsql);
                return sdr;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行查询T-SQL语句，返回DataSet对象
        /// </summary>
        /// <param name="strsql">T-SQL语句或存储过程名称</param>
        /// <param name="commType">T-SQL语句类型</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string strsql, CommandType commType, params OracleParameter[] param)
        {
            using (OracleConnection conn = new OracleConnection(_dbConStr))
            {
                OracleDataAdapter sda = null;
                DataSet ds = null;
                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand(strsql, conn);
                    comm.CommandType = commType;
                    if (param.Length > 0)
                        comm.Parameters.AddRange(param);
                    sda = new OracleDataAdapter(comm);
                    ds = new DataSet();
                    sda.Fill(ds);
                    return ds;

                }
                catch (Exception ex)
                {
                    LogHelper.Log("DBHelper.GetDataSet", ex, "执行失败 >> " + strsql);
                    return ds;
                }
                finally
                {
                    sda.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询T-SQL语句，返回DataTable对象
        /// </summary>
        /// <param name="strsql">T-SQL语句或存储过程名称</param>
        /// <param name="commType">T-SQL语句类型</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string strsql, CommandType commType, params OracleParameter[] param)
        {
            using (OracleConnection conn = new OracleConnection(_dbConStr))
            {
                OracleDataAdapter sda = null;
                DataTable dt = null;
                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand(strsql, conn);
                    comm.CommandType = commType;
                    if (param.Length > 0)
                        comm.Parameters.AddRange(param);
                    sda = new OracleDataAdapter(comm);
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;

                }
                catch (Exception ex)
                {
                    LogHelper.Log("DBHelper.GetDataTable", ex, "执行失败 >> " + strsql);
                    return dt;
                }
                finally
                {
                    sda.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行查询T-SQL语句，返回第一行第一列对象
        /// </summary>
        /// <param name="strsql">T-SQL语句或存储过程名称</param>
        /// <param name="commType">T-SQL语句类型</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static object DoExecuteScalar(string strsql, CommandType commType, params OracleParameter[] param)
        {


            object obj = null;
            using (OracleConnection conn = new OracleConnection(_dbConStr))
            {

                try
                {
                    conn.Open();
                    OracleCommand comm = new OracleCommand(strsql, conn);
                    comm.CommandType = commType;
                    if (param.Length > 0)
                        comm.Parameters.AddRange(param);


                    obj = comm.ExecuteScalar();
                    return obj;
                }
                catch (Exception ex)
                {
                    LogHelper.Log("DBHelper.DoExecuteScalar", ex, "执行失败 >> " + strsql);
                    return obj;
                }
                finally
                {
                    conn.Dispose();
                }
            }
        }
    }
}
