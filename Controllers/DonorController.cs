using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DonorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDonorService _donorService;
        //private readonly ILogger<Donor> _logger;

        public DonorController(IDonorService donorService, IMapper mapper)//, ILogger<Donor> logger)
        {
            this._donorService = donorService;
            this._mapper = mapper;
            //this._logger = logger;
        }
        // GET: api/<DonorController>
        [HttpGet]
        [Route("GetDonors")]
        public async Task<ActionResult<List<DonorDTO>>> GetDonors()
        {
            try
            {                
                var donors = await _donorService.GetDonorsList();
                var _donors = _mapper.Map<List<DonorDTO>>(donors);
                return Ok(_donors);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetDonors function");
            }
        }

        // POST api/<DonorController>
        [HttpPost]
        [Route("AddDonor")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<int>> AddDonor([FromBody] DonorDTO d)
        {
            try
            {
                User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;

                var _donor = _mapper.Map<Donor>(d);
                if (ModelState.IsValid)
                {
                    await _donorService.AddDonor(_donor);
                    return CreatedAtAction("AddDonor", new { _donor.Id }, d);
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch
            {
                return NoContent();
            }

            //return new JsonResult("Something went wrong") { StatusCode = 500 };
            //return await _donorService.AddDonor(d);
        }

        // PUT api/<DonorController>/5
        [HttpPut]
        [Route("EditDonor")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> EditDonor([FromBody] DonorDTO d)
        {
            try
            {
                var _donor = _mapper.Map<Donor>(d);
                if (ModelState.IsValid)
                {
                    return Ok(await _donorService.EditDonor(_donor));
                }
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE api/<DonorController>/5
        [HttpDelete]
        [Route("DeleteDonor")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<bool>> DeleteDonor(int id)
        {
            try
            {
                return await _donorService.DeleteDonor(id);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("GetDonationList")]
        public async Task<ActionResult<List<Present>>> GetDonationList(int id)
        {
            try
            {
                return Ok(await _donorService.GetDonationList(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetDonationList function");
            }
        } 
        [HttpGet]
        [Route("FilterDonors")]

        //צריך להוסיף maper בסיעתא דשמיא זה יעבוד
        public async Task<ActionResult<List<Donor>>> FilterDonors(string? name, string? email, string? present)
        {
            List<Donor> donors = await _donorService.FilterDonors(name, email, present);
            List<DonorDTO> _donors = _mapper.Map<List<DonorDTO>>(donors);
            try
            {
              
                    if (donors != null)
                {
                    return Ok(_donors);
                }
                return NoContent();
            }
            catch
            {
                return NotFound();
            }



            //try
            //{
            //    var _donor = _mapper.Map<Donor>(d);
            //    if (ModelState.IsValid)
            //    {
            //        return Ok(await _donorService.EditDonor(_donor));
            //    }
            //    return new JsonResult("Something went wrong") { StatusCode = 500 };
            //}
            //catch
            //{
            //    return NotFound();
            //}
            //[HttpGet]
            //[Route("FilterByName")]
            //public async Task<ActionResult<List<Donor>>> FilterByName(string name)
            //{
            //   List<Donor> donors =  await _donorService.FilterByName(name);
            //    if (donors != null)
            //    {
            //        return Ok(donors);
            //    }
            //    else
            //        return NotFound();

            //}


            //[HttpGet]
            //[Route("FilterByEmail")]
            //public async Task<ActionResult<List<Donor>>> FilterByEmail(string email)
            //{
            //    List<Donor> donors = await _donorService.FilterByEmail(email);
            //    if (donors != null)
            //    {
            //        return Ok(donors);
            //    }
            //    else
            //        return NotFound();

            //}


            //[HttpGet]
            //[Route("FilterByPresent")]
            //public async Task<ActionResult<List<Donor>>> FilterByPresent(string present)
            //{
            //    List<Donor> donors = await _donorService.FilterByPresent(present);
            //    if (donors != null)
            //    {
            //        return Ok(donors);
            //    }
            //    else
            //        return NotFound();

            //}

        }
    }
}

