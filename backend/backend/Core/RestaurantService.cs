using DataModel;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace backend.Core
{
    public class RestaurantService
    {
        private readonly UlEatsDb _context;

        public RestaurantService(UlEatsDb context)
        {
            _context = context;
        }

        public Restaurant? GetRestaurantById(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public IEnumerable<Restaurant> GetRestaurantsByUser(int userId)
        {
            return _context.Restaurants.Where(r => r.UserId == userId).ToList();
        }

        public Restaurant? CreateRestaurant(Restaurant restaurant)
        {
            var id = _context.InsertWithInt32Identity(restaurant);
            if (id > 0)
            {
                restaurant.RestaurantId = id;
                return restaurant;
            }
            return null;
        }

        public bool UpdateRestaurant(int id, Restaurant updated)
        {
            var affected = _context.Restaurants
                .Where(r => r.RestaurantId == id)
                .Set(r => r.Name, updated.Name)
                .Set(r => r.Address, updated.Address)
                .Set(r => r.Phone, updated.Phone)
                .Set(r => r.UserId, updated.UserId)
                .Set(r => r.Description, updated.Description)
                .Set(r => r.LogoUrl, updated.LogoUrl)
                .Update();
            return affected > 0;
        }

        public bool DeleteRestaurant(int id)
        {
            var affected = _context.Restaurants
                .Where(r => r.RestaurantId == id)
                .Delete();
            return affected > 0;
        }
    }
}