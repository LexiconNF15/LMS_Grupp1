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
                    return View("GroupIndex", db.Groups.ToList());
                }
                else
                {
                   Group group = db.Groups.Find(groupId);
                   return View("CourseIndex", group);
                }
            } 
            return View(db.Groups.ToList());
        }


        [Authorize(Roles="Teacher")]
        public ActionResult GroupIndex()
        {
            return View(db.Groups.ToList());
        }


        [Authorize(Roles = "Teacher")]
        public ActionResult AddTeacher(int id)
        {
            var teacher = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .First();
            teacher.GroupId = id;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult RemoveTeacher()
        {
            var teacher = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .First();
            teacher.GroupId = null;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Groups/UserDetails/5

        [Authorize(Roles = "Teacher, Student")]
        public ActionResult UserIndex()
        {
            int? groupId = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(g => g.GroupId)
                .First();
            if (groupId != null)
            {
                Group group = db.Groups.Find(groupId);
                return View(group);
            }
            return RedirectToAction("Index");
        }

        // GET: Groups/CourseDetails/5
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult CourseIndex()
        {
            int? groupId = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(g => g.GroupId)
                .First();
            if (groupId != null)
            {
                Group group = db.Groups.Find(groupId);
                return View(group);
            }
            return RedirectToAction("Index");
        }

        // GET: Groups/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            Group group = new Group();
            group.StartTime = DateTime.Now;
            group.EndTime = group.StartTime.AddDays(90.0);
            return View(group);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,Name,StartTime,EndTime")] Group group)
        {
            if (group.StartTime < DateTime.Now)
            {
                ModelState.AddModelError("StartTime", "Gruppens startid har passerats.");
            }
            if (group.EndTime < DateTime.Now)
            {
                ModelState.AddModelError("EndTime", "Gruppens sluttid har passerats.");
            }
            else
            {
                if (group.StartTime > group.EndTime)
                {
                    ModelState.AddModelError("StartTime", "Gruppens sluttid är mindre än starttid");
                }
            }
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
            foreach (var course in group.Courses)
            {
                     db.Activities.RemoveRange(course.Activities);
            }
            db.Courses.RemoveRange(group.Courses);
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
