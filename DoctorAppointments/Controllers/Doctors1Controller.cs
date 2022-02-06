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

        [HttpGet]
        public ActionResult ShowAppointmentsPerWeek(long doctorAMKA)
        {
            DateTime currentDate = DateTime.Now;
            DateTime currentWeek = currentDate.AddDays(7).Date;
            List<Appointment> currentAppointments = db.Appointments.Where(x => x.doctorAMKA == doctorAMKA)
                                                    .Where(d => d.appointmentDate <= currentWeek)
                                                    .Where(w => w.appointmentDate >= currentDate.Date)
                                                    .Where(y => y.isAvailable == false)
                                                    .ToList();
            return View(currentAppointments);
        }


    }
}
