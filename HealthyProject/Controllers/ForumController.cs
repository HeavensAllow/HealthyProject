﻿using System;
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
        public ActionResult CreatePost([Bind( Include="Titulo,Texto")] Post newPost, int? subcategoria)
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
            return View("Index");
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

            newComment.UserID = Convert.ToInt32(User.Identity.GetUserId());
            newComment.Data = DateTime.Now;
            newComment.PostID = (int)postId;

            db.Comentarios.Add(newComment);
            db.SaveChanges();
            return View(post);
        }

        [HttpPost]
        public ActionResult Gosto(int? id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            Opiniao o = new Opiniao();
            o.Opiniao1 = true;
            o.userID = Convert.ToInt32(User.Identity.GetUserId());
            o.commentID = comentario.CommentID;
            db.Opiniaos.Add(o);
            db.SaveChanges();
            return View("Post", comentario.Post);
        }
        [HttpPost]
        public ActionResult naoGosto(int? id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            Opiniao o = new Opiniao();
            o.Opiniao1 = true;
            o.userID = Convert.ToInt32(User.Identity.GetUserId());
            o.commentID = comentario.CommentID;
            db.Opiniaos.Add(o);
            db.SaveChanges();
            return View("Post", comentario.Post);
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