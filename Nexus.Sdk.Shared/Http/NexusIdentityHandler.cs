﻿using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Shared.Authentication;
using Nexus.Sdk.Token.Extensions;

namespace Nexus.Sdk.Shared.Http
{
    public class NexusIdentityHandler : DelegatingHandler
    {
        private readonly IAuthProvider _provider;

        public NexusIdentityHandler(IAuthProvider provider)
        {
            _provider = provider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var provider = request.Options.TryGetValue(
                ApplicationScopeHttpClientExtensions.ScopeKey,
                out var serviceProvider)
                ? serviceProvider.GetRequiredService<IAuthProvider>()
                : _provider;
            var accessToken = await provider.GetAccessTokenAsync();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
