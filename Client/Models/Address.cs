namespace Client.Models;

public record Address(
    string StreetName,
    string ZipCode,
    string City
)
{
    public override string ToString()
    {
        return $"{StreetName}, {ZipCode} {City}";
    }
}
