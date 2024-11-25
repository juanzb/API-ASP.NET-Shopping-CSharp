using Models;
using Repository.Interfaces.Actions;

namespace Repository.Interfaces
{
    public interface IProductsRepository : 
        IReadRepository<Products, int>,
        ICreateRepository<Products>,
        IUpdateRepository<Products>,
        IRemoveRepository<int>
    {
    }
}
