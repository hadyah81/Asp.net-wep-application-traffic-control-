using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string Customername { get; set; }
        public String Email { get; set; }

        public string Mobile { get; set; }

        public double TotalAmount { get; set; }
        [NotMapped]
        public String TransactionId { get; set; }
        [NotMapped]
        public string OrderId { get; set; }

    }
}
