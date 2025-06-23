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

        /// <summary>
        /// Add a new Order to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The order if added to database correctly </returns>
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

        /// <summary>
        /// Get an Order by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Order with given id if any </returns>
        public Order? GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <returns> List with all orders </returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        /// <summary>
        /// Create a new Order in the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns> Order if created correctly</returns>
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

        /// <summary>
        /// Update an existing Order in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateOrder(int id, OrderCreateDTO dto)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                .Set(o => o.DeliveryId, dto.DeliveryId)
                .Set(o => o.Status, dto.Status)
                .Update();
            return affected > 0;
        }

        /// <summary>
        /// Get all Orders associated with a specific Delivery ID
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns> list with all orders from a delivery </returns>
        public IEnumerable<Order> GetOrdersByDeliveryId(int deliveryId)
        {
            return _context.Orders.Where(o => o.DeliveryId == deliveryId).ToList();
        }

        /// <summary>
        /// Get all Orders associated with a specific Customer ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> list with all orders from a customer</returns>
        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        /// <summary>
        /// Delete an Order by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> true if deleted correctly </returns>
        public bool DeleteOrder(int id)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == id)
                .Delete();
            return affected > 0;
        }

        /// <summary>
        /// Get all Orders associated with a specific Restaurant User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> list with all orders from a restaurant </returns>
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
        /// <summary>
        /// Update the status and delivery ID of an Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <param name="deliveryId"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateOrderStatusAndDelivery(int orderId, string status, int? deliveryId)
        {
            var affected = _context.Orders
                .Where(o => o.OrderId == orderId)
                .Set(o => o.Status, status)
                .Set(o => o.DeliveryId, deliveryId)
                .Update();
            return affected > 0;
        }
    }
}
