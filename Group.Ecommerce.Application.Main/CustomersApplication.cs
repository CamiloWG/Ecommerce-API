using AutoMapper;
using Group.Ecommerce.Application.DTO;
using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Domain.Entity;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Group.Ecommerce.Application.Main
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly ICustomersDomain _customersDomain;
        private readonly IMapper _mapper;

        public CustomersApplication(ICustomersDomain customersDomain, IMapper mapper)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;
        }


        #region Metodos Sincronos
        public Response<bool> Insert(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Insert(customer);
                if(response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }
        public Response<bool> Update(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Update(customer);
                if (response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se actualizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }
        public Response<bool> Delete(string CustomerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = _customersDomain.Delete(CustomerId);
                if (response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se actualizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }

        public Response<CustomersDto> Get(string CustomerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                var customer = _customersDomain.Get(CustomerId);
                response.Data = _mapper.Map<CustomersDto>(customer);
                if (response.Data != null)
                {
                    response.IsSucces = true;
                    response.Message = "La consula se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }

        public Response<IEnumerable<CustomersDto>> GetAll()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                var customers = _customersDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSucces = true;
                    response.Message = "La consula se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }

        #endregion

        #region Metodos Asincronos
        public async Task<Response<bool>> InsertAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.InsertAsync(customer);
                if (response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<bool>> UpdateAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.UpdateAsync(customer);
                if (response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se actualizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<bool>> DeleteAsync(string CustomerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _customersDomain.DeleteAsync(CustomerId);
                if (response.Data)
                {
                    response.IsSucces = true;
                    response.Message = "El registro se actualizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<CustomersDto>> GetAsync(string CustomerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                var customer = await _customersDomain.GetAsync(CustomerId);
                response.Data = _mapper.Map<CustomersDto>(customer);
                if (response.Data != null)
                {
                    response.IsSucces = true;
                    response.Message = "La consula se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<Response<IEnumerable<CustomersDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                var customers = await _customersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSucces = true;
                    response.Message = "La consula se realizó exitosamente!";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
            }

            return response;
        }

        #endregion
    }
}