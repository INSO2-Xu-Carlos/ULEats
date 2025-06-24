using backend.Model;
using DataModel;
using LinqToDB;

namespace backend.Core
{
    public class OrderItemService
    {
        private readonly UlEatsDb _context;

        public OrderItemService(UlEatsDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new OrderItem to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The orderItem if client and product exists and added correctly </returns>
        /// <exception cref="Exception"></exception>
        public OrderItem? AddOrderItem(OrderItemCreateDto dto)
        {
            var customerExists = _context.Customers.Any(c => c.CustomerId == dto.CustomerId);
            if (!customerExists)
                throw new Exception($"El cliente con ID {dto.CustomerId} no existe.");

            var productExists = _context.Products.Any(p => p.ProductId == dto.ProductId);
            if (!productExists)
                throw new Exception($"El producto con ID {dto.ProductId} no existe.");

            var orderItem = new OrderItem
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice
            };

            var id = _context.InsertWithInt32Identity(orderItem);
            if (id > 0)
            {
                orderItem.OrderItemId = id;
                return orderItem;
            }
            return null;
        }

        /// <summary>
        /// Get an OrderItem by its ID
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns> The orderItem with given id if any </returns>
        public OrderItem? GetOrderItemById(int orderItemId)
        {
            return _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
        }

        /// <summary>
        /// Get all OrderItems
        /// </summary>
        /// <returns> List with all OrderItems</returns>
        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        /// <summary>
        /// Update an existing OrderItem in the database
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateOrderItem(OrderItem orderItem)
        {
            var updated = _context.Update(orderItem);
            return updated > 0;
        }

        /// <summary>
        /// Delete all OrderItems by CustomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> true if all ordersItems from a given customer are deleted correctly </returns>
        public bool DeleteOrderItemsByCustomerId(int customerId)
        {
            var deleted = _context.OrderItems.Delete(oi => oi.CustomerId == customerId);
            return deleted > 0;
        }

        /// <summary>
        /// Delete an OrderItem by its ID
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns> true if a single orderItem is deleted correctly </returns>
        public bool DeleteOrderItem(int orderItemId)
        {
            var deleted = _context.OrderItems.Delete(oi => oi.OrderItemId == orderItemId);
            return deleted > 0;
        }

        /// <summary>
        /// Get all OrderItems for a specific customer, including product names
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> List with all orderItem from a given customer </returns>
        public IEnumerable<OrderItemWithProductNameDto> GetOrderItemsByCustomer(int customerId)
        {
            var query = from oi in _context.OrderItems
                        join p in _context.Products on oi.ProductId equals p.ProductId
                        where oi.CustomerId == customerId
                        select new OrderItemWithProductNameDto
                        {
                            OrderItemId = oi.OrderItemId,
                            ProductId = oi.ProductId,
                            ProductName = p.Name,
                            Quantity = oi.Quantity,
                            UnitPrice = oi.UnitPrice
                        };

            return query.ToList();
        }
    }
}
