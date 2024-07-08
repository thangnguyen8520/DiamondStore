using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Enums
{
    public enum OrderStatusEnums
    {
        Waiting,
        Preparing,
        Prepared,
        Shipping,
        DeliveryFailed,
        DeliverySuccessful,
        Received,
        Completed
    }
}
