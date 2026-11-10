using Avalonia.Controls;
using Avalonia.Interactivity;
using Client.Models;

namespace Client.Views.Components.Generic;

public partial class ConfirmationDialog : Window
{
    public ConfirmationDialog(string header, string message)
    {
        InitializeComponent();
        DataContext = new ConfirmationDialogData
        {
            Header = header,
            Message = message
        };
    }

    private void Confirm_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
