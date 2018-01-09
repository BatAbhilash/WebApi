using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace WebApi.Handlers
{
    public class Ping : IRequest<IEnumerable<Customer>> { }
    public class PingHandler : Repository<Customer>, IRequestHandler<Ping, IEnumerable<Customer>>
    {
        public PingHandler(IOptions<ConnectionStrings> connectionStrings): base(connectionStrings.Value.DefaultConnection)
        {
        }

        public async Task<IEnumerable<Customer>> Handle(Ping request, CancellationToken cancellationToken)
        {
            List<Customer> listCustomers = new List<Customer>();
            var sqlCommand = "SELECT CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax "
                            + " FROM dbo.Customers";

            return base.Get(sqlCommand);
            
        }

        public override Customer PopulateRecord(IDataReader reader)
        {
            return new Customer(reader);
        }
    }
}
