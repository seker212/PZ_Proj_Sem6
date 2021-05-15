using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.Helpers
{
    public interface IOrderStatusCaster
    {
        OrderStatus ToEnum(string status);
        string ToStr(OrderStatus status);
        string ToQuery(OrderStatus status);
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
        string ToQuery(ProductStatus status);
    }

    public interface IDiscountTypeCaster
    {
        DiscountType ToEnum(string type);
        string ToStr(DiscountType type);
        string ToQuery(DiscountType status);
    }
}