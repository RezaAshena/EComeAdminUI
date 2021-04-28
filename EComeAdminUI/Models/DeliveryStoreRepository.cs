using Microsoft.AspNetCore.Mvc;
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


                var deliveryStoreList = searchResponse.Hits.Select(hit =>
                 {
                     hit.Source.Id = hit.Id;
                     return hit.Source;
                 }).ToList();

                return deliveryStoreList;
            }
            else
            {
                return null;
            }

        }

        public async Task<DeliveryStore> GetDeliveryStoreById(string id)
        {
            var getRequest = new GetRequest<DeliveryStore>(id);
            var searchResponse = await _elasticClient.GetAsync<DeliveryStore>(getRequest);

            return searchResponse.Source;
        }

        public async Task<DeliveryStore> GetDeliveryStoreByFSA(string fsa)
        {
            var searchRequest = new SearchRequest<DeliveryStore>()
            {
                Query = new MatchQuery
                {
                    Field = Infer.Field<DeliveryStore>(o => o.FSA),
                    Query = fsa
                }
            };

            var searchResponse = await _elasticClient.SearchAsync<DeliveryStore>(searchRequest);

            if (searchResponse.Documents.Count > 0)
            {
                var storeResponse = searchResponse.Documents.FirstOrDefault();
                DeliveryStore ds = new()
                {
                    Id = storeResponse.Id,
                    FSA = storeResponse.FSA,
                    StoreNumber = storeResponse.StoreNumber,
                    DeliveryVendorId = storeResponse.DeliveryVendorId,
                    DeliveryVendorName = storeResponse.DeliveryVendorName,
                    DeliveryFeePLU = storeResponse.DeliveryFeePLU,
                    DeliveryFeePromo = storeResponse.DeliveryFeePromo,
                    ClientCode = storeResponse.ClientCode
                };
                return ds;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddDeliveryStore(DeliveryStore deliveryStore)
        {
            var response = await _elasticClient.IndexDocumentAsync(deliveryStore);
            return response.IsValid;
        }


        [HttpPost]
        public async Task<bool> UpdateDeliveryStore(DeliveryStore deliveryStore)
        {
            var response = await _elasticClient.IndexDocumentAsync(deliveryStore);
            return response.IsValid;
        }


        public async Task<bool> DeleteDeliveryStore(string id)
        {
            var response = await _elasticClient.DeleteAsync<DeliveryStore>(id);
            return response.IsValid;

        }


    }
}
