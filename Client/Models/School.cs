using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Models;

public class School(
    IRepository<Course> courseRepo,
    IRepository<Student> studentRepo,
    IRepository<Staff> staffRepo
)
{
    public bool AddCourse(Course course)
    {
        var exists = courseRepo.GetById(course.Id);
        if (exists is not null)
        {
            return false;
        }

        courseRepo.Add(course);

        return true;
    }
    public bool AddPerson(Student student)
    {
        var exists = studentRepo.GetById(student.Id);
        if (exists is not null)
        {
            return false;
        }

        studentRepo.Add(student);

        return true;
    }
    public bool AddPerson(Staff staff)
    {
        var exists = staffRepo.GetById(staff.Id);
        if (exists is not null)
        {
            return false;
        }

        staffRepo.Add(staff);

        return true;
    }

    public IReadOnlyList<Course> GetCourses()
    {
        return courseRepo.GetCollection();
    }
    public IReadOnlyList<Student> GetStudents()
    {
        return studentRepo.GetCollection();
    }
    public IReadOnlyList<Staff> GetStaff()
    {
        return staffRepo.GetCollection();
    }

    public void DeleteCourse(Course course)
    {
        courseRepo.DeleteById(course.Id);
    }
    public void DeletePerson(Student student)
    {
        studentRepo.DeleteById(student.Id);
    }
    public void DeletePerson(Staff staff)
    {
        staffRepo.DeleteById(staff.Id);
    }




}
