using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnvironmentApp.Models;
using System.Web.Http;

namespace EnvironmentApp.Controllers
{
    public class DrivesController : Controller
    {
        private InvMgmtEntities _db = new InvMgmtEntities();

        // GET: Drives
        public ActionResult Index()
        {
            var drives = _db.Drives.Include(d => d.Standard);
            return View(drives.ToList());
        }

        // GET: JSON string for servers list
        [System.Web.Mvc.HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult GetServers([FromBody]string code)
        {
            var availStandards = (from s in _db.Standards
                                 where s.appCode == code
                                 select s.sName).ToList();

            return Json(new SelectList(availStandards, "sName", "sName"));
        }

        // GET: Drives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = _db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            return View(drive);
        }

        // GET: Drives/Create
        public ActionResult Create()
        {
            ViewBag.sName = new SelectList(_db.Standards, "sName", "sName");
            ViewBag.appCode = new SelectList(_db.Applications, "appCode", "appCode");

            using (InvMgmtEntities InvMgmtEntities = new InvMgmtEntities())
            {
                ViewBag.AppList = InvMgmtEntities.Applications.ToList();
            }
            return View();
        }

        // POST: Drives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dCode,sName,diskCSize,diskCName,diskDSize,diskDName,diskESize,diskEName,diskFSize,diskFName,diskKSize,diskKName,diskLSize,diskLName,diskMSize,diskMName,diskPSize,diskPName,diskTSize,diskTName,diskSSize,diskSName")] Drive drive)
        {
            if (ModelState.IsValid)
            {
                _db.Drives.Add(drive);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sName = new SelectList(_db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // GET: Drives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = _db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            ViewBag.sName = new SelectList(_db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // POST: Drives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dCode,sName,diskCSize,diskCName,diskDSize,diskDName,diskESize,diskEName,diskFSize,diskFName,diskKSize,diskKName,diskLSize,diskLName,diskMSize,diskMName,diskPSize,diskPName,diskTSize,diskTName,diskSSize,diskSName")] Drive drive)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(drive).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sName = new SelectList(_db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // GET: Drives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = _db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            return View(drive);
        }

        // POST: Drives/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drive drive = _db.Drives.Find(id);
            _db.Drives.Remove(drive);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
