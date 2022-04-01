using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDIPaladins.Infrastructure.Dapper
{
    public interface IDBContext
    {
        string ConnectionString { get; }
    }

    public class DBContext : IDBContext
    {
        public DBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
    }
}
