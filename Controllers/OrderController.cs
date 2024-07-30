using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.DTOs;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{ 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this._orderService = orderService;
            this._mapper = mapper;
        } 

        [HttpPost]
        [Route("AddCart")]
        
        public async Task<ActionResult<int>> AddCart()
        {
            try
            {
                //var _order = _mapper.Map<Order>(order);
                //if (ModelState.IsValid)
                //{
                var user = User.Claims.FirstOrDefault(c => c.Type == "userId");
                int.TryParse(user?.Value, out int userId);

                return await _orderService.AddCart(userId);
                    //return CreatedAtAction("AddCart", new { _order.Id }, order);
                //}
                //return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the AddCart function");
            }
        }

        // POST api/<OrderController>
        [HttpGet]
        [Route("GetCart")]
        public async Task<ActionResult<Order>> GetCart(int userId)
        {
            try
            {
                 return Ok(await _orderService.GetCart(userId));
            }
                //return new JsonResult("Something went wrong") { StatusCode = 500 };
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetCart function");
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete]
        [Route("DeleteCart")]
        public async Task<ActionResult<int>> DeleteCart(int id)
        {
            return await _orderService.DeleteCart(id);
        }

        [HttpGet]
        [Route("GetPurchacersDetails")]
    
        public async Task<ActionResult<List<User>>> GetPurchacersDetails()
        {
            try
            {
                 
                //var _res = _mapper.Map<UserRegisterDTO>(res);
                return Ok(await _orderService.GetPurchacersDetails());
            }
            //return new JsonResult("Something went wrong") { StatusCode = 500 };
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the GetPurchacersDetails function");
            }
        }

        [HttpGet]
        [Route("Pay")]
        public async Task<ActionResult<int>> Pay(int orderId)
        {
            try
            {
                //var _order = _mapper.Map<Order>(order);
                //if (ModelState.IsValid)
                //{
                var res =await _orderService.Pay(orderId);
                return Ok(res);
                //}
                //return new JsonResult("Something went wrong") { StatusCode = 500 };
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + "This error occured from the Pay function");
            }

        }

        [Route("GetTotalPrice")]
        [HttpGet]
        async public Task<int> GetTotalPrice(int userId)
        {
            var selectedPresents = await _orderService.GetCart(userId);
            int count = 0;
            return selectedPresents.Sum;
            //foreach (var p in selectedPresents)
            //{
            //    count += p.Cost;
            //}
            //return count;
        }


        [HttpGet]
        [Route("GetSumOfCarts")]
        [Authorize(Roles = "admin")]
        public async Task<int> GetSumOfCarts()
        {
            return await _orderService.GetSumOfCarts();
        }
    }

}
