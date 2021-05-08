using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.Helpers
{
    public class EnumCaster
    {
        public static OrderStatus OrderStatusFromString(string status)
        {
            switch (status)
            {
                case "Preparing":
                    return OrderStatus.Preparing;
                case "Serving":
                    return OrderStatus.Serving;
                case "Finished":
                    return OrderStatus.Finished;
                case "Canceled":
                    return OrderStatus.Canceled;
                default:
                    throw new ArgumentException("Status should be one of Preparing, Serving, Finished, Canceled");
            }
        }

        public static string OrderStatusToString(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Preparing:
                    return "Preparing";
                case OrderStatus.Serving:
                    return "Serving";
                case OrderStatus.Finished:
                    return "Finished";
                case OrderStatus.Canceled:
                    return "Canceled";
                default:
                    throw new Exception();
            }
        }

        public static UserType UserTypeFromString(string type)
        {
            switch (type)
            {
                case "Admin":
                    return UserType.Admin;
                case "Manager":
                    return UserType.Manager;
                default:
                    throw new ArgumentException("Type should be Admin or Manager");
            }
        }

        public static string UserTypeToString(UserType type)
        {
            switch (type)
            {
                case UserType.Admin:
                    return "Admin";
                case UserType.Manager:
                    return "Manager";
                default:
                    throw new Exception();
            }
        }

        public static ProductStatus ProductStatusFromString(string status)
        {
            switch (status)
            {
                case "Available":
                    return ProductStatus.Available;
                case "Withdrawn":
                    return ProductStatus.Withdrawn;
                case "Paused":
                    return ProductStatus.Paused;
                default:
                    throw new ArgumentException("Status should be one of Available, Withdrawn, Paused");
            }
        }

        public static string ProductStatusToString(ProductStatus status)
        {
            switch (status)
            {
                case ProductStatus.Available:
                    return "Available";
                case ProductStatus.Withdrawn:
                    return "Withdrawn";
                case ProductStatus.Paused:
                    return "Paused";
                default:
                    throw new Exception();
            }
        }

        public static DiscountType DiscountTypeFromString(string type)
        {
            switch (type)
            {
                case "ItemsSet":
                    return DiscountType.ItemsSet;
                case "PriceDrop":
                    return DiscountType.PriceDrop;
                case "PercentagePriceDrop":
                    return DiscountType.PercentagePriceDrop;
                default:
                    throw new ArgumentException("Type should be one of ItemsSet, PriceDrop, PercentagePriceDrop");
            }
        }

        public static string DiscountTypeToString(DiscountType type)
        {
            switch (type)
            {
                case DiscountType.ItemsSet:
                    return "ItemsSet";
                case DiscountType.PriceDrop:
                    return "PriceDrop";
                case DiscountType.PercentagePriceDrop:
                    return "PercentagePriceDrop";
                default:
                    throw new Exception();
            }
        }
    }
}
