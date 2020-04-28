using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeRaffle.Data;
using RaffleLogic.Models;
using RaffleLogic.Services;

namespace AcmeRaffle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleApiController : ControllerBase
    {
        private readonly RaffleDbContext _context;
        private readonly EntryValidator _validator;

        public RaffleApiController(RaffleDbContext context)
        {
            _context = context;
            _validator = new EntryValidator();
        }

        //// GET: api/RaffleApi
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RaffleEntry>>> GetEntries()
        //{
        //    return await _context.Entries.ToListAsync();
        //}


        // POST: api/RaffleApi
        [HttpPost]
        public async Task<ActionResult<RaffleEntry>> PostRaffleEntry([Bind("FirstName,LastName,Email,Age,SoldProduct")] RaffleEntry entry)
        {
            
            bool validEntry = _validator.ValidateEntry(_context.SoldProducts.AsQueryable(),
                             _context.Entries.AsQueryable(), entry);
            if (validEntry)
            {
                _context.Entries.Add(entry);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.OK);
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

    }
}
