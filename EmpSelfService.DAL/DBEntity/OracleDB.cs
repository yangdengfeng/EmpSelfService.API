/* ==============================================================================
 * 功能描述：OrcaleDB
 * 公司名称: 雄帝
 * 创 建 者：xlx
 * 创建日期：2015/3/13 10:12:55
 * ==============================================================================*/
#pragma warning disable 0618
using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;

namespace EmpSelfService.DAL.DBEntity
{
    /// <summary>
	/// OrcaleBHelper Oracle数据存取类
	/// </summary>
    public class OracleDb : IDbHelper
    {
        // connection to data source
		private OracleConnection _conn;
        private string _strCon = "";//System.Configuration.ConfigurationManager.AppSettings["DBConnection"];
		private OracleTransaction _trans; //事务 
        private bool _bTransaction = false; //事务标志

        //private string ServerName;//定义数据库服务器服务名--oralce为配置文件中的实例名
        //private string UID;//定义数据库访问登陆用户名
        //private string PWD;//定义数据库访问密码
        //private static string DbConStr;//数据库连接串

		#region struct method
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="strConn">连接字符串</param>
        public OracleDb(string strConn)
		{
			_conn=new OracleConnection(strConn);
		}
		#endregion

		#region   open or close database
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		public void Open() 
		{
			if (_conn == null) 
			{
				_conn = new OracleConnection(_strCon);
				_conn.Open();
			}
			else 
			{
				if (_conn.State == ConnectionState.Closed)
				{
					_conn.Open();
				}
			}
		}

		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		public void Close()
		{
		    if (_conn != null)
		        _conn.Close();
		}

		/// <summary>
		/// 释放系统资源
		/// </summary>
		public void Dispose()
		{
			// make sure connection is closed
			if (_conn != null) 
			{
				_conn.Dispose();
				_conn = null;
			}				
		}

        /// <summary>
        /// 开始事务
        /// </summary>
		public  void BeginTrans()
        {
            this.Open();
            _trans = _conn.BeginTransaction();
            _bTransaction = true;
        } 

        /// <summary>
        /// 提交事务
        /// </summary>
		public  void CommitTrans()
        {
            _trans.Commit();
            this.Close();
            _bTransaction = false;
        } 

        /// <summary>
        /// 回滚事务
        /// </summary>
		public  void RollbackTrans() 
		{ 
   			_trans.Rollback();
            this.Close();
            _bTransaction = false;
		}

        /// <summary>
        /// 产生一个事务并开始 
        /// </summary>
        /// <returns>返回此事物</returns>
        public OracleTransaction CreateTransaction()
        {
            OracleTransaction transaction = _conn.BeginTransaction();
            
            return transaction;
        }
		#endregion

		#region run sql command
		/// <summary>
		/// 把执行存储过程sqlcommand的命令参数值进行自动分配
		/// 执行存储过程的时候只需要把参数值传入就可以了。
		/// </summary>
		/// <param name="Params">参数值数组</param>
		/// <param name="cmd">命令对象</param>
		public void AssignParameter(object[] Params,IDbCommand cmd)
		{
			int i=0;
			if(cmd.CommandType==CommandType.StoredProcedure)  OracleCommandBuilder.DeriveParameters((OracleCommand)cmd);
			 
			System.Collections.IEnumerator myEnumerator = Params.GetEnumerator();
			
			while(myEnumerator.MoveNext())
			{
				if(cmd.CommandType==CommandType.StoredProcedure)
				{
					((OracleCommand)cmd).Parameters[i+1].Value= myEnumerator.Current;
				}
				else
				{
					((OracleCommand)cmd).Parameters.Add((OracleParameter)myEnumerator.Current);
				}
				i += 1;
			}

		}

		/// <summary>
		/// 执行一条Sql命令(主要是insert,update命令)，返回是否执行成功的标志
		/// </summary>
		/// <param name="commandText">sql命令行</param>
		/// <returns>1表示成功，0表示失败</returns>
		public int GetReturn(string commandText) 
		{ 
			//int i;
			this.Open();

			OracleCommand cmd = new OracleCommand(commandText,this._conn);

			object r = cmd.ExecuteScalar();
			this.Close();

			if(Object.Equals(r, DBNull.Value))
			{
				return 0;
			}
			else
			{
				return (int)r;
			}
		}

