using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using com.sun.org.glassfish.external.statistics.annotations;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentsOrderController : ControllerBase
    {
        private readonly IPresentsOrderService _presentsOrderService;
        private readonly IMapper _mapper;
        public PresentsOrderController(IPresentsOrderService presentsOrderService, IMapper mapper)
        {
            this._presentsOrderService = presentsOrderService;
            this._mapper = mapper;
        }

        // GET: api/<PresentsOrderController>
        [HttpGet]
        [Route("GetPresentsOrder")]
        public async Task<ActionResult<List<PresentsOrderDTO>>> GetPresentsOrder()
        {
            try
            {
                var allPo = await _presentsOrderService.GetPresentsOrder();
                var _AllPo = _mapper.Map<List<PresentsOrder>>(allPo);
                return Ok(_AllPo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetPresentsOrder function");
            }
        }

        // POST api/<PresentsOrderController>
        [HttpPost]
        [Route("AddPresentToCart")]
        public async Task<ActionResult<int>> AddPresentToCart([FromBody] PresentsOrderDTO present)
        {
            try
            {
                var _present = _mapper.Map<PresentsOrder>(present);
                return await _presentsOrderService.AddPresentToCart(_present);
                //PresentsOrderDTO presentsOrder = new PresentsOrderDTO();
                //presentsOrder.OrderId = orderId;
                //presentsOrder.PresentId = p.Id;
                //var _presentsOrder = _mapper.Map<PresentsOrder>(presentsOrder);
                //if (ModelState.IsValid)
                //{
                   // await _presentsOrderService.AddPresentToCart(_presentsOrder);
                   // return CreatedAtAction("AddPresentToCart", new { _presentsOrder.Id }, presentsOrder);
                //}
               // return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch
            {
                return NoContent();
            }
        }

        // DELETE api/<PresentsOrderController>/5
        [HttpDelete]
        [Route("DeletePresentFromCart")]
        public async Task<ActionResult<int>> DeletePresentFromCart(int opId)
        {
            try
            {
                var res = await _presentsOrderService.DeletePresentFromCart(opId);
                if (res !=-1)
                    return Ok(res);
                else
                    return new JsonResult("You can delete present after its paid") { StatusCode = 500 };

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetCartsByUserId")]
        public async Task<ActionResult<List<PresentsOrder>>> GetCartsByUserId(int userId)
        {
            try
            {
                var res = await _presentsOrderService.GetCartsByUserId(userId);
                if (res != null)
                    return Ok(res);
                else
                    return new JsonResult("You can delete present after its paid") { StatusCode = 500 };

            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet]
            [Route("GetThePurchasesForEachPresent")]
            public async Task<ActionResult<List<PresentsOrderDTO>>> GetThePurchasesForEachPresent(int presentId)
            {
                try
                {
                    var PoForPresent = await _presentsOrderService.GetThePurchasesForEachPresent(presentId);
                    var _PoForPresent = _mapper.Map<List<PresentsOrder>>(PoForPresent);
                    return Ok(_PoForPresent);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message + "This error occured from the GetThePurchasesForEachPresent function");
                }
            }

            [HttpGet]
            [Route("SortByTheMostPurchasedPresent")]
            public async Task<ActionResult<List<PresentDTO>>> SortByTheMostPurchasedPresent()
            {
                try
                {
                    var res = await _presentsOrderService.SortByTheMostPurchasedPresent();
                    var _res = _mapper.Map<List<PresentDTO>>(res);
                    return Ok(_res);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message + "This error occured from the SortByTheMostPurchasedPresent function");
                }
            }

            [HttpGet]
            [Route("SortByTheMostExpensivePresent")]
            public async Task<ActionResult<List<PresentDTO>>> SortByTheMostExpensivePresent()
            {
                try
                {
                    var res = await _presentsOrderService.SortByTheMostExpensivePresent();
                    var _res = _mapper.Map<List<PresentDTO>>(res);
                    return Ok(_res);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message + "This error occured from the SortByTheMostExpensivePresent function");
                }
            }
    }
    }

