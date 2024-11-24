using Models;
using UnitOfWork.Interfaces;

namespace Services
{
    public class InvoicesDetailsServices
    {
        IUnitOfWork _unitOfWork;
        public InvoicesDetailsServices(IUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public List<InvoicesDetails> AllInvoiceDetailsService()
        {
            List<InvoicesDetails> details;
            try
            {
                using (IUnitOfWorkAdapter connect = _unitOfWork.Create())
                {
                    details = connect.Repositories.InvoiceDetailsRespository.GetAll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return details;
        }

        //public void InsertInvoiceDetail(int invoiceID, List<InvoicesDetails> details, MySqlConnection connect, MySqlTransaction transaction)
        //{
        //    try
        //    {
        //        // Insertar data de la compra en la tabal "invoicedetails"
        //        const string queryDB = "INSERT INTO invoicesdetails " +
        //            "(invoiceID, productID, quantity, price, iva, subtotal, total) " +
        //            "VALUES (@A,@B,@C,@D,@E,@F,@G)";
        //        foreach (var detail in details)
        //        {
        //            using (var command = new MySqlCommand(queryDB, connect, transaction))
        //            {
        //                command.Parameters.AddWithValue("@A", invoiceID);
        //                command.Parameters.AddWithValue("@B", detail.ProductID);
        //                command.Parameters.AddWithValue("@C", detail.Quantity);
        //                command.Parameters.AddWithValue("@D", detail.Price);
        //                command.Parameters.AddWithValue("@E", detail.Iva);
        //                command.Parameters.AddWithValue("@F", detail.SubTotal);
        //                command.Parameters.AddWithValue("@G", detail.Total);
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inserting invoice detail: {ex.Message}");
        //        throw;
        //    }
        //}

        //public void DeleteInvoiceDetail(int invoiceID, MySqlConnection connect, MySqlTransaction transaction)
        //{
        //    try
        //    {
        //        // Eliminar data de la compra en la tabla "invoicedetails"
        //        const string queryDB = "Delete FROM invoicesdetails WHERE invoiceID=@invoiceID";
        //        using (var command = new MySqlCommand(queryDB, connect, transaction))
        //        {
        //            command.Parameters.AddWithValue("@invoiceID", invoiceID);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deleting invoice detail: {ex.Message}");
        //        throw;
        //    }
        //}
    }
}
