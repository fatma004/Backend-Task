using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Commodities { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryShipperName { get; set; }
        public string DeliveryShipperPhone { get; set; }
        public string DeliveryDate { get; set; }
        public int orderShipmentId { get; set; }

        [JsonIgnore, ForeignKey("orderShipmentId")]
        public OrderShipment orderShipment { get; set; }

    }
}