		/// <summary>
		/// 执行一条SQL命令，返回要查询的某一个字段值
		/// </summary>
		/// <param name="commandText">执行的查询命令行</param>
		/// <returns>返回的某个字段的值</returns>
		public string GetReturnStr(string commandText) 
		{ 
			this.Open();
			OracleCommand cmd = new OracleCommand(commandText,this._conn);

			object r = cmd.ExecuteScalar();
			this.Close();

			if(Object.Equals(r,null))
			{ 
				return "";
			}
			else
			{
				return r.ToString();
			}
		}
        /// <summary>
        /// Run SQL command.
        /// </summary>
        /// <param name="commandText">Command of SQL.</param>
        /// <returns>Command Execute Successful Falg.</returns>
        public int RunCommandRowCount(string commandText)
        {
            this.Open();
            OracleCommand cmd = new OracleCommand(commandText, this._conn);
            if (this._bTransaction) cmd.Transaction = this._trans;
            int ret = cmd.ExecuteNonQuery();
            if (!_bTransaction) this.Close();
            return ret;
        }

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <returns>Command Execute Successful Falg.</returns>
		public int RunCommand(string commandText) 
		{ 
			this.Open();
			OracleCommand cmd = new OracleCommand(commandText,this._conn);
		    if (this._bTransaction) cmd.Transaction = this._trans;
			cmd.ExecuteNonQuery();
			if(!_bTransaction) this.Close();
			return 0;
		}

        /// <summary>
        /// Run SQL command.
        /// </summary>
        /// <param name="commandText">Command of SQL.</param>
        /// <param name="transaction">Transaction of SQL</param>
        /// <returns>Command Execute Successful Falg.</returns>
        public int RunCommand(string commandText, OracleTransaction transaction)
        {
            this.Open();
            OracleCommand cmd = new OracleCommand(commandText, this._conn);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
            return 0;
        }

		/// <summary>
		/// Run SQL command with Parameters.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
        /// <param name="parameters">Command params.</param>
		/// <returns>Command Execute Successful Falg.</returns>
		public int RunCommand(string commandText, object[] parameters) 
		{
			this.Open();
			OracleCommand cmd = new OracleCommand(commandText,_conn);
			// Add command Parameters
			this.AssignParameter(parameters,cmd);

			if(this._bTransaction) cmd.Transaction=this._trans;
			cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
			cmd.Dispose();
			cmd=null;
			this.Close();
			return 0;
			
		}

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <param name="dataReader">Return result of Command.</param>
		public void RunCommand(string commandText, out IDataReader dataReader) 
		{
			Open();
			OracleCommand cmd = new OracleCommand(commandText,_conn);
			dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}

		/// <summary>
		/// run sql command return DataSet
		/// </summary>
		/// <param name="commandText">sql command text</param>
        /// <param name="dataTable">a dataset of the sql run result</param>
		public void RunCommand(string commandText,out DataTable dataTable)
		{
			Open();
			DataTable Dt=new DataTable();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
            if (this._bTransaction) dap.SelectCommand.Transaction = this._trans;
			dap.Fill(Dt);
			dataTable=Dt;
			if (!_bTransaction) Close();
		}

		/// <summary>
		/// run sql command return DataSet
		/// </summary>
		/// <param name="commandText">sql command text</param>
		/// <param name="dataSet">a dataset of the sql run result</param>
		public void RunCommand(string commandText,out DataSet dataSet)
		{
            Open();
            DataSet ds = new DataSet();
            OracleDataAdapter dap = new OracleDataAdapter(commandText, _conn);
            if (_bTransaction) dap.SelectCommand.Transaction = _trans;
            dap.Fill(ds);
            dataSet = ds;
            if (!_bTransaction) Close();
		}

        /// <summary>
        /// run sql command return DataSet
        /// </summary>
        /// <param name="commandText">sql command text</param>
        /// <param name="dataSet">a dataset of the sql run result</param>
        /// <param name="transaction">Transaction of SQL</param>
        public void RunCommand(string commandText, out DataSet dataSet,OracleTransaction transaction)
        {
            try
            {
                Open();
                DataSet Ds = new DataSet();
                OracleDataAdapter dap = new OracleDataAdapter(commandText, _conn);
                dap.SelectCommand.Transaction = transaction;
                dap.Fill(Ds);
                dataSet = Ds;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception(ex.InnerException.Message);
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }

        }
		
