using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminApp.Models;

namespace AdminApp
{
    public class EnumCaster
    {
        public static int OrderStatusToNumber(OrderStatus status)
        {
            switch(status)
            {
                case OrderStatus.Preparing:
                    return 0;
                case OrderStatus.Serving:
                    return 1;
                case OrderStatus.Finished:
                    return 2;
                case OrderStatus.Canceled:
                    return 3;
                default:
                    throw new Exception();
            }
        }

        public static OrderStatus NumberToOrderStatus(int number)
        {
            switch (number)
            {
                case 0:
                    return OrderStatus.Preparing;
                case 1:
                    return OrderStatus.Serving;
                case 2:
                    return OrderStatus.Finished;
                case 3:
                    return OrderStatus.Canceled;
                default:
                    throw new Exception();
            }
        }
    }
}
