using DatabaseAPI.ApiModels;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IOrderServices
    {
        Task<IEnumerable<OrderProducts>> GetKitchenOrders();
        Task<IEnumerable<OrderProducts>> GetServiceOrders();
        Task<bool> UpdateStatus(Guid key, OrderStatus newStatus);
    }
}