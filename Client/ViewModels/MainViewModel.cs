using Client.Enums;
using Client.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly PageViewFactory _pageViewFactory;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsCoursesActive))]
    [NotifyPropertyChangedFor(nameof(IsStudentsActive))]
    [NotifyPropertyChangedFor(nameof(IsTeachersActive))]
    public partial ViewModelBase CurrentView { get; set; } = new();

    public bool IsCoursesActive => CurrentView is CoursesViewModel;

    public bool IsStudentsActive => CurrentView is StudentsViewModel;

    public bool IsTeachersActive => CurrentView is TeachersViewModel;

    public MainViewModel(PageViewFactory pageViewFactory)
    {
        _pageViewFactory = pageViewFactory;
        GoTo(PageViewsEnum.Courses);
    }

    [RelayCommand]
    private void GoTo(PageViewsEnum page) => CurrentView = _pageViewFactory.LoadPageViewModel(page);
}
