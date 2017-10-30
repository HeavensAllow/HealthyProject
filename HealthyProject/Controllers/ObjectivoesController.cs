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
using Newtonsoft.Json;
using System.Dynamic;

namespace HealthyProject.Controllers
{
    public class ObjectivoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Objectivoes
        public ActionResult Index()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var objectivoes = db.Objectivoes.Include(o => o.Utilizador);
            var refeicoes = db.RegistoDiarios.Include(i => i.Objectivo);
            Objectivo objectivo = objectivoes.FirstOrDefault(o => o.UserID == userId && o.Data_fim == null);
            RegistoPeso peso = db.RegistoPesoes.FirstOrDefault(o => o.User_ID == userId && o.Data == DateTime.Today);
            var today = DateTime.Now;
            
            int dayOfWeek = (int)today.DayOfWeek;

            int delta = (int)DayOfWeek.Monday - dayOfWeek;
            if (delta > 0)
            {
                delta -= 7;
            }
            double counter = 0;
            List<DataPoint> datapoints = new List<DataPoint>{};
            List<DataPoint> intake = new List<DataPoint> { };
            while (delta <= 0)
            {
                var day = today.AddDays(delta);
                var dailyMeal = refeicoes.FirstOrDefault(p => p.Data == day);
                if(dailyMeal == null)
                {
                    datapoints.Add(new DataPoint(counter, null, day.ToString("dddd")));
                    intake.Add(new DataPoint(counter, objectivo.Intake_diarioR, day.ToString("dddd")));
                    counter++;
                }
                else
                {
                    datapoints.Add(new DataPoint(counter,dailyMeal.Total_Kcal, day.ToString("dddd")));
                    intake.Add(new DataPoint(counter, objectivo.Intake_diarioR, day.ToString("dddd")));
                    counter++;
                }
                delta++;
            }

            while (intake.Count < 7)
            {
                today = today.AddDays(1);
                datapoints.Add(new DataPoint(counter, null, today.ToString("dddd")));
                intake.Add(new DataPoint(counter, objectivo.Intake_diarioR, today.ToString("dddd")));
                counter++;
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints);
            ViewBag.IntakeR = JsonConvert.SerializeObject(intake);
            ViewBag.IMath = JsonConvert.SerializeObject(objectivo.Intake_diarioR);
            if((int)DateTime.Now.DayOfWeek == 1 && peso == null)
            {
                ViewBag.Teste = "Por favor indique o seu novo peso";
                return View();
            }
            else
            {
                return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            }
        }

        public ActionResult getPeso()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador user = new Utilizador();
            user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            return PartialView("_Peso", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getPeso([Bind(Include = "UserID,Nome,Genero,Data_nascimento,Peso,Altura,Actividade_fisica,Nr_horas_sono,Nr_refeicoes,Habitos_alcoolicos,MMuscular,MGorda")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizador).State = EntityState.Modified;
                RegistoPeso peso = new RegistoPeso();
                peso.Peso = utilizador.Peso;
                peso.Data = DateTime.Today;
                peso.User_ID = 10;
                db.RegistoPesoes.Add(peso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", utilizador.UserID);
            return View(utilizador);
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
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            //var objectivos = db.Objectivoes.Include(o => o.Utilizador);
            var actual = db.Objectivoes.FirstOrDefault(p => p.UserID == userId && p.Data_fim == null);
            //var procura = objectivoes.Where(p => p.UserID == userId);
            //var actual = procura.Where(l => l.Data_fim == null);
            if (actual == null)
            {
                ViewBag.UserID = new SelectList(db.Utilizadors, "UserID", "Nome");
                return View();
            }
            else
            {
                TempData["Alert"] = "Ja existe um objectivo activo. Por favor conclua o objectivo actual antes de iniciar um novo";
                return RedirectToAction("Index");
            }
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
                var userId = Convert.ToInt32(User.Identity.GetUserId());
                var actual = db.Objectivoes.FirstOrDefault(p => p.UserID == userId && p.Data_fim == null);
                if (actual == null)
                {
                    objectivo.UserID = Convert.ToInt32(userId);
                    Utilizador utilizador = db.Utilizadors.Find(userId);
                    UtilizadorsController x = new UtilizadorsController();
                    var age = x.GetAge((DateTime)utilizador.Data_nascimento);
                    if (utilizador.Genero == "Feminino")
                    {
                        objectivo.Intake_diarioA = Convert.ToInt32(354 - (6.91 * age) + (utilizador.Actividade_fisica * (9.36 * utilizador.Peso + 726 * (utilizador.Altura / 100))));
                        objectivo.Intake_diarioR = Convert.ToInt32(354 - (6.91 * age) + (utilizador.Actividade_fisica * (9.36 * objectivo.Peso_objectivo + 726 * (utilizador.Altura / 100))));
                    }
                    else
                    {
                        objectivo.Intake_diarioA = Convert.ToInt32(662 - (9.53 * age) + (utilizador.Actividade_fisica * (15.91 * utilizador.Peso + 539.6 * (utilizador.Altura / 100))));
                        objectivo.Intake_diarioR = Convert.ToInt32(662 - (9.53 * age) + (utilizador.Actividade_fisica * (15.91 * objectivo.Peso_objectivo + 539.6 * (utilizador.Altura / 100))));
                    }
                    db.Objectivoes.Add(objectivo);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["notice"] = "<script>alert('Ja existe um objectivo criado e por terminar')>;</script>";
                    return RedirectToAction("Index", "Objectivo");
                }
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
        public ActionResult Delete()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var objectivoes = db.Objectivoes.Include(o => o.Utilizador);
            var refeicoes = db.RegistoDiarios.Include(i => i.Objectivo);
            Objectivo objectivo = objectivoes.FirstOrDefault(o => o.UserID == userId && o.Data_fim == null);
            if (objectivo.ObjectivoID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
    }
}
