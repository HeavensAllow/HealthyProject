using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HealthyProject.Models;

[assembly: OwinStartupAttribute(typeof(HealthyProject.Startup))]
namespace HealthyProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login    
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            var roleManager = new RoleManager<CustomRole,int>(new CustomRoleStore(context));
            var UserManager = new UserManager<ApplicationUser,int>(new CustomUserStore(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new CustomRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
        }
    }
}
