using TwoFactorAuth.Interfaces;
using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _AppDbContext;
        private readonly ShoppingCart _ShoppingCart;
        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _AppDbContext = appDbContext;
            _ShoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _AppDbContext.Orders.Add(order);
            var shoppingCartItems = _ShoppingCart.ShoppingCartItems;
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = (int)item.Amount,
                    ItemId = item.Item.ItemId,
                    OrderId = order.OrderId,
                    Price = item.Item.Price

                };
                _AppDbContext.OrderDetails.Add(orderDetail);

            }
            _AppDbContext.SaveChanges();
        }
    }
}

