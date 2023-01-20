using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group.Ecommerce.Transversal.Common
{
    internal interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }

    }
}
