using AdventOfCode2023;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Benchmarks;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
public class December1_1Benchmarks
{
    private IEnumerable<string> _input;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "Dec1_Input.txt"));
    }

    [Benchmark(Baseline = true)]
    public int Calculate1() => December1.Calculate_1(_input);

    [Benchmark]
    public int Task1() => December1.Task1(_input);
}