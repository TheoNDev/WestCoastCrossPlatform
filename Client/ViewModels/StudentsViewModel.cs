using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Enums;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class StudentsViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;
    public StudentFormViewModel Form { get; } = new();
    [ObservableProperty]
    public partial ObservableCollection<Student> Students { get; set; } = [];


    public bool IsListVisible => CurrentMode == StudentViewMode.List;
    public bool IsFormVisible => CurrentMode == StudentViewMode.Add;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListVisible))]
    [NotifyPropertyChangedFor(nameof(IsFormVisible))]
    public partial StudentViewMode CurrentMode { get; set; } = StudentViewMode.List;

    public string FormTitle => "Registrera student";

    public string FormDescription =>
        "Ange information om den nya studenten.";

    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }

    public StudentsViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        PageTitle = "Studenter";
        PageDescription = "Hantera utbildnignens studenter";
        InitAsync();
    }

    [RelayCommand]
    private async Task SaveStudentAsync()
    {
        if (!Form.ValidateForm())
            return;

        Student student = Form.ToStudent();

        try
        {
            ErrorMessage = null;
            await _studentService.AddAsync(student);
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = HttpErrorHelper.GetMessage(ex);
            return;
        }

        CancelForm();
        await LoadStudentsAsync();
    }

    [RelayCommand]
    private void ShowAddForm()
    {
        CurrentMode = StudentViewMode.Add;
    }
    [RelayCommand]
    private void CancelForm()
    {
        Form.ResetForm();
        CurrentMode = StudentViewMode.List;
    }

    private async void InitAsync()
    {
        await LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync()
    {
        try
        {
            var students = await _studentService.GetAllAsync();

            Students = new ObservableCollection<Student>(students);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
