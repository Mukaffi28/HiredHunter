using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiredHunters.Models;

namespace HiredHunters.Views
{
    public class ContractUsController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: ContractUs
        public ActionResult Index()
        {
            return View(db.ContractUs.ToList());
        }

        // GET: ContractUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractUs contractUs = db.ContractUs.Find(id);
            if (contractUs == null)
            {
                return HttpNotFound();
            }
            return View(contractUs);
        }

        // GET: ContractUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apply_no,Full_name,Email_Address,Phonenumber,Con_Message")] ContractUs contractUs)
        {
            if (ModelState.IsValid)
            {
                db.ContractUs.Add(contractUs);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View("Create");
        }

        // GET: ContractUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractUs contractUs = db.ContractUs.Find(id);
            if (contractUs == null)
            {
                return HttpNotFound();
            }
            return View(contractUs);
        }

        // POST: ContractUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "apply_no,Full_name,Email_Address,Phonenumber,Con_Message")] ContractUs contractUs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractUs);
        }
        public ActionResult Del(int? id)
        {
            ContractUs contractUs = db.ContractUs.Find(id);
            db.ContractUs.Remove(contractUs);
            db.SaveChanges();
            return RedirectToAction("Index");



        }
        // GET: ContractUs/Delete/5
        public ActionResult Delete(int? id)
        {
            ContractUs contractUs = db.ContractUs.Find(id);
            db.ContractUs.Remove(contractUs);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        // POST: ContractUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractUs contractUs = db.ContractUs.Find(id);
            db.ContractUs.Remove(contractUs);
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
