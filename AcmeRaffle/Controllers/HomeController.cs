﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcmeRaffle.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaffleLogic.Models;
using AcmeRaffle.Models;
using Microsoft.EntityFrameworkCore;
using RaffleLogic.Services;
using Newtonsoft.Json;

namespace AcmeRaffle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RaffleDbContext _context;

        public HomeController(ILogger<HomeController> logger, RaffleDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([Bind("FirstName,LastName,Email,Age,SoldProduct")] RaffleEntry entry)
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44317/api/RaffleApi");
            request.Content = new StringContent(JsonConvert.SerializeObject(entry), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                await client.SendAsync(request);
            }
            return RedirectToAction("Index");
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
