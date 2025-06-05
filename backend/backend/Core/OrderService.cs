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
                //CustomerId = dto.CustomerId,
                RestaurantId = dto.RestaurantId,
                DeliveryId = dto.DeliveryId,
                OrderDate = dto.OrderDate,
                Status = dto.Status,
                DeliveryAddress = dto.DeliveryAddress,
                Subtotal = dto.Subtotal,
                DeliveryFee = dto.DeliveryFee,
                TotalAmount = dto.TotalAmount,
                EstimatedDeliveryTime = dto.EstimatedDeliveryTime,
                ActualDeliveyTime = dto.ActualDeliveyTime,
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

        public bool UpdateOrder(int id, Order updated)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                //.Set(o => o.CustomerId, updated.CustomerId)
                .Set(o => o.RestaurantId, updated.RestaurantId)
                .Set(o => o.DeliveryId, updated.DeliveryId)
                .Set(o => o.OrderDate, updated.OrderDate)
                .Set(o => o.Status, updated.Status)
                .Set(o => o.DeliveryAddress, updated.DeliveryAddress)
                .Set(o => o.Subtotal, updated.Subtotal)
                .Set(o => o.DeliveryFee, updated.DeliveryFee)
                .Set(o => o.TotalAmount, updated.TotalAmount)
                .Set(o => o.EstimatedDeliveryTime, updated.EstimatedDeliveryTime)
                .Set(o => o.ActualDeliveyTime, updated.ActualDeliveyTime)
                .Set(o => o.SpecialInstructions, updated.SpecialInstructions)
                .Update();
            return affected > 0;
        }

        public bool DeleteOrder(int id)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                .Delete();
            return affected > 0;
        }
    }
}
