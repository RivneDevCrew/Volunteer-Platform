using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VolunteerSystem.Models;

namespace VolunteerSystem.Controllers
{
    public class CarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Driver")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            using(var ctx = new ApplicationDbContext())
            {
                return View(ctx.Cars.Where(c => c.DriverId == userId).ToList());
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return View(ctx.Cars.ToList());
            }
        }

        [Authorize(Roles = "Driver")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Driver")]
        public ActionResult Create([Bind(Include = "Plate,CargoMaxWeight,ModelDescription")] Car car)
        {
            car.DriverId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        [Authorize(Roles = "Driver")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        [Authorize(Roles = "Driver")]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [Authorize(Roles = "Driver")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}