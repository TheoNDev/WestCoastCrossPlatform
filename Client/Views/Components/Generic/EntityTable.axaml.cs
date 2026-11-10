using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Client.Views.Components.Generic;

public partial class EntityTable : UserControl
{

    public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
        AvaloniaProperty.Register<EntityTable, IEnumerable?>(nameof(ItemsSource));

    public IEnumerable? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly StyledProperty<object?> SelectedEntityProperty =
        AvaloniaProperty.Register<EntityTable, object?>(
            nameof(SelectedEntity),
            defaultBindingMode: BindingMode.TwoWay
            );
    public object? SelectedEntity
    {
        get => GetValue(SelectedEntityProperty);
        set => SetValue(SelectedEntityProperty, value);
    }
    public EntityTable()
    {
        InitializeComponent();
    }

    private void OnAutoGeneratingColumn(
        object? sender,
        DataGridAutoGeneratingColumnEventArgs e
    )
    {
        var firstItem = ItemsSource?.Cast<object>().FirstOrDefault();
        Type? modelType = firstItem?.GetType();

        var property = modelType?.GetProperty(e.PropertyName);

        var scaffold = property?.GetCustomAttribute<ScaffoldColumnAttribute>();
        if (scaffold?.Scaffold == false)
        {
            e.Cancel = true;
            return;
        }

        var display = property?.GetCustomAttribute<DisplayAttribute>();

        if (display is not null)
        {
            e.Column.Header = display.GetName();
        }

        if (
            property?.PropertyType == typeof(DateTime) ||
            property?.PropertyType == typeof(DateTime?)
            )
        {
            if (e.Column is DataGridTextColumn column)
            {
                column.Binding = new Binding(e.PropertyName)
                {
                    StringFormat = "{0:yyyy-MM-dd}",
                    Mode = BindingMode.OneWay
                };
            }
        }
    }
}