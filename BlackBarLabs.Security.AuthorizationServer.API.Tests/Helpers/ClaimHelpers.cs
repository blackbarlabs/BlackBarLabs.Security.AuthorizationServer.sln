﻿using System;
using System.Net;
using System.Threading.Tasks;

using BlackBarLabs.Api.Tests;
using BlackBarLabs.Security.Authorization;
using BlackBarLabs.Security.AuthorizationServer.API.Controllers;
using BlackBarLabs.Security.CredentialProvider.Facebook.Tests;
using System.Net.Http;

namespace BlackBarLabs.Security.AuthorizationServer.API.Tests
{
    public static class ClaimHelpers
    {
        public static async Task<HttpResponseMessage> ClaimPostAsync(this TestSession testSession, 
            Guid authId, string type, string value, string issuer = default(string))
        {
            if (default(string) == issuer)
                issuer = "http://example.com/issuer";

            var claim = new Resources.ClaimPost()
            {
                Id = Guid.NewGuid(),
                AuthorizationId = authId,
                Issuer = new Uri(issuer),
                Type = new Uri(type),
                Value = value,
                Signature = "",
            };
            return await testSession.PostAsync<ClaimController>(claim);
        }
    }
}