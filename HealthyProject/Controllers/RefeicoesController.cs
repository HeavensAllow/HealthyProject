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
            List<Contas> muitaComida = new List<Contas>();
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var objectivo = db.Objectivoes.FirstOrDefault(c => c.UserID == userId);
            var refeicao = db.Refeicoes.FirstOrDefault(c => c.RegistoDiario.Objectivo.UserID == userId);
            var prato = db.RefeicaoPratos.FirstOrDefault(c => c.Refeico.RegistoDiario.Objectivo.UserID == userId);
            if (dateInput == null)
            {
                var registo = db.RegistoDiarios.FirstOrDefault(c => c.Objectivo.UserID == userId && c.Data == DateTime.Today);
                if(registo != null)
                {
                    foreach (var tipo in registo.Refeicoes.OrderBy(c => c.RefeicaoID))
                    {
                        Contas comida = new Contas();

                        SqlParameter RegistoPratos = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterPratos_Result> registoPratos = db.Database.SqlQuery<CounterPratos_Result>("CounterPratos @RefeicaoID", RegistoPratos).ToList();
                        SqlParameter RegistoIngredientes = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterIngredientes_Result> registoIngredientes = db.Database.SqlQuery<CounterIngredientes_Result>("CounterIngredientes @RefeicaoID", RegistoIngredientes).ToList();
                        SqlParameter RegistoBebidas = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterBebidas_Result> registoBebidas = db.Database.SqlQuery<CounterBebidas_Result>("CounterBebidas @RefeicaoID", RegistoBebidas).ToList();

                        comida.Tipo = tipo.Tipo;
                        comida.ListaBebidas = tipo.RefeicaoBebidas;
                        comida.ListaPratos = tipo.RefeicaoPratos;
                        comida.ListaIngredientes = tipo.RefeicaoIngredientes;
                        comida.RefeicaoID = tipo.RefeicaoID;

                        comida.Total_kcal = (double)(registoBebidas[0].Kcal + registoIngredientes[0].Kcal + registoPratos[0].Kcal);
                        comida.Total_proteinas = (double)(registoBebidas[0].Proteinas + registoIngredientes[0].Proteinas + registoPratos[0].Proteinas);
                        comida.Total_gordura = (double)(registoBebidas[0].Gordura + registoIngredientes[0].Gordura + registoPratos[0].Gordura);
                        comida.Total_hidratos = (double)(registoBebidas[0].Hidratos + registoIngredientes[0].Hidratos + registoPratos[0].Hidratos);
                        muitaComida.Add(comida);
                    }
                }
                if (objectivo != null)
                {
                    if (refeicao == null)
                    {
                        ViewBag.UserGuide = "Next Step";
                    }
                    if (refeicao != null && prato == null)
                    {
                        ViewBag.UserGuide2 = "Next Step";
                    }
                }

                return View(muitaComida);

            }
            else
            {
                var registo = db.RegistoDiarios.FirstOrDefault(c => c.Objectivo.UserID == userId && c.Data == dateInput);
                if (registo != null)
                {
                    foreach (var tipo in registo.Refeicoes.OrderBy(c => c.RefeicaoID))
                    {
                        Contas comida = new Contas();
                        SqlParameter RegistoPratos = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterPratos_Result> registoPratos = db.Database.SqlQuery<CounterPratos_Result>("CounterPratos @RefeicaoID", RegistoPratos).ToList();
                        SqlParameter RegistoIngredientes = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterIngredientes_Result> registoIngredientes = db.Database.SqlQuery<CounterIngredientes_Result>("CounterIngredientes @RefeicaoID", RegistoIngredientes).ToList();
                        SqlParameter RegistoBebidas = new SqlParameter("@RefeicaoID", tipo.RefeicaoID);
                        IList<CounterBebidas_Result> registoBebidas = db.Database.SqlQuery<CounterBebidas_Result>("CounterBebidas @RefeicaoID", RegistoBebidas).ToList();

                        comida.Tipo = tipo.Tipo;
                        comida.ListaBebidas = tipo.RefeicaoBebidas;
                        comida.ListaPratos = tipo.RefeicaoPratos;
                        comida.ListaIngredientes = tipo.RefeicaoIngredientes;
                        comida.RefeicaoID = tipo.RefeicaoID;

                        comida.Total_kcal = (double)(registoBebidas[0].Kcal + registoIngredientes[0].Kcal + registoPratos[0].Kcal);
                        comida.Total_proteinas = (double)(registoBebidas[0].Proteinas + registoIngredientes[0].Proteinas + registoPratos[0].Proteinas);
                        comida.Total_gordura = (double)(registoBebidas[0].Gordura + registoIngredientes[0].Gordura + registoPratos[0].Gordura);
                        comida.Total_hidratos = (double)(registoBebidas[0].Hidratos + registoIngredientes[0].Hidratos + registoPratos[0].Hidratos);
                        muitaComida.Add(comida);

                    }
                }
                if (objectivo != null)
                {
                    if (refeicao == null)
                    {
                        ViewBag.UserGuide = "Next Step";
                    }
                    if (refeicao != null && prato == null)
                    {
                        ViewBag.UserGuide2 = "Next Step";
                    }
                }
                return View(muitaComida);
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
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var refeicao = db.Refeicoes.FirstOrDefault(c => c.RegistoDiario.Objectivo.UserID == userId);
            if (refeicao == null)
            {
                ViewBag.UserGuide = "Next Step";
            }
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
                var userId = Convert.ToInt32(User.Identity.GetUserId());
                var refeicao = db.Refeicoes.FirstOrDefault(c => c.RegistoDiario.Objectivo.UserID == userId);
                int counter = 0;
                if (refeicao == null)
                {
                    counter++;
                }
                double counterkcal = 0, counterproteinas = 0, countergordura = 0, counterhc = 0;
                RegistoDiario registo = db.RegistoDiarios.FirstOrDefault(o => o.Objectivo.UserID == userId && o.Data == refeico.Data);
                if (registo == null)
                {
                    RegistoDiario registo1 = new RegistoDiario();
                    var refeicoes = db.RefeicaoPratos.Include(i => i.Refeico).Where(q => q.Refeico.RegistoID == registo1.RegistoID);
                    var user = Convert.ToInt32(User.Identity.GetUserId());
                    Objectivo objectivo = db.Objectivoes.FirstOrDefault(o => o.UserID == user && o.Data_fim == null);
                    if (objectivo != null)
                    {
                        registo1.ObjectivoID = objectivo.ObjectivoID;
                        registo1.Data = refeico.Data;
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
                        counter++;
                        if (counter == 2)
                        {
                            TempData["UserGuide"] = "UserGuide";
                        }
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
                        foreach (RefeicaoPrato i in refeicoes)
                        {
                            counterkcal += (i.Dose / 100) * i.Prato.Kcal;
                            counterproteinas += (i.Dose / 100) * i.Prato.Proteinas;
                            countergordura += (i.Dose / 100) * i.Prato.Gordura;
                            counterhc += (i.Dose / 100) * i.Prato.HidCarbono;
                        }
                        refeico.RegistoID = registo.RegistoID;
                        db.Refeicoes.Add(refeico);
                        db.SaveChanges();
                        counter++;
                        if (counter == 2)
                        {
                            TempData["UserGuide"] = "UserGuide";
                        }
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
            return View();  //Andre diz para usar PartialView
        }

        public ActionResult KcalInfo(int RefeicaoID)
        {
            SqlParameter refID = new SqlParameter("@RefeicaoID", RefeicaoID);
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