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
    public class RefeicoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Refeicoes
        public ActionResult Index()
        {


            var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario);

            return View(refeicoes.ToList());
        }
       
      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeico refeico = db.Refeicoes.Find(id);
            if (refeico == null)
            {
                return HttpNotFound();
            }
            return View(refeico);
        }

        // GET: Refeicoes/Create
        public ActionResult Create()
        {
            ViewBag.RegistoID = new SelectList(db.RegistoDiarios, "RegistoID", "RegistoID");
            return View();
        }

        // POST: Refeicoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Refeicoes

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string qqr)
        {
            string textoData = "26/10/2017";
            var dataFiltro = DateTime.Parse(textoData);
            var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == dataFiltro);

            return View(refeicoes.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefeicaoID,RegistoID,Data,Tipo")] Refeico refeico)
        {
            if (ModelState.IsValid)
            {
                db.Refeicoes.Add(refeico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegistoID = new SelectList(db.RegistoDiarios, "RegistoID", "RegistoID", refeico.RegistoID);
            return View(refeico);
        }

        // GET: Refeicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeico refeico = db.Refeicoes.Find(id);
            if (refeico == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegistoID = new SelectList(db.RegistoDiarios, "RegistoID", "RegistoID", refeico.RegistoID);
            return View(refeico);
        }

        // POST: Refeicoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefeicaoID,RegistoID,Data,Tipo")] Refeico refeico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refeico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegistoID = new SelectList(db.RegistoDiarios, "RegistoID", "RegistoID", refeico.RegistoID);
            return View(refeico);
        }

        // GET: Refeicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeico refeico = db.Refeicoes.Find(id);
            if (refeico == null)
            {
                return HttpNotFound();
            }
            return View(refeico);
        }

        // POST: Refeicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Refeico refeico = db.Refeicoes.Find(id);
            db.Refeicoes.Remove(refeico);
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
//// GET: Refeicoes
//public ActionResult Index()
//{
//    string textoData = "26/10/2017";
//    var dataFiltro = DateTime.Parse(textoData);
//    var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == dataFiltro);

//    return View(refeicoes.ToList());
//}