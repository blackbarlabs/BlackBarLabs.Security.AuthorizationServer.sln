﻿using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BlackBarLabs.Security.AuthorizationServer.API.Models;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using BlackBarLabs.Api;
using BlackBarLabs.Security.Authorization;

namespace BlackBarLabs.Security.AuthorizationServer.API.Resources
{
    [DataContract]
    public class CredentialPost : Credential, IHttpActionResult
    {
        #region Actionables
        
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var foo = new Authorizations.CreateCredentialResult();
            var creationResults = await Context.Authorizations.CreateCredentialsAsync(AuthorizationId,
                this.Method, this.Provider, this.UserId, this.Token, this.ClaimsProviders,
                () => Request.CreateResponse(HttpStatusCode.Created, this),
                () => Request.CreateErrorResponse(HttpStatusCode.Conflict, "Authentication failed"),
                () => Request.CreateErrorResponse(HttpStatusCode.Conflict, "Authorization does not exist"),
                (alreadyAssociatedAuthId) =>
                {
                    var alreadyAssociatedAuthIdUrl = (string)"";
                    return Request.CreateResponse(HttpStatusCode.Conflict, alreadyAssociatedAuthIdUrl);
                });
            return creationResults;
        }

        #endregion
    }
}
