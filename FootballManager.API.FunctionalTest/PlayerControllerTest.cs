using FootballManager.API.Requests;
using FootballManager.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballManager.API.FunctionalTest
{

    [TestFixture]
    public class PlayerControllerTest
    {
        private ApiTestFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _factory = new ApiTestFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetExistingPlayerReturnsOk()
        {
            var result = await _client.GetAsync("/api/players/3");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetNonExistingPlayerReturnsNotFound()
        {
            var result = await _client.GetAsync("/api/players/300");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PutInvalidPlayerStatusReturnsBadRequest()
        {
            var request = new PlayerStatusRequest()
            {
                StatusId = 55
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var result = await _client.PutAsync("/api/players/1/status", jsonContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PutValidPlayerStatusReturnsOk()
        {
            var request = new PlayerStatusRequest()
            {
                StatusId = PlayerStatus.Injured.Id
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var result = await _client.PutAsync("/api/players/1/status", jsonContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
