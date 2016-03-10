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
    //public class DocumentsController : Controller
    //{
    //    private ApplicationDbContext db = new ApplicationDbContext();

    //    // GET: Documents
    //    public ActionResult Index(string path, int id)
    //    {
    //        //var documents = db.Documents.Include(d => d.Activity).Include(d => d.Course).Include(d => d.DocumentType).Include(d => d.Group);
    //        var documents = db.Documents
    //            .Where(t => t.LocationUrl == path && t.RequesterId == id);
    //        var path = Server.MapPath("~/Documents");
    //        return View(documents.ToList());
    //    }

    //    // GET: Documents/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Document document = db.Documents.Find(id);
    //        if (document == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(document);
    //    }

    //    // GET: Documents/Create
    //    public ActionResult Create(DocumentType type)
    //    {
    //        Document document = new Document();
    //        document.Type = type;
    //        document.Deadline = DateTime.Now.AddMonths(1);
    //        document.Feedback = "";

    //        return View();
    //    }

    //    // POST: Documents/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,Name,Description,Feedback,TimeStamp,Deadline,LocationUrl,DocumentTypeId,UserId,RequesterId,Type")] Document document)
    //    {
    //        //Upload a document to document folder

    //        foreach (string upload in Request.Files)
    //        {
    //            if (Request.Files[upload].ContentLength == 0) continue;
    //            string pathToSave = Server.MapPath("~/Document/");
    //            document.Feedback = "";
    //            document.Name = Path.GetFileName(Request.Files[upload].FileName);
    //            document.LocationUrl = Path.Combine(pathToSave, document.Name);
    //            document.Deadline = DateTime.Now.AddMonths(1);
    //            document.TimeStamp = DateTime.Now;
    //            document.UserId = User.Identity.GetUserId();

    //            if (ModelState.IsValid)
    //            {
    //                Request.Files[upload].SaveAs(document.LocationUrl);
    //                db.Documents.Add(document);
    //                db.SaveChanges();
    //            }

    //            return RedirectToAction("Index");
    //        }

    //        return View(document);
    //    }

    //    // GET: Documents/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Document document = db.Documents.Find(id);
    //        if (document == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
    //        ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
    //        ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name", document.DocumentTypeId);
    //        ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", document.GroupId);
    //        return View(document);
    //    }

    //    // POST: Documents/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Name,Description,Feedback,TimeStamp,Deadline,LocationUrl,DocumentTypeId,Originator,ActivityId,CourseId,GroupId")] Document document)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(document).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
    //        ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
    //        ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name", document.DocumentTypeId);
    //        ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", document.GroupId);
    //        return View(document);
    //    }

    //    // GET: Documents/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Document document = db.Documents.Find(id);
    //        if (document == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(document);
    //    }

    //    // POST: Documents/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        Document document = db.Documents.Find(id);
    //        db.Documents.Remove(document);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
