using System;
using System.ComponentModel.DataAnnotations;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class TeacherFormViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Förnamn måste anges")]
    public partial string FirstName { get; set; } = "";
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Efternamn måste anges")]
    public partial string LastName { get; set; } = "";
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Telefonnummer måste anges")]
    public partial string PhoneNumber { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Personnummer måste anges")]
    public partial string SocialSecurityNumber { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Gatuaddress måste anges")]
    public partial string StreetName { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Postnummer måste anges")]
    public partial string ZipCode { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Stad måste anges")]
    public partial string City { get; set; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Kunskapsområde måste anges")]
    public partial string FieldOfKnowledge { get; set; }

    public bool ValidateForm()
    {
        ValidateAllProperties();
        return !HasErrors;
    }

    public void ResetForm()
    {
        FirstName = "";
        LastName = "";
        PhoneNumber = "";
        SocialSecurityNumber = "";
        StreetName = "";
        ZipCode = "";
        City = "";
        FieldOfKnowledge = "";
        ClearErrors();
    }

    public Teacher ToTeacher(string? id)
    {
        Address address = new(StreetName, ZipCode, City);
        Teacher teacher = new(
            FirstName,
            LastName,
            PhoneNumber,
            SocialSecurityNumber,
            address,
            FieldOfKnowledge,
            []
        )
        {
            Id = id
        };

        return teacher;
    }

    public void LoadTeacher(Teacher teacher)
    {
        FirstName = teacher.FirstName;
        LastName = teacher.LastName;
        PhoneNumber = teacher.PhoneNumber;
        SocialSecurityNumber = teacher.SocialSecurityNumber;
        StreetName = teacher.Address.StreetName;
        ZipCode = teacher.Address.ZipCode;
        City = teacher.Address.City;
        FieldOfKnowledge = teacher.FieldOfKnowledge;
    }
}
