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

namespace HealthyProject.Controllers
{
    public class ForumController : Controller
    {
        private HealthyEntities db = new HealthyEntities();


        public ActionResult Index()
        {
            var Categorias = db.Categorias.Include(c => c.Subcategorias);
            return View(Categorias.ToList());
        }

        public ActionResult Subcategoria(int? id)
        {
            var result = db.Subcategorias.Include(s => s.Posts.Select(p => p.AspNetUser).Select(a => a.Utilizador));
            //var result = db.Subcategorias.Include("Subcategoria.Posts.AspNetUser.Utilizador");


            Subcategoria subcategoria = result.FirstOrDefault(s => s.SubcategoriaID == id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        //GET
        public ActionResult CreatePost()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "Titulo,Texto")] Post newPost, int? subcategoria)
        {
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            newPost.UserID = Convert.ToInt32(User.Identity.GetUserId());
            newPost.Data = DateTime.Now;
            newPost.SubcategoriaID = (int)subcategoria;

            db.Posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Post", new { id = newPost.PostID });

        }
            //GET

            public ActionResult Post(int? id)
            {
                Post post = db.Posts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);

            }
       
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post([Bind(Include = "Comment")] Comentario newComment, int? postId)
        {
            if (postId == null)
            {
                return HttpNotFound();
            }
            Post post = db.Posts.Find(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            newComment.AspNetUser = db.AspNetUsers.Find(Convert.ToInt32(User.Identity.GetUserId()));
            newComment.Data = DateTime.Now;
            newComment.PostID = (int)postId;

            db.Comentarios.Add(newComment);
            db.SaveChanges();
            post = db.Posts.Find(postId);
            return PartialView("_PostPartial", post);

        }

        [HttpPost]
        public ActionResult Opiniao(int? id, bool selectedOpinion)
        {
            var userID = Convert.ToInt32(User.Identity.GetUserId());
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }

            Opiniao opiniao = comentario.Opiniaos.FirstOrDefault(c => c.userID == userID);

            if (opiniao == null)
            {
                opiniao = new Opiniao();
                opiniao.userID = userID;
                opiniao.commentID = comentario.CommentID;
                opiniao.Opiniao1 = selectedOpinion;
                db.Opiniaos.Add(opiniao);
            }
            else //update
            {
                opiniao.Opiniao1 = selectedOpinion;
                db.Entry(opiniao).State = EntityState.Modified;
            }
            db.SaveChanges();
            ViewBag.Likes = comentario.Opiniaos.Where(o => o.Opiniao1 == true).Count() - comentario.Opiniaos.Where(o => o.Opiniao1 == false).Count();
            return PartialView("_PostPartial", comentario.Post);
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
