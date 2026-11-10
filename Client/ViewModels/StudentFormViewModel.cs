using System;
using System.ComponentModel.DataAnnotations;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class StudentFormViewModel : ViewModelBase
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
        ClearErrors();
    }

    public Student ToStudent()
    {
        Address address = new(StreetName, ZipCode, City);
        Student student = new(
            FirstName,
            LastName,
            PhoneNumber,
            SocialSecurityNumber,
            address
        );

        return student;
    }
}
