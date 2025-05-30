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
        public bool login(String email, String password) 
        {
            User user = _context.GetTable<User>().Where(x => x.Email == email &&  x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
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
        public bool register(String name, String password, String email, String surname, String? phone, string usertype)
        {
            var user = _context.GetTable<User>().FirstOrDefault(u => u.Email == email);
            if (user != null) { return false; }

            var newUser = new User
            {
                FirstName = name,
                LastName = surname,
                Email = email,
                Password = password,
                Phone = phone,
                UserType = usertype

            };

            _context.Insert(newUser);

            return true;
        }

        public User? getUserById(int userId)
        {
            return _context.GetTable<User>().FirstOrDefault(u => u.UserId == userId);
        }

    }
}
