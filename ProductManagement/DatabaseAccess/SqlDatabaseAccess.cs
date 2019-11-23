using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace ProductManagement.Database
{
    //This is a helper class for executing requests and querying SQL server
    public class SqlDatabaseAccess : IDatabaseAccess
    {
        public string connectionString { get; set; }

        public SqlDatabaseAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T LoadSingle<T>(string sql)
        {
            using (IDbConnection idbcon = new SqlConnection(connectionString))
            {
                var c = idbcon.Query<T>(sql);
                return c.SingleOrDefault();
            }
        }

        public List<T> LoadData<T>(string sql)
        {
            using (IDbConnection idbcon = new SqlConnection(connectionString))
            {
                return idbcon.Query<T>(sql).ToList();
            }
        }

        public int SaveData<T>(string sql, T data)
        {
            using (IDbConnection idbcon = new SqlConnection(connectionString))
            {
                return idbcon.Execute(sql, data);
            }
        }

        public int Execute(string sql)
        {
            using (IDbConnection idbcon = new SqlConnection(connectionString))
            {
                return idbcon.Execute(sql);
            }
        }
    }
}
