using CoolGateway.Api.IntegrationTests.Common;
using CoolGateway.Application.Payments.Queries;
using CoolGateway.WebApi.Dtos;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace CoolGateway.Api.IntegrationTests.Payments
{
    public class PaymentTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PaymentTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldCreatePaymentAndReturnNewPaymentId()
        {
            var client = _factory.CreateClient();

            var paymentCommand = new ProcessPaymentDto
            {
                MerchantId = Guid.NewGuid(),
                CardNumber = "1234567890123452",
                CardHolder = "test tester",
                CardExpiryMonth = 10,
                CardExpiryYear = 25,
                CardCvv = 123,
                Amount = 100,
                Currency = "COOL"
            };

            var content = new StringContent(JsonConvert.SerializeObject(paymentCommand), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Payments", content);

            var resultStr = await response.Content.ReadAsStringAsync();
            var resultDto = JsonConvert.DeserializeObject<ProcessPaymentResultDto>(resultStr);

            response.EnsureSuccessStatusCode();
            resultDto.Should().NotBeNull();
            resultDto.PaymentId.Should().NotBe(Guid.Empty);
            resultDto.Should().BeOfType<ProcessPaymentResultDto>();
        }

        [Fact]
        public async Task ShouldNotCreatePayment()
        {
            var client = _factory.CreateClient();

            var paymentCommand = new ProcessPaymentDto
            {
                MerchantId = Guid.NewGuid(),
                CardNumber = "4444555566667777",
                CardHolder = "test tester",
                CardExpiryMonth = 10,
                CardExpiryYear = 25,
                CardCvv = 123,
                Amount = 100,
                Currency = "NotCOOL"
            };

            var content = new StringContent(JsonConvert.SerializeObject(paymentCommand), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Payments", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldGetSavedPaymentById()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Payments/{Utilities.SeedId}");

            var resultStr = await response.Content.ReadAsStringAsync();
            var resultDto = JsonConvert.DeserializeObject<PaymentQueryDto>(resultStr);

            response.EnsureSuccessStatusCode();
            resultDto.Should().NotBeNull();
            resultDto.Id.Should().Be(Utilities.SeedId);
            resultDto.Should().BeOfType<PaymentQueryDto>();
        }

        [Fact]
        public async Task ShouldNotGetNonSavedPaymentById()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Payments/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
