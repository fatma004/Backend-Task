using System.Collections.Generic;


namespace DAL.Models
{
    public class OrderShipment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PickupShipperName { get; set; }
        public string PickupShipperPhone { get; set; }
        public string PickupShipperAddress { get; set; }
        public string PickupDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ShipmentId { get; set; }
        public bool IsShipped { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
