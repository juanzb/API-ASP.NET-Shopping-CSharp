using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServer : IUnitOfWork
    {
        private readonly string _conectionString;
        public UnidOfWorkMySqlServer (string connectionString) 
        {
            _conectionString = connectionString;
        }
        public IUnitOfWorkAdapter Create()
        {
            return new UnidOfWorkMySqlServerAdapter(_conectionString);
        }
    }
}
