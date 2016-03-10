using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_grupp1.Models
{
    public enum DocumentType
    {
        Public,
        Group, 
        Course, 
        Activity, 
        Private
    }

    public class Document
    {
        public int Id { get; set; }

        [DisplayName("Dokument namn")]
        public string Name { get; set; }

        [DisplayName("Beskrivnining")]
        public string Description { get; set; }

        [DisplayName("Kommentar")]
        public string Feedback { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Datum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TimeStamp { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Slutdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Deadline { get; set; }

        public string LocationUrl { get; set; }

        [DisplayName("Ansvarig")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}