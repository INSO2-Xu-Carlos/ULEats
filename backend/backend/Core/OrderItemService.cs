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
        public OrderItem? AddOrderItem(OrderItem orderItem)
        {
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

        // Eliminar OrderItem
        public bool DeleteOrderItem(int orderItemId)
        {
            var deleted = _context.OrderItems.Delete(oi => oi.OrderItemId == orderItemId);
            return deleted > 0;
        }
    }
}
