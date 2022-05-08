namespace RedArborToni.DataAccess
{
    public interface IUnitOfWork
    {
        void Dispose();
        void SaveChanges();
    }
}
