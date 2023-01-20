using Group.Ecommerce.Domain.Entity;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Infraestructure.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Group.Ecommerce.Domain.Core
{
    public class CustomersDomain : ICustomersDomain
    {
        private readonly ICustomersRepository _costumersRepository;
        public CustomersDomain(ICustomersRepository costumersRepository)
        {
            _costumersRepository = costumersRepository;
        }

        #region Metodos Sincronos

        public bool Insert(Customers customers)
        {
            return _costumersRepository.Insert(customers);
        }

        public bool Update(Customers customers)
        {
            return _costumersRepository.Update(customers);
        }

        public bool Delete(string customerId)
        {
            return _costumersRepository.Delete(customerId);
        }

        public Customers Get(string customerId)
        {
            return _costumersRepository.Get(customerId);
        }

        public IEnumerable<Customers> GetAll()
        {
            return _costumersRepository.GetAll();
        }


        #endregion

        #region Metodos Asincronos

        public async Task<bool> InsertAsync(Customers customers)
        {
            return await _costumersRepository.InsertAsync(customers);
        }

        public async Task<bool> UpdateAsync(Customers customers)
        {
            return await _costumersRepository.UpdateAsync(customers);
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _costumersRepository.DeleteAsync(customerId);
        }

        public async Task<Customers> GetAsync(string customerId)
        {
            return await _costumersRepository.GetAsync(customerId);
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            return await _costumersRepository.GetAllAsync();
        }

        #endregion
    }
}