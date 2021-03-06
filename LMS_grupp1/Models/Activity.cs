﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LMS_grupp1.Models
{
    public class Activity
    {

        public int Id { get; set; }

        [DisplayName("Benämning")]
        public string Name { get; set; }

        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Startdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Slutdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }

       
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        


    }
}