using Group.Ecommerce.Domain.Entity;

namespace Group.Ecommerce.Domain.Interface
{
    public interface IUsersDomain
    {
        Users Authenticate(string username, string password);
    }
}
