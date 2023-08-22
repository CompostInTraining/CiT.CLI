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
public class MeasuresApiTests
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
    public void MeasuresApi_GetInstanceAccounts_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceAccounts("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    public void MeasuresApi_GetInstanceFollowers_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceFollowers("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    public void MeasuresApi_GetInstanceFollows_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceFollows("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    public void MeasuresApi_GetInstanceMediaAttachments_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceMediaAttachments("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    public void MeasuresApi_GetInstanceReports_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceReports("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    public void MeasuresApi_GetInstanceStatuses_Test()
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
        var api = new MeasuresApi(_configManager, httpClient);

        // Act
        var result = api.GetInstanceStatuses("social.example.com").Result;

        // Assert
        Assert.IsNotNull(result);
    }
}
