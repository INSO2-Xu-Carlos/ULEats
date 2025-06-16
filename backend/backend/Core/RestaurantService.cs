using backend.Model;
using DataModel;
using LinqToDB;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace backend.Core
{
    public class RestaurantService
    {
        private readonly UlEatsDb _context;

        public RestaurantService(UlEatsDb context)
        {
            _context = context;
        }

        // Crear un nuevo restaurante
        public Restaurant? AddRestaurant(RestaurantCreateDto dto)
        {
            // Obtener el último ID existente (o 0 si la tabla está vacía)
            int lastId = _context.Restaurants
                .OrderByDescending(r => r.RestaurantId)
                .Select(r => r.RestaurantId)
                .FirstOrDefault();

            var restaurant = new Restaurant
            {
                RestaurantId = lastId + 1,
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                UserId = dto.UserId,
                Description = dto.Description,
                LogoUrl = dto.LogoUrl
            };

            // Insertar el restaurante con el ID asignado manualmente
            var rows = _context.Insert(restaurant);
            if (rows > 0)
            {
                return restaurant;
            }
            return null;
        }

        // Obtener restaurante por ID
        public Restaurant? GetRestaurantById(int restaurantId)
        {
            return _context.Restaurants.FirstOrDefault(r => r.RestaurantId == restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurantsByUser(int user_id)
        {
            return _context.Restaurants.Where(r => r.UserId == user_id).ToList();
        }
        // Obtener todos los restaurantes
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        // Actualizar restaurante
        public bool UpdateRestaurant(Restaurant restaurant)
        {
            var existing = _context.Restaurants.Find(restaurant.RestaurantId);
            if (existing == null) return false;

            existing.Name = restaurant.Name;
            existing.Address = restaurant.Address;
            existing.Phone = restaurant.Phone;
            existing.Description = restaurant.Description;
            existing.LogoUrl = restaurant.LogoUrl;
            existing.UserId = restaurant.UserId;

            _context.Update(existing);
            return true;
        }

        // Eliminar restaurante
        public bool DeleteRestaurant(int restaurantId)
        {
            using var transaction = _context.BeginTransaction(); // Asegura atomicidad

            try
            {
                var deletedProducts = _context.Products
                    .Where(p => p.RestaurantId == restaurantId)
                    .Delete();

                var deletedRestaurant = _context.Restaurants
                    .Delete(r => r.RestaurantId == restaurantId);

                transaction.Commit();

                return deletedRestaurant > 0;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}