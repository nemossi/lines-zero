// <copyright file="Program.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace LinesZero.Benchmarks;

using BenchmarkDotNet.Running;

public class Program
{
    public static void Main(string[] args)
        => BenchmarkRunner.Run<ContentViewBenchmarks>();
}
