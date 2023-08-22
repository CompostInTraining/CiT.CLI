using System.Net;
using System.Net.Mime;
using System.Text;
using CiT.Core.Configuration;
using CiT.Core.Mastodon;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CiT.Core.Tests.Mastodon;

[TestClass]
public class EmailDomainBlocksApiTests
{
    [TestInitialize]
    public void Init()
    {
        var config = new Dictionary<string, string?>
        {
            {
                "Instance:Url", "https://mastodon.example.com"
            },
            {
                "Instance:AccessToken", RandomString.New()
            }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();
        IConfigManager configManager = new ConfigManager(configuration);
        _configManager = configManager;
        _instanceUrl = _configManager.GetConfigValue("Instance:Url");
    }
    private IConfigManager _configManager;
    private string _instanceUrl;
    [TestMethod]
    public void EmailDomainBlocksApi_AddEmailDomainBlock_Test()
    {
        // Arrange
        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = HttpStatusCode.OK;

        Mock<HttpMessageHandler> mockHandler = new();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Post && r.RequestUri.ToString()
                        .StartsWith(_instanceUrl)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(mockHandler.Object);
        var api = new EmailDomainBlocksApi(_configManager, httpClient);

        // Act
        (int statusCode, string? responseString) = api.AddEmailDomainBlock("bad.example.com").Result;

        // Assert
        Assert.AreEqual((int)httpResponse.StatusCode, statusCode);
    }
    [TestMethod]
    public void EmailDomainBlocksApi_RemoveEmailBlockedDomain()
    {
        // Arrange
        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = HttpStatusCode.OK;

        Mock<HttpMessageHandler> mockHandler = new();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Delete && r.RequestUri.ToString()
                        .StartsWith(_instanceUrl)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(mockHandler.Object);
        var api = new EmailDomainBlocksApi(_configManager, httpClient);

        // Act
        (int statusCode, string? responseString) = api.RemoveEmailDomainBlock(new BlockedEmailDomain
        {
            Id = "123456"
        }).Result;

        // Assert
        Assert.AreEqual((int)httpResponse.StatusCode, statusCode);
    }
    [TestMethod]
    public void EmailDomainBlocksApi_GetInstanceBlockedEmailDomains()
    {
        // Arrange
        var expectedBlockList = new List<BlockedEmailDomain>
        {
            new()
            {
                Id = "377",
                Domain = "1.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:51.902Z")
            },
            new()
            {
                Id = "376",
                Domain = "2.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:49.879Z")
            }
        };
        string json = JsonConvert.SerializeObject(expectedBlockList);

        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = HttpStatusCode.OK;
        httpResponse.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        Mock<HttpMessageHandler> mockHandler = new();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get && r.RequestUri.ToString()
                        .StartsWith(_instanceUrl)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(mockHandler.Object);
        var api = new EmailDomainBlocksApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceBlockedEmailDomains().Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Any(r => r.Domain == "1.example.com"));
        Assert.IsTrue(result.Any(r => r.Domain == "2.example.com"));
    }
    [TestMethod]
    public void EmailDomainBlocksApi_GetInstanceBlockedEmailDomains_With_More()
    {
        // Arrange
        var expectedBlockList = new List<BlockedEmailDomain>
        {
            new()
            {
                Id = "377",
                Domain = "1.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:51.902Z")
            },
            new()
            {
                Id = "376",
                Domain = "2.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:49.879Z")
            },
            new()
            {
                Id = "1312",
                Domain = "3.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:49.879Z")
            },
            new()
            {
                Id = "420",
                Domain = "4.example.com",
                CreatedAt = DateTime.Parse("2023-07-27T22:16:49.879Z")
            }
        };
        string json = JsonConvert.SerializeObject(expectedBlockList.Take(2));
        string json2 = JsonConvert.SerializeObject(expectedBlockList.Skip(2).Take(2));

        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = HttpStatusCode.OK;
        httpResponse.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        httpResponse.Headers.Add("Link", $@"<{_instanceUrl}/api/v1/admin/email_domain_blocks?limit=200&max_id=1312>; rel=""next"", <{_instanceUrl}/api/v1/admin/email_domain_blocks?limit=200&min_id=13>; rel=""prev""");

        var secondHttpResponse = new HttpResponseMessage();
        secondHttpResponse.StatusCode = HttpStatusCode.OK;
        secondHttpResponse.Content = new StringContent(json2, Encoding.UTF8, MediaTypeNames.Application.Json);
        secondHttpResponse.Headers.Remove("Link");

        Mock<HttpMessageHandler> mockHandler = new();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get && r.RequestUri.ToString()
                        .StartsWith(_instanceUrl) &&
                    r.RequestUri.ToString().EndsWith("limit=200")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get &&
                    r.RequestUri.ToString().StartsWith(_instanceUrl) &&
                    r.RequestUri.ToString().Contains("max_id")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(secondHttpResponse);

        var httpClient = new HttpClient(mockHandler.Object);
        var api = new EmailDomainBlocksApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceBlockedEmailDomains().Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedBlockList.Count, result.Count);
        Assert.IsTrue(result.Any(r => r.Domain == "1.example.com"));
        Assert.IsTrue(result.Any(r => r.Domain == "2.example.com"));
        Assert.IsTrue(result.Any(r => r.Domain == "3.example.com"));
        Assert.IsTrue(result.Any(r => r.Domain == "4.example.com"));
    }
}
