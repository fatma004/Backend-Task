using AutoMapper;
using BL.Dto;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<Order, OrderModel>().ReverseMap();
            this.CreateMap<Shipment,ShipmentModel>().ReverseMap();
            this.CreateMap<OrderShipment, OrderShipmentModel>().ReverseMap();


        }
    }
}