using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_grupp1.Models
{
    public class Document
    {
        public int Id { get; set; }

        [DisplayName("Dokument namn")]
        public string Name { get; set; }

        [DisplayName("Beskrivnining")]
        public string Description { get; set; }

        [DisplayName("Kommentar")]
        public string Feedback { get; set; }

        [DisplayName("Datum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TimeStamp { get; set; }

        [DisplayName("Slutdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Deadline { get; set; }

        public string LocationUrl { get; set; }

        [DisplayName("Dokumenttyp")]
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        [DisplayName("Ansvarig")]
        public int? Originator { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

    }
}