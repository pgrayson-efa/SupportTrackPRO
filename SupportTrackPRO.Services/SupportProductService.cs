using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupportTrackPRO.Services
{
    public class SupportProductService
    {
        private readonly Guid _userId;

        public SupportProductService(Guid userId, int mySupportCompanyId)
        {
            _userId = userId;
            int _mySupportCompanyId = mySupportCompanyId;
        }

        public bool CreateSupportProduct(SupportProductCreate model,int mySupportCompanyId)
        {
            var entity = new SupportProduct()
            {
                SupportCompanyId = mySupportCompanyId,
                ProductName = model.ProductName,
                ModelNumber = model.ModelNumber,
                Version = model.Version,
                WarrantyLengthInDays = model.WarrantyLengthInDays

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SupportProducts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SupportProductList> GetSupportProducts(string userName, int mySupportCompanyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SupportProducts
                        .Where(e => e.SupportCompanyId == mySupportCompanyId)
                        .Select(
                            e =>
                                new SupportProductList
                                {
                                    SupportProductId = e.SupportProductId,
                                    SupportCompanyId = e.SupportCompanyId,
                                    ProductName = e.ProductName,
                                    ModelNumber = e.ModelNumber,
                                    Version = e.Version,
                                    WarrantyLengthInDays = e.WarrantyLengthInDays
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
