﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorAppointments.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        public ActionResult UserRegistration()
        {
            return View();
        }
    }
}