using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_grupp1.Models;
using System.IO;
using Microsoft.AspNet.Identity;


namespace LMS_grupp1.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const string path = "~\\Documents\\"; 

        // GET: Documents
        public ActionResult Index(DocumentLevel level, int id)
        {
            return PartialView(db.Documents
                .Where(d => d.Level == level && d.LevelId == id)
                .OrderBy(n => n.Name)
                .ToList()
                );
        }

        public ActionResult Download(DocumentLevel level, int id)
        {
            Document document = db.Documents.Find(id);
            string path = Server.MapPath(Path.Combine(document.LocationUrl, document.GuidName));
            byte[] content = System.IO.File.ReadAllBytes(path);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, document.Name);
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Feedback,TimeStamp,Deadline,LocationUrl,Originator")] Document document)
        {

            //Upload a document to document folder

            if (ModelState.IsValid)
            {
                foreach (string upload in Request.Files)
                {
                    if (Request.Files[upload].ContentLength == 0) continue;
                    string pathToSave = Server.MapPath("~/Document/");
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(pathToSave, filename));

                    document.TimeStamp = DateTime.Now;
                    document.Originator = User.Identity.GetUserId();
                    
                    
                    document.LocationUrl = Server.MapPath("/Document/") + filename + Guid.NewGuid();
                    ViewBag.filename = filename;
                }
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Feedback,TimeStamp,Deadline,LocationUrl,Originator")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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
