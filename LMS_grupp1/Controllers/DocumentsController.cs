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
        [ChildActionOnly]
        [Authorize(Roles="Teacher, Student")]
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
                    Originator = db.Users.Where(u => u.Id == m.UserId).FirstOrDefault().Email
                });
            if (level == DocumentLevel.PrivateLevel && User.IsInRole("Student"))
            {
                model = model.Where(u => u.Originator == User.Identity.GetUserName());
            }
            return PartialView(model);
        }

        [Authorize(Roles = "Teacher, Student")]
        public FileResult Download(int id)
        {
            Document document = db.Documents.Find(id);
            string path = Server.MapPath(Path.Combine(locationUrl, document.GuidName + document.Extension));
            byte[] content = System.IO.File.ReadAllBytes(path);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, document.Name);
        }

        // GET: Documents/Details/5
        [Authorize(Roles = "Teacher, Student")]
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
        [Authorize(Roles = "Teacher, Student")]
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
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Feedback,Deadline,Level,LevelId")] Document document)
        {
            //Upload a document to document folder
            document.TimeStamp = DateTime.Now;
            document.UserId = User.Identity.GetUserId();

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
                        if (document.Level == DocumentLevel.ActivityLevel ||
                            document.Level == DocumentLevel.PrivateLevel)
                        {
                            return RedirectToAction("Details", "Activities", new { id = document.LevelId });
                        }
                    }
                }
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        [Authorize(Roles = "Teacher, Student")]
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
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Edit([Bind(Include = "Id,Description,Feedback,Deadline")] Document document)
        {
            Document model = db.Documents.Find(document.Id);
            if (document.Deadline == null)
            {
                model.Deadline = DateTime.Now.AddMonths(1);
            }
            if (!string.IsNullOrEmpty(document.Feedback))
            {
                model.Feedback = document.Feedback;
            }
            if (!string.IsNullOrEmpty(document.Description))
            {
                model.Description = document.Description;
            }
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                if (document.Level == DocumentLevel.GroupLevel)
                {
                    return RedirectToAction("Index", "Groups", new { id = document.LevelId });
                }
                if (document.Level == DocumentLevel.CourseLevel)
                {
                    return RedirectToAction("Details", "Courses", new { id = document.LevelId });
                }
                if (document.Level == DocumentLevel.ActivityLevel ||
                    document.Level == DocumentLevel.PrivateLevel)
                {
                    return RedirectToAction("Details", "Activities", new { id = document.LevelId });
                }
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize(Roles = "Teacher, Student")]
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
        [Authorize(Roles = "Teacher, Student")]
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
            if (document.Level == DocumentLevel.ActivityLevel ||
                document.Level == DocumentLevel.PrivateLevel)
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
