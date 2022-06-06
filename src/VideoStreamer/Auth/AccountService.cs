using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VideoStreamer.Shared;

namespace VideoStreamer.Auth
{
    public interface IAccountService
    {
        Task LogoutAsync();
        Task LoginAsync(string deviceId, string code, string video);
        void Login(string token);
    }

    public class AccountService : IAccountService
    {
        private readonly TokenAuthenticationProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenSettings _tokenSettings;

        public AccountService(TokenAuthenticationProvider authenticationStateProvider,
            ILocalStorageService localStorageService, IOptions<TokenSettings> tokeOptions, HttpClient httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _tokenSettings = tokeOptions.Value;
        }

        public async void Login(string token)
        {
            await _localStorageService.SetItemAsync("token", token);

            _authenticationStateProvider.Notify();
        }


        public async Task LoginAsync(string deviceId, string code, string video)
        {
            await _localStorageService.SetItemAsync($"token", Token(deviceId, code, video));

            _authenticationStateProvider.Notify();
        }


        public async Task<string> TokenAsync(string deviceId, string code)
        {
            using var msg = await _httpClient.PostAsJsonAsync("/api/auth/login", new AuthorizationRequest()
            {
                DeviceId = deviceId,
                Code = code
            });
            msg.EnsureSuccessStatusCode();


            var result = await msg.Content.ReadFromJsonAsync<AuthorizationResponse>();
            return result.jwtBearer;
        }

        public string Token(string deviceId, string code, string video)
        {
            // var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            // var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var userCliams = new Claim[]
            {
                new Claim("deviceId", deviceId),
                new Claim("resource", video),
                // new Claim("videoId", videoId),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: $"{deviceId}",
                expires: DateTime.Now.AddMinutes(20),
                // signingCredentials: credentials,
                claims: userCliams
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }


        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("token");

            _authenticationStateProvider.Notify();
        }
    }
}