using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public class DeliveryStoreRepository : IDeliveryStoreRepository
    {
        private List<DeliveryStore> _deliveryStore;
        public DeliveryStoreRepository()
        {
            _deliveryStore = new List<DeliveryStore>()
            {
                new DeliveryStore(){Id=Guid.NewGuid(),FSA="L4T",StoreNumber="128",DeliveryFeePLU=Convert.ToInt32(9.004),ClientCode="1023",DeliveryVendorId=14,DeliveryVendorName="MM_Delivery",DeliveryFeePromo="8.004" },
                new DeliveryStore(){Id=Guid.NewGuid(),FSA="N1K",StoreNumber="121",DeliveryFeePLU=Convert.ToInt32(9.004),ClientCode="1022",DeliveryVendorId=14,DeliveryVendorName="MM_Delivery",DeliveryFeePromo="8.004" },
                new DeliveryStore(){Id=Guid.NewGuid(),FSA="V1Y",StoreNumber="124",DeliveryFeePLU=Convert.ToInt32(9.004),ClientCode="1021",DeliveryVendorId=14,DeliveryVendorName="MM_Delivery",DeliveryFeePromo="8.004" },
                new DeliveryStore(){Id=Guid.NewGuid(),FSA="LOP",StoreNumber="128",DeliveryFeePLU=Convert.ToInt32(9.004),ClientCode="1020",DeliveryVendorId=14,DeliveryVendorName="MM_Delivery",DeliveryFeePromo="8.004" },
            };
        }
        public DeliveryStore AddDeliveryStore(DeliveryStore deliveryStore)
        {
            deliveryStore.Id = new Guid();
            _deliveryStore.Add(deliveryStore);
            return deliveryStore;
        }



        public IEnumerable<DeliveryStore> GetAllDeliveryStore()
        {
            return _deliveryStore;
        }

        public DeliveryStore GetDeliveryStore(string fsa)
        {
            return _deliveryStore.FirstOrDefault(d => d.FSA == fsa);
        }

        public async Task<List<DeliveryStore>> GetAll()
        {
            throw new NotImplementedException();
            var searchRequest = new SearchRequest<DeliveryStore>();
            
        }
    }
}
