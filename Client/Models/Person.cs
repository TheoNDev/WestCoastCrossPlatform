using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models;

public abstract class Person
(
    string firstName,
    string lastName,
    string phoneNumber,
    string socialSecurityNumber,
    Address address
)
{
    [JsonPropertyName("_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ScaffoldColumn(false)]
    public string? Id { get; set; }
    [Display(Name = "Förnamn", Order = 1)]
    public string FirstName { get; set; } = firstName;
    [Display(Name = "Efternamn", Order = 2)]
    public string LastName { get; set; } = lastName;
    [Display(Name = "Telefonnummer", Order = 3)]
    public string PhoneNumber { get; set; } = phoneNumber;
    [Display(Name = "Personnummer", Order = 4)]
    public string SocialSecurityNumber { get; set; } = socialSecurityNumber;
    [Display(Name = "Address", Order = 5)]
    public Address Address { get; set; } = address;
}
