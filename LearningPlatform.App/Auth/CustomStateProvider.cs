﻿using LearningPlatform.App.Contracts;
using LearningPlatform.App.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LearningPlatform.App.Auth
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationService authService;
        private readonly ITokenService tokenService;

        public CustomStateProvider(IAuthenticationService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await tokenService.GetTokenAsync();
                if (userInfo != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "user logged") };
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Logout()
        {
            await authService.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Login(LoginViewModel loginParameters)
        {
            await authService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterStudentViewModel registerParameters)
        {
            await authService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task RegisterTeacher(RegisterTeacherViewModel registerTeacherParameters)
        {
            await authService.RegisterTeacher(registerTeacherParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}