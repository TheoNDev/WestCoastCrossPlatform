using WestcoastEducation.Enums;
using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Models;

public class Course(string courseNumber, string title, int duration, DateTime startDate, DateTime endDate, CourseLocation location) : IIdentifiable
{
    public string CourseNumber { get; set; } = courseNumber;
    public string Title { get; set; } = title;
    public int Duration { get; set; } = duration;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
    public CourseLocation Location { get; set; } = location;

    public string Id => CourseNumber;

    public override string ToString()
    {
        return $"""
        Kursnummer:        {CourseNumber}
        Titel:             {Title}
        Längd:             {Duration} veckor
        Startdatum:        {StartDate:yyyy-MM-dd}
        Slutdatum:         {EndDate:yyyy-MM-dd}
        Undervisningsform: {Location}
        """;
    }
}
