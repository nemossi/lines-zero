// <copyright file="MarkdownLineProcessor.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace LinesZero;

/// <summary>
/// The line processor to extract next line from markdown input.
/// </summary>
public class MarkdownLineProcessor
(
    int width = 0,
    string eol = MarkdownLineProcessor.DefaultEol,
    bool dual = false
) : ILineProcessor
{
    /// <summary>
    /// The default line ending symbol to use.
    /// </summary>
    public const string DefaultEol = LineEndings.CRLF;

    /// <summary>
    /// Gets the width of line to process.
    /// </summary>
    public int Width => width;

    /// <summary>
    /// Gets a value indicating whether markdown paragraph mode is enabled.
    /// </summary>
    public bool DualLineEndings => dual;

    /// <summary>
    /// Gets the line ending symbol to use.
    /// </summary>
    public string Eol => dual ? $"{eol}{eol}" : eol;

    /// <inheritdoc />
    public int ProcessLine(string input, int start, out bool hasMore)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(start, input.Length, nameof(start));
        ArgumentOutOfRangeException.ThrowIfLessThan(start, 0, nameof(start));

        var count = this.Width > 0 ? Math.Min(this.Width, input.Length - start) : input.Length - start;
        var index = input.IndexOf(this.Eol, start, count, StringComparison.InvariantCultureIgnoreCase);
        var next = index >= 0 ? index + this.Eol.Length : start + count;
        hasMore = next < input.Length;
        return next;
    }
}
