using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VolunteerSystem.Models;

namespace VolunteerSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<string> states = new List<string>();
            List<string> cities = new List<string>();

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var warehouse in ctx.Warehouses.ToList())
                {
                    states.Add(warehouse.State);
                    cities.Add(warehouse.City);
                }
            }

            ViewBag.states = states;
            ViewBag.cities = cities;

            return View();
        }       

        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Donate()
        { 
            ViewBag.resList = GetResources();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Donate([Bind(Include = "Count,ResourceInfoId,WarehouseId")] ResWarehouses data)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    foreach (var tmp in ctx.ResTable.ToList())
                    {
                        if(tmp.ResourceInfoId == data.ResourceInfoId && 
                            tmp.WarehouseId == data.WarehouseId)
                        {
                            tmp.Count += data.Count;
                            ctx.Entry(tmp).State = EntityState.Modified;
                            ctx.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    ctx.ResTable.Add(data);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(data);
        }

        #region HelperMethods
        public List<SelectListItem> GetResources()
        {
            List<SelectListItem> resList = new List<SelectListItem>();

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var res in ctx.Resources)
                {
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

                foreach (var wh in ctx.Warehouses)
                {
                    if (wh.StoredResType == resType)
                    {
                        whList.Add(new SelectListItem
                        {
                            Text = wh.Alias + "(м.  " + wh.City + ", " + wh.State + " область)",
                            Value = wh.Id.ToString()
                        });
                    }
                }
            }

            return Json(whList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}