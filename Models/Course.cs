using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApp.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // Nghiac là trường này sẽ không được tạo tự động từ database mà do người dùng cung cấp
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
