using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServer : IUnitOfWork
    {
        public IUnitOfWorkAdapter Create()
        {
            return new UnidOfWorkMySqlServerAdapter();
        }
        
    }
}
