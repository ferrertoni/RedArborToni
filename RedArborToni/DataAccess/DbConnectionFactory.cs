using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace RedArborToni.DataAccess
{
    public class DbConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        public DbConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName");  
            _name = "localhost";
            _provider = DbProviderFactories.GetFactory("System.Data.SqlClient");
            _connectionString = "Data Source=localhost;Initial Catalog=RedArbor;Persist Security Info=True;User ID=sa;Password=saroot";
        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Failed to create a connection using the connection string named '{0}' in app/web.config.", _name));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
