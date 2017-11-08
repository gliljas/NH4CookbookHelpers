using System;

namespace MappingRecipes.ManyToMany
{
    public class Student
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }

        //public virtual IEnumerable<Course> Courses { get; set; }

        //public virtual void AddCourse(Course course)
        //{
        //    if (!_courses.Contains(course))
        //    {
        //        _courses.Add(course);
        //        course.AddStudent(this);
        //    }
        //}
    }
}