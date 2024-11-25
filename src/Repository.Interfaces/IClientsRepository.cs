using Models;
using Repository.Interfaces.Actions;

namespace Repository.Interfaces
{
    public interface IClientsRepository :
        IReadRepository<Clients, int>,
        ICreateRepository<Clients>,
        IUpdateRepository<Clients>,
        IRemoveRepository<int>
    {
    }
}
