using BenchmarkDotNet.Running;

Environment.SetEnvironmentVariable("DOTNET_TieredPGO", "0");

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
