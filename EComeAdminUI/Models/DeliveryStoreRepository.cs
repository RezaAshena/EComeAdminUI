using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public class DeliveryStoreRepository : IDeliveryStoreRepository
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly IElasticClient _elasticClient;
        private List<DeliveryStore> _deliveryStore;
        public DeliveryStoreRepository(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _elasticClient = _databaseClient.GetDeliveryStoreIndexElasticClient();
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
            DeliveryStore deliveryStore = null;

              var searchRequest = new SearchRequest<DeliveryStore>()
            {
                From = 0,
                Size = 1000,
                Scroll = "5m",
                Query = new QueryContainer(new MatchAllQuery()),
            };

            var searchResponse = await _elasticClient.SearchAsync<DeliveryStore>(searchRequest);
            if (searchResponse.Documents.Count > 0)
            {
                var storeResponse = searchResponse.Documents;
                List<DeliveryStore> dsList = new List<DeliveryStore>();
                foreach(var item in storeResponse)
                {
                    deliveryStore = new DeliveryStore
                    {
                        Id= item.Id,
                        FSA=item.FSA,
                        StoreNumber=item.StoreNumber,
                        DeliveryVendorId = item.DeliveryVendorId,
                        DeliveryVendorName = item.DeliveryVendorName,
                        DeliveryFeePLU = item.DeliveryFeePLU,
                        DeliveryFeePromo = item.DeliveryFeePromo,
                        ClientCode = item.ClientCode
                    };
                    dsList.Add(deliveryStore);
                }

                //return  storeResponse.ToList();
                return dsList;
            }
            else
            {
                return null;
            }

        }


    }
}
