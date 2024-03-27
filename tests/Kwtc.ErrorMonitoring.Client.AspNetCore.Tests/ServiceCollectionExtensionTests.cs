using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;


namespace Kwtc.ErrorMonitoring.Client.AspNetCore.Tests;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void AddErrorMonitoring_ValidConfiguration_ShouldNotThrow()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidClientName" },
                                { ConfigurationKeys.ChannelCapasity, "100" }
                            }!)
                            .Build();

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void AddErrorMonitoring_MissingApiKey_ShouldThrow()
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


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
    public void AddErrorMonitoring_InvalidApiKeyConfiguration_ShouldThrow(string apiKey)
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApiKey));
    }
    
    [Theory]
    [InlineData("6d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3")]
    public void AddErrorMonitoring_ValidApiKeyConfiguration_ShouldNotThrow(string apiKey)
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddErrorMonitoring_MissingApplicationKey_ShouldThrow()
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


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
    public void AddErrorMonitoring_InvalidApplicationKeyConfiguration_ShouldThrow(string applicationKey)
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ApplicationKey));
    }
    
    [Theory]
    [InlineData("6d4780de2a6e4b4395954afd592407e3")]
    [InlineData("6d4780de-2a6e-4b43-9595-4afd592407e3")]
    public void AddErrorMonitoring_ValidApplicationKeyConfiguration_ShouldNotThrow(string applicationKey)
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddErrorMonitoring_ConfigurationMissingEndpoint_ShouldThrow()
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


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
    public void AddErrorMonitoring_ValidEndpointUriConfiguration_ShouldNotThrow(string endpointUri)
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    [Fact]
    public void AddErrorMonitoring_ConfigurationHttpClientNameProvidedButInvalid_ShouldThrow()
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

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.HttpClientName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("InvalidChannelCapasity")]
    public void AddErrorMonitoring_ConfigurationChannelCapasityProvidedButInvalid_ShouldThrow(string channelCapasity)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidHttpClientName" },
                                { ConfigurationKeys.ChannelCapasity, channelCapasity }
                            }!).Build();

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .Throw<ValidationException>()
           .Where(e => e.Message.Contains(ConfigurationKeys.ChannelCapasity));
    }
    
    [Theory]
    [InlineData("1")]
    [InlineData("1000")]
    public void AddErrorMonitoring_ConfigurationChannelCapasityProvidedAndValid_ShouldNotThrow(string channelCapasity)
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>
                            {
                                { ConfigurationKeys.ApiKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.ApplicationKey, Guid.NewGuid().ToString() },
                                { ConfigurationKeys.EndpointUri, "http://localhost" },
                                { ConfigurationKeys.HttpClientName, "ValidHttpClientName" },
                                { ConfigurationKeys.ChannelCapasity, channelCapasity }
                            }!).Build();

        var sut = GetSut();

        // Act
        var act = () => sut.AddErrorMonitoring(configuration);


        // Assert
        act.Should()
           .NotThrow();
    }

    private static ServiceCollection GetSut() => [];
}
