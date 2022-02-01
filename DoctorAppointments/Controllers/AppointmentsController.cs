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
    public class AppointmentsController : Controller
    {
        private AppointmentsEntities db = new AppointmentsEntities();



        [HttpPost]
        public ActionResult RegisterAvailableAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Appointments.Add(appointment);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
            //return View(appointment)
        }



        // GET: Appointments
        //public ActionResult Index()
        //{
        //    var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
        //    return View(appointments.ToList());
        //}

        //// GET: Appointments/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Appointment appointment = db.Appointments.Find(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(appointment);
        //}

        //// GET: Appointments/Create
        //public ActionResult Create()
        //{
        //    ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password");
        //    ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password");
        //    return View();
        //}

        //// POST: Appointments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "appointmentID,appoinmtentDate,startAppTime,endAppTime,doctorAMKA,patientAMKA,isAvailable")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Appointments.Add(appointment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password", appointment.doctorAMKA);
        //    ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password", appointment.patientAMKA);
        //    return View(appointment);
        //}

        //// GET: Appointments/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Appointment appointment = db.Appointments.Find(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password", appointment.doctorAMKA);
        //    ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password", appointment.patientAMKA);
        //    return View(appointment);
        //}

        //// POST: Appointments/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "appointmentID,appoinmtentDate,startAppTime,endAppTime,doctorAMKA,patientAMKA,isAvailable")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(appointment).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password", appointment.doctorAMKA);
        //    ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password", appointment.patientAMKA);
        //    return View(appointment);
        //}

        //// GET: Appointments/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Appointment appointment = db.Appointments.Find(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(appointment);
        //}

        //// POST: Appointments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Appointment appointment = db.Appointments.Find(id);
        //    db.Appointments.Remove(appointment);
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
