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

        [ValidateNever]
        public Major Major { get; set; }

        [ValidateNever]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