        /// <summary>
		/// run sql command return a DataSet
		/// </summary>
		/// <param name="commandText">sql command </param>
		/// <param name="tableName">return table name </param>
		/// <param name="dataSet">the sql execute result datatable</param>
		public void RunCommand(string commandText,string tableName,out DataSet dataSet)
		{
			Open();
			DataSet Ds=new DataSet();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
			dap.Fill(Ds,tableName);
			dataSet=Ds;
			Close();
		}

		/// <summary>
		/// run sql command  
		/// </summary>
		/// <param name="commandText">sql command</param>
		/// <param name="tableName">table name</param>
		/// <param name="pintPageNum">the current page num </param>
		/// <param name="pintPageSize">the page size </param>
		/// <param name="dataSet">return dataset </param>
		public void RunCommand(string commandText,string tableName,int pintPageNum,int pintPageSize,out DataSet dataSet)
		{
			//cal the start data record 
			int pintStartRec=(pintPageNum-1)*pintPageSize;
			Open();
			DataSet Ds=new DataSet();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
			dap.Fill(Ds,pintStartRec,pintPageSize,tableName);
			dataSet=Ds;
			Close();
		}

		/// <summary>
		/// run sql command return the special page DataSet
		/// </summary>
		/// <param name="commandText">sql command </param>
		/// <param name="tableName">return table name</param>
		/// <param name="prams">sql parameters</param>
		/// <param name="dataSet"></param>
		public void RunCommand(string commandText,string tableName,object[] prams,out DataSet dataSet)
		{
			Open();
			DataSet ds=new DataSet();
			OracleCommand cmd=new OracleCommand();
			OracleDataAdapter dap=new OracleDataAdapter();
			
			this.AssignParameter(prams,cmd);
			cmd.CommandText=commandText;
			cmd.Connection=_conn;
			dap.SelectCommand=cmd;
			dap.Fill(ds,tableName);
			dataSet=ds;
			Close();
		}

		/// <summary>
		/// Run SQL command.
		/// </summary>
		/// <param name="commandText">Command of SQL.</param>
		/// <param name="prams">Command params.</param>
		/// <param name="dataReader">Return result of Command.</param>
		public void RunCommand(string commandText, object[] prams, out IDataReader dataReader) 
		{
			Open();
			OracleCommand cmd = new OracleCommand(commandText,_conn);
			if (prams != null) 
			{
				this.AssignParameter(prams,cmd);
			}
			dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
			cmd.Parameters.Clear();
		}
		#endregion

		#region  run stored procedure
		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <returns>Stored procedure return value.</returns>
		public int RunProc(string procName, object[] prams) 
		{
			int i;
			OracleCommand cmd = CreateCommand(procName, prams);
			cmd.ExecuteNonQuery();
			this.Close();
			i= (int)cmd.Parameters["@RETURN_VALUE"].Value;
			cmd.Parameters.Clear();
			return i;
		}

		/// <summary>
		/// run stored procedure
		/// </summary>
		/// <param name="procName">stored procedure name</param>
		/// <returns>int stored procedure  return value</returns>
		public int RunProc(string procName) 
		{
			OracleCommand cmd = CreateCommand(procName, null);
			cmd.Connection=this._conn;
			cmd.ExecuteNonQuery();
			this.Close();
			return (int)cmd.Parameters["@RETURN_VALUE"].Value;
		}
		
		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public void RunProc(string procName, out IDataReader dataReader) 
		{
			OracleCommand cmd = CreateCommand(procName, null);
			dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}
		
		/// <summary>
		/// return dataset
		/// </summary>
		/// <param name="procName">the procedure name </param>
		/// <param name="prams">sql parameters value </param>
		/// <param name="dataSet">return a dataset</param>
		public void RunProc(string procName,object[] prams,out DataSet dataSet)
		{
			Open();
			DataSet ds=new DataSet();
			OracleCommand cmd=new OracleCommand();
			OracleDataAdapter dap=new OracleDataAdapter();
			
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandText=procName;
			cmd.Connection=_conn;
			this.AssignParameter(prams,cmd);
			dap.SelectCommand=cmd;
			dap.Fill(ds);
			dataSet=ds;
			Close();
		}

