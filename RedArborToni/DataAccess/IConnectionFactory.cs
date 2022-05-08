using System.Data;

namespace RedArborToni.DataAccess
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
