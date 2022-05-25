using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace BL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {

        private DbContext dbContext;

        public OrderRepository(DbContext _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region CRUB

        public IEnumerable<Order> GetAllOrders()
        {
            return GetAll()
                .ToList();
        }

        public bool InsertOrder(Order order)
        {
            return Insert(order);
        }
        public void UpdateOrder(Order order)
        {
            Update(order);
        }
        public void DeleteOrder(int id)
        {
            Delete(id);
        }

        public bool CheckOrderExists(Order order)
        {
            return GetAny(l => l.Id == order.Id);
        }
        public Order GetOrderById(int id)
        {
            var order = DbSet
                .FirstOrDefault(o => o.Id == id);
            return order;
        }
        #endregion
    }
}
