using DataModel;
using LinqToDB;
using LinqToDB.SqlQuery;

namespace backend.Core
{
    public class ClientService
    {
        private readonly UlEatsDb context;
        public ClientService(UlEatsDb context)
        {
            this.context = context;
        }
        /// <summary>
        /// Check if username and passwords are valid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>true if login is valid, else if login is not valid</returns>
        public bool login(String name, String password) 
        {
            User user = context.GetTable<User>().Where(x => x.Email == name &&  x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public bool register(String name, String password)
        {
            var user = (from U in context.GetTable<User>() from O in context.GetTable<Order>() where  U.UserId == O.CustomerId && U.FirstName == name select O).ToList(); 


            return false;
        }

    }
}
