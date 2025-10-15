namespace TransactionTestApp.Common.Responses;

/// <summary>
/// RFC 9457 Problem Details for HTTP APIs
/// </summary>
public sealed class ProblemDetails
{
    public string? ExceptionType { get; set; }

    /// <summary>
    /// В RFC 9457 используется термин type
    /// </summary>
    public string? Path { get; set; }

    public string? Url { get; set; }

    public required string Title { get; set; }

    public required string Detail {  get; set; }

    public required string Instance { get; set; }

    public int RequestStatus { get; set; }

    /// <summary>
    /// Ошибка предыдущего сервиса
    /// </summary>
    public ProblemDetails? InternalDetails { get; set; }
}
