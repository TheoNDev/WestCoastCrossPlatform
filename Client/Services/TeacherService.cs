using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class TeacherService(IHttpService<Teacher> httpService) : ITeacherService
{
    private readonly IHttpService<Teacher> _httpService = httpService;
    private readonly string _resource = "teachers";
    public async Task AddAsync(Teacher teacher)
    {
        await _httpService.PostAsync(_resource, teacher);
    }

    public Task<List<Teacher>> GetAllAsync()
    {
        return _httpService.GetAllAsync(_resource);
    }

    public async Task UpdateAsync(Teacher teacher, string id)
    {
        await _httpService.PutAsync(_resource, teacher, id);
    }
}
