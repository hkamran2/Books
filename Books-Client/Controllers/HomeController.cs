using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Books_Client.Http;
using Microsoft.AspNetCore.Mvc;
using Books_Client.Models;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Models.DTO;

namespace Books_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IHttpAction<BookDTO> _action;
        public HomeController(IHttpClientFactory client)
        {
            _client = client;
            _action = new HttpAction<BookDTO>(client);

        }
        public async Task<IActionResult> Index()
        {
            return View(await _action.GetCollection("books"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
