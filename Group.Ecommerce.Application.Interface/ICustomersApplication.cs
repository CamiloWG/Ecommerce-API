using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group.Ecommerce.Application.DTO;
using Group.Ecommerce.Transversal.Common;


namespace Group.Ecommerce.Application.Interface
{
    public interface ICustomersApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomersDto customer);
        Response<bool> Update(CustomersDto customer);
        Response<bool> Delete(string CustomerId);

        Response<CustomersDto> Get(string CustomerId);
        Response<IEnumerable<CustomersDto>>  GetAll();

        #endregion

        #region Metodos Asincronos
        Task<Response<bool>> InsertAsync(CustomersDto customer);
        Task<Response<bool>> UpdateAsync(CustomersDto customer);
        Task<Response<bool>> DeleteAsync(string CustomerId);

        Task<Response<CustomersDto>> GetAsync(string CustomerId);
        Task<Response<IEnumerable<CustomersDto>>> GetAllAsync();

        #endregion
    }
}
