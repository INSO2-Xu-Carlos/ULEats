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

        // Crear un nuevo OrderItem
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

        // Obtener OrderItem por ID
        public OrderItem? GetOrderItemById(int orderItemId)
        {
            return _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
        }

        // Obtener todos los OrderItems
        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        // Actualizar OrderItem
        public bool UpdateOrderItem(OrderItem orderItem)
        {
            var updated = _context.Update(orderItem);
            return updated > 0;
        }

        public bool DeleteOrderItemsByCustomerId(int customerId)
        {
            var deleted = _context.OrderItems.Delete(oi => oi.CustomerId == customerId);
            return deleted > 0;
        }

        // Eliminar OrderItem
        public bool DeleteOrderItem(int orderItemId)
        {
            var deleted = _context.OrderItems.Delete(oi => oi.OrderItemId == orderItemId);
            return deleted > 0;
        }

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
