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
    public class PatientsController : Controller
    {
        private AppointmentsEntities2 db = new AppointmentsEntities2();


        [HttpGet]
        public ActionResult Login(string username, string password, string role)
        {

            if (username == null || password == null || role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (role.Contains("Patient"))
            {
                Patient user = db.Patients.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                return RedirectToAction("RequestAmka", "Appointments");
            }
            else if (role.Contains("Doctor"))
            {
                Doctor user = db.Doctors.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                return RedirectToAction("RequestAmka1", "Doctors1");
            }
            else
            {
                Admin user = db.Admins.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                //return View("/Admins/UserRegistration",Admin);
                return RedirectToAction("UserRegistration", "Admins");
            }
        }

       
        public ActionResult Create(string text)
        {
            ViewBag.Message = text;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientAMKA,patientID,password,name,surname,username")] Patient patients)
        {
            ViewBag.Message = null;
            if (ModelState.IsValid)
            {
                ViewBag.Message = string.Format("Patient {0} has been successfully registered!", patients.username);
                db.Patients.Add(patients);
                db.SaveChanges();
                return RedirectToAction("Create", new { text = ViewBag.Message });
            }

            return View(patients);
        }

        public ActionResult SearchForDate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchForDate(List<Appointment>  resultAppointments)
        {
            return View(resultAppointments);
        }
        
        public ActionResult CancelAppointments(long patientAMKA = 12345678987)
        {
            DateTime currentDate = DateTime.Now;
            List<Appointment> previousAppointments = db.Appointments
                                                        .Where(x => x.appointmentDate >= currentDate)
                                                        .Where(w => w.patientAMKA == patientAMKA)
                                                        .ToList();
            Patient patient = db.Patients.Find(patientAMKA);
            return View(patient);
            
        }
        public ActionResult CloseAppointment()
        {
            return View();
        }


    }
}
