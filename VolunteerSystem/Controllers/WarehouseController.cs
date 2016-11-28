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
    public class WarehouseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return View(ctx.Warehouses.ToList());
            }
        }

        public List<SelectListItem> GetStates()
        {
            List<SelectListItem> states = new List<SelectListItem>();

            foreach (int val in Enum.GetValues(typeof(States)))
            {
                string value = Enum.GetName(typeof(States), val);
                states.Add(new SelectListItem
                {
                    Text = value,
                    Value = value
                });
            }

            return states;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.states = GetStates();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Alias,State,City,Address")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Warehouses.Add(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.states = GetStates();
            return View(warehouse);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.states = GetStates();
            return View(warehouse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.states = GetStates();
            return View(warehouse);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Warehouse warehouse = db.Warehouses.Find(id);
            db.Warehouses.Remove(warehouse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
