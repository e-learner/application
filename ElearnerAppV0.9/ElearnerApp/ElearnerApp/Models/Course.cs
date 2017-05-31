namespace ElearnerApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Course
    {
        public Course()
        {
            Questions = new HashSet<Question>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public int TeacherId { get; set; }

        public virtual Content Content { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
