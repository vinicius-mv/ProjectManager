using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;
        private readonly ITokenRepository tokenRepository;

        public WebApiExecuter(string baseUrl, HttpClient httpClient, ITokenRepository tokenRepository)
        {
            this.baseUrl = baseUrl;
            this.httpClient = httpClient;
            this.tokenRepository = tokenRepository;
            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SetApiVersion(double apiVersion)
        {
            httpClient.DefaultRequestHeaders.Add("x-api-version", apiVersion.ToString("F1"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            AddTokenHeader();
            return await httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            AddTokenHeader();
            var response = await httpClient.PostAsJsonAsync<T>(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<string> InvokePostReturnStringAsync<T>(string uri, T obj)
        {
            AddTokenHeader();
            var response = await httpClient.PostAsJsonAsync<T>(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            AddTokenHeader();
            var response = await httpClient.PutAsJsonAsync<T>(GetUrl(uri), obj);
            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete(string uri)
        {
            AddTokenHeader();
            var response = await httpClient.DeleteAsync(GetUrl(uri));
            response.EnsureSuccessStatusCode();
        }

        private string GetUrl(string uri)
        {
            return $"{baseUrl}/{uri}";
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }

        private void AddTokenHeader()
        {
            if (tokenRepository == null || string.IsNullOrEmpty(tokenRepository.Token))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Remove("TokenHeader");
            httpClient.DefaultRequestHeaders.Add("TokenHeader", tokenRepository.Token);
        }
    }
}
