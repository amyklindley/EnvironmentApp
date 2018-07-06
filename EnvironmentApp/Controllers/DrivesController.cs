using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnvironmentApp.Models;

namespace EnvironmentApp.Controllers
{
    public class DrivesController : Controller
    {
        private InvMgmtEntities db = new InvMgmtEntities();

        // GET: Drives
        public ActionResult Index()
        {
            var drives = db.Drives.Include(d => d.Standard);
            return View(drives.ToList());
        }

        // GET: Drives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            return View(drive);
        }

        // GET: Drives/Create
        public ActionResult Create()
        {
            ViewBag.sName = new SelectList(db.Standards, "sName", "sName");
            ViewBag.appCode = new SelectList(db.Applications, "appCode", "appCode");

            using (InvMgmtEntities InvMgmtEntities = new InvMgmtEntities())
            {
                ViewBag.AppList = InvMgmtEntities.Applications.ToList();
            }
            return View();
        }

        // POST: Drives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dCode,sName,diskCSize,diskCName,diskDSize,diskDName,diskESize,diskEName,diskFSize,diskFName,diskKSize,diskKName,diskLSize,diskLName,diskMSize,diskMName,diskPSize,diskPName,diskTSize,diskTName,diskSSize,diskSName")] Drive drive)
        {
            if (ModelState.IsValid)
            {
                db.Drives.Add(drive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sName = new SelectList(db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // GET: Drives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            ViewBag.sName = new SelectList(db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // POST: Drives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dCode,sName,diskCSize,diskCName,diskDSize,diskDName,diskESize,diskEName,diskFSize,diskFName,diskKSize,diskKName,diskLSize,diskLName,diskMSize,diskMName,diskPSize,diskPName,diskTSize,diskTName,diskSSize,diskSName")] Drive drive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sName = new SelectList(db.Standards, "sName", "appCode", drive.sName);
            return View(drive);
        }

        // GET: Drives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drive drive = db.Drives.Find(id);
            if (drive == null)
            {
                return HttpNotFound();
            }
            return View(drive);
        }

        // POST: Drives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drive drive = db.Drives.Find(id);
            db.Drives.Remove(drive);
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
