using Models;
using System.Collections.Generic;
using UnitOfWork.Interfaces;

namespace Services
{
    public class ProductsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsServices (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Products> AllProductsService()
        {
            List <Products> products = new List<Products>();
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    products = connect.Repositories.ProductsRepository.GetAll();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return products;
        }

        public Products GetProductcsByIdService(int id)
        {
            Products product;
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    product = connect.Repositories.ProductsRepository.GetById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public void CreateProductcsService(Products product)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ProductsRepository.Create(product);
                    connect.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProductcsService(Products product)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ProductsRepository.Update(product);
                    connect.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductcsService(int id)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.ProductsRepository.Remove(id);
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
