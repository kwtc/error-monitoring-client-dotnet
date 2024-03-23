using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client.Validation;

/// <summary>
/// Validator for configuration required by the client.
/// </summary>
public class ClientConfigurationValidator : AbstractValidator<IConfiguration>
{
    public ClientConfigurationValidator()
    {
        // Required configuration keys
        this.RuleFor(x => x[ConfigurationKeys.ApiKey])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.ApiKey + " is required.")
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage(ConfigurationKeys.ApiKey + " input is invalid.");

        this.RuleFor(x => x[ConfigurationKeys.ApplicationKey])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.ApplicationKey + " is required.")
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage(ConfigurationKeys.ApplicationKey + " input is invalid.");
        ;

        this.RuleFor(x => x[ConfigurationKeys.EndpointUri])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.EndpointUri + " is required.")
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute))
            .WithMessage(ConfigurationKeys.EndpointUri + " input is invalid.");
        ;

        // Optional configuration keys
        this.RuleFor(x => x[ConfigurationKeys.HttpClientName])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.HttpClientName + " input is invalid.")
            .When(x => !string.IsNullOrEmpty(x[ConfigurationKeys.HttpClientName]));
    }
}
