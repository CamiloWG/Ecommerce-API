using Group.Ecommerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Group.Ecommerce.Infraestructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;

                sqlConnection.ConnectionString = _config.GetConnectionString("NorthwindConnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }
}