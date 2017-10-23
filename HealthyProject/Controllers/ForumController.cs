using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyProject.Models;

namespace HealthyProject.Controllers
{
    public class ForumController : Controller
    {
        private HealthyEntities db = new HealthyEntities();


        public ActionResult Index()
        {
            var Categorias= db.Categorias.Include(c => c.Subcategorias);
            return View(Categorias.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
