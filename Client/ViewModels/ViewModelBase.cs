using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class ViewModelBase : ObservableValidator
{
    [ObservableProperty]
    public partial string PageTitle { get; set; } = "";
    [ObservableProperty]
    public partial string PageDescription { get; set; } = "";
}
