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
    [Route("api/[controller]")]
    [ApiController]
    public class GetConnectedByIDController : ControllerBase
    {
        private readonly CitizenDbContext _context;

        public GetConnectedByIDController(CitizenDbContext context)
        {
            _context = context;
        }



        // GET: api/GetConnectedByID/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FizikPiri>>> GetConnectedFizikPiri(string Piradoba)
        {
            //collenction for ids
            List<int> ids = new List<int>();

            var fizikPiri = await _context.FizikPiris.Where(x=>x.Piradoba == Piradoba).FirstOrDefaultAsync();

            if (fizikPiri == null)
            {
                return NotFound();
            }

            var connectedbyid = (from Co in _context.ConnectedPersons
                                
                                
                                 where Co.FizikPiriId == fizikPiri.FizikPiriId
                                 select Co).ToList();

            List<FizikPiri> pirebi = new List<FizikPiri>();

            //adding ids to collections  
            for (int i = 0; i < connectedbyid.Count; i++)
            {

                ids.Add(connectedbyid[i].PersonTobeConnecedId);
            }

            var result = from F in _context.FizikPiris
                         select F;


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
       
