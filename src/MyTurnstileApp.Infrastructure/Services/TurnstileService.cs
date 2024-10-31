using Microsoft.Extensions.Configuration;
using MyTurnstileApp.Application.Interfaces;
using Newtonsoft.Json;

namespace MyTurnstileApp.Infrastructure.Services
{
    public class TurnstileService : ITurnstileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;
        private readonly ILoggerService _logger;

        public TurnstileService(HttpClient httpClient, 
                                IConfiguration configuration,
                                ILoggerService logger)
        {
            _httpClient = httpClient;
            //_secretKey = configuration["Turnstile:SecretKey"];
            _secretKey = configuration["Google:SecretKey"];
            _logger = logger;
        }

        public async Task<bool> ValidateAsync(string token, string remoteIp)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"secret", _secretKey },
                { "response", token },
                //{"remoteip", remoteIp}
            });

            try
            {


                //var response = await _httpClient.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", requestContent);
                var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", requestContent);
                if (!response.IsSuccessStatusCode)
                {
                    await _logger.Error($"Token: {token}\n"
                                      + $"Token validation result: false");
                    return false;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<TurnstileResponse>(jsonString);
                var result = JsonConvert.DeserializeObject<GoogleResponse>(jsonString);
                await _logger.Information($"Token: { token }\n" 
                                        + $"Token validation result: {result.Success}");
                return result.Success;
            }
            catch (Exception)
            {
                // Log the exception (opcional)
                await _logger.Error("Error al validar el token de Turnstile.");
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

    public class GoogleResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
