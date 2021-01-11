using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupportTrackPRO.Services
{
    public class SupportTicketService
    {
        private readonly Guid _userId;

        public SupportTicketService(Guid userId, int mySupportCompanyId)
        {
            _userId = userId;
            int _mySupportCompanyId = mySupportCompanyId;
        }

        public bool UpdateSupportTicket(SupportTicketEdit model, Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SupportTickets.Single(e => e.SupportTicketId == model.SupportTicketId);
                entity.Status = model.Status;
                entity.SupportProviderId = model.SupportProviderId;
                entity.RegisteredWarrantyId = model.RegisteredWarrantyId;
                entity.CustomerId = model.CustomerId;
                entity.ReasonForSupport = model.ReasonForSupport;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool CreateSupportTicket(SupportTicketCreate model, int myCustomerId)
        {
            var entity = new SupportTicket()
            {
                SupportTicketId = model.SupportTicketId,
                Status = model.Status,
                SupportProviderId = model.SupportProviderId,
                RegisteredWarrantyId = model.RegisteredWarrantyId,
                CustomerId = myCustomerId,
                ReasonForSupport = model.ReasonForSupport
            };
            if (entity.Status != "New")
            {
                entity.Status = "New";
            }
            using (var ctx = new ApplicationDbContext())
            {
                ctx.SupportTickets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SupportTicketList> GetSupportTickets(int customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SupportTickets
                        .Select(
                            e =>
                                new SupportTicketList
                                {
                                    SupportTicketId = e.SupportTicketId,
                                    Status = e.Status,
                                    SupportProviderId = e.SupportProviderId,
                                    RegisteredWarrantyId = e.RegisteredWarrantyId,
                                    CustomerId = e.CustomerId,
                                    ReasonForSupport = e.ReasonForSupport
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<SupportTicketList> GetSupportTicketsBySupportCustomerId(int customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.SupportTickets
                        .Where(e => e.CustomerId == customerId)
                        .Select(
                            e =>
                                new SupportTicketList
                                {
                                    SupportTicketId = e.SupportTicketId,
                                    Status = e.Status,
                                    SupportProviderId = e.SupportProviderId,
                                    RegisteredWarrantyId = e.RegisteredWarrantyId,
                                    CustomerId = e.CustomerId,
                                    ReasonForSupport = e.ReasonForSupport
                                }
                        );

                return query.ToArray();
            }
        }
        public SupportTicketDetail GetSupportTicketBySupportTicketId(int supportTicketId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.SupportTickets
                        //.Where(e => e.SupportTicketId == supportTicketId)
                        .Single(
                            e => e.SupportTicketId == supportTicketId);
                return
                                new SupportTicketDetail
                                {
                                    SupportTicketId = query.SupportTicketId,
                                    Status = query.Status,
                                    SupportProviderId = query.SupportProviderId,
                                    RegisteredWarrantyId = query.RegisteredWarrantyId,
                                    CustomerId = query.CustomerId,
                                    ReasonForSupport = query.ReasonForSupport
                                };

            }
        }

        public SupportTicketDetail GetSupportTicketDetailBySupportTicketId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SupportTickets.Single(e => e.SupportTicketId == id);
                return new SupportTicketDetail
                {
                    SupportTicketId = entity.SupportTicketId,
                    Status = entity.Status,
                    SupportProviderId = entity.SupportProviderId,
                    RegisteredWarrantyId = entity.RegisteredWarrantyId,
                    CustomerId = entity.CustomerId,
                    ReasonForSupport = entity.ReasonForSupport
                };
            }
        }

        public bool DeleteSupportTicket(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SupportTickets
                        .Single(e => e.SupportTicketId == id);

                ctx.SupportTickets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
