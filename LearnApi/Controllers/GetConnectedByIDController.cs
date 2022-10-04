using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnApi.Models;

namespace LearnApi.Controllers
{
    [Route("v1/GetConnectedByID")]
    [ApiController]
    public class GetConnectedByIDController : ControllerBase
    {
        private readonly CitizenDbContext _context;

        public GetConnectedByIDController(CitizenDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FizikPiri>>> GetConnectedFizikPiri(string Piradoba)
        {
            List<int> ids = new();

            var fizikPiri = await _context.FizikPiris.Where(x=>x.Piradoba == Piradoba).FirstOrDefaultAsync();

            if (fizikPiri == null)
            {
                return NotFound();
            }

            var connectedbyid = (from Co in _context.ConnectedPersons                              
                                 where Co.FizikPiriId == fizikPiri.FizikPiriId
                                 select Co).ToList();

            List<FizikPiri> pirebi = new();

            //adding ids to collections  
            for (int i = 0; i < connectedbyid.Count; i++)
            {
                ids.Add(connectedbyid[i].PersonTobeConnecedId);
            }

            var result = from list in _context.FizikPiris
                         select list;

            foreach (var item in ids)
            {
                foreach (var it in _context.FizikPiris.Where(x=>x.FizikPiriId==item))
                {
                    pirebi.Add(it);
                }
            }

            return pirebi;
        }
    }
}
       
