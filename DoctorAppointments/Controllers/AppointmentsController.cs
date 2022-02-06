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

        [HttpGet]
        public ActionResult PrevAppointments(long patientAMKA = 12345678987)
        {
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
    }

}
