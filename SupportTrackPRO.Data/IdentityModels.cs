using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SupportTrackPRO.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SupportCompany> SupportCompanies { get; set; }
        public DbSet<SupportProduct> SupportProducts { get; set; }
        public DbSet<SupportProvider> SupportProviders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RegisteredWarranty> RegisteredWarranties { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<SupportTicketLog> SupportTicketLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    Configuration.LazyLoadingEnabled = true;

        //    modelBuilder
        //    .Conventions
        //        .Remove<PluralizingTableNameConvention>();

        //    modelBuilder
        //        .Configurations
        //        .Add(new IdentityUserLoginConfiguration())
        //        .Add(new IdentityUserRoleConfiguration());

        //    var user = modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

        //    user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
        //    user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
        //    user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
        //    user.Property(u => u.UserName).IsRequired();

        //    modelBuilder.Entity<IdentityUserRole>()
        //        .HasKey(r => new { r.UserId, r.RoleId })
        //        .ToTable("AspNetUserRoles");

        //    modelBuilder.Entity<IdentityUserLogin>()
        //        .HasKey(l => new { l.UserId, l.LoginProvider, l.ProviderKey })
        //        .ToTable("AspNetUserLogins");

        //    modelBuilder.Entity<IdentityUserClaim>()
        //        .ToTable("AspNetUserClaims");

        //    var role = modelBuilder.Entity<IdentityRole>()
        //        .ToTable("AspNetRoles");
        //    role.Property(r => r.Name).IsRequired();
        //    role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);


        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}

    }

    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }

    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }


}