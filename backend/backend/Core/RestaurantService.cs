using DataModel;
using LinqToDB;
using LinqToDB.SqlQuery;
namespace backend.Core
{
    public class RestaurantService
    {
        private readonly UlEatsDb _context;

        public RestaurantService(UlEatsDb context)
        {
            _context = context;
        }

        public bool CreateRestaurant(int userId, string name, string address, string phone, string description, string logoUrl)
        {
            // Comprobar si ya existe un restaurante para ese usuario
            var exists = _context.Restaurants.FirstOrDefault(r => r.UserId == userId);
            if (exists != null)
                return false;

            var newRestaurant = new Restaurant
            {
                Name = name,
                Address = address,
                Phone = phone,
                UserId = userId,
                Description = description,
                LogoUrl = logoUrl
            };

            _context.Insert(newRestaurant);
            return true;
        }
    }
}
