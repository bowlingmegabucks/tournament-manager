using System;
using System.Diagnostics.CodeAnalysis;

namespace BowlingMegabucks.TournamentManager.Api;

/// <summary>
/// Marker interface for the API assembly.
/// </summary>
[SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Required to be public for integration testing with WebApplicationFactory.")]
[SuppressMessage(
    "Design",
    "CA1040:Avoid empty interfaces",
    Justification = "Required to be public for integration testing with WebApplicationFactory.")]
public interface IApiAssemblyMarker;
