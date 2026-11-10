using System;
using System.ComponentModel.DataAnnotations;
using Client.Enums;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class CourseFormViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Kursnummer måste anges")]
    [MinLength(2, ErrorMessage = "Kursnumret måste innehålla minst 2 tecken")]

    public partial string CourseNumber { get; set; } = "";
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Kursnamn måste anges")]
    [MinLength(3, ErrorMessage = "Kursnamnet måste innehålla minst 3 tecken")]
    public partial string Title { get; set; } = "";
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Kurslängd måste anges")]
    [Range(1, 104, ErrorMessage = "Kurslängden måste vara mellan 1 och 104 veckor")]
    public partial decimal? Duration { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Startdatum måste anges")]
    public partial DateTime? StartDate { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Slutdatum måste anges")]
    [CustomValidation(typeof(CourseFormViewModel), nameof(ValidateEndDate))]
    public partial DateTime? EndDate { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Du måste välja om kursen är på plats eller på distans")]
    public partial CourseLocation? Location { get; set; }
    public Array Locations { get; } = Enum.GetValues<CourseLocation>();

    public bool ValidateForm()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
    public static ValidationResult? ValidateEndDate(
        DateTime? endDate,
        ValidationContext context
    )
    {
        var vm = (CourseFormViewModel)context.ObjectInstance;

        if (endDate is null || vm.StartDate is null)
            return ValidationResult.Success;

        if (endDate.Value <= vm.StartDate.Value)
            return new ValidationResult(
                "Slutdatum måste vara senare än startdatum"
            );

        return ValidationResult.Success;
    }

    public void ResetForm()
    {
        CourseNumber = "";
        Title = "";
        Duration = null;
        StartDate = null;
        EndDate = null;
        Location = null;
        ClearErrors();
    }

    public Course ToCourse(string? id)
    {
        int duration = (int)Duration!.Value;

        Course course = new(
            CourseNumber,
            Title,
            duration,
            StartDate!.Value,
            EndDate!.Value,
            (CourseLocation)Location!
        )
        {
            Id = id
        };

        return course;
    }
    public void LoadCourse(Course course)
    {
        CourseNumber = course.CourseNumber;
        Title = course.Title;
        Duration = course.Duration;
        StartDate = course.StartDate;
        EndDate = course.EndDate;
        Location = course.Location;
    }
}
