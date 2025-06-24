using backend.Model;
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

        /// <summary>
        /// Add a new OrderTracking to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The orderTracking added if added correctly </returns>
        public OrderTracking? AddOrderTracking(OrderTrackingCreateDto dto)
        {
            var tracking = new OrderTracking
            {
                OrderId = dto.OrderId,
                Status = dto.Status,
                ChangedAt = dto.ChangedAt,
                ChangedBy = dto.ChangedBy,
                Notes = dto.Notes
            };

            var id = _context.InsertWithInt32Identity(tracking);
            if (id > 0)
            {
                tracking.TrackingId = id;
                return tracking;
            }
            return null;
        }

        /// <summary>
        /// Get an OrderTracking by its ID
        /// </summary>
        /// <param name="trackingId"></param>
        /// <returns> OrderTracking with given id if any </returns>
        public OrderTracking? GetOrderTrackingById(int trackingId)
        {
            return _context.OrderTrackings.FirstOrDefault(t => t.TrackingId == trackingId);
        }

        /// <summary>
        /// Get all OrderTrackings
        /// </summary>
        /// <returns> list with all OrderTrackings </returns>
        public IEnumerable<OrderTracking> GetAllOrderTrackings()
        {
            return _context.OrderTrackings.ToList();
        }

        /// <summary>
        /// Update an existing OrderTracking
        /// </summary>
        /// <param name="tracking"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateOrderTracking(OrderTracking tracking)
        {
            var updated = _context.Update(tracking);
            return updated > 0;
        }

        /// <summary>
        /// Delete an OrderTracking by its ID
        /// </summary>
        /// <param name="trackingId"></param>
        /// <returns> true if deleted correctly </returns>
        public bool DeleteOrderTracking(int trackingId)
        {
            var deleted = _context.OrderTrackings.Delete(t => t.TrackingId == trackingId);
            return deleted > 0;
        }
    }
}