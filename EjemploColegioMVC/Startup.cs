using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EjemploColegioMVC.Models;

[assembly: OwinStartupAttribute(typeof(EjemploColegioMVC.Startup))]
namespace EjemploColegioMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoleUser();
        }

        //Metodo para crear roles y usuarios por defecto
        void CreateRoleUser()
        {
            //Instanciamos el modelo de seguridad
            ApplicationDbContext context = new ApplicationDbContext();

            //Manejador de rol y Manejador de usuario
            var ManejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var ManejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //Rol administrador
            if (!ManejadorRol.RoleExists("Admin"))
            {
                //sino existe, se crea el rol y se asigna un nuevo usuario con ese rol
                var rol = new IdentityRole();
                rol.Name = "Admin";
                ManejadorRol.Create(rol);
                //creamos un primer usuario para ese rol
                var user = new ApplicationUser();
                user.UserName = "Admin@unan.edu.ni";
                user.Email = "Admin@unan.edu.ni";
                string PWD = "12345678";
                var chkUser = ManejadorUsuario.Create(user, PWD);
                //si se creo con exito
                if (chkUser.Succeeded)
                {
                    //Vinculamos el rol con el usuario
                    ManejadorUsuario.AddToRole(user.Id, "Admin");
                }
            }
            //Rol Docente
            if (!ManejadorRol.RoleExists("Docente"))
            {
                //sino existe, se crea el rol y se asigna un nuevo usuario con ese rol
                var rol = new IdentityRole();
                rol.Name = "Docente";
                ManejadorRol.Create(rol);

                //creamos un primer usuario para ese rol
                var user = new ApplicationUser();
                user.UserName = "Docente@unan.edu.ni";
                user.Email = "Docente@unan.edu.ni";
                string PWD = "12345678";
                var chkUser = ManejadorUsuario.Create(user, PWD);
                //si se creo con exito
                if (chkUser.Succeeded)
                {
                    //Vinculamos el rol con el usuario
                    ManejadorUsuario.AddToRole(user.Id, "Docente");
                }
            }
            //Rol Tutor
            if (!ManejadorRol.RoleExists("Tutor"))
            {
                //sino existe, se crea el rol y se asigna un nuevo usuario con ese rol
                var rol = new IdentityRole();
                rol.Name = "Tutor";
                ManejadorRol.Create(rol);

                //creamos un primer usuario para ese rol
                var user = new ApplicationUser();
                user.UserName = "Tutor@unan.edu.ni";
                user.Email = "Tutor@unan.edu.ni";
                string PWD = "12345678";
                var chkUser = ManejadorUsuario.Create(user, PWD);
                //si se creo con exito
                if (chkUser.Succeeded)
                {
                    //Vinculamos el rol con el usuario
                    ManejadorUsuario.AddToRole(user.Id, "Tutor");
                }
            }

        }
    }
}
