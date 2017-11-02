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
    public class RefeicaoBebidasController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: RefeicaoBebidas
        public ActionResult Index()
        {
            var refeicaoBebidas = db.RefeicaoBebidas.Include(r => r.Bebida).Include(r => r.Refeico);
            return View(refeicaoBebidas.ToList());
        }

        // GET: RefeicaoBebidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoBebida refeicaoBebida = db.RefeicaoBebidas.Find(id);
            if (refeicaoBebida == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoBebida);
        }

        // GET: RefeicaoBebidas/Create
        public ActionResult Create(int RefeicaoID)
        {
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo");
            ViewBag.BebidaID = new SelectList(db.Bebidas.OrderBy(b => b.Nome), "BebidasID", "Nome");
            return View();
        }

        // POST: RefeicaoBebidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BebidaID,RefeicaoID,Quantidade")] RefeicaoBebida refeicaoBebida)
        {
            if (ModelState.IsValid)
            {
                db.RefeicaoBebidas.Add(refeicaoBebida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BebidaID = new SelectList(db.Bebidas, "BebidasID", "Categoria", refeicaoBebida.BebidaID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoBebida.RefeicaoID);
            return View(refeicaoBebida);
        }

        // GET: RefeicaoBebidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoBebida refeicaoBebida = db.RefeicaoBebidas.Find(id);
            if (refeicaoBebida == null)
            {
                return HttpNotFound();
            }
            ViewBag.BebidaID = new SelectList(db.Bebidas, "BebidasID", "Categoria", refeicaoBebida.BebidaID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoBebida.RefeicaoID);
            return View(refeicaoBebida);
        }

        // POST: RefeicaoBebidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BebidaID,RefeicaoID,Quantidade")] RefeicaoBebida refeicaoBebida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refeicaoBebida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BebidaID = new SelectList(db.Bebidas, "BebidasID", "Categoria", refeicaoBebida.BebidaID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoBebida.RefeicaoID);
            return View(refeicaoBebida);
        }

        // GET: RefeicaoBebidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoBebida refeicaoBebida = db.RefeicaoBebidas.Find(id);
            if (refeicaoBebida == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoBebida);
        }

        // POST: RefeicaoBebidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefeicaoBebida refeicaoBebida = db.RefeicaoBebidas.Find(id);
            db.RefeicaoBebidas.Remove(refeicaoBebida);
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
