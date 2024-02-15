using System;
using System.Globalization;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SportLize.Profile.Api.Profile.Repository.Enumeration;
using SportLize.Profile.Api.Profile.Shared;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.ClientHttp.Abstraction
{
	public interface IClientHttp
	{
        Task<List<UserReadDto>?> GetAllUsers(CancellationToken cancellationToken = default);
        Task<UserReadDto?> GetUser(int userId, CancellationToken cancellationToken = default);

        Task<List<PostReadDto>?> GetAllPost(CancellationToken cancellationToken = default);
    }
}

