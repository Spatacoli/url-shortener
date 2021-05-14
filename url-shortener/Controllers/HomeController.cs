﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using url_shortener.Models;
using url_shortener.Domain;
using url_shortener.Domain.Interfaces;

namespace url_shortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("/")]
        public IActionResult PostUrl([FromBody] string url)
        {
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }

            var spatacoliUrl = _unitOfWork.SpatacoliUrls.GetByUrl(url);

            if (spatacoliUrl != null)
            {
                Response.StatusCode = 405;
                return Json(new UrlResponse()
                {
                    url = url,
                    status = "Already shortened",
                    token = spatacoliUrl.Token
                });
            }

            Shortener shortener = new Shortener(url, _unitOfWork);
            return Json(new UrlResponse()
            {
                url = "https://spataco.li/" + shortener.Token,
                status = "Ok",
                token = shortener.Token
            });
        }

        [HttpGet, Route("/OU812")]
        public IActionResult SpatacoliAdmin()
        {
            var urls = _unitOfWork.SpatacoliUrls.GetAll();
            ViewBag.Urls = urls;
            return View();
        }

        [HttpGet, Route("/{token}")]
        public IActionResult SpatacoliRedirect([FromRoute] string token)
        {
            var url = _unitOfWork.SpatacoliUrls.GetByToken(token);
            url.Clicked++;
            _unitOfWork.Complete();
            return Redirect(url.Url);
        }
    }
}
