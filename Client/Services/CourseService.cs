using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class CourseService(IHttpService<Course> httpService) : ICourseService
{
    private readonly IHttpService<Course> _httpService = httpService;
    private readonly string _resource = "courses";
    public async Task AddAsync(Course course)
    {
        await _httpService.PostAsync(_resource, course);
    }

    public async Task DeleteAsync(string id)
    {
        await _httpService.DeleteAsync(_resource, id);
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _httpService.GetAllAsync(_resource);
    }

    public async Task UpdateAsync(Course course, string id)
    {
        await _httpService.PutAsync(_resource, course, id);
    }
}
