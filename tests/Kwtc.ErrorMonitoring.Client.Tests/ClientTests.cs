using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Severity = Kwtc.ErrorMonitoring.Client.Payload.Severity;

namespace Kwtc.ErrorMonitoring.Client.Tests;

public class ClientTests
{
    private readonly Mock<IHttpClientFactory> httpClientFactoryMock = new();

    [Fact]
    public void Client_ValidConfiguration_ShouldNotThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Client_MissingApiKey_ShouldThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApiKey));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("InvalidApiKey")]
    [InlineData("76d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3-1234")]
    public void Client_InvalidApiKeyConfiguration_ShouldThrow(string apiKey)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, apiKey },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApiKey));
    }

    [Theory]
    [InlineData("6d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3")]
    public void Client_ValidApiKeyConfiguration_ShouldNotThrow(string apiKey)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, apiKey },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void Client_MissingApplicationKey_ShouldThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApplicationKey));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("InvalidApplicationKey")]
    [InlineData("76d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3-1234")]
    public void Client_InvalidApplicationKeyConfiguration_ShouldThrow(string applicationKey)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, applicationKey },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApplicationKey));
    }

    [Theory]
    [InlineData("6d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3")]
    public void Client_ValidApplicationKeyConfiguration_ShouldNotThrow(string applicationKey)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, applicationKey },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void Client_ConfigurationMissingEndpoint_ShouldThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.EndpointUri));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("InvalidEndpointUri")]
    [InlineData("http:/localhost")]
    [InlineData("/localhost")]
    public void Client_InvalidEndpointUriConfiguration_ShouldThrow(string endpointUri)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, endpointUri },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.EndpointUri));
    }

    [Theory]
    [InlineData("http://localhost")]
    [InlineData("http://localhost:3000")]
    [InlineData("http://localhost.com")]
    [InlineData("http://www.localhost.com")]
    [InlineData("http://www.localhost.com:3000")]
    [InlineData("localhost:3000")]
    [InlineData("www.localhost.com:3000")]
    public void Client_ValidEndpointUriConfiguration_ShouldNotThrow(string endpointUri)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, endpointUri },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void Client_ConfigurationHttpClientNameProvidedButInvalid_ShouldThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, string.Empty }
                            }!)
                            .Build();

        // Act
        var act = () => this.GetSut(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.HttpClientName));
    }

    [Fact]
    public async Task NotifyAsync_ExceptionIsNull_ShouldThrow()
    {
        // Arrange
        var configuration = GetValidConfiguration();

        // Act
        var act = () => this.GetSut(configuration).NotifyAsync(null, Severity.Error);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task NotifyAsync_SeverityIsInvalid_ShouldThrow()
    {
        // Arrange
        var configuration = GetValidConfiguration();

        // Act
        var act = () => this.GetSut(configuration).NotifyAsync(new Exception(), 0);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task NotifyAsync_SeverityIsInvalid_ShouldCreateClientAndPost()
    {
        // Arrange
        var configuration = GetValidConfiguration();
        this.httpClientFactoryMock.Setup(x => x.CreateClient(configuration[ConfigurationKeys.HttpClientName]!))
            .Returns(new HttpClient());

        // Act
        var response= await this.GetSut(configuration).NotifyAsync(new Exception(), Severity.Error);

        // Assert
        this.httpClientFactoryMock.Verify(x => x.CreateClient(configuration[ConfigurationKeys.HttpClientName]!), Times.Once);
        
        response.Should().NotBeNull();
    }

    private Client GetSut(IConfiguration configuration)
    {
        return new Client(configuration, this.httpClientFactoryMock.Object);
    }

    private static IConfiguration GetValidConfiguration()
    {
        return new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                   { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                   { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                   { ConfigurationKeys.EndpointUri, "http://localhost" },
                   { ConfigurationKeys.HttpClientName, "ValidClientName" }
               }!)
               .Build();
    }
}
