using AutoMapper;
using Group.Ecommerce.Application.DTO;
using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Transversal.Common;

namespace Group.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IMapper _mapper;
        private readonly IUsersDomain _usersDomain;
        public UsersApplication(IUsersDomain usersDomain, IMapper mapper) 
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
        }
        public Response<UsersDto> Authenticate(string username, string password)
        {
            var response = new Response<UsersDto>();
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Error los parametros no pueden ser vacios";
                return response;
            }
            try
            {
                var user = _usersDomain.Authenticate(username, password);
                response.Data = _mapper.Map<UsersDto>(user);
                response.IsSucces = true;
                response.Message = "La autenticación se ha realizado exitosamente";
            }
            catch(InvalidOperationException)
            {
                response.IsSucces = true;
                response.Message = "El usuario no existe!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
