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
                return View(user);
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
        public ActionResult PreviousAppointments()
        {
            DateTime currentDate = DateTime.Now;
            List<Appointment> previousAppointments = db.Appointments
                                                        .Where(x=>x.appointmentDate <= currentDate)
                                                        .ToList();
            return View(previousAppointments);

        }

        // GET: Patients
        //public ActionResult Index()
        //{
        //    return View(db.Patients.ToList());
        //}

        //// GET: Patients/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = db.Patients.Find(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// GET: Patients/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Patients/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "patientAMKA,patientID,password,name,surname,username")] Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Patients.Add(patient);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(patient);
        //}

        //// GET: Patients/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = db.Patients.Find(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// POST: Patients/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "patientAMKA,patientID,password,name,surname,username")] Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(patient).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(patient);
        //}

        //// GET: Patients/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = db.Patients.Find(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// POST: Patients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Patient patient = db.Patients.Find(id);
        //    db.Patients.Remove(patient);
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
