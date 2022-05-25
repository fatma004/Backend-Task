using System.Collections.Generic;


namespace BL.Dto
{
    public class OrderShipmentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PickupShipperName { get; set; }
        public string PickupShipperPhone { get; set; }
        public string PickupShipperAddress { get; set; }
        public string PickupDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsShipped { get; set; }
        public virtual List<OrderModel> Orders { get; set; }
    }
}
