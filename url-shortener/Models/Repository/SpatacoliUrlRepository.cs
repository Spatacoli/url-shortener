using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_shortener.Domain;
using url_shortener.Domain.Interfaces;

namespace url_shortener.Models.Repository
{
    public class SpatacoliUrlRepository : GenericRepository<SpatacoliUrl>, ISpatacoliUrlRepository
    {
        public SpatacoliUrlRepository(ApplicationContext context) : base(context)
        { }

        public SpatacoliUrl GetByToken(string token)
        {
            return _context.Set<SpatacoliUrl>().Where(u => u.Token == token).FirstOrDefault();
        }

        public SpatacoliUrl GetByUrl(string url)
        {
            return _context.Set<SpatacoliUrl>().Where(u => u.Url == url).FirstOrDefault();
        }
    }
}
