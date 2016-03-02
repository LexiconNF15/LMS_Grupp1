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
            return View();
        }


        [Authorize(Roles="Teacher")]
        public ActionResult GroupIndex()
        {
            return View(db.Groups.ToList());
        }


        [Authorize(Roles = "Teacher")]
        public ActionResult AddTeacher(int groupId)
        {
            var teacher = db.Users
                .Where(u => u.Email == User.Identity.Name)
                .First();
            teacher.GroupId = groupId;
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
        public ActionResult GroupCreate()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult GroupCreate([Bind(Include = "Id,Name,StartTime,EndTime")] Group group)
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
        public ActionResult GroupEdit(int? id)
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
        public ActionResult GroupEdit([Bind(Include = "Id,Name,StartTime,EndTime")] Group group)
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
        public ActionResult GroupDelete(int? id)
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
        [HttpPost, ActionName("GroupDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult GroupDeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Courses/Details/5
        public ActionResult CourseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult CourseCreate(int groupId)
        {
            // A Group Id list to get a drop down list in a create course page
            // ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.GroupId = groupId;
            return View();

        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseCreate([Bind(Include = "Id,Name,Description,StartTime,EndTime,GroupId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Courses/Edit/5
        public ActionResult CourseEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseEdit([Bind(Include = "Id,Name,Description,StartTime,EndTime,GroupId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult CourseDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("CourseDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CourseDeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
