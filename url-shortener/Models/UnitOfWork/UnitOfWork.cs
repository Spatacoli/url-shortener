using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_shortener.Domain.Interfaces;
using url_shortener.Models.Repository;

namespace url_shortener.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            SpatacoliUrls = new SpatacoliUrlRepository(_context);
        }

        public ISpatacoliUrlRepository SpatacoliUrls { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
