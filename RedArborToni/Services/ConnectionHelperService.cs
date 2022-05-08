using RedArborToni.DataAccess;

namespace RedArborToni.Services
{
    public static class ConnectionHelperService
    {
        public static IConnectionFactory GetConnection()
        {
            return new DbConnectionFactory("ConnectionString");
        }
    }
}
