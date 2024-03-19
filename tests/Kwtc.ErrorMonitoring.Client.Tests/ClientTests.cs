using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client.Tests;

public class ClientTests
{
    [Fact]
    public void Client_ValidConfiguration_ShouldNotThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


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
                                { ConfigurationKeys.EndpointUri, "http://localhost" }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
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
                                { ConfigurationKeys.EndpointUri, "http://localhost" }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
    }

    [Fact]
    public void Client_MissingApplicationKey_ShouldThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
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
                                { ConfigurationKeys.EndpointUri, "http://localhost" }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
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
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
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
                                { ConfigurationKeys.EndpointUri, endpointUri }
                            }!)
                            .Build();

        // Act
        var act = () => new Client(configuration);


        // Assert
        act.Should().Throw<ErrorMonitoringException>();
    }
}
