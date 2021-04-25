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
                return  storeResponse.ToList();
            }
            else
            {
                return null;
            }

        }


    }
}
