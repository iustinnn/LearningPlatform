﻿using LearningPlatform.App.Contracts;
using LearningPlatform.App.ViewModels;
using System.Net.Http.Json;

namespace LearningPlatform.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public AuthenticationService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task Login(LoginViewModel loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/authentication/login", loginRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            await tokenService.SetTokenAsync(token);
        }

        public async Task Logout()
        {
            await tokenService.RemoveTokenAsync();
            var result = await httpClient.PostAsync("api/v1/authentication/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterStudentViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register/student", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }

        public async Task RegisterTeacher(RegisterTeacherViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register/teacher", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }
    }
}
