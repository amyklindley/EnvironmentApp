using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnvironmentApp.Models;
using EnvironmentApp.Classes;

namespace EnvironmentApp.Controllers
{
    public class StandardsController : Controller
    {
        private InvMgmtEntities db = new InvMgmtEntities();

        // GET: Standards
        public ActionResult Index()
        {
            var standards = db.Standards.Include(s => s.Application);
            return View(standards.ToList());
        }

        // GET: Standards/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        // GET: Standards/Create
        public ActionResult Create()
        {
            ViewBag.appCode = new SelectList(db.Applications, "appCode", "appEnvName");
            return View();
        }

        // POST: Standards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sName,appCode,sType,sDescription,sVCPU,sRAM,sIP,sOS,sSize,sSQLVers,sNotes")] Standard standard)
        {
            if (ModelState.IsValid)
            {
                db.Standards.Add(standard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.appCode = new SelectList(db.Applications, "appCode", "appEnvName", standard.appCode);
            return View(standard);
        }

        // GET: Standards/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            ViewBag.appCode = new SelectList(db.Applications, "appCode", "appEnvName", standard.appCode);
            return View(standard);
        }

        // POST: Standards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sName,appCode,sType,sDescription,sVCPU,sRAM,sIP,sOS,sSize,sSQLVers,sNotes")] Standard standard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.appCode = new SelectList(db.Applications, "appCode", "appEnvName", standard.appCode);
            return View(standard);
        }

        // GET: Standards/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = db.Standards.Find(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        // POST: Standards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Standard standard = db.Standards.Find(id);
            db.Standards.Remove(standard);
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
