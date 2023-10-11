using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Learner
    {

        public int LearnerID { get; set; }
        public string LastName { get; set; }
		public string FirstMidName { get; set; }

		public DateTime EnrollmentDate { get; set; }
       
		public int MajorID { get; set; }

        public virtual Major? Major { get; set; }

        [ValidateNever]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
