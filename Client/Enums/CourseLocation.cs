using System.Text.Json.Serialization;

namespace Client.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<CourseLocation>))]
public enum CourseLocation
{
    Distans,
    Klassrum
}