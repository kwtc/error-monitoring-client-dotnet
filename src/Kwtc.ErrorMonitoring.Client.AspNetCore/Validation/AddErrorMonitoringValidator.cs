using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore.Validation;

/// <summary>
/// Validator for configuration required by the Error Monitoring client and event service.
/// </summary>
public class AddErrorMonitoringValidator : AbstractValidator<IConfiguration>
{
    public AddErrorMonitoringValidator()
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

        this.RuleFor(x => x[ConfigurationKeys.EndpointUri])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.EndpointUri + " is required.")
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute))
            .WithMessage(ConfigurationKeys.EndpointUri + " input is invalid.");

        // Optional configuration keys
        this.RuleFor(x => x[ConfigurationKeys.HttpClientName])
            .NotEmpty()
            .WithMessage(ConfigurationKeys.HttpClientName + " is required.")
            .When(x => x[ConfigurationKeys.HttpClientName] != null);
        
        this.RuleFor(x => x[ConfigurationKeys.ChannelCapasity])
            .Must(x => int.TryParse(x, out _) && int.Parse(x) > 0)
            .WithMessage(ConfigurationKeys.ChannelCapasity + " input is invalid.")
            .When(x => x[ConfigurationKeys.ChannelCapasity] != null);
    }
}
