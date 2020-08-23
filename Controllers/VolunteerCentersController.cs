using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeForHomeless.Models;
using HomeForHomeless.ViewModel;
using PagedList;

namespace HomeForHomeless.Controllers
{
    public class VolunteerCentersController : Controller
    {
        private VolunteerCenterEntities db = new VolunteerCenterEntities();

        // GET: VolunteerCenters
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var results = from x in db.VolunteerCenter
                          select x;
            int pagesize = 9, pageindex = 1;
            VCList temp = new VCList();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Showing data based on the search query string and the star rating selected from the dropdown.

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s =>  s.Name.Contains(searchString) || s.Address.Contains(searchString) ||s.Suburb.Contains(searchString) || s.State.Contains(searchString)  || s.Business_Category.Contains(searchString) || s.LGA.Contains(searchString) || s.Region.Contains(searchString) || s.Business_Category.Contains(searchString) || s.Postcode.ToString().Contains(searchString));

            }
            else
            {
                results = results.Where(x => x.State == "VIC");
            }

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = results.ToList();
            temp.VCs = list.ToPagedList(pageindex, pagesize);
            return View(temp);

        }

        // GET: VolunteerCenters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenter.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VolunteerCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerCenter.Add(volunteerCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenter.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // POST: VolunteerCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenter.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // POST: VolunteerCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolunteerCenter volunteerCenter = db.VolunteerCenter.Find(id);
            db.VolunteerCenter.Remove(volunteerCenter);
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
