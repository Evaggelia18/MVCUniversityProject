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

        
        [HttpGet]
        public ActionResult FindDoctorsBySpeciality(string speciality)
        {
            List<Doctor> doctors = db.Doctors.Where(x => x.speciality == speciality).ToList();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult ShowAppointmentsPerDay(string AMKA)
        {
            long doctorAMKA = long.Parse(AMKA);
            DateTime currentDate = DateTime.Now;
            
            List<Appointment> currentAppointments = db.Appointments.Where(x => x.doctorAMKA == doctorAMKA )
                                                    .Where( w => w.appointmentDate == currentDate.Date )
                                                    .Where( y => y.isAvailable == false)
                                                    .ToList();
            return View(currentAppointments);
            
        }

        public ActionResult ShowAppointmentsPerWeek(string AMKA)
        {
            long doctorAMKA = long.Parse(AMKA);
            //Doctor doctor = db.Doctors.Find(doctorID);
            DateTime currentDate = DateTime.Now;
            DateTime currentWeek = currentDate.AddDays(7).Date;
            List<Appointment> currentAppointments = db.Appointments.Where(x => x.doctorAMKA == doctorAMKA)
                                                    .Where(d => d.appointmentDate <= currentWeek)
                                                    .Where(w => w.appointmentDate >= currentDate.Date)
                                                    .Where(y => y.isAvailable == false)
                                                    .ToList();
            return View(currentAppointments);
        }
        // GET: Doctors/Create
        public ActionResult Create(string text)
        {
            ViewBag.Message = text;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doctorAMKA,doctorID,password,name,surname,username,speciality")] Doctor doctors)
        {
            ViewBag.Message = null;
            if (ModelState.IsValid)
            {

                ViewBag.Message = string.Format("Doctor {0} has been successfully registered!", doctors.username);
                db.Doctors.Add(doctors);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Create", new { text = ViewBag.Message });
            }

            return View(doctors);
        }

        public ActionResult RequestAmka1()
        {
            return View();
        }

        public ActionResult RequestAmka2()
        {
            return View();
        }

        public ActionResult RequestAmka3()
        {
            return View();
        }

    }
}
