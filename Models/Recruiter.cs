//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiredHunters.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recruiter
    {
        public int r_no { get; set; }
        public string PreFix { get; set; }
        public string Recruiter_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string R_address { get; set; }
        public Nullable<float> Rating { get; set; }
        public Nullable<int> Total_job_Posted { get; set; }
        public string username { get; set; }
    }
}
