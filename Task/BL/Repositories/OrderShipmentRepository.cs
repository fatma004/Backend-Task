using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace BL.Repositories
{
    public class OrderShipmentRepository : BaseRepository<OrderShipment>
    {

        private DbContext dbContext;

        public OrderShipmentRepository(DbContext _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region CRUB

        public IEnumerable<OrderShipment> GetAllOrderShipment()
        {
            return GetAll()
                .ToList();
        }
        public IEnumerable<OrderShipment> GetAllOrderShipmentByUserId(string userId)
        {
            return GetAll().Where(os => os.UserId == userId)
                .ToList();
        }
        public bool InsertOrderShipment(OrderShipment orderShipment)
        {
            return Insert(orderShipment);
        }
        public void UpdateOrderShipment(OrderShipment orderShipment)
        {
            Update(orderShipment);
        }
        public void DeleteOrderShipment(int id)
        {
            Delete(id);
        }

        public bool CheckOrderShipmentExists(OrderShipment orderShipment)
        {
            return GetAny(l => l.Id == orderShipment.Id);
        }
        public OrderShipment GetOrderShipmentById(int id)
        {
            var orderShipment = DbSet
                .FirstOrDefault(o => o.Id == id);
            return orderShipment;
        }
        #endregion

    }
}
