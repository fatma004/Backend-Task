using System.Collections.Generic;


namespace BL.Dto
{
    public class ShipmentModel
    {
        public int Id { set; get; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<int> OrderShipmentsIds { get; set; }
    }
}
