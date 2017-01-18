using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteRepositorioFotos.Dao;
using TesteRepositorioFotos.Models;

namespace TesteRepositorioFotos.Controllers
{
    public class FotosController : Controller
    {

        private TesteBlob blob;
        private Contexto contexto;
        string accountName = "foodtruckimg";
        string accountKey = "8EdkctIIc1w/00L+2YFVIPhn27SDrDHk/NZLAxa0m7pKFeh5EQUkUe6Ud7bhhYplavJpFdyrGv5r01V0KJBQsQ==";

        public FotosController()
        {
            blob = new TesteBlob(accountName, accountKey);
            contexto = new Contexto();
        }
        // GET: Fotos
        public ActionResult Index()
        { 
            return View(contexto.Fotos.ToList());
        }

        // GET: Fotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = contexto.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // GET: Fotos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FotosId,Nome,Url")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                contexto.Fotos.Add(fotos);
                contexto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fotos);
        }

        public ActionResult UploadImagen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImagen(HttpPostedFileBase file)
        {
             if (file != null)
            {
                string ContainerName = "containerfoodtruck"; //nome do container
                file = file ?? Request.Files["file"];
                string nomeFile = Path.GetFileName(file.FileName);
                Stream imagenStrem = file.InputStream;
                var result = blob.UploadBlob(nomeFile, ContainerName, imagenStrem);
                if (result != null)
                {
                    Fotos fotos = new Fotos();
                    fotos.FotosId = new Random().Next();
                    fotos.Nome = "TESTE";
                    fotos.Url = result.Uri.ToString();
                    contexto.Fotos.Add(fotos);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("UploadImagen", "Fotos");
            }
            
        }

        // GET: Fotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = contexto.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FotosId,Nome,Url")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                contexto.Entry(fotos).State = EntityState.Modified;
                contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fotos);
        }

        // GET: Fotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = contexto.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fotos fotos = contexto.Fotos.Find(id);
            contexto.Fotos.Remove(fotos);
            contexto.SaveChanges();
            string BlobNomeDelete = fotos.Url.Split('/').Last();
            blob.DeletaBlob(BlobNomeDelete, "containerfoodtruck");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contexto.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
