
using Moq;
using Moq.Protected;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Tests.Helpers;
using System.Collections;
using System.Net;

namespace Nexus.SDK.Shared.Tests
{
    public class RequestBuilderTests
    {
        private Mock<HttpMessageHandler> _mockHandler;

        [SetUp]
        public void Setup()
        {
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            // Already setup default handler
            _mockHandler.Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{}"),
               })
               .Verifiable();
        }

        [Test]
        public void RequestBuilderTests_Segments_Reset_After_AuthException_Is_Thrown()
        {
            var authProvider = new MockAuthProviderWithException();
            var requestBuilder = new MockRequestBuilder(authProvider, _mockHandler.Object);

            // Verify that the first GET throws an authentication exceptions
            Assert.ThrowsAsync<AuthProviderException>(() => requestBuilder.Get<string>(new string[] { "birds" }));

            // Verify that the second GET also throws an authentication exception and not new Exception("URL segments have already been added.");
            Assert.ThrowsAsync<AuthProviderException>(() => requestBuilder.Get<string>(new string[] { "birds" }));
        }

        [Test]
        [TestCaseSource(typeof(QueryCases))]
        public async Task RequestBuilderTests_Build_QueryAsync(IDictionary<string, string> query, string expectedUrl)
        {
            var authProvider = new MockAuthProviderWithSuccess();
            var requestBuilder = new MockRequestBuilder(authProvider, _mockHandler.Object);

            await requestBuilder.Get<string>(new string[] { "birds" }, query);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri(expectedUrl);

            _mockHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        class QueryCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[]
                {
                    new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" }},
                    "https://mockrequestbuilder.com/birds?key1=value1&key2=value2"
                };

                yield return new object[]
                {
                    new Dictionary<string, string>(),
                    "https://mockrequestbuilder.com/birds"
                };

                yield return new object?[]
                {
                    null,
                    "https://mockrequestbuilder.com/birds"
                };
            }
        }
    }
}