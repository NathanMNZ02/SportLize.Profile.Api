using System;
using System.Globalization;
using System.Threading;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportLize.Profile.Api.Profile.ClientHttp.Abstraction;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.ClientHttp
{
	public class ClientHttp : IClientHttp
	{
		private readonly HttpClient _httpClient;

		public ClientHttp(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        public async Task<List<UserReadDto>?> GetAllUsers(CancellationToken cancellationToken = default){
            var response = await _httpClient.GetAsync($"ProfileUser/GetAllUsers", cancellationToken);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<List<UserReadDto>?>(cancellationToken: cancellationToken);
        }

        public async Task<UserReadDto?> GetUser(int userId, CancellationToken cancellationToken = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>() {
                { "id", userId.ToString(CultureInfo.InvariantCulture) }
            });
            var response = await _httpClient.GetAsync($"ProfileUser/GetUser{queryString}", cancellationToken);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<UserReadDto?>(cancellationToken: cancellationToken);
        }

        public async Task<List<PostReadDto>?> GetAllPost(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"ProfilePost/GetAllPosts", cancellationToken);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<List<PostReadDto>?>(cancellationToken: cancellationToken);
        }
    }
}

