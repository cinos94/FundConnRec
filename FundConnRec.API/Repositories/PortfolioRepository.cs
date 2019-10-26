using FundConnRec.API.Models;
using FundConnRec.API.Repositories.Interfaces;
using FundConnRec.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories
{
    public class PortfolioRepository : IDataRepository<Portfolio>
    {
        private readonly FundConnContext _context;

        public PortfolioRepository(FundConnContext context)
        {
            _context = context;
        }

        public void Add(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public Portfolio Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> GetAll()
        {
            _context.Positions.Include(x => x.Security).ToList();
            return _context.Portfolios.Include(x => x.Positions).ToList();
        }

        public void Update(Portfolio dbEntity, Portfolio entity)
        {
            throw new NotImplementedException();
        }
    }
}
