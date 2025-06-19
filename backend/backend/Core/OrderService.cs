using backend.Model;
using DataModel;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace backend.Core
{
    public class OrderService
    {
        private readonly UlEatsDb _context;

        public OrderService(UlEatsDb context)
        {
            _context = context;
        }

        public Order? AddOrder(OrderCreateDTO dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                RestaurantId = dto.RestaurantId,
                DeliveryId = dto.DeliveryId,
                OrderDate = dto.OrderDate,
                Status = dto.Status,
                DeliveryAddress = dto.DeliveryAddress,
                Subtotal = dto.Subtotal,
                DeliveryFee = dto.DeliveryFee,
                TotalAmount = dto.TotalAmount,
                EstimatedDeliveryTime = dto.EstimatedDeliveryTime,
                ActualDeliveyTime = dto.ActualDeliveryTime,
                SpecialInstructions = dto.SpecialInstructions
            };

            var id = _context.InsertWithInt32Identity(order);
            if (id > 0)
            {
                order.OrderId = id;
                return order;
            }
            return null;
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order? CreateOrder(Order order)
        {
            var id = _context.InsertWithInt32Identity(order);
            if (id > 0)
            {
                order.OrderId = id;
                return order;
            }
            return null;
        }

        public bool UpdateOrder(int id, OrderCreateDTO dto)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                .Set(o => o.DeliveryId, dto.DeliveryId)
                .Set(o => o.Status, dto.Status)
                .Update();
            return affected > 0;
        } 

        public IEnumerable<Order> GetOrdersByDeliveryId(int deliveryId)
        {
            return _context.Orders.Where(o => o.DeliveryId == deliveryId).ToList();
        }

        public bool DeleteOrder(int id)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                .Delete();
            return affected > 0;
        }

        public List<object> GetOrdersByRestaurantUser(int userId)
        {

            var restaurant = _context.Restaurants.Where(r => r.UserId == userId).Select(r => r.RestaurantId).ToList();

            var orders = _context.Orders.Where(o => restaurant.Contains(o.RestaurantId)).Select(o => new
            {
                o.OrderId,
                Total = o.TotalAmount,
                o.Status,
                CustomerName = o.Customer != null && o.Customer.User != null ? o.Customer.User.FirstName : "?",
                RestaurantName = o.Restaurant.Name,
                RestaurantAddress = o.Restaurant.Address,
            }).ToList<object>();

            return orders;
        }
    }
}
