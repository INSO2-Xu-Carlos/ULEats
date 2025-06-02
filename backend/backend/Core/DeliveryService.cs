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

        public Delivery? GetDeliveryById(int id)
        {
            return _context.Deliveries.FirstOrDefault(d => d.DeliveryId == id);
        }

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _context.Deliveries.ToList();
        }

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

        public bool DeleteDelivery(int id)
        {
            var affected = _context.Deliveries
                .Where(d => d.DeliveryId == id)
                .Delete();
            return affected > 0;
        }
    }
}