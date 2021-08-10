﻿using System.Threading.Tasks;
using ASBDDS.Shared.Models.Requests;
using ASBDDS.Shared.Models.Responses;

namespace ASBDDS.Web.Shared.Interfaces
{
    public interface IAuthenticationService
    {
        TokenRequest TokenRequest { get; }
        TokenResponse TokenResponse { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
}