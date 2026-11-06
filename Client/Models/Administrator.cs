using WestcoastEducation.Enums;

namespace WestcoastEducation.Models;

public class Administrator
(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address,
    string fieldOfKnowledge,
    List<Course> courses,
    DateTime employmentDate
) : Staff
(
    firstName,
    lastName,
    phoneNumber,
    socialSecurityNumber,
    address,
    Role.Administratör,
    fieldOfKnowledge,
    courses
)
{
    public DateTime EmploymentDate { get; set; } = employmentDate;

    public override string ToString()
    {
        return $"""
        {base.ToString()}
        Anställningsdatum: {EmploymentDate:yyyy-MM-dd}
        """;
    }
}
