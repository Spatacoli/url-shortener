using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_shortener.Domain;
using url_shortener.Domain.Interfaces;

namespace url_shortener.Models
{
    public class Shortener
    {
        public string Token { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        private SpatacoliUrl biturl;

        // The method with which we geenrate the token
        private Shortener GenerateToken()
        {
            string urlsafe = string.Empty;
            Enumerable.Range(48, 75)
                .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
                .OrderBy(o => new Random().Next())
                .ToList()
                .ForEach(i => urlsafe += Convert.ToChar(i)); // Store each char into urlsafe
            Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
            return this;
        }

        public Shortener(string url, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var urls = _unitOfWork.SpatacoliUrls.GetAll();

            // While the token exists in our LiteDB we generate a new one
            // It basically means that if a token already exists we simply generate a new one
            while (urls.Any(u => u.Token == GenerateToken().Token)) ;
            // Store the values in the SpatacoliUrl model
            biturl = new SpatacoliUrl()
            {
                Token = Token,
                Url = url,
                ShortenedUrl = "https://spataco.li/" + Token
            };
            if (urls.Any(u => u.Url == url))
            {
                throw new Exception("URL already exists");
            }
            // Save the SpatacoliUrl model to the DB
            _unitOfWork.SpatacoliUrls.Add(biturl);
            _unitOfWork.Complete();
        }
    }
}
