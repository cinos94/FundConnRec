using FundConnRec.API.Models;
using FundConnRec.API.Repositories.Interfaces;
using FundConnRec.Models.Exceptions;
using FundConnRec.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly FundConnContext _context;

        private readonly ISecurityRepository _securityRepository;

        private readonly IConfiguration _configurationRepository;

        public PortfolioRepository(FundConnContext context, ISecurityRepository securityRepository, IConfiguration configuration)
        {
            _context = context;
            _securityRepository = securityRepository;
            _configurationRepository = configuration;
        }

        public async Task Add(Portfolio portfolio)
        {
            portfolio.Date = portfolio.Date.Date;
            Portfolio portfolioInDB = await Get(portfolio.ISIN, portfolio.Date);
            if (portfolioInDB == null)
            {
                await _securityRepository.AddRange(portfolio.Positions.Select(x => x.Security));
                _context.Portfolios.Add(portfolio);
                await _context.SaveChangesAsync();
            }
            else throw new ChangeConflictException("There is already Portfolio with ISIN and Date speicfied");
        }

        public bool IsInToleranceRange(Portfolio portfolio)
        {
            decimal.TryParse(_configurationRepository["AppSettings:PortfolioValueTolerance"], out decimal ToleranceValue);
            if (portfolio.MarketValue * ToleranceValue > Math.Abs(portfolio.MarketValue - portfolio.Positions.Sum(x => x.MarketValue)))
            {
                return true;
            }
            else return false;
        }

        public async Task Delete(Portfolio entity)
        {
            var portfolio = await Get(entity.PortfolioId);
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
        }

        public void Update(Portfolio dbEntity, Portfolio entity)
        {
            throw new NotImplementedException();
        }
    }
}
