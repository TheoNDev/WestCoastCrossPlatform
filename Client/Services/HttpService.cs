using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Client.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

public class HttpService<T>(HttpClient httpClient) : IHttpService<T> where T : class
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public async Task DeleteAsync(string resource, string objectId)
    {
        using var response = await _httpClient.DeleteAsync($"{resource}/{objectId}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<T>> GetAllAsync(string resource)
    {
        using var response = await _httpClient.GetAsync(resource);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(result, _options) ?? [];
    }

    public async Task PostAsync(string resource, T data)
    {
        using var response = await _httpClient.PostAsJsonAsync(resource, data, _options);

        response.EnsureSuccessStatusCode();
    }

    public async Task PutAsync(string resource, T data, string objectId)
    {
        using var response = await _httpClient.PutAsJsonAsync(
            $"{resource}/{objectId}",
            data,
            _options
        );

        response.EnsureSuccessStatusCode();
    }
}
