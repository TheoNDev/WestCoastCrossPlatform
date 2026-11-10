using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IHttpService<T> where T : class
{
    Task DeleteAsync(string resource, string objectId);
    Task<List<T>> GetAllAsync(string resource);
    Task PostAsync(string resource, T data);
    Task PutAsync(string resource, T data, string objectId);
}
