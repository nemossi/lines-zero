// <copyright file="ContentView.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace LinesZero;

/// <summary>
/// The content view in lines.
/// </summary>
public ref struct ContentView
{
    private readonly List<int> lines = new () { 0 };

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentView"/> struct.
    /// </summary>
    /// <param name="content">The content to view.</param>
    /// <param name="lineProcessor">The line processor to use.</param>
    public ContentView(string content, ILineProcessor lineProcessor)
    {
        this.LineProcessor = lineProcessor;
        this.Content = content;
        this.ProcessLines();
    }

    /// <summary>
    /// Gets the line processor used to process lines.
    /// </summary>
    public ILineProcessor LineProcessor { get; private set; }

    /// <summary>
    /// Gets the content to view.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Gets the number of lines in the content view.
    /// </summary>
    public readonly int Count => this.lines.Count;

    /// <summary>
    /// Gets the line at the specified index.
    /// </summary>
    /// <param name="ln">The line index.</param>
    /// <returns>The line at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the line index is out of range.</exception>
    public readonly ReadOnlySpan<char> this[int ln]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(ln, 0, nameof(ln));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(ln, this.Count, nameof(ln));

            var start = this.lines[ln];
            var end = ln + 1 < this.lines.Count ? this.lines[ln + 1] : this.Content.Length;
            var count = end - start;
            return this.Content.AsSpan(start, count);
        }
    }

    /// <summary>
    /// Finds the line number that contains the specified position.
    /// </summary>
    /// <param name="pos">The position to find.</param>
    /// <returns>The line number that contains the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the position is out of range.</exception>
    public readonly int FindLineNumber(int pos)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pos, 0, nameof(pos));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(pos, this.Content.Length, nameof(pos));
        var index = this.lines.BinarySearch(pos);
        return index >= 0 ? index : ~index - 1;
    }

    private readonly void ProcessLines()
    {
        var index = 0;
        var hasMore = true;
        while (hasMore)
        {
            index = this.LineProcessor.ProcessLine(this.Content, index, out hasMore);
            if (hasMore)
            {
                this.lines.Add(index);
            }
        }
    }
}
