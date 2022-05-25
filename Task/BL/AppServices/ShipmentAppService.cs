using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.AppServices
{
    public class ShipmentAppService : AppServiceBase
    {
        public ShipmentAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<ShipmentModel> GetAll()
        {
            IEnumerable<Shipment> all = TheUnitOfWork.Shipment.GetAllShipments();
            var res = Mapper.Map<IEnumerable<ShipmentModel>>(all);
            foreach(var sh in res)
            {
                sh.OrderShipmentsIds = GetALLOrderShipmentOfShipment(sh.Id).ToList();
            }
            return res;
        }
        public IEnumerable<int> GetALLOrderShipmentOfShipment(int id)
        {
            var res = TheUnitOfWork.OrderShipment.GetAll().Where(os => os.ShipmentId == id).Select(os=>os.Id);
            return res;
        }
        public bool SaveShipment(ShipmentModel shipmentModel)
        {
            if (shipmentModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var shipment = Mapper.Map<Shipment>(shipmentModel);
            if (TheUnitOfWork.Shipment.Insert(shipment))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public int ShipmentCount()
        {
            return TheUnitOfWork.Shipment.CountEntity();
        }
    }

}


