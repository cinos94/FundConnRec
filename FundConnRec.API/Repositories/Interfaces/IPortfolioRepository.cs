using FundConnRec.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories.Interfaces
{
    public interface IPortfolioRepository : IDataRepository<Portfolio>
    {
        Task<Portfolio> Get(string isin, DateTime date);
    }
}
