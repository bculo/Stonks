using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stonk.Application.Extensions;
using Stonk.Application.Models;
using Stonk.Application.Options.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stonk.Service.Clients.Common
{
    public abstract class HttpBaseClient
    {
        protected readonly UrlBaseOptions _url;
        protected readonly ILogger<HttpBaseClient> _logger;
        protected readonly HttpClient _http;

        private Dictionary<string, string> QueryParams { get; set; }

        public HttpBaseClient(HttpClient http, UrlBaseOptions url, ILogger<HttpBaseClient> logger)
        {
            _http = http;
            _url = url;
            _logger = logger;

            QueryParams = new Dictionary<string, string>();

            SetBaseAddress();
        }

        private void SetBaseAddress()
        {
            if (string.IsNullOrEmpty(_url.Url))
                throw new ArgumentNullException(nameof(_url.Url));

            _logger.LogInformation("Setting up HTTP client base address");

            _http.BaseAddress = new Uri(_url.Url);

            _logger.LogInformation("Base address defiend {0}", _url.Url);
        }

        private async Task<Result<T>> HandleGoodRequest<T>(HttpResponseMessage response)
        {
            var content = await ReadResponseContent(response);

            var instnace = JsonConvert.DeserializeObject<T>(content);

            return ResultExtensions.Success(instnace);
        }

        private Result<T> HandleBadRequest<T>(HttpResponseMessage response)
        {
            var result = ResultExtensions.Failure<T>("unexpected error");

            return result;
        }

        protected async Task<string> ReadResponseContent(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<Result<string>> HandleClientResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await ReadResponseContent(response);

                return ResultExtensions.Success(content);
            }

            return HandleBadRequest<string>(response);
        }

        protected async Task<Result<T>> HandleClientResponse<T>(HttpResponseMessage response) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                return await HandleGoodRequest<T>(response);
            }

            return HandleBadRequest<T>(response);
        }

        public void AddQueryParam(string key, string value)
        {
            QueryParams.Add(key, value);
        }

        public string BuildQuery()
        {
            return QueryHelpers.AddQueryString(string.Empty, QueryParams);
        }
    }
}
