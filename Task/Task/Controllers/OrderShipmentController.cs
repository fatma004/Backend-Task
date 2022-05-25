using BL.AppServices;
using BL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Task.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderShipmentController : ControllerBase
    {
        private readonly OrderAppService _orderAppService;
        private readonly OrderShipmentAppService _orderShipmentAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderShipmentController(OrderShipmentAppService orderShipmentAppService, IHttpContextAccessor httpContextAccessor, OrderAppService orderAppService)
        {
            this._orderShipmentAppService = orderShipmentAppService;
            this._httpContextAccessor = httpContextAccessor;
            this._orderAppService = orderAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderShipmentAppService.GetAll());
        }

        [HttpGet("OrderShipmentByUserId/{UserId}")]
        public IActionResult GetAllOrderShipmentByUserId(string UserId)
        {
            return Ok(_orderShipmentAppService.GetAllOrderShipmentByUserId(UserId));
        }

        [HttpGet("GetAllOrderShipmentByDates/{UserId}")]
        public IActionResult GetAllOrderShipment(string UserId,string PickUpDate,string DeliveryDate)
        {
            return Ok(_orderShipmentAppService.GetAllOrderShipment(UserId,PickUpDate, DeliveryDate));
        }

        [HttpPost]
        public IActionResult Create(OrderShipmentModel orderShipmentModel)
        {
            var userID = "560139d4-50e5-4595-a7de-e5aca18b54d4";//_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "uid").Value;
            orderShipmentModel.UserId = userID;

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _orderShipmentAppService.Save(orderShipmentModel);
                return Created("Created", orderShipmentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_orderShipmentAppService.GetOrderShipmentById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderShipmentAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
