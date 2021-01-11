using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupportTrackPRO.Services
{
    public class CustomerService
    {
        private readonly Guid _userId;

        public CustomerService(Guid userId, int mySupportCompanyId)
        {
            _userId = userId;
            int _mySupportCompanyId = mySupportCompanyId;
        }

        public bool UpdateCustomer(CustomerEdit model, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Customers.Single(e => e.CustomerId == model.CustomerId && e.ApplicationUserId == userId);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Address1 = model.Address1;
                entity.Address2 = model.Address2;
                entity.City = model.City;
                entity.ZipCode = model.ZipCode;
                entity.PhoneNumber1 = model.PhoneNumber1;
                entity.PhoneNumber2 = model.PhoneNumber2;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool CreateCustomer(CustomerCreate model,int mySupportCompanyId)
        {
            var entity = new Customer()
            {
                CustomerId = model.CustomerId,
                ApplicationUserId = model.ApplicationUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PhoneNumber1 = model.PhoneNumber1,
                PhoneNumber2 = model.PhoneNumber2

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CustomerList> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Select(
                            e =>
                                new CustomerList
                                {
                                    CustomerId = e.CustomerId,
                                    ApplicationUserId = e.ApplicationUserId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Address1 = e.Address1,
                                    Address2 = e.Address2,
                                    City = e.City,
                                    State = e.State,
                                    ZipCode = e.ZipCode,
                                    PhoneNumber1 = e.PhoneNumber1,
                                    PhoneNumber2 = e.PhoneNumber2
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<CustomerList> GetCustomerByCustomerId(int customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Customers
                        .Where(e => e.CustomerId == customerId)
                        .Select(
                            e =>
                                new CustomerList
                                {
                                    CustomerId = e.CustomerId,
                                    ApplicationUserId = e.ApplicationUserId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Address1 = e.Address1,
                                    Address2 = e.Address2,
                                    City = e.City,
                                    State = e.State,
                                    ZipCode = e.ZipCode,
                                    PhoneNumber1 = e.PhoneNumber1,
                                    PhoneNumber2 = e.PhoneNumber2

                                }
                        );

                return query.ToArray();
            }
        }

        public CustomerDetail GetCustomerDetailByCustomerId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Customers.Single(e => e.CustomerId == id);
                return new CustomerDetail
                    {
                        CustomerId = entity.CustomerId,
                        ApplicationUserId = entity.ApplicationUserId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Address1 = entity.Address1,
                        Address2 = entity.Address2,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        PhoneNumber1 = entity.PhoneNumber1,
                        PhoneNumber2 = entity.PhoneNumber2
                    };
            }
        }
    }
}
