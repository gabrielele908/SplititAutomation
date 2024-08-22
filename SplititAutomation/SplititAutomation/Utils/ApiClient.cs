/*using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SplititAutomation.Utils
{
    public class ApiClient
    {
        private HttpClient _client;

        public ApiClient(string baseUrl, string accessToken)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<string> SendTransactionRequest(Dictionary<string, string> parameters)
        {
            var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            var requestUri = $"main/new-transaction?{queryString}";
            var response = await _client.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}*/
