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

        [HttpGet]
        public ActionResult ShowAppointmentsPerDay(long doctorAMKA)
        {
            DateTime currentDate = DateTime.Now;
            
            List<Appointment> currentAppointments = db.Appointments.Where(x => x.doctorAMKA == doctorAMKA )
                                                    .Where( w => w.appointmentDate == currentDate.Date )
                                                    .Where( y => y.isAvailable == false)
                                                    .ToList();
            return View(currentAppointments);
            
        }

        // GET: Doctors/Create
        public ActionResult AppointmentSchedule()
        {
            return View();
        }
        //Unfinished
        //[HttpGet]
        //public ActionResult ShowAppointmentsPerWeek(long doctorAMKA)
        //{
        //    DateTime currentDate = DateTime.Now;

        //    List<Appointment> currentAppointments = db.Appointments.Where(x => x.doctorAMKA == doctorAMKA)
        //                                            .Where(w => w.appointmentDate == currentDate.Date)
        //                                            .Where(y => y.isAvailable == false)
        //                                            .ToList();
        //    return View(currentAppointments);
        //}
        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doctorAMKA,doctorID,password,name,surname,username,speciality")] Doctor doctors)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctors);
        }


    }
}
