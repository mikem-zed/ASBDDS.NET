using System.Threading.Tasks;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;
using ASBDDS.Web.Shared.Interfaces;
using ASBDDS.Web.Shared.Providers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ASBDDS.Web.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private AuthenticationStateProvider _authenticationStateProvider;

        public TokenResponse TokenResponse { get; private set; }
        public TokenRequest TokenRequest { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Initialize()
        {
            TokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("TokenResponse");
            TokenResponse = await _localStorageService.GetItemAsync<TokenResponse>("TokenRequest");
        }

        public async Task Login(string username, string password)
        {
            TokenRequest = new TokenRequest() {UserName = username, Password = password };
            var resp = await _httpService.Post<ApiResponse<TokenResponse>>("api/users/jwt", TokenRequest);
            if (resp != null && resp.Status.Code == 0)
            {
                TokenResponse = resp.Data;
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