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
    public class RefeicaoIngredientesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: RefeicaoIngredientes
        public ActionResult Index()
        {
            var refeicaoIngredientes = db.RefeicaoIngredientes.Include(r => r.Ingrediente).Include(r => r.Refeico);
            return View(refeicaoIngredientes.ToList());
        }

        // GET: RefeicaoIngredientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.Find(id);
            if (refeicaoIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoIngrediente);
        }

        // GET: RefeicaoIngredientes/Create
        public ActionResult Create(int RefeicaoID)
        {
            var Refeicao = db.Refeicoes.FirstOrDefault(r => r.RefeicaoID == RefeicaoID);
            ViewBag.Refeicao = Refeicao;

            ViewBag.IngredienteID = new SelectList(db.Ingredientes, "IngredientesID", "Categoria");
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo");
            return View();
        }

        // POST: RefeicaoIngredientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredienteID,RefeicaoID,Quantidade")] RefeicaoIngrediente refeicaoIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.RefeicaoIngredientes.Add(refeicaoIngrediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredienteID = new SelectList(db.Ingredientes, "IngredientesID", "Categoria", refeicaoIngrediente.IngredienteID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoIngrediente.RefeicaoID);
            return View(refeicaoIngrediente);
        }

        // GET: RefeicaoIngredientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.Find(id);
            if (refeicaoIngrediente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredienteID = new SelectList(db.Ingredientes, "IngredientesID", "Categoria", refeicaoIngrediente.IngredienteID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoIngrediente.RefeicaoID);
            return View(refeicaoIngrediente);
        }

        // POST: RefeicaoIngredientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredienteID,RefeicaoID,Quantidade")] RefeicaoIngrediente refeicaoIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refeicaoIngrediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredienteID = new SelectList(db.Ingredientes, "IngredientesID", "Categoria", refeicaoIngrediente.IngredienteID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoIngrediente.RefeicaoID);
            return View(refeicaoIngrediente);
        }

        // GET: RefeicaoIngredientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.Find(id);
            if (refeicaoIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoIngrediente);
        }

        // POST: RefeicaoIngredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.Find(id);
            db.RefeicaoIngredientes.Remove(refeicaoIngrediente);
            db.SaveChanges();
            return RedirectToAction("Index");
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
