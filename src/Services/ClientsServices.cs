using Models;
using UnitOfWork.Interfaces;

namespace Services
{
    public class ClientsServices
    {
        private IUnitOfWork _unitOfWork;

        public ClientsServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<Clients> AllClientsService()
        {
            List<Clients> clients;
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    clients = connect.Repositories.ClientsRepository.GetAll();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clients;
        }

        public Clients GetByIdClientService(int idClient)
        {
            Clients result;
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    result = connect.Repositories.ClientsRepository.GetById(idClient);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void CreateClientService (Clients client)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ClientsRepository.Create(client);
                    connect.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateClientService(Clients client)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ClientsRepository.Update(client);
                    connect.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteClientService(int clientId)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ClientsRepository.Remove(clientId);
                    connect.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
