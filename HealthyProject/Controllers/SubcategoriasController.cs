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
    [Authorize(Roles = "Admin")]
    public class SubcategoriasController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Subcategorias
        public ActionResult Index()
        {
            var subcategorias = db.Subcategorias.Include(s => s.Categoria);
            return View(subcategorias.ToList());
        }


        //GET
        
        public ActionResult CreateSubcategoria()
        {
            ViewBag.Categorias = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        ////POST
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubcategoria([Bind(Include = "CategoriaID,Nome")] Subcategoria newSubcategoria)
        {
            ViewBag.Categorias = new SelectList(db.Categorias, "CategoriaID", "Nome");
            if (newSubcategoria.Nome == null)
            {
                ModelState.AddModelError("Nome", "Por favor insira o nome");
                return View();
            }
            db.Subcategorias.Add(newSubcategoria);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", subcategoria.CategoriaID);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcategoriaID,Nome,CategoriaID")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", subcategoria.CategoriaID);
            return View(subcategoria);
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
