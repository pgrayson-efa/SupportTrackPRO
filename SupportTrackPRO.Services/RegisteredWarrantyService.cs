using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupportTrackPRO.Services
{
    public class RegisteredWarrantyService
    {
        private readonly Guid _userId;

        public RegisteredWarrantyService(Guid userId, int mySupportCompanyId)
        {
            _userId = userId;
            int _mySupportCompanyId = mySupportCompanyId;
        }

        public bool CreateRegisteredWarranty(RegisteredWarrantyCreate model,int mySupportCompanyId, int myCustomerId)
        {
            var entity = new RegisteredWarranty()
            {
                RegisteredWarrantyId = model.RegisteredWarrentyId,
                SupportProductId = model.SupportProductId,
                SupportCompanyId = model.SupportCompanyId,
                CustomerId = myCustomerId,
                SerialNumber = model.SerialNumber,
                DateRegistered = DateTimeOffset.Now

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.RegisteredWarranties.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RegisteredWarrantyList> GetRegisteredWarranties(int CustomerId, int mySupportCompanyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .RegisteredWarranties
                        .Where(w => w.CustomerId == CustomerId)
                        .Select(
                            w =>
                                new RegisteredWarrantyList
                                {
                                    RegisteredWarrantyId = w.RegisteredWarrantyId,
                                    SupportCompanyId = w.SupportCompanyId,
                                    SupportProductId = w.SupportProductId,
                                    CustomerId = w.CustomerId,
                                    SerialNumber = w.SerialNumber,
                                    DateRegistered = w.DateRegistered
                                }
                        );
                return query.ToArray();
            }
        }
    }
}
