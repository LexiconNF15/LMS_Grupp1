﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_grupp1.Models;

namespace LMS_grupp1.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        [ChildActionOnly]
        public ActionResult Index(int? courseId)
        {
            //// Code below to return only the activities that related to choosen course id
            var activities = db.Activities
                .Where(c => c.CourseId == courseId)
                .OrderBy(c => c.StartTime);
            return PartialView(activities.ToList());
        }

        // GET: Activities/Create
        public ActionResult Create(int? courseId)
        {
            Activity activity = new Activity();
            if (courseId != null)
            {
                activity.CourseId = (int)courseId;
                Course course = db.Courses.Find(activity.CourseId);
                activity.EndTime = new DateTime(course.EndTime.Year,
                                                course.EndTime.Month,
                                                course.EndTime.Day);

                DateTime time = new DateTime(course.StartTime.Year,
                                             course.StartTime.Month,
                                             course.StartTime.Day);

                Activity lastActivity = db.Activities
                    .Where(c => c.CourseId == courseId)
                    .OrderByDescending(c => c.StartTime)
                    .FirstOrDefault();

                if (lastActivity != null)
                {
                    time = new DateTime(lastActivity.EndTime.Year,
                                        lastActivity.EndTime.Month,
                                        lastActivity.EndTime.Day);
                }
                activity.StartTime = time;

                if (activity.EndTime > activity.StartTime.AddDays(5.0))
                {
                    activity.EndTime = time.AddDays(5.0);
                }
            }
            return View(activity);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartTime,EndTime, CourseId")] Activity activity)
        {
            Course course = db.Courses.Find(activity.CourseId);
            if (activity.EndTime.Date > course.EndTime.Date)
            {
                ModelState.AddModelError("EndTime", "Aktivitetens sluttid utanför kursens kurstid.");
            }
            if (activity.StartTime.Date < course.StartTime.Date)
            {
                ModelState.AddModelError("StartTime", "Aktivitetens startid utanför kursens kurstid.");
            }
            else
            {
                if (activity.StartTime.Date > activity.EndTime.Date)
                {
                    ModelState.AddModelError("EndTime", "Ogiltigt aktivitetsintervall");
                }
                else
                {
                    List<Activity> activities = db.Activities
                        .Where(c => c.CourseId == activity.CourseId)
                        .ToList();
                    foreach (var item in activities)
                    {
                        if ((activity.StartTime.Date > item.StartTime.Date && activity.StartTime.Date < item.EndTime.Date) ||
                          (activity.EndTime.Date > item.StartTime.Date && activity.EndTime.Date < item.EndTime.Date))
                        {
                            ModelState.AddModelError("StartTime", "Aktiviteten ligger i en annan aktivitets tids intervall");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = activity.CourseId });
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }


        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartTime,EndTime,CourseId")] Activity activity)
        {
            Course course = db.Courses.Find(activity.CourseId);
            if (activity.EndTime.Date > course.EndTime.Date)
            {
                ModelState.AddModelError("EndTime", "Aktivitetens sluttid utanför kursens kurstid.");
            }
            if (activity.StartTime.Date < course.StartTime.Date)
            {
                ModelState.AddModelError("StartTime", "Aktivitetens startid utanför kursens kurstid.");
            }
            else
            {
                if (activity.StartTime.Date > activity.EndTime.Date)
                {
                    ModelState.AddModelError("EndTime", "Ogiltigt aktivitetsintervall");
                }
                else
                {
                    List<Activity> activities = db.Activities
                        .Where(c => c.CourseId == activity.CourseId && c.Id != activity.Id)
                        .ToList();
                    foreach (var item in activities)
                    {
                        if ((activity.StartTime.Date > item.StartTime.Date && activity.StartTime.Date < item.EndTime.Date) ||
                          (activity.EndTime.Date > item.StartTime.Date && activity.EndTime.Date < item.EndTime.Date))
                        {
                            ModelState.AddModelError("StartTime", "Aktiviteten ligger i en annan aktivitets tids intervall");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = activity.CourseId });
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            var documents = db.Documents
                .Where(d => (d.Level == DocumentLevel.ActivityLevel ||
                    d.Level == DocumentLevel.PrivateLevel) &&
                    d.LevelId == id)
                .ToList();
            foreach (var item in documents)
            {
                item.Level = DocumentLevel.DeleteLevel;
            }
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Details", "Courses", new { id = activity.CourseId });
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
