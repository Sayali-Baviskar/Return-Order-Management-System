using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wwe.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ComponentProcessing")]
        public int RequestId { get; set; }
        public ComponentProcessing ComponentProcessing { get; set; }
        public string Name { get; set; }
        public double ProcessingCharge { get; set; }
        public double PackagingAndDeliveryCharge { get; set; }
        public string CreditCardNumber { get; set; }
        public double TotalCharge { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
