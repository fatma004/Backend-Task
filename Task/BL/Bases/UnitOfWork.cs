using BL.Interfaces;
using BL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext DbContext { get; set; }
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        #endregion


        #region Constructors
        public UnitOfWork(ApplicationDbContext DbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.DbContext = DbContext;
        }
        #endregion
        public OrderRepository order;
        public OrderRepository Order
        {
            get
            {
                if (order == null)
                    order = new OrderRepository(DbContext);
                return order;
            }
        }
        public OrderShipmentRepository orderShipment;
        public OrderShipmentRepository OrderShipment
        {
            get
            {
                if (orderShipment == null)
                    orderShipment = new OrderShipmentRepository(DbContext);
                return orderShipment;
            }
        }
        public ShipmentRepository shipment;
        public ShipmentRepository Shipment
        {
            get
            {
                if (shipment == null)
                    shipment = new ShipmentRepository(DbContext);
                return shipment;
            }
        }
        #region Methods
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
        #endregion
    }
}