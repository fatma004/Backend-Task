using BL.AppServices;
using BL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Task.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly OrderShipmentAppService _orderShipmentAppService;
        private readonly ShipmentAppService _shipmentAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShipmentController(OrderShipmentAppService orderShipmentAppService, IHttpContextAccessor httpContextAccessor, ShipmentAppService shipmentAppService)
        {
            this._orderShipmentAppService = orderShipmentAppService;
            this._shipmentAppService = shipmentAppService;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_shipmentAppService.GetAll());
        }
        [HttpPost]
        public IActionResult Create(ShipmentModel shipmentModel)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
               if(_shipmentAppService.SaveShipment(shipmentModel))
                {
                    int shipmentId = _shipmentAppService.GetAll().ToList().Last().Id;
                    foreach(var id in shipmentModel.OrderShipmentsIds)
                    {
                        _orderShipmentAppService.Update(id, shipmentId);
                    }
                }
                return Created("Created", shipmentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}
