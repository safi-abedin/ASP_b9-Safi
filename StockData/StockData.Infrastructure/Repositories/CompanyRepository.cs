using Microsoft.EntityFrameworkCore;
using StockData.Domain.Entities;
using StockData.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<IList<Company>> GetCompany(string tradingCode)
        {
            Expression<Func<Company, bool>> expression = null;

            if (!string.IsNullOrWhiteSpace(tradingCode))
            {
                expression = x => x.TradeCode.Contains(tradingCode);
            }
            return await GetAsync(expression,null);
        }

        public async Task<int> GetCompanyCountAsync(string tradingCode)
        {
            Expression<Func<Company,bool>> expression = null;

            if (!string.IsNullOrWhiteSpace(tradingCode))
            {
                expression = x => x.TradeCode.Contains(tradingCode);
            }

            return await GetCountAsync(expression);
        }

    }
}
