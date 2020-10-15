using GovTown.Core;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Student:BaseEntity
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

