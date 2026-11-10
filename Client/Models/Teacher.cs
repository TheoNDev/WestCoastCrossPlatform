using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Client.Models;

public class Teacher(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address,
    string fieldOfKnowledge,
    List<Course> courses
        ) : Person(
            firstName,
            lastName,
            phoneNumber,
            socialSecurityNumber,
            address
        )

{
    [Display(Name = "Kunskapsområde", Order = 6)]
    public string FieldOfKnowledge { get; set; } = fieldOfKnowledge;
    [ScaffoldColumn(false)]
    public List<Course> Courses { get; set; } = courses;
}
