using FundConnRec.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories.Interfaces
{
    public interface ISecurityRepository : IDataRepository<Security>
    {
        Task AddRange(IEnumerable<Security> securities);

        Task<Security> Get(string ISIN);
    }
}
