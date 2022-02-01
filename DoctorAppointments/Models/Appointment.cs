//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorAppointments.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int appointmentID { get; set; }
        public System.DateTime appoinmtentDate { get; set; }
        public System.DateTime startAppTime { get; set; }
        public System.DateTime endAppTime { get; set; }
        public long doctorAMKA { get; set; }
        public Nullable<long> patientAMKA { get; set; }
        public bool isAvailable { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
