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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return new Clients();
        }

        public void CreateClientService (Clients client)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ClientsRepository.Create(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
