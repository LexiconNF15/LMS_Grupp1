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
        private const string locationUrl = "~/Documents/";

        // GET: Documents
        public ActionResult Index(DocumentLevel level, int id)
        {
            var model = db.Documents
                .Where(d => d.Level == level && d.LevelId == id)
                .OrderBy(n => n.Name)
                .Select(m => new DocumentView
                {
                    Id = m.Id,
                    Name = m.Name,
                    TimeStamp = m.TimeStamp,
                    Deadline = m.Deadline,
                    Originator = db.Users.Where(u => u.Id == m.Originator).FirstOrDefault().Email
                });
            return PartialView(model);
        }

        public FileResult Download(int id)
        {
            Document document = db.Documents.Find(id);
            string path = Server.MapPath(Path.Combine(locationUrl, document.GuidName + document.Extension));
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
        public ActionResult Create(DocumentLevel level, int id)
        {
            Document document = new Document();
            document.Level = level;
            document.LevelId = id;
            return View(document);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GuidName,Extension,Description,Feedback,TimeStamp,Deadline,Originator,Level,LevelId")] Document document)
        {
            //Upload a document to document folder
            document.TimeStamp = DateTime.Now;
            document.Originator = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        document.GuidName = Guid.NewGuid();
                        document.Name = Path.GetFileName(file.FileName);
                        document.Extension = Path.GetExtension(file.FileName);

                        db.Documents.Add(document);
                        db.SaveChanges();

                        string path = Server.MapPath(locationUrl);
                        file.SaveAs(Path.Combine(path, document.GuidName + document.Extension));

                        if (document.Level == DocumentLevel.GroupLevel)
                        {
                            return RedirectToAction("Index", "Groups", new { id = document.LevelId });
                        }
                        if (document.Level == DocumentLevel.CourseLevel)
                        {
                            return RedirectToAction("Details", "Courses", new { id = document.LevelId });
                        }
                        if (document.Level == DocumentLevel.ActivityLevel)
                        {
                            return RedirectToAction("Details", "Activities", new { id = document.LevelId });
                        }
                    }
                }
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
        public ActionResult Edit([Bind(Include = "Id,Name,GuidName,Description,Feedback,TimeStamp,Deadline,Originator,Level,LevelId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();

                if (document.Level == DocumentLevel.GroupLevel)
                {
                    return RedirectToAction("Index", "Groups", new { id = document.LevelId });
                }
                if (document.Level == DocumentLevel.CourseLevel)
                {
                    return RedirectToAction("Details", "Courses", new { id = document.LevelId });
                }
                if (document.Level == DocumentLevel.ActivityLevel)
                {
                    return RedirectToAction("Details", "Activities", new { id = document.LevelId });
                }
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
            string path = Server.MapPath(Path.Combine(locationUrl, document.GuidName + document.Extension));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            if (document.Level == DocumentLevel.GroupLevel)
            {
                return RedirectToAction("Index", "Groups", new { id = document.LevelId });
            }
            if (document.Level == DocumentLevel.CourseLevel)
            {
                return RedirectToAction("Details", "Courses", new { id = document.LevelId });
            }
            if (document.Level == DocumentLevel.ActivityLevel)
            {
                return RedirectToAction("Details", "Activities", new { id = document.LevelId });
            }
            return View(document);
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
