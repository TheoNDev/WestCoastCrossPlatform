using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ITeacherService : IEntityService<Teacher>
{
    Task UpdateAsync(Teacher teacher, string id);
}
