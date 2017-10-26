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
            var today = DateTime.Now;
            
            int dayOfWeek = (int)today.DayOfWeek;

            int delta = (int)DayOfWeek.Monday - dayOfWeek;
            if (delta > 0)
            {
                delta -= 7;
            }
            List<DataPoint> datapoints = new List<DataPoint>{};
            while (delta <= 0)
            {
                var day = today.AddDays(delta);
                var dailyMeal = refeicoes.FirstOrDefault(p => p.Data == day);
                if(dailyMeal == null)
                {
                    datapoints.Add(new DataPoint(day.ToString("dddd"), null));
                }
                else
                {
                    datapoints.Add(new DataPoint(day.ToString("dddd"),dailyMeal.Total_Kcal));
                }
                delta++;
            }

            while (datapoints.Count < 7)
            {
                today = today.AddDays(1);
                datapoints.Add(new DataPoint(today.ToString("dddd"), null));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints);
            return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());


            //switch (day) {
            //    case 0:

            //        var Tdomingo = today.AddDays(-1);
            //        var Tdomingo1 = today.AddDays(-2);
            //        var Tdomingo2 = today.AddDays(-3);
            //        var Tdomingo3 = today.AddDays(-4);
            //        var Tdomingo4 = today.AddDays(-5);
            //        var Tdomingo5 = today.AddDays(-6);
            //        var Domingo = refeicoes.FirstOrDefault(p => p.Data == today);
            //        var Segunda = refeicoes.FirstOrDefault(p => p.Data == Tdomingo5);
            //        var Terca = refeicoes.FirstOrDefault(p => p.Data == Tdomingo4);
            //        var Quarta = refeicoes.FirstOrDefault(p => p.Data == Tdomingo3);
            //        var Quinta = refeicoes.FirstOrDefault(p => p.Data == Tdomingo2);
            //        var Sexta = refeicoes.FirstOrDefault(p => p.Data == Tdomingo1);
            //        var Sabado = refeicoes.FirstOrDefault(p => p.Data == Tdomingo);
            //        List<DataPoint> datapoints6 = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda.Total_Kcal),
            //                new DataPoint("Terca", Terca.Total_Kcal),
            //                new DataPoint("Quarta", Quarta.Total_Kcal),
            //                new DataPoint("Quinta", Quinta.Total_Kcal),
            //                new DataPoint("Sexta", Sexta.Total_Kcal),
            //                new DataPoint("Sabado", Sabado.Total_Kcal),
            //                new DataPoint("Domingo", Domingo.Total_Kcal),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints6);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 1:
            //        var Segunda1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        List<DataPoint> datapoints = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda1.Total_Kcal),
            //                new DataPoint("Terca", null),
            //                new DataPoint("Quarta", null),
            //                new DataPoint("Quinta", null),
            //                new DataPoint("Sexta", null),
            //                new DataPoint("Sabado", null),
            //                new DataPoint("Domingo", null),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 2:
            //        var Tterca = today.AddDays(-1);
            //        var Segunda2 = refeicoes.FirstOrDefault(p => p.Data == Tterca);
            //        var Terca1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        List<DataPoint> datapoints1 = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda2.Total_Kcal),
            //                new DataPoint("Terca", Terca1.Total_Kcal),
            //                new DataPoint("Quarta", null),
            //                new DataPoint("Quinta", null),
            //                new DataPoint("Sexta", null),
            //                new DataPoint("Sabado", null),
            //                new DataPoint("Domingo", null),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints1);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 3:
            //        var Tquarta = today.AddDays(-1);
            //        var Tquarta1 = today.AddDays(-2);
            //        var Segunda3 = refeicoes.FirstOrDefault(p => p.Data == Tquarta1);
            //        var Terca2 = refeicoes.FirstOrDefault(p => p.Data == Tquarta);
            //        var Quarta1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        List<DataPoint> datapoints2 = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda3.Total_Kcal),
            //                new DataPoint("Terca", Terca2.Total_Kcal),
            //                new DataPoint("Quarta", Quarta1.Total_Kcal),
            //                new DataPoint("Quinta", null),
            //                new DataPoint("Sexta", null),
            //                new DataPoint("Sabado", null),
            //                new DataPoint("Domingo", null),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints2);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 4:
            //        var Tquinta = today.AddDays(-1);
            //        var Tquinta1 = today.AddDays(-2);
            //        var Tquinta2 = today.AddDays(-3);
            //        var Segunda4 = refeicoes.FirstOrDefault(p => p.Data == Tquinta2);
            //        var Terca3 = refeicoes.FirstOrDefault(p => p.Data == Tquinta1);
            //        var Quarta2 = refeicoes.FirstOrDefault(p => p.Data == Tquinta);
            //        var Quinta1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        if(Quinta1 == null)
            //        {
            //            if(Quarta2 == null)
            //            {
            //                if(Terca3 == null)
            //                {
            //                    if(Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //                else
            //                {
            //                    if(Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if(Terca3 == null)
            //                {
            //                    if(Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //                else
            //                {
            //                    if(Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", null),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //            }
            //        }
            //        else if (Quinta1 != null)
            //        {
            //            if (Quarta2 == null)
            //            {
            //                if (Terca3 == null)
            //                {
            //                    if (Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //                else
            //                {
            //                    if (Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", null),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (Terca3 == null)
            //                {
            //                    if (Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", null),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //                else
            //                {
            //                    if (Segunda4 == null)
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", null),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                    else
            //                    {
            //                        List<DataPoint> datapoint3 = new List<DataPoint>
            //                        {
            //                            new DataPoint("Segunda", Segunda4.Total_Kcal),
            //                            new DataPoint("Terca", Terca3.Total_Kcal),
            //                            new DataPoint("Quarta", Quarta2.Total_Kcal),
            //                            new DataPoint("Quinta", Quinta1.Total_Kcal),
            //                            new DataPoint("Sexta", null),
            //                            new DataPoint("Sabado", null),
            //                            new DataPoint("Domingo", null),
            //                        };
            //                        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoint3);
            //                    }
            //                }
            //            }
            //        }
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 5:
            //        var Tsexta = today.AddDays(-1);
            //        var Tsexta1 = today.AddDays(-2);
            //        var Tsexta2 = today.AddDays(-3);
            //        var Tsexta3 = today.AddDays(-4);
            //        var Segunda5 = refeicoes.FirstOrDefault(p => p.Data == Tsexta3);
            //        var Terca4 = refeicoes.FirstOrDefault(p => p.Data == Tsexta2);
            //        var Quarta3 = refeicoes.FirstOrDefault(p => p.Data == Tsexta1);
            //        var Quinta2 = refeicoes.FirstOrDefault(p => p.Data == Tsexta);
            //        var Sexta1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        List<DataPoint> datapoints4 = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda5.Total_Kcal),
            //                new DataPoint("Terca", Terca4.Total_Kcal),
            //                new DataPoint("Quarta", Quarta3.Total_Kcal),
            //                new DataPoint("Quinta", Quinta2.Total_Kcal),
            //                new DataPoint("Sexta", Sexta1.Total_Kcal),
            //                new DataPoint("Sabado", null),
            //                new DataPoint("Domingo", null),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints4);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //    case 6:
            //        var Tsabado = today.AddDays(-1);
            //        var Tsabado1 = today.AddDays(-2);
            //        var Tsabado2 = today.AddDays(-3);
            //        var Tsabado3 = today.AddDays(-4);
            //        var Tsabado4 = today.AddDays(-5);
            //        var Segunda6 = refeicoes.FirstOrDefault(p => p.Data == Tsabado4);
            //        var Terca5 = refeicoes.FirstOrDefault(p => p.Data == Tsabado3);
            //        var Quarta4 = refeicoes.FirstOrDefault(p => p.Data == Tsabado2);
            //        var Quinta3 = refeicoes.FirstOrDefault(p => p.Data == Tsabado1);
            //        var Sexta2 = refeicoes.FirstOrDefault(p => p.Data == Tsabado);
            //        var Sabado1 = refeicoes.FirstOrDefault(p => p.Data == today);
            //        List<DataPoint> datapoints5 = new List<DataPoint>
            //            {
            //                new DataPoint("Segunda", Segunda6.Total_Kcal),
            //                new DataPoint("Terca", Terca5.Total_Kcal),
            //                new DataPoint("Quarta", Quarta4.Total_Kcal),
            //                new DataPoint("Quinta", Quinta3.Total_Kcal),
            //                new DataPoint("Sexta", Sexta2.Total_Kcal),
            //                new DataPoint("Sabado", Sabado1.Total_Kcal),
            //                new DataPoint("Domingo", null),
            //        };
            //        ViewBag.DataPoints = JsonConvert.SerializeObject(datapoints5);
            //        return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
            //}
            //return View(objectivoes.Where(o => o.UserID == userId && o.Data_fim == null).ToList());
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
