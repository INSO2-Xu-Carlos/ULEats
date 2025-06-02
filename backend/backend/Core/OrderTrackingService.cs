using DataModel;
using LinqToDB;

namespace backend.Core
{
    public class OrderTrackingService
    {
        private readonly UlEatsDb _context;

        public OrderTrackingService(UlEatsDb context)
        {
            _context = context;
        }

        // Crear un nuevo tracking
        public OrderTracking? AddOrderTracking(OrderTracking tracking)
        {
            var id = _context.InsertWithInt32Identity(tracking);
            if (id > 0)
            {
                tracking.TrackingId = id;
                return tracking;
            }
            return null;
        }

        // Obtener tracking por ID
        public OrderTracking? GetOrderTrackingById(int trackingId)
        {
            return _context.OrderTrackings.FirstOrDefault(t => t.TrackingId == trackingId);
        }

        // Obtener todos los trackings
        public IEnumerable<OrderTracking> GetAllOrderTrackings()
        {
            return _context.OrderTrackings.ToList();
        }

        // Actualizar tracking
        public bool UpdateOrderTracking(OrderTracking tracking)
        {
            var updated = _context.Update(tracking);
            return updated > 0;
        }

        // Eliminar tracking
        public bool DeleteOrderTracking(int trackingId)
        {
            var deleted = _context.OrderTrackings.Delete(t => t.TrackingId == trackingId);
            return deleted > 0;
        }
    }
}