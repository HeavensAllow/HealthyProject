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
    public class RefeicaoPratoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: RefeicaoPratoes
        public ActionResult Index()
        {
            var refeicaoPratos = db.RefeicaoPratos.Include(r => r.Prato).Include(r => r.Refeico);
            return View(refeicaoPratos.ToList());
        }

        // GET: RefeicaoPratoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoPrato refeicaoPrato = db.RefeicaoPratos.Find(id);
            if (refeicaoPrato == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoPrato);
        }

        // GET: RefeicaoPratoes/Create
        public ActionResult Create()
        {
            ViewBag.PratoID = new SelectList(db.Pratos, "PratosID", "Nome");
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo");
            return View();
        }

        // POST: RefeicaoPratoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefeicaoID,PratoID,Dose")] RefeicaoPrato refeicaoPrato)
        {
            if (ModelState.IsValid)
            {
                db.RefeicaoPratos.Add(refeicaoPrato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PratoID = new SelectList(db.Pratos, "PratosID", "Nome", refeicaoPrato.PratoID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoPrato.RefeicaoID);
            return View(refeicaoPrato);
        }

        // GET: RefeicaoPratoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoPrato refeicaoPrato = db.RefeicaoPratos.Find(id);
            if (refeicaoPrato == null)
            {
                return HttpNotFound();
            }
            ViewBag.PratoID = new SelectList(db.Pratos, "PratosID", "Nome", refeicaoPrato.PratoID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoPrato.RefeicaoID);
            return View(refeicaoPrato);
        }

        // POST: RefeicaoPratoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefeicaoID,PratoID,Dose")] RefeicaoPrato refeicaoPrato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refeicaoPrato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PratoID = new SelectList(db.Pratos, "PratosID", "Nome", refeicaoPrato.PratoID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoPrato.RefeicaoID);
            return View(refeicaoPrato);
        }

        // GET: RefeicaoPratoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoPrato refeicaoPrato = db.RefeicaoPratos.Find(id);
            if (refeicaoPrato == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoPrato);
        }

        // POST: RefeicaoPratoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefeicaoPrato refeicaoPrato = db.RefeicaoPratos.Find(id);
            db.RefeicaoPratos.Remove(refeicaoPrato);
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
