using backend.Model;
using DataModel;
using LinqToDB;
namespace backend.Core
{
    public class CustomerService
    {
        private readonly UlEatsDb _context;

        public CustomerService(UlEatsDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The customer added if added correctly </returns>
        public Customer? AddCustomer(CustomerCreateDTO dto)
        {
            var customer = new Customer
            {
                Address = dto.Address,
                UserId = dto.UserId
            };

            var id = _context.InsertWithInt32Identity(customer);
            if (id > 0)
            {
                customer.CustomerId = id;
                return customer;
            }
            return null;
        }

        /// <summary>
        /// Get a customer by their ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> Customer with given id if any </returns>
        public Customer? GetCustomerById(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns> List with all customers </returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        /// <summary>
        /// Update an existing customer in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdateCustomer(Customer customer)
        {
            var updated = _context.Update(customer);
            return updated > 0;
        }

        /// <summary>
        /// Delete a customer by their ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns> true if its deleted correctly </returns>
        public bool DeleteCustomer(int customerId)
        {
            var deleted = _context.Customers.Delete(c => c.CustomerId == customerId);
            return deleted > 0;
        }
    }
}
