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
    public class RequestController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            using (var ctx = new ApplicationDbContext())
            {
                return View(ctx.Requests.Where(r => r.CustomerId == userId).ToList());
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return View(ctx.Requests.ToList());
            }
        }

        [Authorize(Roles = "Driver")]
        public ActionResult IndexDriver()
        {
            using(var ctx = new ApplicationDbContext())
            {
                return View(ctx.Requests.Where(req => req.Status == RequestStatus.Очікує_виконання).ToList());
            }
        }

        [Authorize(Roles = "Driver")]
        public ActionResult IndexProcessingByDriver()
        {
            string driverId = User.Identity.GetUserId();
            using (var ctx = new ApplicationDbContext())
            {
                return View(ctx.Requests.Where(req => req.Status == RequestStatus.Виконується && req.DriverId == driverId).ToList());
            }
        }

        #region HelperMethods
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
        public List<SelectListItem> GetResources(string resource = null)
        {
            List<SelectListItem> resList = new List<SelectListItem>();

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var res in ctx.Resources)
                {
                    if (res.Description == resource) {
                        resList.Add(new SelectListItem { Selected = true, Text = res.Description, Value = res.Id.ToString() });
                    }
                    resList.Add(new SelectListItem { Text = res.Description, Value = res.Id.ToString() });
                }
            }

            return resList.OrderBy(x => x.Text).ToList();
        }
        public ActionResult GetWH(int resourceId)
        {
            List<SelectListItem> whList = new List<SelectListItem>();

            using (var ctx = new ApplicationDbContext())
            {
                ResourceInfo resource = ctx.Resources.Where(r => r.Id == resourceId).FirstOrDefault();
                ResTypes resType = resource.ResType;

                foreach (var wh in ctx.Warehouses.ToList())
                {
                    if (wh.StoredResType == resType)
                    {
                        foreach(var data in ctx.ResTable)
                        {
                            if(data.WarehouseId == wh.Id && data.Count > 0 && data.ResourceInfoId == resource.Id)
                            {
                                whList.Add(new SelectListItem
                                {
                                    Text = wh.Alias + "(м.  " + wh.City + ", " + wh.State + " область)",
                                    Value = wh.Id.ToString()
                                });
                            }
                        }
                    }
                }
            }

            return Json(whList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [Authorize(Roles = "User")]
        public ActionResult Create(string resource)
        {
            ViewBag.resList = GetResources(resource);
            ViewBag.states = GetStates();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "ResourceInfoId,Count,WarehouseId,DestinationState,DestinationCity")]CustomerRequest request )
        {
            request.CustomerId = User.Identity.GetUserId();
            request.CreationTime = DateTime.Now;
            request.DestinationTime = DateTime.Now;
            request.Status = RequestStatus.Відкрита;

            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.resList = GetResources();
            ViewBag.states = GetStates();
            return View(request);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.resList = GetResources();
            ViewBag.states = GetStates();
            return View(request);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Edit(CustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.resList = GetResources();
            ViewBag.states = GetStates();
            return View(request);
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerRequest request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            request.Status = RequestStatus.Очікує_виконання;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        public ActionResult ConfirmProcessing(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            request.Status = RequestStatus.Закрита;
            request.DestinationTime = DateTime.Now;
            request.TransitTime = request.DestinationTime - request.CreationTime;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Driver")]
        public ActionResult ConfirmByDriver(int? id)
        {
            string driverId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            
            request.Status = RequestStatus.Виконується;
            request.DriverId = driverId;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("IndexDriver");
        }

        [Authorize(Roles = "Driver")]
        public ActionResult ConfirmDelivering(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRequest request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            request.Status = RequestStatus.Очікує_підтвердження;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("IndexDriver");
        }
    }
}