        /// <summary>
        /// return dataset
        /// </summary>
        /// <param name="procName">the procedure name</param>
        /// <param name="prams">sql parameters value</param>
        /// <param name="dataTable">return a datatable</param>
		public void RunProc(string procName,object[] prams,out DataTable dataTable)
		{
			Open();
			DataTable dt=new DataTable();
			OracleCommand cmd=new OracleCommand();
			OracleDataAdapter dap=new OracleDataAdapter();
			
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandText=procName;
			cmd.Connection=_conn;
			this.AssignParameter(prams,cmd);
			dap.SelectCommand=cmd;
			dap.Fill(dt);
			dataTable = dt;
			Close();
		}

        /// <summary>
        /// return dataset
        /// </summary>
        /// <param name="procName">the procedure name</param>
        /// <param name="prams">sql parameters value</param>
        /// <param name="tableName">the table name</param>
        /// <param name="dataSet">return a dataset</param>
		public void RunProc(string procName,object[] prams,string tableName,out DataSet dataSet)
		{
			Open();
			DataSet ds=new DataSet();
			OracleCommand cmd=new OracleCommand();
			OracleDataAdapter dap=new OracleDataAdapter();
			
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandText=procName;
			cmd.Connection=_conn;
			this.AssignParameter(prams,cmd);
			dap.SelectCommand=cmd;
			dap.Fill(ds);
			dataSet=ds;
			Close();
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public void RunProc(string procName, object[] prams, out IDataReader dataReader) 
		{
			OracleCommand cmd = CreateCommand(procName, prams);
			dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
			cmd.Parameters.Clear();
		}


		/// <summary>
		/// Create command object used to call stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Params to stored procedure.</param>
		/// <returns>Command object.</returns>
		private OracleCommand CreateCommand(string procName, object[] prams)
		{
			// make sure connection is open
			Open();
			OracleCommand cmd = new OracleCommand(procName, _conn);
			cmd.CommandType = CommandType.StoredProcedure;

			// Add Proc Parameters
			if (prams != null) 
			{
				this.AssignParameter(prams,cmd);
			}
			
			// Add return param
			cmd.Parameters.Add(
				new OracleParameter("@RETURN_VALUE", OracleType.Int32, 4,
				ParameterDirection.ReturnValue, false, 0, 0,
				string.Empty, DataRowVersion.Default, null));

			return cmd;
		}
		#endregion
		
		#region execute a Command object
		/// <summary>
		/// 执行一个命令对象
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <returns>返回1成功，0失败</returns>
		public int ExecuteCommand(IDbCommand cmdp)
		{
			this.Open();
			OracleCommand cmd = (OracleCommand)cmdp;
			cmd.Connection=_conn;
			if(_bTransaction)  
				cmd.Transaction=_trans;
			else
				cmd.Transaction=null;
			int ret=cmd.ExecuteNonQuery();
			if(!_bTransaction) this.Close();
			if(cmd.CommandType==CommandType.StoredProcedure)
			{
				return (int)cmd.Parameters["@RETURN_VALUE"].Value;
			}
			else
			{
				return ret;
			}
		}

		/// <summary>
		/// 执行一个命令对象，返回只读的资料集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <param name="dataReader">返回的只读的资料集合</param>
		/// <returns></returns>
		public void ExecuteCommand(IDbCommand cmdp,out IDataReader dataReader)
		{
			OracleCommand cmd =(OracleCommand)cmdp;
			cmd.Connection=_conn;
			dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}

		/// <summary>
		/// 执行一个命令对象，返回DataSet数据集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
		/// <param name="dataSet">返回的DataSet数据集合</param>
		/// <returns></returns>
		public void ExecuteCommand(IDbCommand cmdp,out DataSet dataSet)
		{
			DataSet ds=new DataSet();
			OracleDataAdapter dap=new OracleDataAdapter();
			OracleCommand cmd =(OracleCommand)cmdp;
			cmd.Connection=_conn;
			dap.SelectCommand=cmd;
			dap.Fill(ds);
			dataSet=ds;
			Close();
		}

		/// <summary>
		/// 执行一个命令对象，返回待名称的DataSet数据集合
		/// </summary>
		/// <param name="cmdp">命令对象</param>
        /// <param name="tableName">表名称</param>
		/// <param name="dataSet">要返回的数据集合</param>
		/// <returns></returns>
		public void ExecuteCommand(IDbCommand cmdp,string tableName,out DataSet dataSet)
		{
			Open();
			DataSet Ds=new DataSet();
			OracleDataAdapter dap=new OracleDataAdapter();
			OracleCommand cmd =(OracleCommand)cmdp;
			cmd.Connection=_conn;
			dap.SelectCommand=cmd;
			dap.Fill(Ds,tableName);
			dataSet=Ds;
			Close();
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
			this.Open();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
			OracleCommandBuilder OracleCB=new OracleCommandBuilder(dap);
			dap.Update(ds,ds.Tables[0].TableName);

			return ds;

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
			this.Open();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
			OracleCommandBuilder OracleCB=new OracleCommandBuilder(dap);
			dap.Update(ds,ds.Tables[tblIndex].TableName);

			return ds;
		}

		/// <summary>
		///执行一条Sql命令，对该Sql命令返回的DataTable数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sql命令行</param>
		/// <param name="dt">要更新的数据集合</param>
		/// <returns></returns>
		public DataTable UpdateDb(string commandText, DataTable  dt) 
		{
			this.Open();
			OracleDataAdapter dap=new OracleDataAdapter(commandText,_conn);
			OracleCommandBuilder OracleCB=new OracleCommandBuilder(dap);
			dap.Update(dt);
			return dt;
		}

		/// <summary>
		///  执行一条Sql命令集合，对该Sql命令返回的DataSet数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sqlSelect命令集合</param>
		/// <param name="ds">返回的DataSet资料集合</param>
		/// <returns>更新后的数据集合</returns>
		public DataSet UpdateDb(ArrayList commandText,DataSet ds)
		{
			//定义系统变量
			OracleTransaction myTrans;
			OracleCommandBuilder OracleCB;
			OracleDataAdapter dap;
			
			//打开数据库连接
			this.Open();
			myTrans=this._conn.BeginTransaction();

			try
			{
				//如果没有纪录，抛出异常
				if(commandText.Count!=ds.Tables.Count)
				{
					throw new Exception("没有要更新的纪录");
					//return ds;
				}
				
				DataTable dt;
				string strSql;

				for(int i=0;i<commandText.Count;i++)
				{
					strSql=commandText[i].ToString();
					dt=ds.Tables[i];
					//SqlCommandBuilder自动生成要更新的sql命令
					dap=new OracleDataAdapter(strSql,_conn);
					OracleCB=new OracleCommandBuilder(dap);
					//执行事务，提交到数据库
					dap.SelectCommand.Transaction=myTrans;
					dap.Update(dt);

				}
			

				//事务提交

				myTrans.Commit();

			}
			catch(Exception ex)
			{
				ex.ToString();
				myTrans.Rollback();
			}

			finally
			{
				_conn.Close();
				
			}
			return ds;

		}
		
		/// <summary>
		///  执行一条Sql命令集合，对该Sql命令返回的DataSet数据集合更新到数据库中
		/// </summary>
		/// <param name="commandText">T-sqlSelect命令集合</param>
		/// <param name="dss">返回的DataSet资料集合</param>
		/// <returns>更新后的数据集合</returns>
		public ArrayList UpdateDb(ArrayList commandText,ArrayList dss)
		{
			//变量定义
			OracleTransaction myTrans;
			OracleCommandBuilder OracleCB;
			OracleDataAdapter dap;
			string strSql;
			DataSet ds;
			//打开数据库连接
			this.Open();
			myTrans=this._conn.BeginTransaction();

			try
			{
				System.Collections.IEnumerator commandEnum=commandText.GetEnumerator();
				System.Collections.IEnumerator dsEnum=dss.GetEnumerator();

				while(commandEnum.MoveNext())
				{
					dsEnum.MoveNext();
					strSql=commandEnum.Current.ToString();
					ds=(DataSet)dsEnum.Current;

					dap=new OracleDataAdapter(strSql,_conn);
					OracleCB=new OracleCommandBuilder(dap);
					
					dap.UpdateCommand.Transaction=myTrans;
					dap.Update(ds,ds.Tables[0].TableName);

				}

				//事务提交
				myTrans.Commit();
			}
			catch(Exception ex)
			{
				ex.ToString();
				myTrans.Rollback();
			}

			finally
			{
				_conn.Close();
			}
			return dss;
		}
		#endregion

        /// <summary>
        ///  执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程的名称</param>
        /// <param name="parameters">存储过程的参数</param>
        public void RunProcedure(string storedProcName, OracleParameter[] parameters)
        {
            Open();
            OracleCommand cmd = new OracleCommand(storedProcName, _conn);
            cmd.CommandText = storedProcName;//声明存储过程名
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            cmd.ExecuteNonQuery();//执行存储过程
            Close();
        }
    }
}
