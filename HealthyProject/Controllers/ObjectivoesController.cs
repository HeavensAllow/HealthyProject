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
using System.Data.SqlClient;

namespace HealthyProject.Controllers
{
    public class ObjectivoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Objectivoes
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if (user.Nome == null)
            {
                ViewBag.SemDados = "Nao tem dados inseridos";
                return View();
            }
            var objectivoes = db.Objectivoes.Include(o => o.Utilizador).Where(o => o.UserID == userId);
            Objectivo objectivo = objectivoes.FirstOrDefault(o => o.UserID == userId && o.Data_fim == null);
            var refeicoes = db.RegistoDiarios.Include(i => i.Objectivo).Where(o => o.Objectivo.ObjectivoID == objectivo.ObjectivoID);
            RegistoPeso peso = db.RegistoPesoes.FirstOrDefault(o => o.User_ID == userId && o.Data == DateTime.Today);
            var today = DateTime.Now;
            double counter = 0;
            double counter2 = 0;
            if (objectivo != null)
            {
                int dayOfWeek = (int)today.DayOfWeek;
                int delta = (int)DayOfWeek.Monday - dayOfWeek;
                if (delta > 0)
                {
                    delta -= 7;
                }
                List<DataPoint> datapoints = new List<DataPoint> { };
                List<DataPoint> intake = new List<DataPoint> { };
                while (delta <= 0)
                {
                    var day = today.AddDays(delta);
                    var dailyMeal = refeicoes.FirstOrDefault(p => p.Data == day);
                    if (dailyMeal == null)
                    {
                        datapoints.Add(new DataPoint(counter, null, day.ToString("dddd")));
                        intake.Add(new DataPoint(counter, objectivo.Intake_diarioR, day.ToString("dddd")));
                        counter++;
                    }
                    else
                    {
                        datapoints.Add(new DataPoint(counter, dailyMeal.Total_Kcal, day.ToString("dddd")));
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
                List<DataPoint> registod = new List<DataPoint> { };
                counter = 0;
                foreach (RegistoDiario d in refeicoes)
                {
                    registod.Add(new DataPoint(counter, d.Total_Kcal, d.Data.ToString("dd-MM-yyyy")));
                    counter++;
                }
                if (registod.Count() > 0)
                {
                    ViewBag.Registos = JsonConvert.SerializeObject(registod);
                }
            }

            var total = db.RegistoPesoes.Include(o => o.Utilizador).Where(i => i.User_ID == userId);
            List<DataPoint> kg = new List<DataPoint> { };
            counter = 0;
            foreach (RegistoPeso i in total)
            {
                kg.Add(new DataPoint(counter, i.Peso, i.Data.Value.ToString("dd-MM-yyyy")));
                counter++;
            }
            List<DataPoint> objectiv = new List<DataPoint> { };
            counter = 0;
            foreach (Objectivo i in objectivoes)
            {
                if (i.Data_fim != null && i.Peso_Final != i.Peso_objectivo)
                {
                    counter2++;
                }
                if (i.Data_fim != null && i.Peso_Final == i.Peso_objectivo)
                {
                    counter++;
                }
            }

            objectiv.Add(new DataPoint(0, counter, "Concluidos com sucesso"));
            objectiv.Add(new DataPoint(1, counter2, "Terminados sem sucesso"));
            List<DataPoint> ordemCount = new List<DataPoint> { };
            counter = 0;
            SqlParameter Id = new SqlParameter("@UserID", userId);
            IList<Favorites> refeicao = db.Database.SqlQuery<Favorites>("Top5 @UserID", Id).ToList();
            List<DataPoint> favoritos = new List<DataPoint> { };
            for (int i = 0; i < refeicao.Count(); i++)
            {
                var refeicaoID = refeicao[i].PratoID;
                refeicao[i].Nome = db.Pratos.FirstOrDefault(o => o.PratosID == refeicaoID).Nome;
                favoritos.Add(new DataPoint(i, refeicao[i].Ocorrencias, refeicao[i].Nome));
            }
            counter = 0;
            counter2 = 0;
            List<DataPoint> dias = new List<DataPoint> { };
            var listaobjectivos = db.Objectivoes.Where(u => u.UserID == userId).OrderBy(q => q.Data_inicio);
            for (int i = 0; i < listaobjectivos.Count(); i++)
            {
                DateTime inicio = (DateTime)listaobjectivos.ToList()[i].Data_inicio;
                var fim = listaobjectivos.ToList()[i].Data_fim;

                if (inicio != null && fim != null)
                {
                    DateTime fim1 = (DateTime)listaobjectivos.ToList()[i].Data_fim;
                    counter += fim1.Subtract(inicio).TotalDays;
                    if (i + 1 < listaobjectivos.Count())
                    {
                        var novo = listaobjectivos.ToList()[i + 1].Data_inicio;
                        if (novo != null)
                        {
                            DateTime novo1 = (DateTime)listaobjectivos.ToList()[i + 1].Data_inicio;
                            counter2 += novo1.Subtract((DateTime)fim1).TotalDays;
                        }
                    }
                }
                if (inicio != null && fim == null)
                {
                    counter += inicio.Subtract(DateTime.Today).TotalDays;
                }
            }
            dias.Add(new DataPoint(0, counter, "Dias com objectivos"));
            dias.Add(new DataPoint(1, counter2, "Dias sem objectivos"));
            ViewBag.Total = JsonConvert.SerializeObject(kg);
            ViewBag.Object = JsonConvert.SerializeObject(objectiv);
            if (favoritos.Count() > 0)
            {
                ViewBag.Favoritos = JsonConvert.SerializeObject(favoritos);
            }
            if (dias.Count() > 0)
            {
                ViewBag.Count = JsonConvert.SerializeObject(dias);
            }
            if (objectivo != null)
            {
                if (objectivo.Data_inicio > DateTime.Now)
                {
                    DateTime start = (DateTime)objectivo.Data_inicio;
                    ViewBag.TooSoon = Convert.ToInt32(start.Subtract(DateTime.Now).TotalDays);
                    return View();
                }
            }

            if (objectivo == null)
            {
                ViewBag.Sem = "Sem objectivo";
                return View();
            }
            if ((int)DateTime.Now.DayOfWeek == 4 && peso == null)
            {
                ViewBag.Teste = "Por favor indique o seu novo peso";
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult GetPeso()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador user = new Utilizador();
            user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            return PartialView("_Peso", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetPeso([Bind(Include = "UserID,Nome,Genero,Data_nascimento,Peso,Altura,Actividade_fisica,Nr_horas_sono,Nr_refeicoes,Habitos_alcoolicos,MMuscular,MGorda")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(User.Identity.GetUserId());
                Utilizador mudar = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
                mudar.Peso = utilizador.Peso;
                db.Entry(mudar).State = EntityState.Modified;
                var actual = db.Objectivoes.FirstOrDefault(p => p.UserID == userId && p.Data_fim == null);
                UtilizadorsController x = new UtilizadorsController();
                RegistoPeso peso = new RegistoPeso();
                var age = x.GetAge((DateTime)mudar.Data_nascimento);
                double Actividade = 0;
                if (utilizador.Genero == "F")
                {
                    switch (utilizador.Actividade_fisica)
                    {
                        case 1:
                            Actividade = 1;
                            break;
                        case 2:
                            Actividade = 1.12;
                            break;
                        case 3:
                            Actividade = 1.27;
                            break;
                        case 4:
                            Actividade = 1.45;
                            break;
                    }
                    actual.Intake_diarioA = Convert.ToInt32(354 - (6.91 * age) + (Actividade * (9.36 * mudar.Peso + (726 * (mudar.Altura / 100)))));
                    int Intake_diarioR = Convert.ToInt32(354 - (6.91 * age) + (Actividade * (9.36 * actual.Peso_objectivo + (726 * (mudar.Altura / 100)))));
                    if (Intake_diarioR > 1800 & actual.Intake_diarioA - Intake_diarioR > 500)
                    {
                        actual.Intake_diarioR = actual.Intake_diarioA - 500;
                    }
                    else if (Intake_diarioR < 1800 & actual.Intake_diarioA - Intake_diarioR < 500)
                    {
                        actual.Intake_diarioR = 1800;
                    }
                    else
                    {
                        actual.Intake_diarioR = Intake_diarioR;
                    }
                }
                else
                {
                    switch (utilizador.Actividade_fisica)
                    {
                        case 1:
                            Actividade = 1;
                            break;
                        case 2:
                            Actividade = 1.11;
                            break;
                        case 3:
                            Actividade = 1.25;
                            break;
                        case 4:
                            Actividade = 1.48;
                            break;
                    }
                    actual.Intake_diarioA = Convert.ToInt32(662 - (9.53 * age) + (Actividade * (15.91 * mudar.Peso + (539.6 * (mudar.Altura / 100)))));
                    int Intake_diarioR = Convert.ToInt32(662 - (9.53 * age) + (Actividade * (15.91 * actual.Peso_objectivo + (539.6 * (mudar.Altura / 100)))));
                    if (Intake_diarioR > 1800 & actual.Intake_diarioA - Intake_diarioR > 500)
                    {
                        actual.Intake_diarioR = actual.Intake_diarioA - 500;
                    }
                    else if (Intake_diarioR < 1800 & actual.Intake_diarioA - Intake_diarioR < 500)
                    {
                        actual.Intake_diarioR = 1800;
                    }
                    else
                    {
                        actual.Intake_diarioR = Intake_diarioR;
                    }
                }
                db.Entry(actual).State = EntityState.Modified;
                peso.Peso = utilizador.Peso;
                peso.Data = DateTime.Today;
                peso.User_ID = userId;
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
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if (user.Nome == null)
            {
                ViewBag.SemDados = "Nao tem dados inseridos";
                return View();
            }
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
    
            Utilizador user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if (user.Nome == null)
            {
                ViewBag.SemDados = "Nao tem dados inseridos";
                return View();
            }


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
            var error = false;
            if(objectivo.Data_inicio == null)
            {
                ModelState.AddModelError("Data de Início", "Por favor introduza a data a dar início ao objectivo");
                error = true;
            }
            if(objectivo.Peso_objectivo == null)
            {
                ModelState.AddModelError("Peso objectivo", "Por favor introduza o peso objectivo a atingir");
                error = true;
            }
            if(error == true)
            {
                return View();
            }
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
                    double Actividade = 0;
                    if (utilizador.Genero == "F")
                    {
                        switch (utilizador.Actividade_fisica)
                        {
                            case 1:
                                Actividade = 1;
                                break;
                            case 2:
                                Actividade = 1.12;
                                break;
                            case 3:
                                Actividade = 1.27;
                                break;
                            case 4:
                                Actividade = 1.45;
                                break;
                        }
                        objectivo.Intake_diarioA = Convert.ToInt32(354 - (6.91 * age) + (Actividade * (9.36 * utilizador.Peso + (726 * (utilizador.Altura / 100)))));
                        int Intake_diarioR = Convert.ToInt32(354 - (6.91 * age) + (Actividade * (9.36 * objectivo.Peso_objectivo + (726 * (utilizador.Altura / 100)))));
                        if (Intake_diarioR > 1800 & objectivo.Intake_diarioA - Intake_diarioR > 500)
                        {
                            objectivo.Intake_diarioR = objectivo.Intake_diarioA - 500;
                        }
                        else if (Intake_diarioR < 1800 & objectivo.Intake_diarioA - Intake_diarioR < 500)
                        {
                            objectivo.Intake_diarioR = 1800;
                        }
                        else
                        {
                            objectivo.Intake_diarioR = Intake_diarioR;
                        }
                    }
                    else
                    {
                        switch (utilizador.Actividade_fisica)
                        {
                            case 1:
                                Actividade = 1;
                                break;
                            case 2:
                                Actividade = 1.11;
                                break;
                            case 3:
                                Actividade = 1.25;
                                break;
                            case 4:
                                Actividade = 1.48;
                                break;
                        }
                        objectivo.Intake_diarioA = Convert.ToInt32(662 - (9.53 * age) + (Actividade * (15.91 * utilizador.Peso + (539.6 * (utilizador.Altura / 100)))));
                        int Intake_diarioR = Convert.ToInt32(662 - (9.53 * age) + (Actividade * (15.91 * objectivo.Peso_objectivo + (539.6 * (utilizador.Altura / 100)))));
                        if (Intake_diarioR > 1800 & objectivo.Intake_diarioA - Intake_diarioR > 500)
                        {
                            objectivo.Intake_diarioR = objectivo.Intake_diarioA - 500;
                        }
                        else if (Intake_diarioR < 1800 & objectivo.Intake_diarioA - Intake_diarioR < 500)
                        {
                            objectivo.Intake_diarioR = 1800;
                        }
                        else
                        {
                            objectivo.Intake_diarioR = Intake_diarioR;
                        }
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
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            Utilizador user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if (user.Nome == null)
            {
                ViewBag.SemDados = "Nao tem dados inseridos";
                return View();
            }
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
            Utilizador user = db.Utilizadors.FirstOrDefault(o => o.UserID == userId);
            if (user.Nome == null)
            {
                ViewBag.SemDados = "Nao tem dados inseridos";
                return View();
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
        public ActionResult DeleteConfirmed(int? id)
        {

            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var utilizador = db.Utilizadors.FirstOrDefault(i => i.UserID == userId);
            Objectivo objectivo = db.Objectivoes.Include(o => o.Utilizador).FirstOrDefault(i => i.UserID == userId && i.Data_fim == null);
            objectivo.Data_fim = DateTime.Today;
            objectivo.Peso_Final = utilizador.Peso;
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
