using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using com.sun.org.glassfish.external.statistics.annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RaffleController : ControllerBase
    {
        private readonly IRaffleService _raffleService;
        private readonly IMapper _mapper;
        public RaffleController(IRaffleService raffleService, IMapper mapper)
        {
            this._raffleService = raffleService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("RaffleReport")]
        public async Task<ActionResult<List<Raffle>>> RaffleReport()
        {
            try
            {
                var report = await _raffleService.RaffleReport();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the RaffleReport function");
            }
        }

        [HttpGet]
        [Route("RaffleWinners")]
        public async Task<User> RaffleWinners(int presentId)
        {
            try
            {
                //var _present = _mapper.Map<Present>(present);
                return await _raffleService.RaffleWinner(presentId);
              
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}