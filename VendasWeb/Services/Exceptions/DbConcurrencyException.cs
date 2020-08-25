using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasWeb.Services.Exceptions
{
    public class DbConcurrencyException :ApplicationException
    {
        public DbConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
