using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Services
{
    public class SupportCompanyService
    {
        private readonly Guid _userId;

        public SupportCompanyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSupportCompany(SupportCompanyCreate model)
        {
            var entity = new SupportCompany()
            {
                CompanyName = model.CompanyName,
                MainPhoneNumber = model.MainPhoneNumber,
                StreetAddress1 = model.StreetAddress1,
                StreetAddress2 = model.StreetAddress2,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SupportCompanies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SupportCompanyList> GetSupportCompanies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.SupportCompanies.Select(e => new SupportCompanyList
                {
                    SupportCompanyId = e.SupportCompanyId,
                    CompanyName = e.CompanyName
                });

                return query.ToArray();
            }
        }
    }


}
