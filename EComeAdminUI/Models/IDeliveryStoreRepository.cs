﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Models
{
    public interface IDeliveryStoreRepository
    {

        DeliveryStore GetDeliveryStore(string Id);

        IEnumerable<DeliveryStore> GetAllDeliveryStore();

        DeliveryStore AddDeliveryStore(DeliveryStore deliveryStore);

        Task<List<DeliveryStore>> GetAll();

        DeliveryStore DeleteDeliveryStore(string fsa);
        DeliveryStore UpdateDeliveryStore(DeliveryStore deliveryStore);

        Task<DeliveryStore> GetDeliveryStoreByFSA(string fsa);


    }
}
