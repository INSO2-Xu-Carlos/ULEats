using backend.Model;
using DataModel;
using LinqToDB;

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

        // Obtener todos los restaurantes
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        // Actualizar restaurante
        public bool UpdateRestaurant(Restaurant restaurant)
        {
            var updated = _context.Update(restaurant);
            return updated > 0;
        }

        // Eliminar restaurante
        public bool DeleteRestaurant(int restaurantId)
        {
            var deleted = _context.Restaurants.Delete(r => r.RestaurantId == restaurantId);
            return deleted > 0;
        }
    }
}