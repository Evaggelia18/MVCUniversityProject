using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorAppointments.Models;

namespace DoctorAppointments.Controllers
{
    public class Doctors1Controller : Controller
    {
        private AppointmentsEntities2 db = new AppointmentsEntities2();

        [HttpPost]
        public ActionResult DoctorRegister(Doctor doctor)
        {
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Doctors.Add(doctor);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
            //return View(appointment)
        }

        [HttpGet]
        public ActionResult FindDoctorsBySpeciality(string speciality)
        {
            List<Doctor> doctors = db.Doctors.Where(x => x.speciality == speciality).ToList();
            return View(doctors);
        }

        // GET: Doctors1
        //public ActionResult Index()
        //{
        //    return View(db.Doctors.ToList());
        //}

        //// GET: Doctors1/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Doctor doctor = db.Doctors.Find(id);
        //    if (doctor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(doctor);
        //}

        //// GET: Doctors1/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Doctors1/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "doctorAMKA,doctorID,password,name,surname,username,speciality")] Doctor doctor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Doctors.Add(doctor);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(doctor);
        //}

        //// GET: Doctors1/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Doctor doctor = db.Doctors.Find(id);
        //    if (doctor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(doctor);
        //}

        //// POST: Doctors1/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "doctorAMKA,doctorID,password,name,surname,username,speciality")] Doctor doctor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(doctor).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(doctor);
        //}

        //// GET: Doctors1/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Doctor doctor = db.Doctors.Find(id);
        //    if (doctor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(doctor);
        //}

        //// POST: Doctors1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Doctor doctor = db.Doctors.Find(id);
        //    db.Doctors.Remove(doctor);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
