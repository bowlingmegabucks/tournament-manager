using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Api.Divisions;

/// <summary>
/// Data Transfer Object for retrieving detailed information about a division in a tournament.
/// </summary>
public sealed record DivisionDetailDto
{
    /// <summary>
    /// The unique identifier for the division.
    /// </summary>
    public required DivisionId Id { get; init; }

    /// <summary>
    /// The name of the division.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The minimum age requirement for the division (if applicable).
    /// </summary>
    public short? MinimumAge { get; init; }

    /// <summary>
    /// The maximum age requirement for the division (if applicable).
    /// </summary>
    public short? MaximumAge { get; init; }

    /// <summary>
    /// The minimum average required for the division (if applicable).
    /// </summary>
    public int? MinimumAverage { get; init; }

    /// <summary>
    /// The maximum average required for the division (if applicable).
    /// </summary>
    public int? MaximumAverage { get; init; }

    /// <summary>
    /// The gender requirement for the division (if applicable).
    /// </summary>
    [JsonConverter(typeof(SmartEnumNameConverter<Gender, int>))]
    public Gender? Gender { get; init; }
}