// <copyright file="LineEndings.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace LinesZero;

/// <summary>
/// Provides constant definitions for common line ending symbols.
/// </summary>
public static class LineEndings
{
    /// <summary>
    /// Windows-style line ending: Carriage Return + Line Feed (CRLF).
    /// </summary>
    public const string CRLF = "\r\n";

    /// <summary>
    /// Classic Mac-style line ending: Carriage Return (CR).
    /// </summary>
    public const string CR = "\r";

    /// <summary>
    /// Unix/Linux-style line ending: Line Feed (LF).
    /// </summary>
    public const string LF = "\n";
}
