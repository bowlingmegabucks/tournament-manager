﻿
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
/// <param name="message"></param>
/// <param name="returnCode"></param>
public class ErrorDetail(string message, int returnCode)
{
    /// <summary>
    /// 
    /// </summary>
    public string Message { get; internal set; } = message;

    /// <summary>
    /// 
    /// </summary>
    public int ReturnCode { get; internal set; } = returnCode;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    public ErrorDetail(Exception ex)
        : this(ex?.Message ?? throw new ArgumentNullException(nameof(ex)))
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ErrorDetail(string message)
        : this(message, -1)
    {

    }
}

/// <summary>
/// 
/// </summary>
public static class ErrorDetailExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static IEnumerable<ErrorDetail> ToErrorDetails(this IEnumerable<ErrorOr.Error> errors)
        => errors.Select(e => e.ToErrorDetail());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ErrorDetail ToErrorDetail(this ErrorOr.Error error)
        => new(error.Description, -1);
}