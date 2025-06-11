using DataModel;
using LinqToDB;
using LinqToDB.SqlQuery;

namespace backend.Core
{
    public class ClientService
    {
        private readonly UlEatsDb _context;
        public ClientService(UlEatsDb context)
        {
            _context = context;
        }
        /// <summary>
        /// Check if username and passwords are valid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>true if login is valid, else if login is not valid</returns>
        public User? LoginAndGetUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }



        /// <summary>
        /// It register a new User 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="surname"></param>
        /// <param name="phone"></param>    
        /// <param name="usertype"></param>
        /// <returns> true if email is not in database and all fields are not null</returns>
        public User? RegisterAndGetUser(string name, string password, string email, string surname, string? phone, string usertype)
        {
            var exists = _context.Users.Any(u => u.Email == email);
            if (exists) return null;

            var user = new User
            {
                FirstName = name,
                Password = password,
                Email = email,
                LastName = surname,
                Phone = phone,
                UserType = usertype
            };

            var userId = _context.InsertWithInt32Identity(user);
            user.UserId = userId;

            return user;
        }

        public User? getUserById(int userId)
        {
            return _context.GetTable<User>().FirstOrDefault(u => u.UserId == userId);
        }
        public Customer? GetCustomerByUserId(int userId)
        {
            return _context.Customers.FirstOrDefault(c => c.UserId == userId);
        }
        public Restaurant? GetRestaurantByUserId(int userId)
        {
            return _context.Restaurants.FirstOrDefault(r => r.UserId == userId);
        }

        public Delivery? GetDeliveryByUserId(int userId)
        {
            return _context.Deliveries.FirstOrDefault(d => d.UserId == userId);
        }

    }
}
