using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Cwk.Web.Helpers;
using Cwk.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Cwk.Web.Auth;

public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _httpClient;
    public static readonly string TOKENKEY = "TOKENKEY";

    public AuthenticationProviderJWT(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _js = jsRuntime;
        _httpClient = httpClient;
    }

    private static AuthenticationState Anonimo => 
        new(new ClaimsPrincipal(new ClaimsIdentity()));
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.GetFromLocalStorage(TOKENKEY);

        if (string.IsNullOrEmpty(token))
        {
            return Anonimo;
        }

        return BuildAuthenticationState(token);
    }
    
    private AuthenticationState BuildAuthenticationState(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }


    private static List<Claim>? ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        claims.AddRange(keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
        if (keyValuePairs!.TryGetValue("role", out var roleValue))
        {
            claims.Add(new Claim(ClaimTypes.Role, roleValue.ToString()!));
        }
        return claims;
    }

    
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }

    
    public async Task Login(string token)
    {
        await _js.SetInLocalStorage(TOKENKEY, token);
        var authState = BuildAuthenticationState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _js.RemoveItem(TOKENKEY);
        NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
    }

}