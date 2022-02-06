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
        private AppointmentsEntities2 db = new AppointmentsEntities2();



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

        [HttpPost]
        public ActionResult MakeAnAppointment(int appointmentID, long patientAMKA)
        {
            Appointment appointment = db.Appointments.Find(appointmentID);
            appointment.isAvailable = false;
            appointment.patientAMKA = patientAMKA;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        //[HttpPost]
        //public ActionResult CancelAppointment(Appointment appointment)
        //{
        //    Appointment app = db.Appointments.Find(appointment.appointmentID);
        //    app.isAvailable = true;
        //    app.patientAMKA = null;
        //    db.SaveChanges();
        //    return new HttpStatusCodeResult(HttpStatusCode.OK);
        //}

        [HttpPost]
        public ActionResult SearchForAppointment(DateTime appointmentDate, string specialty = "")
        {
            List<Appointment> resultAppointments = db.Appointments
                                                        .Where(x => x.appointmentDate <= appointmentDate).ToList();

            /*int appointmentID
             * Appointment appointment = db.Appointments.Find(appointmentID);
             appointment.isAvailable = false;
             appointment.patientAMKA = patientAMKA;
             db.SaveChanges();
             return new HttpStatusCodeResult(HttpStatusCode.OK);
             */
            return RedirectToAction("SearchForDate", "Patients", resultAppointments);
        }

        public ActionResult Create()
        {
            ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password");
            ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "appointmentID,appointmentDate,startAppTime,endAppTime,doctorAMKA,patientAMKA,isAvailable")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                //appointment.isAvailable = true;
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Create", "Appointments");
            }
            ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password", appointment.doctorAMKA);
            ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password", appointment.patientAMKA);
            return View(appointment);
        }

        [HttpGet]
        public ActionResult PrevAppointments(string AMKA)
        {
            long patientAMKA = long.Parse(AMKA);
            DateTime currentDate = DateTime.Now;
            List<Appointment> previousAppointments = db.Appointments
                                                        .Where(x => x.appointmentDate <= currentDate)
                                                        .Where(w => w.patientAMKA == patientAMKA)
                                                        .ToList();
            //Patient patient = db.Patients.Find(patientAMKA);
            ViewBag.previousAppointments = previousAppointments;
            return View(previousAppointments);

        }

        [HttpGet]
        public ActionResult CancelAppointments(long patientAMKA = 12345678987)
        {
            DateTime currentDate = DateTime.Now;
            List<Appointment> cancelAppointments = db.Appointments
                                                        .Where(x => x.appointmentDate >= currentDate)
                                                        .Where(w => w.patientAMKA == patientAMKA)
                                                        .ToList();
            //Patient patient = db.Patients.Find(patientAMKA);
            ViewBag.cancelAppointments = cancelAppointments;
            return View(cancelAppointments);

        }

        [HttpGet]
        public ActionResult CancelAppointmentsDoc(long doctorAMKA = 11268710672)
        {
            DateTime currentDate = DateTime.Now;
            List<Appointment> cancelAppointmentsdoc = db.Appointments
                                                        .Where(x => x.appointmentDate >= currentDate)
                                                        .Where(w => w.doctorAMKA == doctorAMKA)
                                                        .ToList();
            //Patient patient = db.Patients.Find(patientAMKA);
            ViewBag.cancelAppointmentsdoc = cancelAppointmentsdoc;
            return View(cancelAppointmentsdoc);

        }
        public ActionResult RequestAmka() {
            return View();
        
        }

        // POST: Appointments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("CancelAppointmentsDoc");
        }

    }

}
