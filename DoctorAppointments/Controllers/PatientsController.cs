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
        public ActionResult Login(string username, string password, string role = "Admin")
        {
            if (username == null || password == null || role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (role.Contains("Patient"))
            {
                Patient user = db.Patients.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                return View(user);
            }
            else if (role.Contains("Doctor"))
            {
                Doctor user = db.Doctors.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                return View(user);
            }
            else
            {
                Admin user = db.Admins.Where(x => x.username == username).FirstOrDefault();
                if (user == null) return HttpNotFound();
                //return View("/Admins/UserRegistration",Admin);
                return RedirectToAction("UserRegistration", "Admins");
            }
        }

        [HttpPost]
        public ActionResult PatientRegister(Patient patient)
        {
            if (patient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Patients.Add(patient);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
            //return View(patient)
        }

        [HttpGet]
        public ActionResult PreviousAppointments(long patientAMKA)
        {
            DateTime currentDate = DateTime.Now;
            List<Appointment> previousAppointments = db.Appointments
                                                        .Where(x => x.appointmentDate <= currentDate)
                                                        .Where(w => w.patientAMKA == patientAMKA)
                                                        .ToList();
            return View(previousAppointments);

        }
        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientAMKA,patientID,password,name,surname,username")] Patient patients)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patients);
        }

    }
}
