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

        //GET: RefeicaoIngredientes/Create
        public ActionResult Create(int RefeicaoID)
        {
            var Refeicao = db.Refeicoes.FirstOrDefault(r => r.RefeicaoID == RefeicaoID);
            ViewBag.Refeicao = Refeicao;

           // ViewBag.Categoria = new SelectList(db.Ingredientes.Distinct().OrderBy(c => c.Nome), "Categoria", "Categoria");
            ViewBag.IngredienteID = new SelectList(db.Ingredientes.OrderBy(i => i.Nome), "IngredientesID", "Nome");
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo");
            return View();
        }

        //public ActionResult Create(int RefeicaoID)
        //{
        //    var Refeicao = db.Refeicoes.FirstOrDefault(r => r.RefeicaoID == RefeicaoID);
        //    ViewBag.Refeicao = Refeicao;

        //    var ingrediente = new RefeicaoIngrediente();
        //    var queryCategoria = db.Ingredientes
        //         .OrderBy(c => c.Categoria)
        //         .Select(c => c.Categoria)
        //         .Distinct();
        //    ingrediente.Categoria = new SelectList(queryCategoria);
        //    ViewBag.Categoria = ingrediente.Categoria;

        //    ingrediente.Ingredientes = new[] { new SelectListItem { Value = "", Text = "" } };
        //    ViewBag.IngredienteID = ingrediente.Ingredientes;

        //    ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo");

        //    return View();
        //}

        //public ActionResult GetIngredientes(string categoria)
        //{

        //    var ingredientes = db.Ingredientes.Include(c => c.Categoria).Where(c => c.Categoria == categoria);
        //    ViewBag.IngredienteID = new SelectList(db.Ingredientes.Where(c => c.Categoria == categoria).OrderBy(i => i.Nome), "IngredientesID", "Nome");
        //   // List<SelectListItem> items = new List<SelectListItem>();
        //   // items.Add(new SelectListItem() { Text = "Sub Item 1", Value = "1" });
        //   // items.Add(new SelectListItem() { Text = "Sub Item 2", Value = "8" });
        //   // you may replace the above code with data reading from database based on the id

        //    return Json(ingredientes, JsonRequestBehavior.AllowGet);
        //}

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
                return RedirectToAction("Index", "Refeicoes");
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
                return RedirectToAction("Index", "Refeicoes");
            }
            ViewBag.IngredienteID = new SelectList(db.Ingredientes, "IngredientesID", "Categoria", refeicaoIngrediente.IngredienteID);
            ViewBag.RefeicaoID = new SelectList(db.Refeicoes, "RefeicaoID", "Tipo", refeicaoIngrediente.RefeicaoID);
            return View(refeicaoIngrediente);
        }

        // GET: RefeicaoIngredientes/Delete/5
        public ActionResult Delete(int? RefeicaoID, int? IngredienteID)
        {
            if ((RefeicaoID == null || IngredienteID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.FirstOrDefault(r => r.RefeicaoID == RefeicaoID && r.IngredienteID == IngredienteID);
            if (refeicaoIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(refeicaoIngrediente);
        }

        // POST: RefeicaoIngredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int RefeicaoID, int IngredienteID)
        {
            RefeicaoIngrediente refeicaoIngrediente = db.RefeicaoIngredientes.FirstOrDefault(r => r.RefeicaoID == RefeicaoID && r.IngredienteID == IngredienteID);
            db.RefeicaoIngredientes.Remove(refeicaoIngrediente);
            db.SaveChanges();
            return RedirectToAction("Index", "Refeicoes");
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
