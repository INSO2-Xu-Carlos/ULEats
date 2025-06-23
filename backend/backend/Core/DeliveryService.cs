using backend.Model;
using DataModel;
using LinqToDB;
using System.Linq;

namespace backend.Core
{
    public class DeliveryService
    {
        private readonly UlEatsDb _context;

        public DeliveryService(UlEatsDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new delivery to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The delivery added if added correctly </returns>
        public Delivery? AddDelivery(DeliveryCreateDTO dto)
        {
            var delivery = new Delivery
            {
                UserId = dto.UserId,
                VehiclePlate = dto.VehiclePlate,
                Phone = dto.Phone,
                VehicleType = dto.VehicleType
            };

            var id = _context.InsertWithInt32Identity(delivery);
            if (id > 0)
            {
                delivery.DeliveryId = id;
                return delivery;
            }
            return null;
        }

        /// <summary>
        /// Get a delivery by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Delivery with given id if any </returns>
        public Delivery? GetDeliveryById(int id)
        {
            return _context.Deliveries.FirstOrDefault(d => d.DeliveryId == id);
        }

        /// <summary>
        /// Get all deliveries
        /// </summary>
        /// <returns> List with all deliveries </returns>
        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _context.Deliveries.ToList();
        }

        /// <summary>
        /// Create a new delivery in the database
        /// </summary>
        /// <param name="delivery"></param>
        /// <returns> The delivery if create correctly </returns>
        public Delivery? CreateDelivery(Delivery delivery)
        {
            var id = _context.InsertWithInt32Identity(delivery);
            if (id > 0)
            {
                delivery.DeliveryId = id;
                return delivery;
            }
            return null;
        }

        /// <summary>
        /// Update an existing delivery in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updated"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateDelivery(int id, Delivery updated)
        {
            var affected = _context.Deliveries
                .Where(d => d.DeliveryId == id)
                .Set(d => d.UserId, updated.UserId)
                .Set(d => d.VehiclePlate, updated.VehiclePlate)
                .Set(d => d.Phone, updated.Phone)
                .Set(d => d.VehicleType, updated.VehicleType)
                .Update();
            return affected > 0;
        }

        /// <summary>
        /// Delete a delivery by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> true if its deleted correctly </returns>
        public bool DeleteDelivery(int id)
        {
            var affected = _context.Deliveries
                .Where(d => d.DeliveryId == id)
                .Delete();
            return affected > 0;
        }
    }
}