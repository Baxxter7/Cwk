using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Cwk.Web.Helpers;
using Microsoft.JSInterop;

namespace Cwk.Web.Services;

public class RequestService :IRequestService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;

    public static readonly string TOKENKEY = "TOKENKEY";

    public RequestService(HttpClient httpClient, IJSRuntime js)
    {
        _httpClient = httpClient;
        _js = js;
    }
    
    private static JsonSerializerOptions JsonDefaultOptions => new()
    {
        PropertyNameCaseInsensitive = true,
    };
    
    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await _js.GetFromLocalStorage(TOKENKEY);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
    
    public async Task<T> GetAsync<T>(string url)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, JsonDefaultOptions)!;
    }

    public async Task<T> GetByIdAsync<T>(string url, int id)
    {
        await AddAuthorizationHeaderAsync();
        var requestUrl = $"{url}/{id}";
        var response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, JsonDefaultOptions)!;
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
    {
        await AddAuthorizationHeaderAsync();
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        return response;
    }

    public async Task<object> DeleteAsync(string url)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<HttpResponseMessage> PutAsync<T>(string url, T model)
    {
        await AddAuthorizationHeaderAsync();
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(url, content);
        return response;
    }

    public async Task<HttpResponseMessage> PutAsync(string url)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.PutAsync(url, null);
        return response;
    }
    public async Task<TResponse> PostAndReadAsync<TResponse, TRequest>(string url, TRequest model)
    {
        await AddAuthorizationHeaderAsync();
        var response = await PostAsync(url, model);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(responseContent, JsonDefaultOptions)!;
    }
}