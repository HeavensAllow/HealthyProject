using HealthyProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace HealthyProject.Controllers
{
    public class HomeController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador utilizador = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if(utilizador != null)
            {
               if(utilizador.Nome != null)
                {
                    return View(utilizador);
                }
                else
                {
                    ViewBag.UserGuide = "Primeiro Log";
                }
            }
            return View(utilizador);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}