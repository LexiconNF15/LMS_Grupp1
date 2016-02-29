using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_grupp1.Models;

namespace LMS_grupp1.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int? groupId = db.Users
                    .Where(u => u.Email == User.Identity.Name)
                    .Select(g => g.GroupId)
                    .First();
                if (User.IsInRole("Teacher") && groupId == null)
                {
                    return View("IndexGroup", db.Groups.ToList());
                }
                else
                {
                    //Group group = db.Groups.Find(groupId);
                    ViewBag.GroupId = groupId;
                    var course = db.Courses
                        .Where(c => c.Id == groupId)
                        .ToList();
                    return RedirectToAction("Index", "Courses", course);
                }
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult AddTeacher(int groupId)
        {
            var teacher = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .First();
            teacher.GroupId = groupId;
            db.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }

        // GET: Groups/UserDetails/5

        [Authorize(Roles = "Teacher, Student")]
        public ActionResult UserDetails()
        {
            int? groupId = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(g => g.GroupId)
                .First();
            Group group = db.Groups.Find(groupId);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/CourseDetails/5
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult CourseDetails()
        {
            int? groupId = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(g => g.GroupId)
                .First();
            var course = db.Courses
                .Where(c => c.Id == groupId)
                .ToList();
            return RedirectToAction("Index", "Courses");
        }

        // GET: Groups/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,Name,StartTime,EndTime")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,StartTime,EndTime")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
