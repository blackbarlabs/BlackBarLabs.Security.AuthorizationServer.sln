﻿using BlackBarLabs.Security.Authorization;
using BlackBarLabs.Security.AuthorizationServer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BlackBarLabs.Security.AuthorizationServer.API.Resources
{
    [DataContract]
    public class Session : Resource, ISession
    {
        #region Properties

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AuthorizationId { get; set; }

        [DataMember]
        public AuthHeaderProps SessionHeader { get; set; }

        [IgnoreDataMember]
        private Resources.Credential credentials;

        [DataMember]
        public Resources.Credential Credentials
        {
            get { return this.credentials; }
            set { this.credentials = value; }
        }

        ICredential ISession.Credentials
        {
            get { return this.credentials; }
            set
            {
                this.credentials = new Credential()
                {
                    AuthorizationId = value.AuthorizationId,
                    Method = value.Method,
                    Provider = value.Provider,
                    Token = value.Token,
                    UserId = value.UserId,
                };
            }
        }

        [DataMember]
        public string RefreshToken { get; set; }
        
        #endregion

        protected bool IsCredentialsPopulated()
        {
            if (default(Resources.Credential) == Credentials)
                return false;
            return
                (this.Credentials.Provider != default(Uri)) &&
                (!String.IsNullOrWhiteSpace(this.Credentials.UserId)) &&
                (!String.IsNullOrWhiteSpace(this.Credentials.Token));
        }
    }
}