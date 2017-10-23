using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyProject.Models;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;

namespace HealthyProject.Controllers
{
    public class ObjectivoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Objectivoes
        public ActionResult Index()
        {
            var objectivoes = db.Objectivoes.Include(o => o.Utilizador);
            return View(objectivoes.ToList());
        }

        // GET: Objectivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objectivo objectivo = db.Objectivoes.Find(id);
            if (objectivo == null)
            {
                return HttpNotFound();
            }
            return View(objectivo);
        }

        // GET: Objectivoes/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Utilizadors, "UserID", "Nome");
            return View();
        }

        // POST: Objectivoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjectivoID,Data_inicio,Peso_objectivo")] Objectivo objectivo)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                objectivo.UserID = Convert.ToInt32(userId);
                Utilizador utilizador = db.Utilizadors.Find(userId);
                UtilizadorsController x = new UtilizadorsController();
                var age = x.GetAge((DateTime)utilizador.Data_nascimento);
                if (utilizador.Genero == "Feminino")
                {
                    objectivo.Intake_diarioA = Convert.ToInt32(354 - (6.91 * age) + (utilizador.Actividade_fisica * (9.36 * utilizador.Peso + 726 * (utilizador.Altura / 100))));
                    objectivo.Intake_diarioR = Convert.ToInt32(354 - (6.91 * age) + (utilizador.Actividade_fisica * (9.36 * objectivo.Peso_objectivo + 726 * (utilizador.Altura / 100))));
                }
                else {
                    objectivo.Intake_diarioA = Convert.ToInt32(662 - (9.53 * age) + (utilizador.Actividade_fisica * (15.91 * utilizador.Peso + 539.6 * (utilizador.Altura / 100))));
                    objectivo.Intake_diarioA = Convert.ToInt32(662 - (9.53 * age) + (utilizador.Actividade_fisica * (15.91 * objectivo.Peso_objectivo + 539.6 * (utilizador.Altura / 100))));
                }
                db.Objectivoes.Add(objectivo);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserID = new SelectList(db.Utilizadors, "UserID", "Nome", objectivo.UserID);
            return View(objectivo);
        }

        // GET: Objectivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objectivo objectivo = db.Objectivoes.Find(id);
            if (objectivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Utilizadors, "UserID", "Nome", objectivo.UserID);
            return View(objectivo);
        }

        // POST: Objectivoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjectivoID,UserID,Data_inicio,Data_fim,Peso_objectivo,Intake_diario")] Objectivo objectivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objectivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Utilizadors, "UserID", "Nome", objectivo.UserID);
            return View(objectivo);
        }

        // GET: Objectivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objectivo objectivo = db.Objectivoes.Find(id);
            if (objectivo == null)
            {
                return HttpNotFound();
            }
            return View(objectivo);
        }

        // POST: Objectivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objectivo objectivo = db.Objectivoes.Find(id);
            db.Objectivoes.Remove(objectivo);
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

        public ActionResult MyChart()
        {
            var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                .AddTitle("Chart Title")
                .AddSeries(
                    name: "Employee",
                    xValue: new[] { "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sabado", "Domingo" },
                    yValues: new[] { "2", "6", "4", "5", "3", "7", "2" })
                .Write("png");
            return null;
        }
    }
}
