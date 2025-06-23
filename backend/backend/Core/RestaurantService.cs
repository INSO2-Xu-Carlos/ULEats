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

        /// <summary>
        /// Add a new Restaurant to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The restaurant added if added correctly </returns>
        public Restaurant? AddRestaurant(RestaurantCreateDto dto)
        {
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

            var rows = _context.Insert(restaurant);
            if (rows > 0)
            {
                return restaurant;
            }
            return null;
        }

        /// <summary>
        /// Get a Restaurant by its ID
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns> Restaurant with given id if any </returns>
        public Restaurant? GetRestaurantById(int restaurantId)
        {
            return _context.Restaurants.FirstOrDefault(r => r.RestaurantId == restaurantId);
        }

        /// <summary>
        /// Get all Restaurants by User ID
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns> list with all restaurants from a user </returns>
        public IEnumerable<Restaurant> GetRestaurantsByUser(int user_id)
        {
            return _context.Restaurants.Where(r => r.UserId == user_id).ToList();
        }

        /// <summary>
        /// Get all Restaurants
        /// </summary>
        /// <returns> list with all restaurants </returns>
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        /// <summary>
        /// Update an existing Restaurant
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns> true if updated correctly </returns>
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

        /// <summary>
        /// Delete a Restaurant by its ID
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns> true if deleted correctly </returns>
        public bool DeleteRestaurant(int restaurantId)
        {
            using var transaction = _context.BeginTransaction(); 

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