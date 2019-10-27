using FundConnRec.API.Models;
using FundConnRec.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories.Interfaces
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly FundConnContext _context;

        public SecurityRepository(FundConnContext context)
        {
            _context = context;
        }

        public Task Add(Security entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRange(IEnumerable<Security> securities)
        {
            foreach(Security security in securities)
            {
                Security securityInDB = await Get(security.ISIN);
                if(securityInDB == null)
                {
                    await Add(security);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Update(securityInDB, security);
                }
            }
        }

        public Task Delete(Security entity)
        {
            throw new NotImplementedException();
        }

        public Task<Security> Get(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Security> Get(string ISIN)
        {
            return await _context.Securities.AsNoTracking().Where(x => x.ISIN == ISIN).FirstOrDefaultAsync();
        }

        public IEnumerable<Security> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Security dbEntity, Security entity)
        {
            entity.SecurityId = dbEntity.SecurityId;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
