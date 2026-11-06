using System.Text.Json.Serialization;
using WestcoastEducation.Enums;

namespace WestcoastEducation.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Teacher), "teacher")]
[JsonDerivedType(typeof(EducationManager), "educationalManager")]
[JsonDerivedType(typeof(Administrator), "administrator")]
public abstract class Staff(string firstName, string lastName, string phoneNumber, string socialSecurityNumber, Address address, Role role, string fieldOfKnowledge, List<Course> courses) : Person(firstName, lastName, phoneNumber, socialSecurityNumber, address, role)
{
    public string FieldOfKnowledge { get; set; } = fieldOfKnowledge;
    public List<Course> Courses { get; set; } = courses;

    public override string ToString()
    {
        return $"""
        {base.ToString()}
        Kunskapsområde: {FieldOfKnowledge}
        Kurs/kurser:    {string.Join(", ", Courses.Select(c => $"[{c.CourseNumber}] - {c.Title}"))}
        """;
    }
}
