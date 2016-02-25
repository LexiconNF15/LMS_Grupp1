﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_grupp1.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int GroupId { get; set; }

        public virtual IEnumerable<Group> Group { get; set; }
    }
}