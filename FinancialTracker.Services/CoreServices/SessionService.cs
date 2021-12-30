using FinanceTrackerAPI.Models.DTOs;
using FinancialTracker.Core.Lib.CoreServices;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FinancialTracker.Services.CoreServices
{
    public class SessionService : ISessionService
    {
        private const string ACCESS_TOKEN_NAME = "token";
        private const string USER_NAME_TOKEN = "username";
        private readonly IDistributedCache cache;

        public SessionService(IDistributedCache cache)
        {
            this.cache = cache;
        }


        /// <summary>
        /// Destroys access and refresh tokens from the session.
        /// </summary>
        public void ClearTokens()
        {
            try
            {
                this.cache.Remove(ACCESS_TOKEN_NAME);
            }
            catch (Exception error)
            {
                Console.WriteLine("LOGOUT ERROR: " + error.Message);
            }
        }


        /// <summary>
        /// Return access token, if saved, or an empty string.
        /// </summary>
        public string GetAccessToken()
        {
            return TryGetString(ACCESS_TOKEN_NAME);
        }

        /// <summary>
        /// Sets the access and refresh tokens based on an HTTP response.
        /// </summary>
        public void SetTokens(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> attrs = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                this.cache.SetString(ACCESS_TOKEN_NAME, attrs["access_token"]);
            }
        }

        /// <summary>
        /// Return session value as a string (if it exists), or an empty string.
        /// </summary>
        private string TryGetString(string name)
        {
            byte[] valueBytes = new Byte[700];
            valueBytes = this.cache.GetAsync(name).Result;
            if (valueBytes != null && valueBytes.Length > 0)
            {
                return System.Text.Encoding.UTF8.GetString(valueBytes);
            }
            return null;
        }

        /// <summary>
        /// Sets the access and refresh tokens based on an HTTP response.
        /// </summary>
        public void SetUsername(string username)
        {
             this.cache.SetString(USER_NAME_TOKEN, username);
        }

        /// <summary>
        /// Return user name, if saved, or an empty string.
        /// </summary>
        public string GetUsername()
        {
            return TryGetString(USER_NAME_TOKEN);
        }
    }
}
