using chaves_dayron_proyecto1_3031.DataBase;
using chaves_dayron_proyecto1_3031.Misc;
using chaves_dayron_proyecto1_3031.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace chaves_dayron_proyecto1_3031.Controllers
{
    public class HomeController : Controller
    {
        User currentUser = new User();
        DBCommand dbcmd = new DBCommand();
        API apicmd = new API();
        Notify mailcmd = new Notify();
        BuildFlight flightcmd = new BuildFlight();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
            }
            return View();
        }

        /*
        C O D I G O   P A R A   V I S T A   L O G I N
        + Manejo de sesion
        */
        [HttpPost]
        public ActionResult Login(User authAttempt)
        {

            if (dbcmd.AuthUser(authAttempt))
            {
                //usamos el metodo FormAuthentication para el manejo de sesiones
                //usamos el email como medio de identificacion
                //esto desbloquea todos aquellos metodos que tienen [Authorize] sobre el
                FormsAuthentication.SetAuthCookie(authAttempt.Email, false);
                return Redirect(Request.UrlReferrer.ToString());
            }

            ModelState.AddModelError("AuthError", "Credenciales incorrectas. Intentelo de nuevo.");
            return View(authAttempt);
        }

        [Authorize]
        public ActionResult PreferencesIndex()
        {
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
            }
            return View("Preferences/Index");
        }


        [Authorize]
        public ActionResult UserPreferences()
        {
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
                //obtenemos los datos del usuario para enviarlo a la vista
                currentUser = dbcmd.GetUser(User.Identity.Name);
            }
            return View("Preferences/UserPreferences", currentUser);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserInfo(User newData)
        {
            //si el usuario cambio el correo
            if (User.Identity.Name.ToString() != newData.Email)
            {
                //verificamos que no exista
                if (dbcmd.EmailExist(newData.Email))
                {
                    ModelState.AddModelError("EmailExistError", "El correo ingresado ya existe.");
                    return View("SignUp", newData);
                }
            }

            //obtenemos los datos viejos
            currentUser = dbcmd.GetUser(User.Identity.Name.ToString());
            //enviamos la actualizacion
            dbcmd.UpdateUser(newData, currentUser);
            //forzamos al usuario a iniciar sesion de nuevo
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateFlightPreferences(Preferences newPreferences)
        {
            currentUser = dbcmd.GetUser(User.Identity.Name.ToString());
            newPreferences.UserId = currentUser.UserId;

            //manejo de nulos con operador fusión nula.
            newPreferences.ReturnDate = newPreferences.ReturnDate ?? "";
            newPreferences.TravelClass = newPreferences.TravelClass ?? "";
            
            dbcmd.UpdateFlightPreferences(newPreferences);
            return Redirect("PreferencesIndex");
        }

        [Authorize]
        public ActionResult FlightPreferences()
        {
            Preferences preferences = new Preferences();
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
                currentUser = dbcmd.GetUser(User.Identity.Name);
                //obtenemos las preferencias del usuario para enviarlas a la lista
                preferences = dbcmd.GetFlightPreferences(currentUser);
            }
            return View("Preferences/FlightPreferences", preferences);
        }

        [Authorize]
        public ActionResult Reserves()
        {
            List<Reserve> ReserveList = new List<Reserve>();

            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
                currentUser = dbcmd.GetUser(User.Identity.Name);
                //obtenemos la lista de reservas para enviarlas a la lista
                ReserveList = dbcmd.GetReserve(currentUser);
            }
            return View(ReserveList);
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Travels()
        {
            Preferences userSettings = dbcmd.GetFlightPreferences(dbcmd.GetUser(User.Identity.Name));
            //obtenemos lo datos de la api en un objeto generico
            JObject flightData = await apicmd.GetFlights(userSettings);
            //creamos una lista de objetos tipo vuelo manejable; para enviarla a la lista
            List<Flight> flightList = flightcmd.BuildFlightList(flightData);

            if (User.Identity.Name != null)
            {
                Session["UserName"] = User.Identity.Name;
            }
            return View(flightList);
        }

        /*
            C O D I G O   P A R A   V I S T A   S I G N U P 
         */

        [HttpPost]
        public ActionResult RegistrarUsuario(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (!dbcmd.EmailExist(newUser.Email))
                {
                    //creamos el usuario en la base de datos
                    dbcmd.InsertUser(newUser);
                    //obtenemos sus datos
                    currentUser = dbcmd.GetUser(newUser.Email);
                    //creamos preferencia de vuelo por defecto para el nuevo usuario
                    dbcmd.InsertFlightPreferences(currentUser);
                    //redirigimos el usuario a la vista Login
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("EmailExistError", "El correo ingresado ya existe.");
                return View("SignUp", newUser);
            }
            return View("SignUp", newUser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddToCart(Flight theFlight)
        {
            currentUser = dbcmd.GetUser(User.Identity.Name);
            //insertamos la nueva reserva en base de datos
            dbcmd.InsertReserve(currentUser, theFlight);
            return Redirect("Travels");
        }

        /*
         C O D I G O  P A R A  C O N F I R M A R  R E S E R V A
        + Enviar Correo Electronico
        */

        [HttpPost]
        public ActionResult ConfirmarReserva(Reserve reservaCnfrmd)
        {
            dbcmd.UpdateReserve(reservaCnfrmd);
            //enviamos el correo con los datos del usuario y de la reserva que se ha confirmado
            mailcmd.SendEmail(dbcmd.GetUser(User.Identity.Name), reservaCnfrmd);
            return Redirect("Reserves");
        }
    }
}