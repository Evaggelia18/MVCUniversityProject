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

        [HttpPost]
        public ActionResult CancelAppointment(Appointment appointment)
        {
            Appointment app = db.Appointments.Find(appointment.appointmentID);
            app.isAvailable = true;
            app.patientAMKA = null;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult SearchForAppointment(DateTime appointmentDate, string specialty="")
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
            return RedirectToAction("SearchForDate", "Patients",resultAppointments);
        }

        public ActionResult Create(Doctor doctor)
        {
            ViewBag.docAMKA = doctor.doctorAMKA.ToString();
            ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password");
            ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "appointmentID,appointmentDate,startAppTime,endAppTime,doctorAMKA,patientAMKA,isAvailable")] Appointment appointment, Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Create", "Appointments");
            }
            ViewBag.doctorAMKA = new SelectList(db.Doctors, "doctorAMKA", "password", appointment.doctorAMKA);
            ViewBag.patientAMKA = new SelectList(db.Patients, "patientAMKA", "password", appointment.patientAMKA);
            return View(appointment);
        }


    }

}
