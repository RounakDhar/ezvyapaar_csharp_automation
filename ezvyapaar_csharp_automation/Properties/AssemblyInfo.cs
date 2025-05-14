// File: AssemblyInfo.cs
using NUnit.Framework;
//[assembly: Parallelizable(ParallelScope.Scenarios)]
[assembly: LevelOfParallelism(4)]


[assembly: Parallelizable(ParallelScope.Fixtures)]
