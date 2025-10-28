namespace LinesZero.Test;

public sealed class ContentViewTest
{
    [Theory]
    [InlineData("Hello World", new int[] { 11 })]
    [InlineData("Hello World\n", new int[] { 12 })]
    [InlineData("Hello World\nHello World", new int[] { 12, 11})]
    public void TestCreateContentView(string input, int[] expectedLines)
    {
        // Arrange
        var lineProcessor = new MarkdownLineProcessor(width: 0, LineEndings.LF, dual: false);

        // Act
        var contentView = new ContentView(input, lineProcessor);

        // Assert
        contentView.Count.Should().Be(expectedLines.Length);
        var start = 0;
        for(var i=0; i<contentView.Count; i++)
        {
            var length = expectedLines[i];
            var expectedLine = input.AsSpan(start, length).ToString();
            var actualLine = contentView[i].ToString();
            actualLine.Should().Be(expectedLine);
            start += expectedLines[i];
        }
    }

    [Theory]
    [InlineData("Hello World", 0, 0)]
    [InlineData("Hello World", 5, 0)]
    [InlineData("Hello World", 10, 0)]
    [InlineData("Hello World\n", 0, 0)]
    [InlineData("Hello World\n", 5, 0)]
    [InlineData("Hello World\n", 10, 0)]
    [InlineData("Hello World\n", 11, 0)]
    [InlineData("Hello World\nHello World", 0, 0)]
    [InlineData("Hello World\nHello World", 5, 0)]
    [InlineData("Hello World\nHello World", 10, 0)]
    [InlineData("Hello World\nHello World", 11, 0)]
    [InlineData("Hello World\nHello World", 12, 1)]
    [InlineData("Hello World\nHello World", 15, 1)]
    [InlineData("Hello World\nHello World", 22, 1)]
    public void TestFindLineNumber(string input, int pos, int expectedLn)
    {
        // Arrange
        var lineProcessor = new MarkdownLineProcessor(width: 0, LineEndings.LF, dual: false);
        var contentView = new ContentView(input, lineProcessor);

        // Act
        var actualLn = contentView.FindLineNumber(pos);

        // Assert
        actualLn.Should().Be(expectedLn);
    }
}
