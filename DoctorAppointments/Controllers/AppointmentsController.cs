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
       
    }

}
