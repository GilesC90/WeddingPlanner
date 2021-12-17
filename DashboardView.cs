using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace WeddingPlanner.Models
{
    public class DashboardView
    {
        public string UserName {get; set; }
        public int? LoggedInUser {get; set; }
        public List<Wedding> AllWeddings {get; set; }
        public RSVP status {get; set; }
    }
}