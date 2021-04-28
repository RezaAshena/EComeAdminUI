using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public class DeliveryStore
    {
      
        public string Id { get; set; }

        [Required]
        public string FSA { get; set; } 
        [DisplayName("Store Number")]

        [Required]
        public string StoreNumber { get; set; }
        [DisplayName("Delivery Vendor Id")]
        [Required]
        public int DeliveryVendorId { get; set; }
        [DisplayName("Delivery Vendor Name")]
        [Required]
        public string DeliveryVendorName { get; set; }
        [DisplayName("Delivery Fee PLU")]
        [Required]
        public int DeliveryFeePLU { get; set; }
        [DisplayName("Delivery Fee Promo")]
        [Required]
        public string DeliveryFeePromo { get; set; }
        [DisplayName("Client Code")]
        [Required]
        public string ClientCode { get; set; }
    }
}
