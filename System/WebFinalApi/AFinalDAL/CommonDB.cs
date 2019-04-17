using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFinalDAL
{
    public class CommonDB
    {
        public CommonDB()
        {
            Connection();
        }

        private IDbTransaction _transaction = null;

        private IDbConnection _dbconnection = null;

        public void Connection()
        {
            if (_dbconnection == null)
            {
                _dbconnection = new SqlConnection(ConfigurationManager.AppSettings["DbConnection"]);
            }
        }

        public int Excute(string sql, object param = null)
        {
            Connection();
            if (_transaction == null)
            {
                return _dbconnection.Execute(sql, param);
            }
            else
            {
                return _transaction.Connection.Execute(sql, param, _transaction);
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            Connection();
            if (_transaction == null)
            {
                return _dbconnection.ExecuteScalar<T>(sql, param);
            }
            else
            {
                return _transaction.Connection.ExecuteScalar<T>(sql, param, _transaction);
            }
        }


        public IEnumerable<T> Query<T>(string sql, object param = null) where T : class
        {
            Connection();
            if (_transaction == null)
            {
                return _dbconnection.Query<T>(sql, param);
            }
            else
            {
                return _transaction.Connection.Query<T>(sql, param, _transaction);
            }
        }


        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            Connection();
            if (_transaction == null)
                return _dbconnection.Query(sql, param);
            else
                return _transaction.Connection.Query(sql, param, _transaction);
        }


        public T QueryFirstOrDefault<T>(string sql, object param = null) where T : class
        {
            Connection();
            if (_transaction == null)
            {
                return _dbconnection.QueryFirstOrDefault<T>(sql, param);
            }
            else
            {
                return _transaction.Connection.QueryFirstOrDefault<T>(sql, param, _transaction);
            }
        }

        public dynamic QueryFirstOrDefault(string sql, object param = null)
        {
            Connection();
            if (_transaction == null)
            {
                return _dbconnection.QueryFirstOrDefault(sql, param);
            }
            else
            {
                return _transaction.Connection.QueryFirstOrDefault(sql, param, _transaction);
            }
        }

        #region 事务
        public void BeginTrans()
        {
            Connection();
            if (_dbconnection.State == ConnectionState.Closed)
                _dbconnection.Open();
            _transaction = _dbconnection.BeginTransaction();
            isCommit = false;
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                if (!isCommit)
                    _transaction.Commit();
                isCommit = true;
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                if (!isCommit)
                    _transaction.Rollback();
                isCommit = true;
                _transaction = null;
            }

        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        private bool isCommit = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                if (_transaction != null)
                {
                    if (!isCommit)
                        _transaction.Commit();
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_dbconnection != null)
                {
                    if (_dbconnection.State != ConnectionState.Closed)
                        _dbconnection.Close();
                    _dbconnection.Dispose();
                    _dbconnection = null;
                }
                isCommit = true;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~CommonDB()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }


}
