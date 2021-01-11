using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Services
{
    public class SupportProviderService
    {
        private readonly Guid _userId;

        public SupportProviderService(Guid userId, int mySupportCompanyId)
        {
            _userId = userId;
            int _mySupportCompanyId = mySupportCompanyId;
        }

        public bool CreateSupportProvider(SupportProviderCreate model, int mySupportCompanyId)
        {
            var entity = new SupportProvider()
            {
                SupportCompanyId = mySupportCompanyId,
                UserName = model.UserName,
                Handle = model.Handle
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SupportProviders.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SupportProviderList> GetSupportProviders(string userName, int mySupportCompanyId)
        {
            using (var ctx = new ApplicationDbContext())
            { 
                var query = ctx.SupportProviders
                    .Select(e => new SupportProviderList {
                        SupportProviderId = e.SupportProviderId,
                        SupportCompanyId = mySupportCompanyId, 
                        UserName = e.UserName, 
                        Handle = e.Handle})
                    .Where(e => e.SupportCompanyId == mySupportCompanyId);

                return query.ToArray();
            }
        }
    }
}
