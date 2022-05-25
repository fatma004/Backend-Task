using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace BL.Repositories
{
    public class ShipmentRepository : BaseRepository<Shipment>
    {

        private DbContext dbContext;

        public ShipmentRepository(DbContext _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext;
        }
        #region CRUB

        public IEnumerable<Shipment> GetAllShipments()
        {
            return GetAll()
                .ToList();
        }

        public bool InsertShipment(Shipment shipment)
        {
            return Insert(shipment);
        }
        public void UpdateShipment(Shipment shipment)
        {
            Update(shipment);
        }
        public void DeleteShipment(int id)
        {
            Delete(id);
        }

        public bool CheckShipmentExists(Shipment shipment)
        {
            return GetAny(s => s.Id == shipment.Id);
        }
        public Shipment GetShipmentById(int id)
        {
            var shipment = DbSet
                .FirstOrDefault(s => s.Id == id);
            return shipment;
        }
        #endregion
    }
}
