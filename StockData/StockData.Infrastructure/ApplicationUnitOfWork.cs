/*using Exam1.Application;
using Exam1.Domain.Repositories;*/
using Microsoft.EntityFrameworkCore;
using StockData.Application;
using StockData.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure
{

    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {

        public IStockPriceRepository StockPriceRepository { get; private set; }

        public ICompanyRepository CompanyRepository { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, IStockPriceRepository stockPriceRepository,ICompanyRepository companyRepository) : base((DbContext)dbContext)
        {
            StockPriceRepository = stockPriceRepository;

            CompanyRepository = companyRepository;

        }
    }
}
