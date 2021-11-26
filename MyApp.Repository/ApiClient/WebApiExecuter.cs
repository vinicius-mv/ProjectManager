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
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public WebApiExecuter(string baseUrl, HttpClient httpClient, string apiKey)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SetApiVersion(double apiVersion)
        {
            _httpClient.DefaultRequestHeaders.Add("x-api-version", apiVersion.ToString("F1"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var response = await _httpClient.PostAsJsonAsync<T>(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await _httpClient.PutAsJsonAsync<T>(GetUrl(uri), obj);
            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete(string uri)
        {
            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            response.EnsureSuccessStatusCode();
        }

        private string GetUrl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}
