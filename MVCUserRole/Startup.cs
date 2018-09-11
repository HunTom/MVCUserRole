using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MVCUserRole.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCUserRole.Startup))]
namespace MVCUserRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }


        //In this method we will create default User roles and Admin user for login
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //In startup.cs create first Admin Role and create a default Admin user
            if (!roleManager.RoleExists("Admin"))
            {//create Admin role
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                //Create Admin superuser who will maintain the website
                var user = new ApplicationUser
                {
                    UserName = "Tamas",
                    Email = "tamas.simon@esab.hu"
                };

                var userPWD = "A@Z200711";
                var chkUser = userManager.Create(user, userPWD);

                //Add default user to role admin
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }

            }

            //Create manager role
            if(!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole
                {
                    Name = "Manager"
                };
                roleManager.Create(role);
            }

            //create employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.Create(role);
            }

        }
    }
}
