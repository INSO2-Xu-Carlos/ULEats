using DataModel;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
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
        public virtual User? LoginAndGetUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            var hasher = new PasswordHasher<User>(); 
            var result = hasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success ? user : null;
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
        public virtual User? RegisterAndGetUser(string name, string password, string email, string surname, string? phone, string usertype)
        {
            var exists = _context.Users.Any(u => u.Email == email);
            if (exists) return null;

            var hasher = new PasswordHasher<User>();    
            var user = new User
            {
                FirstName = name,
                Email = email,
                LastName = surname,
                Phone = phone,
                UserType = usertype
            };

            user.Password = hasher.HashPassword(user, password);

            var userId = _context.InsertWithInt32Identity(user);
            user.UserId = userId;

            return user;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user with given id if any </returns>
        public virtual User? getUserById(int userId)
        {
            return _context.GetTable<User>().FirstOrDefault(u => u.UserId == userId);
        }
        /// <summary>
        /// Get customer by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Customer with given id if any </returns>
        public virtual Customer? GetCustomerByUserId(int userId)
        {
            return _context.Customers.FirstOrDefault(c => c.UserId == userId);
        }
        /// <summary>
        /// Get restaurant by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Restaurant from a user with given id if any </returns>
        public virtual Restaurant? GetRestaurantByUserId(int userId)
        {
            return _context.Restaurants.FirstOrDefault(r => r.UserId == userId);
        }

        /// <summary>
        /// Get delivery by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Delivery by given id if any </returns>
        public virtual Delivery? GetDeliveryByUserId(int userId)
        {
            return _context.Deliveries.FirstOrDefault(d => d.UserId == userId);
        }

    }
}
