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
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        [ChildActionOnly]
        public ActionResult Index(int? groupId)
        {
            var courses = db.Courses
                .Where(c => c.GroupId == groupId)
                .OrderBy(c => c.StartTime);
            return PartialView(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
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
            return View("Details", course);
        }

        // GET: Courses/Create
        public ActionResult Create(int? groupId)
        {
            Course course = new Course();
            if (groupId != null)
            {
                course.GroupId = (int)groupId;
                Group group = db.Groups.Find(course.GroupId);
                course.EndTime = new DateTime(group.EndTime.Year,
                                              group.EndTime.Month,
                                              group.EndTime.Day);

                DateTime time = new DateTime(group.StartTime.Year,
                                             group.StartTime.Month,
                                             group.StartTime.Day);

                Course lastCourse = db.Courses
                    .Where(c => c.GroupId == groupId)
                    .OrderByDescending(c => c.StartTime)
                    .FirstOrDefault();

                if (lastCourse != null)
                {
                    time = new DateTime(lastCourse.EndTime.Year,
                                        lastCourse.EndTime.Month,
                                        lastCourse.EndTime.Day);
                }
                course.StartTime = time;

                if (course.EndTime > course.StartTime.AddDays(5.0))
                {
                    course.EndTime = time.AddDays(5.0);
                }
            }
            return View(course);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartTime,EndTime,GroupId")] Course course)
        {
            Group group = db.Groups.Find(course.GroupId);
            if (course.StartTime.Date < group.StartTime.Date)
            {
                ModelState.AddModelError("StartTime", "Kursens startid utanför gruppens kurstid.");
            }
            if (course.EndTime.Date > group.EndTime.Date)
            {
                ModelState.AddModelError("EndTime", "Kursens sluttid utanför gruppens kurstid.");
            }
            else
            {
                if (course.StartTime.Date > course.EndTime.Date)
                {
                    ModelState.AddModelError("EndTime", "Ogiltigt kursintervall");
                }
                else
                {
                    List<Course> courses = db.Courses
                        .Where(c => c.GroupId == course.GroupId)
                        .ToList();
                    foreach (var item in courses)
                    {
                        if ((course.StartTime.Date > item.StartTime.Date && course.StartTime.Date < item.EndTime.Date) ||
                          (course.EndTime.Date > item.StartTime.Date && course.EndTime.Date < item.EndTime.Date))
                        {
                            ModelState.AddModelError("StartTime", "Kursen ligger i en annans kurs tids intervall");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("CourseIndex", "Groups", new { id = course.GroupId });
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartTime,EndTime,GroupId")] Course course)
        {
            Group group = db.Groups.Find(course.GroupId);
            if (course.StartTime.Date < group.StartTime.Date)
            {
                ModelState.AddModelError("StartTime", "Kursens startid utanför gruppens kurstid.");
            }
            if (course.EndTime.Date > group.EndTime.Date)
            {
                ModelState.AddModelError("EndTime", "Kursens sluttid utanför gruppens kurstid.");
            }
            else
            {
                if (course.StartTime.Date > course.EndTime.Date)
                {
                    ModelState.AddModelError("StartTime", "Ogiltigt kursintervall");
                }
                else
                {
                    List<Course> courses = db.Courses
                        .Where(c => c.GroupId == course.GroupId && c.Id != course.Id)
                        .ToList();
                    foreach (var item in courses)
                    {
                        if ((course.StartTime.Date > item.StartTime.Date && course.StartTime.Date < item.EndTime.Date) ||
                          (course.EndTime.Date > item.StartTime.Date && course.EndTime.Date < item.EndTime.Date))
                        {
                            ModelState.AddModelError("StartTime", "Kursen ligger i en annans kurs tids intervall");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Groups", new { id = course.GroupId });
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Activities.RemoveRange(course.Activities);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index", "Groups", new { id = course.GroupId });
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
