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
    }
}
