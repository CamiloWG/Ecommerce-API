using Dapper;
using Group.Ecommerce.Domain.Entity;
using Group.Ecommerce.Infraestructure.Interface;
using Group.Ecommerce.Transversal.Common;

namespace Group.Ecommerce.Infraestructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UsersRepository(IConnectionFactory connectionFactory)
        { 
            _connectionFactory = connectionFactory;
        }
        public Users Authenticate(string username, string password)
        {
            using(var connection = _connectionFactory.GetConnection)
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("password", password);

                var user = connection.QuerySingle<Users>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return user;
            }
        }
    }
}
