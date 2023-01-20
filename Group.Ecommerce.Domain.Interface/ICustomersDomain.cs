using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group.Ecommerce.Domain.Entity;
using System.Threading.Tasks;

namespace Group.Ecommerce.Domain.Interface
{
    public interface ICustomersDomain
    {
        #region Metodos Sincronos
        bool Insert(Customers customer);
        bool Update(Customers customer);
        bool Delete(string CustomerId);

        Customers Get(string CustomerId);
        IEnumerable<Customers> GetAll();

        #endregion

        #region Metodos Asincronos
        Task<bool> InsertAsync(Customers customer);
        Task<bool> UpdateAsync(Customers customer);
        Task<bool> DeleteAsync(string CustomerId);

        Task<Customers> GetAsync(string CustomerId);
        Task<IEnumerable<Customers>> GetAllAsync();

        #endregion
    }
}
