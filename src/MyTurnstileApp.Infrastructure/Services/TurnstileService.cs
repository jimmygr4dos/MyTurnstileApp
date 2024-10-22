using Microsoft.Extensions.Configuration;
using MyTurnstileApp.Application.Interfaces;
using Newtonsoft.Json;

namespace MyTurnstileApp.Infrastructure.Services
{
    public class TurnstileService : ITurnstileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;

        public TurnstileService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _secretKey = configuration["Turnstile:SecretKey"];
        }

        public async Task<bool> ValidateAsync(string token, string remoteIp)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token),
                new KeyValuePair<string, string>("remoteip", remoteIp)
            });

            try
            {
                var response = await _httpClient.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", requestContent);
                if (!response.IsSuccessStatusCode)
                    return false;

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TurnstileResponse>(jsonString);
                return result.Success;
            }
            catch (Exception)
            {
                // Log the exception (opcional)
                return false;
            }
        }
    }

    public class TurnstileResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
