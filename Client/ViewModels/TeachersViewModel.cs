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

public partial class TeachersViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;
    public TeacherFormViewModel Form { get; } = new();
    [ObservableProperty]
    public partial ObservableCollection<Teacher> Teachers { get; set; } = [];
    [ObservableProperty]
    public partial Teacher? SelectedTeacher { get; set; }


    public bool IsListVisible => CurrentMode == TeacherViewMode.List;
    public bool IsDetailsVisible => CurrentMode == TeacherViewMode.Details;
    public bool IsFormVisible => CurrentMode is TeacherViewMode.Add or TeacherViewMode.Edit;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListVisible))]
    [NotifyPropertyChangedFor(nameof(IsDetailsVisible))]
    [NotifyPropertyChangedFor(nameof(IsFormVisible))]
    [NotifyPropertyChangedFor(nameof(FormTitle))]
    [NotifyPropertyChangedFor(nameof(FormDescription))]
    public partial TeacherViewMode CurrentMode { get; set; } = TeacherViewMode.List;
    public string FormTitle => CurrentMode switch
    {
        TeacherViewMode.Add => "Registrera lärare",
        TeacherViewMode.Edit => "Redigera lärare",
        _ => string.Empty
    };
    public string FormDescription => CurrentMode switch
    {
        TeacherViewMode.Add => "Ange information om den nya läraren.",
        TeacherViewMode.Edit => "Ändra information för den valda läraren.",
        _ => string.Empty
    };
    public string DetailsTitle => "Lärarinformation";

    public string DetailsDescription =>
        "Visa och hantera information om den valda läraren.";

    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }

    public TeachersViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        PageTitle = "Lärare";
        PageDescription = "Hantera utbildnignens lärare";
        InitAsync();
    }

    [RelayCommand]
    private async Task SaveTeacherAsync()
    {

        if (!Form.ValidateForm())
            return;

        string? id = CurrentMode == TeacherViewMode.Edit
            ? SelectedTeacher?.Id
            : null;

        Teacher teacher = Form.ToTeacher(id);

        try
        {
            ErrorMessage = null;
            Task operation = CurrentMode switch
            {
                TeacherViewMode.Add =>
                    _teacherService.AddAsync(teacher),

                TeacherViewMode.Edit when id is not null =>
                    _teacherService.UpdateAsync(teacher, id),
                _ =>
                    throw new InvalidOperationException("Det går inte att spara kursen i aktuellt läge."),
            };

            await operation;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = HttpErrorHelper.GetMessage(ex);
            return;
        }

        CancelForm();
        await LoadTeachersAsync();
    }

    [RelayCommand]
    private void ShowAddForm()
    {
        CurrentMode = TeacherViewMode.Add;
    }

    [RelayCommand]
    private void ShowEditForm()
    {
        Form.LoadTeacher(SelectedTeacher!);
        CurrentMode = TeacherViewMode.Edit;
    }
    [RelayCommand]
    private void CancelForm()
    {
        Form.ResetForm();
        SelectedTeacher = null;
        CurrentMode = TeacherViewMode.List;
    }
    [RelayCommand]
    private async Task ToList()
    {
        SelectedTeacher = null;
        await LoadTeachersAsync();
        CurrentMode = TeacherViewMode.List;
    }


    partial void OnSelectedTeacherChanged(Teacher? value)
    {
        if (value is null)
            return;

        CurrentMode = TeacherViewMode.Details;
    }

    private async void InitAsync()
    {
        await LoadTeachersAsync();
    }

    private async Task LoadTeachersAsync()
    {
        try
        {
            var teachers = await _teacherService.GetAllAsync();

            Teachers = new ObservableCollection<Teacher>(teachers);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
