using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ghost.API.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Ghost.API.IntegrationTest
{
    public class ApiIntegrationTest : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiIntegrationTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Development"));
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Fact]
        public async Task PlayShouldReturnAPlayResponse()
        {
            var play = new PlayDto() { Text = "capyb" };
            var postData = new StringContent(JsonConvert.SerializeObject(play), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/play", postData);

            var responseString = await response.Content.ReadAsStringAsync();
            var playResponse = JsonConvert.DeserializeObject<PlayResultDto>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("capyba", playResponse.Text);
        }
    }
}
