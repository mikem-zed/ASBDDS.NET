using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;
using ASBDDS.Web.Shared.Interfaces;
using ASBDDS.Web.Shared.Providers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace ASBDDS.Web.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private AuthenticationStateProvider _authenticationStateProvider;

        public TokenResponse TokenResponse { get; private set; }
        public TokenRequest TokenRequest { get; private set; }
        public TokenRefreshRequest TokenRefreshRequest { get; private set; }

        public AuthenticationService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider
        ) {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Initialize()
        {
            TokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("TokenResponse");
            TokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("TokenRequest");
        }

        public async Task<bool> TryRefreshToken()
        {
            var expiredToken = await _localStorageService.GetItemAsync<TokenResponse>("TokenResponse");


            if (expiredToken == null || expiredToken.RefreshToken == null || expiredToken.AccessToken == null)
                return false;
            
            TokenRefreshRequest = new TokenRefreshRequest
            {
                AccessToken = expiredToken.AccessToken,
                RefreshToken = expiredToken.RefreshToken
            };

            var json = JsonConvert.SerializeObject(TokenRefreshRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/users/jwt/refresh", data);
            var content = await response.Content.ReadAsStringAsync();
            var tokenRefreshResponse = JsonConvert.DeserializeObject<ApiResponse<TokenResponse>>(content);

            if (tokenRefreshResponse != null && tokenRefreshResponse.Status.Code == 0)
            {
                await _localStorageService.RemoveItemAsync("TokenResponse");
                TokenResponse = tokenRefreshResponse.Data;
                await _localStorageService.SetItemAsync("TokenResponse", TokenResponse);
                (_authenticationStateProvider as ApiAuthenticationStateProvider).Notify();
                return true;
            }

            return false;
        }

        public async Task Login(string username, string password)
        {
            TokenRequest = new TokenRequest() {UserName = username, Password = password };

            var json = JsonConvert.SerializeObject(TokenRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/users/jwt", data);
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<ApiResponse<TokenResponse>>(content);

            if (tokenResponse != null && tokenResponse.Status.Code == 0)
            {
                TokenResponse = tokenResponse.Data;
            }
            await _localStorageService.SetItemAsync("TokenResponse", TokenResponse);
            await _localStorageService.SetItemAsync("TokenRequest", TokenRequest);
            (_authenticationStateProvider as ApiAuthenticationStateProvider).Notify();
        }

        public async Task Logout()
        {
            TokenResponse = null;
            TokenRequest = null;
            await _localStorageService.RemoveItemAsync("TokenResponse");
            await _localStorageService.RemoveItemAsync("TokenRequest");
            (_authenticationStateProvider as ApiAuthenticationStateProvider).Notify();
            _navigationManager.NavigateTo("login");
        }
    }
}