// <copyright file="ILineProcessor.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace LinesZero;

/// <summary>
/// The interface to extract next line from input.
/// </summary>
public interface ILineProcessor
{
    /// <summary>
    /// Process next line from input.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="start">The start index of line to process.</param>
    /// <param name="hasMore">True if there are more lines to process.</param>
    /// <returns>The character index of processed line.</returns>
    int ProcessLine(string input, int start, out bool hasMore);
}
