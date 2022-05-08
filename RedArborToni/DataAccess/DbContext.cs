using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace RedArborToni.DataAccess
{
    public class DbContext
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
        private readonly LinkedList<AdoNetUnitOfWork> _unitOfWorks = new LinkedList<AdoNetUnitOfWork>();

        public DbContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.Create();
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();

            _readerWriterLock.EnterReadLock();
            if (_unitOfWorks.Count > 0)
                command.Transaction = _unitOfWorks.First.Value.Transaction;
            _readerWriterLock.ExitReadLock();

            return command;
        }
    }
}
