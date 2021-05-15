using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.Helpers
{
    public sealed class EnumCaster
    {
        public static IOrderStatusCaster OrderStatus => new OrderStatusCaster();
        public static IProductStatusCaster ProductStatus => new ProductStatusCaster();
        public static IDiscountTypeCaster DiscountType => new DiscountTypeCaster();
        public static IUserTypeCaster UserType => new UserTypeCaster();

        class OrderStatusCaster : IOrderStatusCaster
        {
            public OrderStatus ToEnum(string status)
            {
                switch (status)
                {
                    case "Preparing":
                        return DatabaseModels.OrderStatus.Preparing;
                    case "Serving":
                        return DatabaseModels.OrderStatus.Serving;
                    case "Finished":
                        return DatabaseModels.OrderStatus.Finished;
                    case "Canceled":
                        return DatabaseModels.OrderStatus.Canceled;
                    default:
                        throw new ArgumentException("Status should be one of Preparing, Serving, Finished, Canceled");
                }
            }

            public string ToStr(OrderStatus status)
            {
                switch (status)
                {
                    case DatabaseModels.OrderStatus.Preparing:
                        return "Preparing";
                    case DatabaseModels.OrderStatus.Serving:
                        return "Serving";
                    case DatabaseModels.OrderStatus.Finished:
                        return "Finished";
                    case DatabaseModels.OrderStatus.Canceled:
                        return "Canceled";
                    default:
                        throw new Exception();
                }
            }
        }

        class UserTypeCaster : IUserTypeCaster
        {
            public UserType ToEnum(string type)
            {
                switch (type)
                {
                    case "Admin":
                        return DatabaseModels.UserType.Admin;
                    case "Manager":
                        return DatabaseModels.UserType.Manager;
                    default:
                        throw new ArgumentException("Type should be Admin or Manager");
                }
            }

            public string ToStr(UserType type)
            {
                switch (type)
                {
                    case DatabaseModels.UserType.Admin:
                        return "Admin";
                    case DatabaseModels.UserType.Manager:
                        return "Manager";
                    default:
                        throw new Exception();
                }
            }
        }

        class ProductStatusCaster : IProductStatusCaster
        {
            public ProductStatus ToEnum(string status)
            {
                switch (status)
                {
                    case "Available":
                        return DatabaseModels.ProductStatus.Available;
                    case "Withdrawn":
                        return DatabaseModels.ProductStatus.Withdrawn;
                    case "Paused":
                        return DatabaseModels.ProductStatus.Paused;
                    default:
                        throw new ArgumentException("Status should be one of Available, Withdrawn, Paused");
                }
            }

            public string ToStr(ProductStatus status)
            {
                switch (status)
                {
                    case DatabaseModels.ProductStatus.Available:
                        return "Available";
                    case DatabaseModels.ProductStatus.Withdrawn:
                        return "Withdrawn";
                    case DatabaseModels.ProductStatus.Paused:
                        return "Paused";
                    default:
                        throw new Exception();
                }
            }
        }

        class DiscountTypeCaster : IDiscountTypeCaster
        {
            public DiscountType ToEnum(string type)
            {
                switch (type)
                {
                    case "Items set":
                        return DatabaseModels.DiscountType.ItemsSet;
                    case "Price drop":
                        return DatabaseModels.DiscountType.PriceDrop;
                    case "Percentage price drop":
                        return DatabaseModels.DiscountType.PercentagePriceDrop;
                    default:
                        throw new ArgumentException("Type should be one of Items set, Price drop, Percentage price drop");
                }
            }

            public string ToStr(DiscountType type)
            {
                switch (type)
                {
                    case DatabaseModels.DiscountType.ItemsSet:
                        return "Items set";
                    case DatabaseModels.DiscountType.PriceDrop:
                        return "Price drop";
                    case DatabaseModels.DiscountType.PercentagePriceDrop:
                        return "Percentage price drop";
                    default:
                        throw new Exception();
                }
            }
        }
    }
}