namespace Client.Models;

public class Student
(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address
) :
Person
(
    firstName,
    lastName,
    phoneNumber,
    socialSecurityNumber,
    address
)
{

}
