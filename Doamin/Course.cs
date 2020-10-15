using GovTown.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Course : BaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public string Demo { get; set; }
    }
}
