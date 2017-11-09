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
using System.Data.SqlClient;

namespace HealthyProject.Controllers
{
    public class RefeicoesController : Controller
    {
        private HealthyEntities db = new HealthyEntities();

        // GET: Refeicoes
        public ActionResult Index(DateTime? dateInput)
        {
            if (dateInput == null)
            {
                var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == DateTime.Today);
                return View(refeicoes);

            }
            else
            {
                var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == dateInput);
                return View(refeicoes);
            }
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
        public ActionResult Indexx(DateTime? dateInput)
        {
            if (dateInput == null)
            {
                var refeicoes1 = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == DateTime.Today);
                return View(refeicoes1);

            }
            else
            {
                var refeicoes = db.Refeicoes.Include(r => r.RegistoDiario).Where(d => d.Data == dateInput);
                return View(refeicoes);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefeicaoID,RegistoID,Data,Tipo")] Refeico refeico)
        {
            if (ModelState.IsValid)
            {
                double counterkcal = 0, counterproteinas = 0, countergordura = 0, counterhc = 0;
                var registo = db.RegistoDiarios.FirstOrDefault(p => p.Data == DateTime.Today);
                if (registo == null)
                {
                    RegistoDiario registo1 = new RegistoDiario();
                    var refeicoes = db.RefeicaoPratos.Include(i => i.Refeico).Where(q => q.Refeico.RegistoID == registo1.RegistoID);
                    var user = Convert.ToInt32(User.Identity.GetUserId());
                    Objectivo objectivo = db.Objectivoes.FirstOrDefault(o => o.UserID == user && o.Data_fim == null);
                    if (objectivo != null)
                    {
                        registo1.ObjectivoID = objectivo.ObjectivoID;
                        db.RegistoDiarios.Add(registo1);
                        foreach (RefeicaoPrato i in refeicoes)
                        {
                            counterkcal += (i.Dose / 100) * i.Prato.Kcal;
                            counterproteinas += (i.Dose / 100) * i.Prato.Proteinas;
                            countergordura += (i.Dose / 100) * i.Prato.Gordura;
                            counterhc += (i.Dose / 100) * i.Prato.HidCarbono;
                        }
                        refeico.RegistoID = registo1.RegistoID;
                        db.Refeicoes.Add(refeico);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Alert2"] = "Ainda não existe nenhum objectivo. Por favor, crie um objectivo primeiro.";

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var user = Convert.ToInt32(User.Identity.GetUserId());
                    var refeicoes = db.RefeicaoPratos.Include(i => i.Refeico).Where(q => q.Refeico.RegistoID == registo.RegistoID);
                    Objectivo objectivo = db.Objectivoes.FirstOrDefault(o => o.UserID == user && o.Data_fim == null);
                    if (objectivo != null)
                    {
                        registo.ObjectivoID = objectivo.ObjectivoID;

                        foreach (RefeicaoPrato i in refeicoes)
                        {
                            counterkcal += (i.Dose / 100) * i.Prato.Kcal;
                            counterproteinas += (i.Dose / 100) * i.Prato.Proteinas;
                            countergordura += (i.Dose / 100) * i.Prato.Gordura;
                            counterhc += (i.Dose / 100) * i.Prato.HidCarbono;
                        }
                        db.RegistoDiarios.Add(registo);
                        refeico.RegistoID = registo.RegistoID;
                        db.Refeicoes.Add(refeico);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Alert2"] = "Ainda não existe nenhum objectivo. Por favor, crie um objectivo primeiro.";

                        return RedirectToAction("Index");
                    }
                }
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


        public ActionResult NutriInfo(int RefeicaoID)
        {
            SqlParameter refID = new SqlParameter("@RefeicaoID", RefeicaoID);
            IList<CounterInfoRefeicao_Result> NutriInfo = db.Database.SqlQuery<CounterInfoRefeicao_Result>("dbo.CounterInfoRefeicao @RefeicaoID", refID).ToList<CounterInfoRefeicao_Result>();
            return View(NutriInfo);  //Andre diz para usar PartialView
        }

        public ActionResult KcalInfo(int RefeicaoID)
        {
            SqlParameter refID = new SqlParameter("@RefeicaoID", RefeicaoID);
            IList<CounterKcalRefeicao_Result> KcalInfo = db.Database.SqlQuery<CounterKcalRefeicao_Result>("dbo.CounterInfoRefeicao @RefeicaoID", refID).ToList<CounterKcalRefeicao_Result>();
            ViewBag.KcalInfo = KcalInfo;
            return View();
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