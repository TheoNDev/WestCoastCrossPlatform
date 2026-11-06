using WestcoastEducation.Enums;
using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Models;

public abstract class Person
(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address,
    Role role
) : IIdentifiable
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string SocialSecurityNumber { get; set; } = socialSecurityNumber;
    public Address Address { get; set; } = address;
    public Role GetRole { get; } = role;

    public string Id => SocialSecurityNumber;

    public override string ToString()
    {
        return $"""
        Roll:              {GetRole}
        Förnamn:           {FirstName}
        Efternamn:         {LastName}
        TelefonNummer:     {PhoneNumber}
        Address:           
            Gata:          {Address.StreetName}
            Postnummer:    {Address.ZipCode}
            Ort:           {Address.City}
        """;
    }
}
