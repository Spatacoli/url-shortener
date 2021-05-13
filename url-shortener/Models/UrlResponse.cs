using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace url_shortener.Models
{
    public class UrlResponse
    {
        public string url { get; set; }
        public string status { get; set; }
        public string token { get; set; }
    }
}
