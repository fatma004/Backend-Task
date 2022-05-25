using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class OrderShipmentAppService : AppServiceBase
    {
        public OrderShipmentAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<OrderShipmentModel> GetAll()
        {
            IEnumerable<OrderShipment> all = TheUnitOfWork.OrderShipment.GetAllOrderShipment();
            var res = Mapper.Map<IEnumerable<OrderShipmentModel>>(all);
            foreach(var sh in res)
            {
                sh.Orders = GetOrdersOfOrderShipment(sh.Id).ToList();
            }
            return res;
        }
        public IEnumerable<OrderModel> GetOrdersOfOrderShipment(int orderShipmentId)
        {
            var OrdresOfOrderShipment = TheUnitOfWork.Order.GetAll().Where(o => o.orderShipmentId == orderShipmentId);
            return Mapper.Map<IEnumerable<OrderModel>>(OrdresOfOrderShipment);
        }
        public OrderShipmentModel GetOrderShipmentById(int id)
        {
            OrderShipmentModel orderShipmentModel = Mapper.Map<OrderShipmentModel>(TheUnitOfWork.OrderShipment.GetOrderShipmentById(id));
            orderShipmentModel.Orders = GetOrdersOfOrderShipment(id).ToList();
            return orderShipmentModel;
        }
        public bool Save(OrderShipmentModel orderShipmentModel)
        {
            if (orderShipmentModel == null || orderShipmentModel.Orders == null)
            {
                throw new ArgumentNullException();
            }
            bool result = false;
            var order = Mapper.Map<OrderShipment>(orderShipmentModel);
            if (TheUnitOfWork.OrderShipment.Insert(order))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(OrderShipmentModel orderShipmentModel)
        {
            var order = TheUnitOfWork.OrderShipment.GetById(orderShipmentModel.Id);
            Mapper.Map(orderShipmentModel, order);
            TheUnitOfWork.OrderShipment.Update(order);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Update(int orderShipmentId,int shipmentId)
        {
            var sh = TheUnitOfWork.OrderShipment.GetById(orderShipmentId);
            sh.ShipmentId = shipmentId;
            TheUnitOfWork.OrderShipment.Update(sh);
            TheUnitOfWork.Commit();
            return true;
        }
        public IEnumerable<OrderShipmentModel> GetAllOrderShipmentByUserId(string userId)
        {
            IEnumerable<OrderShipment> all = TheUnitOfWork.OrderShipment.GetAllOrderShipmentByUserId(userId);
            var res = Mapper.Map<IEnumerable<OrderShipmentModel>>(all);
            foreach (var sh in res)
            {
                sh.Orders = GetOrdersOfOrderShipment(sh.Id).ToList();
            }
            return res;
        }
        public IEnumerable<OrderShipmentModel> GetAllOrderShipment(string userId,string PickUpDate,string DeliveryDate)
        {
            DateTime pickUpDateToSearch = DateTime.ParseExact(PickUpDate, "dd/MM/yyyy", null);
            DateTime deliveryDateToSearch = DateTime.ParseExact(DeliveryDate, "dd/MM/yyyy", null);
          
            IEnumerable<OrderShipment> allOrderShipmentByUser = TheUnitOfWork.OrderShipment.GetAllOrderShipmentByUserId(userId);

            allOrderShipmentByUser = allOrderShipmentByUser.Where(os => DateTime.ParseExact(os.PickupDate, "dd/MM/yyyy", null) == pickUpDateToSearch).ToList();
            var allOrderShipmentByUser_ = Mapper.Map<IEnumerable<OrderShipmentModel>>(allOrderShipmentByUser);
            List<OrderShipmentModel> res = new List<OrderShipmentModel>();
            foreach (var sh in allOrderShipmentByUser_)
            {
                sh.Orders = GetOrdersOfOrderShipment(sh.Id).ToList();
                bool ok = true;
                for(int idx = 0; idx < sh.Orders.Count && ok; idx++)
                {
                    DateTime orderDeliveryDate = DateTime.ParseExact(sh.Orders[idx].DeliveryDate, "dd/MM/yyyy", null);
                    if (deliveryDateToSearch != orderDeliveryDate)
                    {
                        ok = false;
                    }
                }
                if (ok)
                {
                    res.Add(sh);
                }
            }
            return res;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.OrderShipment.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public int OrderShipmentCount()
        {
            return TheUnitOfWork.OrderShipment.CountEntity();
        }
    }

}
