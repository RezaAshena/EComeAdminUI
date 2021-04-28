using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public interface IDeliveryStoreRepository
    {

        DeliveryStore GetDeliveryStore(string Id);

        IEnumerable<DeliveryStore> GetAllDeliveryStore();

        Task<bool> AddDeliveryStore(DeliveryStore deliveryStore);

        Task<List<DeliveryStore>> GetAll();

        Task<DeliveryStore> GetDeliveryStoreById(string id);


        Task<bool> UpdateDeliveryStore(DeliveryStore deliveryStore);

        Task<DeliveryStore> GetDeliveryStoreByFSA(string fsa);

        Task<bool> DeleteDeliveryStore(string id);


    }
}
