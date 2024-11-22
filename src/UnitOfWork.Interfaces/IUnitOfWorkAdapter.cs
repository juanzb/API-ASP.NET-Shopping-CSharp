
namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; set; }

        void SaveChanges();
    }
}
