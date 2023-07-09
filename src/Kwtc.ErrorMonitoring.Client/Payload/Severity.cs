using System.ComponentModel;

namespace Kwtc.ErrorMonitoring.Client.Payload;

public enum Severity
{
    [Description("info")]
    Info = 1,
    
    [Description("warning")]
    Warning = 2,
    
    [Description("error")]
    Error = 3
}
