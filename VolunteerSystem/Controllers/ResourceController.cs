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
    public class ResourceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Volunteer,Admin,User")]
        public ActionResult Index()
        {
            List<string> whAliases = new List<string>();
            List<List<ResourceDisplayModel>> model = new List<List<ResourceDisplayModel>>();
            List<ResourceDisplayModel> tmp = new List<ResourceDisplayModel>();

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var wh in ctx.Warehouses.ToList())
                {
                    whAliases.Add(wh.Alias + "(м. " + wh.City + ", " + wh.State + " область)");
                    foreach (var data in ctx.ResTable)
                    {
                        if(wh.Id == data.WarehouseId)
                        {
                            ResourceInfo resource = db.Resources.Where(r => r.Id == data.ResourceInfoId).FirstOrDefault();
                            tmp.Add(new ResourceDisplayModel
                            {
                                Amount = data.Count,
                                Description = resource.Description,
                                Measure = resource.ResMeasure
                            });
                        }
                    }
                    model.Add(tmp);
                    tmp = new List<ResourceDisplayModel>();
                }
            }

            ViewBag.modelList = model;
            ViewBag.warehouses = whAliases;

            return View();
        }

        [Authorize(Roles = "Volunteer")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Volunteer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,ResType,ResMeasure")] ResourceInfo resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resource);
        }

        [Authorize(Roles = "Admin,Volunteer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceInfo resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Volunteer")]
        public ActionResult Edit(ResourceInfo resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource);
        }

        [Authorize(Roles = "Admin,Volunteer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResourceInfo resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        [Authorize(Roles = "Admin,Volunteer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResourceInfo resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}