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

        public ActionResult Subcategorias(int?id)
        {
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            if(subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        public ActionResult Post(int?id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);

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
