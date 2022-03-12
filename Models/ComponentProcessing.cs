using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wwe.Models
{
    public class ComponentProcessing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string ComponentType { get; set; }
        public string ComponentName { get; set; }
        public int Quantity { get; set; }
        public bool IsPriorityRequest { get; set; }
        public DateTime OrderPlacedDate { get; set; }
    }
}
