using System;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StatlerWaldorfCorp.LocationReporter.Models;


namespace StatlerWaldorfCorp.LocationReporter.Services
{
    public class TeamServiceHttpClient: ITeamServiceClient
	{
        private readonly ILogger logger;
        private HttpClient httpClient;

		public TeamServiceHttpClient(
            IOptions<TeamServiceOptions> teamServiceOptions,
            ILogger<TeamServiceHttpClient> logger)
		{
            this.logger = logger;
            var url = teamServiceOptions.Value.Url;
            logger.LogInformation($"Team Service HTTP client using URL {url}");

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
		}

        public Guid GetTeamForMember(Guid memberId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format($"/members/{memberId}/team")).Result;

            TeamIdResponse teamIdResponse;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                //teamIdResponse = JsonSerializer.Deserialize<TeamIdResponse>(json);
                teamIdResponse = JsonConvert.DeserializeObject<TeamIdResponse>(json);
                return teamIdResponse.TeamId;
            }

            return Guid.Empty;
        }
    }
}

