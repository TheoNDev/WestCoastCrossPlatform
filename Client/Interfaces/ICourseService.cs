using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ICourseService : IEntityService<Course>
{
    Task DeleteAsync(string id);
    Task UpdateAsync(Course course, string id);
}
