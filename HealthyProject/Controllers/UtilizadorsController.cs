using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyProject.Models;
<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace HealthyProject.Controllers
{
<<<<<<< HEAD
    [Authorize(Roles="Admin")] 
=======
    [Authorize(Roles = "Admin")]
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6
    public class UtilizadorsController : Controller
    {
        private HealthyEntities db = new HealthyEntities();
        private UserManager<ApplicationUser, int> userManager = new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext()));
        //usado para atribuir roles a utilizadores

<<<<<<< HEAD

=======
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6

        // GET: Utilizadors
        public ActionResult Index()
        {
            
            var utilizadors = db.Utilizadors.Include(u => u.AspNetUser);
            return View(utilizadors.ToList());
        }

        // GET: Utilizadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }


       

        // GET: Utilizadors/Create
        public ActionResult Create()
        {
<<<<<<< HEAD
            
            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");

=======
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6
            return View();
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<ActionResult> Create(RegisterViewModel model, string roleName) 
=======
        public async Task<ActionResult> Create([Bind(Include = "Nome,Genero,Data_nascimento,Peso,Altura,Actividade_fisica,Nr_horas_sono,Nr_refeicoes,Habitos_alcoolicos,MMuscular,MGorda")] RegisterViewModel model, string roleName)
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, roleName);
<<<<<<< HEAD
                    
                    Utilizador newUser = new Utilizador();
                    newUser.UserID = user.Id;
                    db.Utilizadors.Add(newUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");

=======
                    Utilizador utilizador = new Utilizador()
                    {
                        UserID = user.Id,
                    };
                    db.Utilizadors.Add(utilizador);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            
            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");
>>>>>>> 2e0c6c14de8590b612bee7b0434aa5304fcf70f6
            return View(model);
        }

        // GET: Utilizadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         

            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", utilizador.UserID);
            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Nome,Genero,Data_nascimento,Peso,Altura,Actividade_fisica,Nr_horas_sono,Nr_refeicoes,Habitos_alcoolicos,MMuscular,MGorda")] Utilizador utilizador, string roleName)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(utilizador).State = EntityState.Modified;
                RegistoPeso peso = new RegistoPeso()
                {
                    Peso = utilizador.Peso,
                    Data = DateTime.Today,
                    User_ID = utilizador.UserID
                };
                db.RegistoPesoes.Add(peso);

                var role = db.AspNetRoles.FirstOrDefault(r => r.Name == roleName);
                if (role == null)
                {
                    return HttpNotFound();
                }
                var userRoles = userManager.GetRoles(utilizador.UserID);
                foreach (string r in userRoles)
                {
                    userManager.RemoveFromRole(utilizador.UserID, r);
                }
                userManager.AddToRole(utilizador.UserID, role.Name);
                db.Entry(utilizador).State = EntityState.Modified; // o utilizador foi modificado
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", utilizador.UserID);
            ViewBag.Roles = new SelectList(db.AspNetRoles, "Name", "Name");
            return View(utilizador);
    
        }

        // GET: Utilizadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizadors.Find(id);
            db.Utilizadors.Remove(utilizador);
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

        public double GetAge(DateTime dateofbirth)
        {
                var today = DateTime.Today;
                var calc = today.Subtract(dateofbirth).TotalDays;
                var calc2 = (calc / 365);
                var age = Math.Floor(calc2);
                return age;
        }
    }
}
