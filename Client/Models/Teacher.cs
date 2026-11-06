using WestcoastEducation.Enums;

namespace WestcoastEducation.Models;

public class Teacher(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address,
    string fieldOfKnowledge,
    List<Course> courses
        ) : Staff(
        firstName,
        lastName,
        phoneNumber,
        socialSecurityNumber,
        address,
        Role.Lärare,
        fieldOfKnowledge,
        courses
        )

{

}
