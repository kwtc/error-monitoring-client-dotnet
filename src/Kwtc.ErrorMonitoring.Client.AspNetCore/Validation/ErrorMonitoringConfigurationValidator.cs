using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Kwtc.ErrorMonitoring.Client.AspNetCore.Validation;

/// <summary>
/// Validator for configuration required by the Error Monitoring client and event service.
/// </summary>
public class ErrorMonitoringConfigurationValidator : AbstractValidator<IConfiguration>
{
    public ErrorMonitoringConfigurationValidator()
    {
        // Required configuration keys
        this.RuleFor(x => x[ConfigurationKeys.ApiKey])
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _));

        this.RuleFor(x => x[ConfigurationKeys.ApplicationKey])
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _));

        this.RuleFor(x => x[ConfigurationKeys.EndpointUri])
            .NotEmpty()
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute));

        // Optional configuration keys
        this.RuleFor(x => x[ConfigurationKeys.HttpClientName])
            .Must(x => Guid.TryParse(x, out _))
            .When(x => !string.IsNullOrEmpty(x[ConfigurationKeys.HttpClientName]));

        this.RuleFor(x => x[ConfigurationKeys.ChannelCapasity])
            .Must(x => int.TryParse(x, out _) && int.Parse(x) > 0)
            .When(x => !string.IsNullOrEmpty(x[ConfigurationKeys.ChannelCapasity]));
    }
}
