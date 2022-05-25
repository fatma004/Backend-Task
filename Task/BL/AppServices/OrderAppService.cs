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
    public class OrderAppService : AppServiceBase
    {
        public OrderAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<OrderModel> GetAll()
        {
            IEnumerable<Order> all = TheUnitOfWork.Order.GetAllOrders();
            return Mapper.Map<IEnumerable<OrderModel>>(all);
        }
        public IEnumerable<OrderModel> GetALLOrderByOrderShipmentId(int id)
        {
            var OrdresOfOrderShipment = TheUnitOfWork.Order.GetAll().Where(o => o.orderShipmentId == id);
            return Mapper.Map<IEnumerable<OrderModel>>(OrdresOfOrderShipment);
        }
        public bool SaveOrder(OrderModel orderModel,int orderShipmentId)
        {
            if (orderModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var order = Mapper.Map<Order>(orderModel);
            order.orderShipmentId = orderShipmentId;
            if (TheUnitOfWork.Order.Insert(order))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public void SaveListOfOrders(List<OrderModel> orders, int orderShipmentId)
        {
            if (orders == null)
            {
                throw new ArgumentNullException();
            }
            foreach(var order in orders)
            {
                if (!SaveOrder(order, orderShipmentId))
                {
                    return;
                }
                /*var order = Mapper.Map<Order>(ordr);
                order.orderShipmentId = orderShipmentId;
                TheUnitOfWork.Order.Insert(order);*/
            }
        }
        public int OrderCount()
        {
            return TheUnitOfWork.Order.CountEntity();
        }
    }

}
