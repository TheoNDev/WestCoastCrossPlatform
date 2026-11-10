using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Factories;

public class PageViewFactory(IServiceProvider serviceProvider) : IPageViewFactory
{
    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        return page switch
        {
            PageViewsEnum.Courses =>
                serviceProvider.GetRequiredService<CoursesViewModel>(),
            PageViewsEnum.Students =>
                serviceProvider.GetRequiredService<StudentsViewModel>(),
            PageViewsEnum.Teachers =>
                serviceProvider.GetRequiredService<TeachersViewModel>(),
            _ => throw new InvalidOperationException("Den begärda sidan kunde inte hittas"),
        };
    }

    // public ViewModelBase LoadPageViewModel(PageViewsEnum page, string itemNumber)
    // {
    //     return page switch
    //     {
    //         PageViewsEnum.EditProduct => new EditProductViewModel(new ProductServices(), itemNumber),
    //         _ => throw new InvalidOperationException("Det fanns inte det du ville ha")
    //     };
    // }
}
