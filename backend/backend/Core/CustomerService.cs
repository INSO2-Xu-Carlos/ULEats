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
        public Customer? GetCustomerById(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public bool UpdateCustomer(Customer customer)
        {
            var updated = _context.Update(customer);
            return updated > 0;
        }

        public bool DeleteCustomer(int customerId)
        {
            var deleted = _context.Customers.Delete(c => c.CustomerId == customerId);
            return deleted > 0;
        }
    }
}
