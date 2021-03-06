namespace ElearnerApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        public int Id { get; set; }

        [Column("Question")]
        [Required]
        [StringLength(50)]
        public string QuestionStr { get; set; }

        public bool Answer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
