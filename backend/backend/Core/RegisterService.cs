using DataModel;
using LinqToDB;
using LinqToDB.SqlQuery;

namespace backend.Core
{
    public class RegisterService
    {
        private readonly UlEatsDb _context;

        public RegisterService(UlEatsDb context) { 
            
            _context = context;
        }


        /// <summary>
        /// Register a new restaurant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="description"></param>
        /// <param name="logoUrl"></param>
        /// <returns>true if </returns>
        public bool RegisterRestaurant(int userId, string name, string address, string phone, string? description, string? logoUrl)
        {
            var existing = _context.GetTable<Restaurant>().FirstOrDefault(r => r.UserId == userId);

            if (existing != null)
            {
                return false;
            }

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
