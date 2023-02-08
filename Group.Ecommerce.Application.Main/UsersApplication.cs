using AutoMapper;
using Group.Ecommerce.Application.DTO;
using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Transversal.Common;
using Group.Ecommerce.Application.Validator;

namespace Group.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IMapper _mapper;
        private readonly IUsersDomain _usersDomain;
        private readonly UsersDtoValidator _userDtoValidator;  
        public UsersApplication(IUsersDomain usersDomain, IMapper mapper, UsersDtoValidator usersDtoValidator) 
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
            _userDtoValidator = usersDtoValidator;
        }
        public Response<UsersDto> Authenticate(string username, string password)
        {
            var response = new Response<UsersDto>();
            var validation = _userDtoValidator.Validate(new UsersDto { UserName = username, Password = password });
            if(!validation.IsValid)
            {
                response.Message = "Error de validación";
                response.Errors = validation.Errors;
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
