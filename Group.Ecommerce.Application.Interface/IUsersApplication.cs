using Group.Ecommerce.Application.DTO;
using Group.Ecommerce.Transversal.Common;

namespace Group.Ecommerce.Application.Interface
{
    public interface IUsersApplication
    {
        Response<UsersDto> Authenticate(string username, string password);
    }
}
