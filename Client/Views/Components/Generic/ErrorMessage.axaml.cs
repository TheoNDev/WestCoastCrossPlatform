using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Views.Components.Generic;

public partial class ErrorMessage : UserControl
{
    public static readonly StyledProperty<string?> MessageProperty =
      AvaloniaProperty.Register<ErrorMessage, string?>(
          nameof(Message)
      );

    public string? Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }
    public ErrorMessage()
    {
        InitializeComponent();
    }
}