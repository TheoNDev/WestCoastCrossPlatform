using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Client.Enums;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Client.Views.Components.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{

    private readonly ICourseService _courseService;
    public CourseFormViewModel Form { get; } = new();


    [ObservableProperty]
    public partial ObservableCollection<Course> Courses { get; set; } = [];
    [ObservableProperty]
    public partial Course? SelectedCourse { get; set; }


    public bool IsListVisible => CurrentMode == CourseViewMode.List;
    public bool IsDetailsVisible => CurrentMode == CourseViewMode.Details;
    public bool IsFormVisible => CurrentMode is CourseViewMode.Add or CourseViewMode.Edit;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListVisible))]
    [NotifyPropertyChangedFor(nameof(IsDetailsVisible))]
    [NotifyPropertyChangedFor(nameof(IsFormVisible))]
    [NotifyPropertyChangedFor(nameof(FormTitle))]
    [NotifyPropertyChangedFor(nameof(FormDescription))]
    public partial CourseViewMode CurrentMode { get; set; } = CourseViewMode.List;
    public string FormTitle => CurrentMode switch
    {
        CourseViewMode.Add => "Registrera kurs",
        CourseViewMode.Edit => "Redigera kurs",
        _ => string.Empty
    };
    public string FormDescription => CurrentMode switch
    {
        CourseViewMode.Add => "Ange information om den nya kursen.",
        CourseViewMode.Edit => "Ändra information för den valda kursen.",
        _ => string.Empty
    };
    public string DetailsTitle => "Kursinformation";

    public string DetailsDescription =>
        "Visa och hantera information om den valda kursen.";

    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }


    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Kurser";
        PageDescription = "Hantera utbildningens kurser";
        InitAsync();
    }

    [RelayCommand]
    private async Task SaveCourseAsync()
    {

        if (!Form.ValidateForm())
            return;

        string? id = CurrentMode == CourseViewMode.Edit
            ? SelectedCourse?.Id
            : null;

        Course course = Form.ToCourse(id);

        try
        {
            ErrorMessage = null;
            Task operation = CurrentMode switch
            {
                CourseViewMode.Add =>
                    _courseService.AddAsync(course),

                CourseViewMode.Edit when id is not null =>
                    _courseService.UpdateAsync(course, id),
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
        await LoadCoursesAsync();
    }

    [RelayCommand]
    private async Task DeleteCourseAsync()
    {

        if (SelectedCourse?.Id is not string id)
            return;

        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            return;

        Window? owner = desktop.MainWindow;

        if (owner is null)
            return;


        var dialog = new ConfirmationDialog(
                        "Radera kurs?",
                        $"Är du säker på att du vill radera kursen {SelectedCourse.Title}?"
                    );

        bool confirmed = await dialog.ShowDialog<bool>(owner);

        if (!confirmed)
            return;


        try
        {
            ErrorMessage = null;
            await _courseService.DeleteAsync(id);
            await ToList();
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = HttpErrorHelper.GetMessage(ex);
        }
    }

    [RelayCommand]
    private void ShowAddForm()
    {
        CurrentMode = CourseViewMode.Add;
    }

    [RelayCommand]
    private void ShowEditForm()
    {
        Form.LoadCourse(SelectedCourse!);
        CurrentMode = CourseViewMode.Edit;
    }

    [RelayCommand]
    private void CancelForm()
    {
        Form.ResetForm();
        SelectedCourse = null;
        CurrentMode = CourseViewMode.List;
    }

    [RelayCommand]
    private async Task ToList()
    {
        SelectedCourse = null;
        await LoadCoursesAsync();
        CurrentMode = CourseViewMode.List;
    }
    partial void OnSelectedCourseChanged(Course? value)
    {
        if (value is null)
            return;

        CurrentMode = CourseViewMode.Details;
    }

    private async void InitAsync()
    {
        await LoadCoursesAsync();
    }
    private async Task LoadCoursesAsync()
    {
        try
        {
            ErrorMessage = null;

            var courses = await _courseService.GetAllAsync();

            Courses = new ObservableCollection<Course>(courses);
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = HttpErrorHelper.GetMessage(ex);
        }
    }
}
