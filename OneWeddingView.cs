using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace WeddingPlanner.Models
{
    public class OneWeddingView
    {
        public Wedding ScheduledWedding {get; set; }
    }
}