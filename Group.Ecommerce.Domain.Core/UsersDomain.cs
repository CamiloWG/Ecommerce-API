using Group.Ecommerce.Domain.Entity;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Infraestructure.Interface;

namespace Group.Ecommerce.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUsersRepository _usersRepository;

        public UsersDomain(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public Users Authenticate(string username, string password)
        {
            return _usersRepository.Authenticate(username, password);
        }
    }
}
