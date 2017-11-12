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

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context)); 
            var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new CustomRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            //primeiro, criar o role User
            if (!roleManager.RoleExists("User"))
            {
                var role = new CustomRole();
                role.Name = "User";
                roleManager.Create(role);

            }

            ApplicationUser newUser = new ApplicationUser();
            newUser.UserName = "admin@gmail.com";
            newUser.Email = "admin@gmail.com"; 
            var password = "1-Obesidade"; 
            var result = UserManager.Create(newUser, password);
           
            if (result.Succeeded)
            {
                UserManager.AddToRole(newUser.Id, "Admin"); // adicionar ao role Admin
            }
            
        }
    }
}
