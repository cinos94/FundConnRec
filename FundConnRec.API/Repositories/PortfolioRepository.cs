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
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly FundConnContext _context;

        public PortfolioRepository(FundConnContext context)
        {
            _context = context;
        }

        public async Task Add(Portfolio entity)
        {
            entity.Date = entity.Date.Date;
            Portfolio portfolioInDB = await Get(entity.ISIN, entity.Date);
            if (portfolioInDB == null)
            {
                _context.Securities.AddRange(entity.Positions.Select(x => x.Security)); //to be fixed
                _context.Portfolios.Add(entity);
                await _context.SaveChangesAsync();
            }
            else throw new Exception("There is already Portfolio with ISIN and Date speicfied");
        }

        public async Task Delete(Portfolio entity)
        {
            var portfolio = await _context.Portfolios.FindAsync(entity.PortfolioId);
            if (portfolio == null)
            {
                throw new ArgumentException(nameof(Portfolio));
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task<Portfolio> Get(long id)
        {
            return await _context.Portfolios.Where(x => x.PortfolioId == id).FirstOrDefaultAsync();
        }

        public async Task<Portfolio> Get(string isin, DateTime date)
        {
            return await _context.Portfolios.Where(x => x.ISIN == isin && x.Date == date.Date).FirstOrDefaultAsync();
        }

        public IEnumerable<Portfolio> GetAll()
        {
            return _context.Portfolios.ToList();
           // _context.Positions.Include(x => x.Security).ToList();
           // return _context.Portfolios.Include(x => x.Positions).ToList();
        }

        public void Update(Portfolio dbEntity, Portfolio entity)
        {
            throw new NotImplementedException();
        }
    }
}
