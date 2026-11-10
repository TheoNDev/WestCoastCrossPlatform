
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Client.Enums;

namespace Client.Models;

public class Course(
    string courseNumber,
    string title,
    int duration,
    DateTime startDate,
    DateTime endDate,
    CourseLocation location
    )
{
    [JsonPropertyName("_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ScaffoldColumn(false)]
    public string? Id { get; set; }
    [Display(Name = "Kursnummer")]
    public string CourseNumber { get; set; } = courseNumber;
    [Display(Name = "Namn")]
    public string Title { get; set; } = title;
    [Display(Name = "Längd")]
    public int Duration { get; set; } = duration;
    [Display(Name = "Startdatum")]
    public DateTime StartDate { get; set; } = startDate;
    [Display(Name = "Slutdatum")]
    public DateTime EndDate { get; set; } = endDate;
    [Display(Name = "Plats")]
    public CourseLocation Location { get; set; } = location;
}
