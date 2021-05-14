using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace url_shortener.Domain.Interfaces
{
    public interface ISpatacoliUrlRepository : IGenericRepository<SpatacoliUrl>
    {
        SpatacoliUrl GetByUrl(string url);
        SpatacoliUrl GetByToken(string token);
    }
}
