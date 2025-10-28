// <copyright file="ContentViewBenchmarks.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace LinesZero.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class ContentViewBenchmarks
{
    private string _smallContent = string.Empty;
    private string _mediumContent = string.Empty;
    private string _largeContent = string.Empty;
    private string _veryLargeContent = string.Empty;
    private MarkdownLineProcessor _lineProcessor = new();

    [GlobalSetup]
    public void Setup()
    {
        // 生成不同大小的测试数据
        _smallContent = GenerateContent(100);      // 100行
        _mediumContent = GenerateContent(1000);    // 1000行
        _largeContent = GenerateContent(10000);    // 10000行
        _veryLargeContent = GenerateContent(50000); // 50000行
        
        _lineProcessor = new MarkdownLineProcessor();
    }

    [Benchmark]
    [ArgumentsSource(nameof(ContentSizes))]
    public ContentView CreateContentView(string content)
    {
        return new ContentView(content, _lineProcessor);
    }

    [Benchmark]
    [ArgumentsSource(nameof(ContentSizes))]
    public int AccessAllLines(string content)
    {
        var view = new ContentView(content, _lineProcessor);
        int totalLength = 0;
        
        for (int i = 0; i < view.Count; i++)
        {
            var line = view[i];
            totalLength += line.Length;
        }
        
        return totalLength;
    }

    [Benchmark]
    [ArgumentsSource(nameof(ContentSizes))]
    public int FindLineNumbers(string content)
    {
        var view = new ContentView(content, _lineProcessor);
        int foundCount = 0;
        
        // 随机查找一些位置的行号
        var random = new Random(42); // 固定种子保证可重复性
        for (int i = 0; i < 100; i++)
        {
            var pos = random.Next(0, content.Length);
            var lineNumber = view.FindLineNumber(pos);
            if (lineNumber >= 0) foundCount++;
        }
        
        return foundCount;
    }

    [Benchmark]
    [ArgumentsSource(nameof(ContentSizes))]
    public int MixedOperations(string content)
    {
        var view = new ContentView(content, _lineProcessor);
        int result = 0;
        var random = new Random(42);
        
        // 混合操作：创建、访问、查找
        for (int i = 0; i < Math.Min(100, view.Count); i++)
        {
            // 访问行
            var line = view[i];
            result += line.Length;
            
            // 查找随机位置的行号
            var pos = random.Next(0, content.Length);
            var lineNumber = view.FindLineNumber(pos);
            result += lineNumber;
        }
        
        return result;
    }

    public IEnumerable<string> ContentSizes()
    {
        yield return "Small";
        yield return "Medium"; 
        yield return "Large";
        yield return "VeryLarge";
    }

    public string GetContent(string size)
    {
        return size switch
        {
            "Small" => _smallContent,
            "Medium" => _mediumContent,
            "Large" => _largeContent,
            "VeryLarge" => _veryLargeContent,
            _ => throw new ArgumentException($"Unknown size: {size}")
        };
    }

    private static string GenerateContent(int lineCount)
    {
        var lines = new List<string>();
        var random = new Random(42);
        
        for (int i = 0; i < lineCount; i++)
        {
            // 生成随机长度的行
            var lineLength = random.Next(10, 100);
            var line = GenerateRandomString(lineLength);
            lines.Add(line);
        }
        
        return string.Join(Environment.NewLine, lines);
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
        var random = new Random(42);
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}