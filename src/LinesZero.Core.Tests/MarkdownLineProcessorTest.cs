namespace LinesZero.Test;

public sealed class MarkdownLineProcessorTest
{
    [Theory]
    [InlineData(LineEndings.LF, false, "Hello World", false, 11)]
    [InlineData(LineEndings.LF, true, "Hello World", false, 11)]
    [InlineData(LineEndings.CRLF, false, "Hello World", false, 11)]
    [InlineData(LineEndings.CRLF, true, "Hello World", false, 11)]
    [InlineData(LineEndings.LF, false, "Hello World\n", false, 12)]
    [InlineData(LineEndings.LF, true, "Hello World\n\n", false, 13)]
    [InlineData(LineEndings.CRLF, false, "Hello World\r\n", false, 13)]
    [InlineData(LineEndings.CRLF, true, "Hello World\r\n\r\n", false, 15)]
    [InlineData(LineEndings.LF, false, "Hello World\nHello World", true, 12)]
    [InlineData(LineEndings.LF, true, "Hello World\n\nHello World", true, 13)]
    [InlineData(LineEndings.CRLF, false, "Hello World\r\nHello World", true, 13)]
    [InlineData(LineEndings.CRLF, true, "Hello World\r\n\r\nHello World", true, 15)]
    public void TestProcessLines(string eol, bool dual, string input, bool expectedHasMore, int expectedIndex)
    {
        // Arrange
        var lineProcessor = new MarkdownLineProcessor(width: 0, eol, dual);

        // Act
        var actualIndex = lineProcessor.ProcessLine(input, 0, out var actualHasMore);

        // Assert
        actualHasMore.Should().Be(expectedHasMore);
        actualIndex.Should().Be(expectedIndex);
    }
}
