using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.DTOs;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentController : ControllerBase
    {
        private readonly IPresentService _presentService;
        private readonly IMapper _mapper;
        private readonly ILogger<PresentDTO> _logger;

        public PresentController(IPresentService presentService, ILogger<PresentDTO> logger, IMapper mapper)
        {
            this._presentService = presentService;
            this._mapper = mapper;
        }
        // GET: api/<PresentController>
        [HttpGet]
        [Route("GetPresentsList")]
        public async Task<ActionResult<List<Present>>> GetPresentsList()
        {
            return await _presentService.GetPresentsList();
        }

        // POST api/<PresentController>
        [HttpPost]
        [Route("AddPresent")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> AddPresent([FromBody] PresentDTO p)
        {
            //try
            //{
            //var p<Pre_present = _mapper.Masent>(p);
            var _present = _mapper.Map<Present>(p);
            //if (ModelState.IsValid)
            //{
            await _presentService.AddPresent(_present);
            return Ok(_present.Id);
            //}
            //return new JsonResult("Something went wrong") { StatusCode = 500 };
            //}
            //catch (Exception ex)
            //{
            //    //_logger.LogError($"error while login: {ex}");
            //    return -1;
            //}
        }

        // PUT api/<PresentController>/5
        [HttpPut]
        [Route("EditPresent")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> EditPresent([FromBody] PresentDTO p)
        {
            try
            {
                var _present = _mapper.Map<Present>(p);
                if (ModelState.IsValid)
                {
                    return Ok(await _presentService.EditPresent(_present));
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE api/<PresentController>/5
        [HttpDelete]
        [Route("DeletePresent")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<int>> DeletePresent(int id)
        {
            try
            {
                return await _presentService.DeletePresent(id);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetDonor")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Donor>> GetDonor(int presentId)
        {
            try
            {
                //var _present = _mapper.Map<Present>(present);
                //if (ModelState.IsValid)
                //{
                Donor d = await _presentService.GetDonor(presentId);
                DonorDTO _d = _mapper.Map<DonorDTO>(d);


                return Ok(_d);
                //}
                //return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("FilterPresentsByNameAndDonor")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Present>>> FilterPresentsByNameAndDonor(string? name, string? donorName)
        {
            List<Present> presents = await _presentService.FilterPresentsByNameAndDonor(name, donorName);
            try
            {
                if (presents != null)
                {
                    return Ok(presents);
                }
                else
                    return NoContent();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("FilterPresentsByNumOfPurchasers")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Present>>> FilterPresentsByNumOfPurchasers(int? numOfPurchasers)
        {
            List<Present> presents = await _presentService.FilterPresentsByNumOfPurchasers(numOfPurchasers);
            try
            {
                if (presents != null)
                {
                    return Ok(presents);
                }
                else
                    return NoContent();
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("SortByCatgoreAndPrice")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Present>>> SortByCatgoreAndPrice([FromQuery] Category?[] category, int? minPrice, int? maxPrice)
        {
            try
            {
                var presents = await _presentService.SortByCatgoreAndPrice(category, minPrice, maxPrice);
                if (presents != null)
                    return Ok(presents);
                else
                    return NoContent();
            }
            catch
            {
                return NoContent();
            }
        }

    }
}
