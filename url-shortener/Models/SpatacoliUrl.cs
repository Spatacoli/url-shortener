using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace url_shortener.Models
{
    public class SpatacoliUrl
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ShortenedUrl { get; set; }
        public string Token { get; set; }
        public int Clicked { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
