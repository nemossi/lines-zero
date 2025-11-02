namespace LinesZero.Core.Tests; 

public sealed class MarkdownLineProcessorTest
{
    [Theory]
    [InlineData(0, LineEndings.LF, false)]
    [InlineData(0, LineEndings.LF, true)]
    [InlineData(0, LineEndings.CRLF, false)]
    [InlineData(0, LineEndings.CRLF, true)]
    [InlineData(80, LineEndings.LF, false)]
    [InlineData(80, LineEndings.LF, true)]
    [InlineData(80, LineEndings.CRLF, false)]
    [InlineData(80, LineEndings.CRLF, true)]
    public void TestCreateInstance(int width, string eol, bool dual)
    {
        // Arrange & Act
        var lineProcessor = new MarkdownLineProcessor(width, eol, dual);

        // Assert
        lineProcessor.Width.Should().Be(width);
        lineProcessor.Eol.Should().Be(dual ? $"{eol}{eol}" : eol);
        lineProcessor.DualLineEndings.Should().Be(dual);
    }


    [Theory]
    [InlineData(0, LineEndings.LF, false, "Hello World", false, 11)]
    [InlineData(0, LineEndings.LF, true, "Hello World", false, 11)]
    [InlineData(0, LineEndings.CRLF, false, "Hello World", false, 11)]
    [InlineData(0, LineEndings.CRLF, true, "Hello World", false, 11)]
    [InlineData(0, LineEndings.LF, false, "Hello World\n", false, 12)]
    [InlineData(0, LineEndings.LF, true, "Hello World\n\n", false, 13)]
    [InlineData(0, LineEndings.CRLF, false, "Hello World\r\n", false, 13)]
    [InlineData(0, LineEndings.CRLF, true, "Hello World\r\n\r\n", false, 15)]
    [InlineData(0, LineEndings.LF, false, "Hello World\nHello World", true, 12)]
    [InlineData(0, LineEndings.LF, true, "Hello World\n\nHello World", true, 13)]
    [InlineData(0, LineEndings.CRLF, false, "Hello World\r\nHello World", true, 13)]
    [InlineData(0, LineEndings.CRLF, true, "Hello World\r\n\r\nHello World", true, 15)]
    [InlineData(80, LineEndings.LF, false, "Hello World", false, 11)]
    [InlineData(11, LineEndings.LF, false, "Hello World", false, 11)]
    [InlineData(10, LineEndings.LF, false, "Hello World", true, 10)]
    [InlineData(5, LineEndings.LF, false, "Hello World", true, 5)]
    public void TestProcessLines(int width, string eol, bool dual, string input, bool expectedHasMore, int expectedIndex)
    {
        // Arrange
        var lineProcessor = new MarkdownLineProcessor(width, eol, dual);

        // Act
        var actualIndex = lineProcessor.ProcessLine(input, 0, out var actualHasMore);

        // Assert
        actualHasMore.Should().Be(expectedHasMore);
        actualIndex.Should().Be(expectedIndex);
    }
}
