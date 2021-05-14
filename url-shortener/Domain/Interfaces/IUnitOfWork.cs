using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace url_shortener.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISpatacoliUrlRepository SpatacoliUrls { get; }
        int Complete();
    }
}
