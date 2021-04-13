using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);

    }
}
