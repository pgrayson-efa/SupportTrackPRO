using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SupportTrackPRO.Data;

[assembly: OwinStartupAttribute(typeof(SupportTrackPRO.WebMVC.Startup))]
namespace SupportTrackPRO.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // create a master SupportTrackPro admin role
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

     
            if (!roleManager.RoleExists("SupportTrackPROAdmin"))
            {
                // first we create SupportProAdmin role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SupportTrackPROAdmin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "stpro.admin";
                user.Email = "stpro@gmail.com";

                string userPWD = "Abc123@";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "SupportTrackPROAdmin");
                }
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("SupportManager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SupportManager";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "manager";
                user.Email = "manager@test.com";
                string userPWD = "123456";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "SupportManager");
                }

            }

            // creating Creating provider role     
            if (!roleManager.RoleExists("SupportProvider"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SupportProvider";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "provider";
                user.Email = "provider@test.com";
                string userPWD = "123456";
                var chkUser = userManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "SupportProvider");
                }

            }

            // creating Customer    
            if (!roleManager.RoleExists("SupportCustomer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SupportCustomer";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "customer";
                user.Email = "customer@test.com";
                string userPWD = "123456";
                var chkUser = userManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "SupportM");
                }

            }
        }
    }
}
