﻿using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAvailableProducts();
        IEnumerable<(Guid id, string name, int quantity)> GetDiscountProducts(Discount discount);
        IEnumerable<(Guid id, string name, int quantity)> GetOrderProducts(Order order);
        public double GetProductPrice(Guid id);
        public IEnumerable<double> GetOrderProductPrices(Order order);
    }
}