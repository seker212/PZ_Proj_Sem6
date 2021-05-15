using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetKitchenOrders();
        IEnumerable<Order> GetServiceOrders();
        bool UpdateStatus(Guid id, OrderStatus newStatus);
    }
}