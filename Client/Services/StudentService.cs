using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class StudentService(IHttpService<Student> httpService) : IStudentService
{
    private readonly IHttpService<Student> _httpService = httpService;
    private readonly string _resource = "students";

    public async Task AddAsync(Student student)
    {
        await _httpService.PostAsync(_resource, student);
    }

    public Task<List<Student>> GetAllAsync()
    {
        return _httpService.GetAllAsync(_resource);
    }
}
