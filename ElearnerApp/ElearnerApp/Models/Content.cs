namespace ElearnerApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }

        [Required]
        [StringLength(50)]
        public string VideoUrl { get; set; }

        public virtual Course Course { get; set; }
    }
}
