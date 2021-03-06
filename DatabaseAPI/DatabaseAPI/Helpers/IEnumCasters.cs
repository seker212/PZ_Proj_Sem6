﻿using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.Helpers
{
    public interface IOrderStatusCaster
    {
        OrderStatus ToEnum(string status);
        string ToStr(OrderStatus status);
    }

    public interface IUserTypeCaster
    {
        UserType ToEnum(string type);
        string ToStr(UserType type);
    }
    public interface IProductStatusCaster
    {
        ProductStatus ToEnum(string status);
        string ToStr(ProductStatus status);
    }
}